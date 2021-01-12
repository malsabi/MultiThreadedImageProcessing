using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using MultiThreadedImageProcessing.Utilities;

namespace MultiThreadedImageProcessing.Codec
{
    public class Encoder
    {
        #region "Private Fields"
        private Bitmap TrailBitmap;
        private readonly List<Rectangle> UpperChanges;
        private readonly List<Rectangle> LowerChanges;
        private readonly JpgCompression Compressor;
        #endregion

        #region "Constructor"
        public Encoder()
        {
            TrailBitmap = null;
            UpperChanges = new List<Rectangle>();
            LowerChanges = new List<Rectangle>();
            Compressor = new JpgCompression(50);
        }
        #endregion

        #region "Public Methods"
        public void Encode(MemoryStream OutStream, Bitmap CurrentBitmap, PixelFormat BitmapFormat)
        {
            if (TrailBitmap == null)
            {
                byte[] EncodedBytes = Compressor.Compress(CurrentBitmap);
                OutStream.Write(BitConverter.GetBytes(EncodedBytes.Length), 0, 4);
                OutStream.Write(EncodedBytes, 0, EncodedBytes.Length);
                TrailBitmap = CurrentBitmap;
            }
            else
            {
                BitmapData TrailData = TrailBitmap.LockBits(new Rectangle(0, 0, TrailBitmap.Width, TrailBitmap.Height), ImageLockMode.ReadOnly, BitmapFormat);
                BitmapData CurrentData = CurrentBitmap.LockBits(new Rectangle(0, 0, CurrentBitmap.Width, CurrentBitmap.Height), ImageLockMode.ReadOnly, BitmapFormat);
                int Stride = Math.Abs(TrailData.Stride);
                Task.WaitAll(new[]
                {
                    Task.Run(() => UpperProcess(0, 0, TrailData.Width, TrailData.Height / 2, Stride, TrailData.Scan0, CurrentData.Scan0)),
                    Task.Run(() => LowerProcess(0, TrailData.Height / 2, TrailData.Width, TrailData.Height, Stride, TrailData.Scan0, CurrentData.Scan0)),
                });
                CurrentBitmap.UnlockBits(CurrentData);
                TrailBitmap.UnlockBits(TrailData);

                List<Rectangle> Changes = new List<Rectangle>(UpperChanges.Count + LowerChanges.Count);
                Changes.AddRange(UpperChanges);
                Changes.AddRange(LowerChanges);

                foreach (Rectangle Change in Changes)
                {
                    OutStream.Write(BitConverter.GetBytes(Change.X), 0, 4);
                    OutStream.Write(BitConverter.GetBytes(Change.Y), 0, 4);
                    OutStream.Write(BitConverter.GetBytes(Change.Width), 0, 4);
                    OutStream.Write(BitConverter.GetBytes(Change.Height), 0, 4);
                    CurrentData = CurrentBitmap.LockBits(new Rectangle(Change.X, Change.Y, Change.Width, Change.Height), ImageLockMode.ReadOnly, BitmapFormat);
                    using (Bitmap ChangedBmp = new Bitmap(Change.Width, Change.Height, CurrentData.Stride, BitmapFormat, CurrentData.Scan0))
                    {
                        byte[] CompressedImage = Compressor.Compress(ChangedBmp);
                        OutStream.Write(BitConverter.GetBytes(CompressedImage.Length), 0, 4);
                        OutStream.Write(CompressedImage, 0, CompressedImage.Length);
                    }
                    CurrentBitmap.UnlockBits(CurrentData);
                }
                TrailBitmap.Dispose();
                TrailBitmap = CurrentBitmap;
                ClearChanges();
            }
        }
        #endregion

        #region "Private Methods"
        private void ClearChanges()
        {
            UpperChanges.Clear();
            LowerChanges.Clear();
        }

