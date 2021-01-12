using MultiThreadedImageProcessing.Forms;
using System.Windows.Forms;

namespace MultiThreadedImageProcessing
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            Application.Run(new StreamingForm());
        }
    }
}