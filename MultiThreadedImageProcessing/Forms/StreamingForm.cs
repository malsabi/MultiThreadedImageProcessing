using MultiThreadedImageProcessing.Utilities;
using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace MultiThreadedImageProcessing.Forms
{
    public partial class StreamingForm : Form
    {
        private Codec.Encoder Encoder;
        private Codec.Decoder Decoder;
        private bool IsRunning = false;
        private readonly ConcurrentQueue<MemoryStream> FrameQueue = new ConcurrentQueue<MemoryStream>();
        private int FrameCounter = 0;
        private int FPS = 0;
        private bool EnableChanges = false;

        public StreamingForm()
        {
            InitializeComponent();
        }

        private void DoStream()
        {
            PixelFormat BitmapFormat = PixelFormat.Format32bppArgb;
            Size BlockScan = new Size(50, 50);
            new Thread(new ThreadStart(Consumer)).Start();
            while (IsRunning)
            {
                MemoryStream OutStream = new MemoryStream();
                Encoder.EncodeDefault(OutStream, BitmapExtensions.CaptureScreen(BitmapFormat), BlockScan, BitmapFormat);
                if (OutStream.Length > 0)
                {
                    FrameQueue.Enqueue(OutStream);
                }
            }
        }

        private void Consumer()
        {
            while (IsRunning)
            {
                if (FrameQueue.Count > 0)
                {
                    if (FrameQueue.TryDequeue(out MemoryStream FrameStream))
                    {
                        FrameCounter++;
                        UpdateFrame(Decoder.Decode(FrameStream));
                        FrameStream.Dispose();
                    }
                }
            }
        }

        private delegate void UpdateFrameDelegate(Bitmap Frame);
        private void UpdateFrame(Bitmap Frame)
        {
            if (StreamBox.InvokeRequired)
            {
                StreamBox.Invoke(new UpdateFrameDelegate(UpdateFrame), new object[] { Frame });
            }
            else
            {
                StreamBox.Image = (Bitmap)Frame.Clone();
            }
        }

        private void UpdateUI()
        {
            FPS = FrameCounter;
            Text = "Remote Desktop FPS: " + FPS.ToString();
            FrameCounter = 0;
        }

        private void FPSTimer_Tick(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void StartStreamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsRunning == false)
            {
                IsRunning = true;
                new Thread(new ThreadStart(DoStream)).Start();
                startStreamToolStripMenuItem.Text = "Stop Stream";
            }
            else
            {
                IsRunning = false;
                startStreamToolStripMenuItem.Text = "Start Stream";
            }
        }

        private void StreamBox_Paint(object sender, PaintEventArgs e)
        {
            if (Decoder.Points.Length > 0 && EnableChanges)
            {
                e.Graphics.DrawRectangles(new Pen(new SolidBrush(Color.Red), 1), Decoder.Points);
            }
        }

        private void ShowChangesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EnableChanges == false)
            {
                EnableChanges = true;
                ShowChangesToolStripMenuItem.Text = "Hide Changes";
            }
            else
            {
                EnableChanges = false;
                ShowChangesToolStripMenuItem.Text = "Show Changes";
            }
            Decoder.EnableChanges(EnableChanges);
        }

        private void StreamingForm_Load(object sender, EventArgs e)
        {
            Encoder = new Codec.Encoder();
            Decoder = new Codec.Decoder(StreamBox.Width, StreamBox.Height);
        }

        private void StreamBox_Resize(object sender, EventArgs e)
        {
            Decoder.ClientWidth = StreamBox.Width;
            Decoder.ClientHeight = StreamBox.Height;
        }
    }
}