        private void UpperProcess(int X, int Y, int Width, int Height, int Stride, IntPtr TrailScan0, IntPtr CurrentScan0)
        {
            unsafe
            {
                byte* TrailPtr = (byte*)TrailScan0.ToInt32();
                byte* CurrentPtr = (byte*)CurrentScan0.ToInt32();
                List<Rectangle> DeltaRows = new List<Rectangle>();
                //Process Row by Row
                for (int y = Y; y < Height; y++)
                {
                    int Offset = (y * Stride) + X;
                    Rectangle Row = new Rectangle(X, y, Width, 1);
                    if (NativeMethods.memcmp(TrailPtr + Offset, CurrentPtr + Offset, (uint)Stride) != 0)
                    {
                        int LastIndex = DeltaRows.Count - 1;
                        if (DeltaRows.Count != 0 && (DeltaRows[LastIndex].Y + DeltaRows[LastIndex].Height) == Row.Y)
                        {
                            DeltaRows[LastIndex] = new Rectangle(X, DeltaRows[LastIndex].Y, DeltaRows[LastIndex].Width, DeltaRows[LastIndex].Height + Row.Height);
                        }
                        else
                        {
                            DeltaRows.Add(Row);
                        }
                    }
                }
                //Process Block by Block per Row
                int BlockSize = 40;
                int PixelSize = 4;
                for (int i = 0; i < DeltaRows.Count; i++)
                {
                    Rectangle ROW = DeltaRows[i];
                    for (int x = 0; x < ROW.Width; x += BlockSize)
                    {
                        if (x + BlockSize > ROW.Width)
                        {
                            BlockSize = ROW.Width - x;
                        }
                        Rectangle Block = new Rectangle(x, ROW.Y, BlockSize, ROW.Height);
                        for (int y = 0; y < ROW.Height; y++)
                        {
                            int Offset = ((ROW.Y + y) * Stride) + (x * PixelSize);
                            if (NativeMethods.memcmp(TrailPtr + Offset, CurrentPtr + Offset, (uint)(BlockSize * PixelSize)) != 0)
                            {
                                UpperChanges.Add(Block);
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void LowerProcess(int X, int Y, int Width, int Height, int Stride, IntPtr TrailScan0, IntPtr CurrentScan0)
        {
            unsafe
            {
                byte* TrailPtr = (byte*)TrailScan0.ToInt32();
                byte* CurrentPtr = (byte*)CurrentScan0.ToInt32();
                List<Rectangle> DeltaRows = new List<Rectangle>();
                //Process Row by Row
                for (int y = Y; y < Height; y++) //n
                {
                    int Offset = (y * Stride) + X;
                    Rectangle Row = new Rectangle(X, y, Width, 1);
                    if (NativeMethods.memcmp(TrailPtr + Offset, CurrentPtr + Offset, (uint)Stride) != 0)
                    {
                        int LastIndex = DeltaRows.Count - 1;
                        if (DeltaRows.Count != 0 && (DeltaRows[LastIndex].Y + DeltaRows[LastIndex].Height) == Row.Y)
                        {
                            DeltaRows[LastIndex] = new Rectangle(DeltaRows[LastIndex].X, DeltaRows[LastIndex].Y, DeltaRows[LastIndex].Width, DeltaRows[LastIndex].Height + Row.Height);
                        }
                        else
                        {
                            DeltaRows.Add(Row);
                        }
                    }
                }
                //Process Block by Block per Row
                int BlockSize = 40;
                int PixelSize = 4;
                for (int i = 0; i < DeltaRows.Count; i++)
                {
                    Rectangle ROW = DeltaRows[i];
                    for (int x = 0; x < ROW.Width; x += BlockSize)
                    {
                        if (x + BlockSize > ROW.Width)
                        {
                            BlockSize = ROW.Width - x;
                        }
                        Rectangle Block = new Rectangle(x, ROW.Y, BlockSize, ROW.Height);
                        for (int y = 0; y < ROW.Height; y++)
                        {
                            int Offset = ((ROW.Y + y) * Stride) + (x * PixelSize);
                            if (NativeMethods.memcmp(TrailPtr + Offset, CurrentPtr + Offset, (uint)(BlockSize * PixelSize)) != 0)
                            {
                                LowerChanges.Add(Block);
                                break;
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}