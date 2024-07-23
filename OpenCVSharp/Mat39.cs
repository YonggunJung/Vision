using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.CPlusPlus;

namespace OpenCVSharpEx1
{
    public partial class Mat39 : Form
    {
        public Mat39()
        {
            InitializeComponent();
        }

        VideoCapture video;
        Mat frame = new Mat();

        private void Mat_Load(object sender, EventArgs e)
        {
            try
            {
                video = new VideoCapture(1);
                video.FrameWidth = 640;
                video.FrameHeight = 480;
            }
            catch
            {
                timer1.Enabled = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            video.Read(frame);
            pictureBoxIpl1.ImageIpl = frame.ToIplImage();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            frame.Dispose();
        }
    }
}
