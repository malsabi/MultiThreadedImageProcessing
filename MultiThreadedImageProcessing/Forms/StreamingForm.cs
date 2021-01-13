using MultiThreadedImageProcessing.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace MultiThreadedImageProcessing.Forms
{
    public partial class StreamingForm : Form
    {

        private readonly Codec.Encoder encoder = new Codec.Encoder();
        private readonly Codec.Decoder decoder = new Codec.Decoder();
        private readonly Queue<MemoryStream> Queue = new Queue<MemoryStream>();
        private int FrameCounter = 0;
        private int FPS = 0;


        public StreamingForm()
        {
            InitializeComponent();
        }

        private void StartStreamBtn_Click(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            new Thread(new ThreadStart(DoStream)).Start();
        }

        private void DoStream()
        {
            PixelFormat BitmapFormat = PixelFormat.Format32bppArgb;
            MemoryStream OutStream = new MemoryStream();
            while (true)
            {
                encoder.Encode(OutStream, BitmapExtensions.CaptureScreen(BitmapFormat), 60, BitmapFormat);
                StreamBox.Image = decoder.Decode(OutStream);
                OutStream.Position = 0;
                OutStream.SetLength(0);
                FrameCounter++;
            }
        }

        private void UpdateUI()
        {
            FPS = FrameCounter;
            FPSLabel.Text = "FPS: " + FPS.ToString();
            FrameCounter = 0;
        }

        private void FPSTimer_Tick(object sender, EventArgs e)
        {
            UpdateUI();
        }
    }
}
