using MultiThreadedImageProcessing.Utilities;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace MultiThreadedImageProcessing.Codec
{
    public class Decoder
    {
        #region "Private Fields"
        private Bitmap TrailBitmap;
        #endregion

        public Decoder()
        {
            TrailBitmap = null;
        }

        #region "Public Methods"
        public Bitmap Decode(MemoryStream EncodedStream)
        {
            EncodedStream.Position = 0;
            if (TrailBitmap == null)
            {
                byte[] Header = new byte[4];
                if (EncodedStream.CanRead)
                {
                    EncodedStream.Read(Header, 0, Header.Length);
                    int ImageLength = BitConverter.ToInt32(Header, 0);
                    byte[] ImageBuffer = new byte[ImageLength];
                    EncodedStream.Read(ImageBuffer, 0, ImageBuffer.Length);
                    TrailBitmap = (Bitmap)Image.FromStream(new MemoryStream(ImageBuffer));
                }
            }
            else
            {
                BitmapData StickData = TrailBitmap.LockBits(new Rectangle(0, 0, TrailBitmap.Width, TrailBitmap.Height), ImageLockMode.ReadWrite, TrailBitmap.PixelFormat);
                int Stride = Math.Abs(StickData.Stride);
                int DataLength = (int)EncodedStream.Length;
                byte[] Header = new byte[20];
                while (DataLength > 0)
                {
                    EncodedStream.Read(Header, 0, Header.Length);
                    Rectangle Rect = new Rectangle(BitConverter.ToInt32(Header, 0), BitConverter.ToInt32(Header, 4), BitConverter.ToInt32(Header, 8), BitConverter.ToInt32(Header, 12));
                    int ImageLength = BitConverter.ToInt32(Header, 16);
                    byte[] ImageBuffer = new byte[ImageLength];
                    EncodedStream.Read(ImageBuffer, 0, ImageBuffer.Length);
                    using (MemoryStream ImgStream = new MemoryStream(ImageBuffer))
                    {
                        using (Bitmap Img = (Bitmap)Image.FromStream(ImgStream))
                        {
                            int PixelSize = BitmapExtensions.BytesPerPixel(Img.PixelFormat);
                            BitmapData ImgData = Img.LockBits(new Rectangle(0, 0, Img.Width, Img.Height), ImageLockMode.WriteOnly, Img.PixelFormat);
                            for (int y = 0; y < Rect.Height; y++)
                            {
                                int SrcOffset = Math.Abs(ImgData.Stride) * y;
                                int DstOffset = ((Rect.Y + y) * Stride) + (Rect.X * PixelSize);
                                NativeMethods.memcpy(StickData.Scan0 + DstOffset, ImgData.Scan0 + SrcOffset, (uint)(Img.Width * PixelSize));
                            }
                            Img.UnlockBits(ImgData);
                        }
                    }
                    DataLength -= 20 + ImageLength;
                }
                TrailBitmap.UnlockBits(StickData);
            }
            return TrailBitmap;
        }
        #endregion
    }
}