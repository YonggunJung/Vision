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
using OpenCvSharp.UserInterface;

namespace OpenCVSharpEx1
{
    public partial class Trackbar38 : Form
    {
        public Trackbar38()
        {
            InitializeComponent();
        }

        IplImage src;
        CvWindowEx window;

        private void Trackbar38_Load(object sender, EventArgs e)
        {
            src = new IplImage(@"C:\Users\admin\source\repos\OpenCVSharpEx1\Images\츄2.jpg", LoadMode.AnyColor);
            using (window = new CvWindowEx(src))
            {
                window.Text = "Trackbar38";   //CvWindowEx의 제목
                //window.CreateTrackbar("내용", 초기값, 최대값, 트랙바이벤트)
                window.CreateTrackbar("Threshold", 127, 255, TrackbarEvent);
                TrackbarEvent(127);
                //CvWindowEx.WaitKey(); 를 이용하여 CvWindowEx 키를 누를때 까지 창이 종료되지 않게함
                CvWindowEx.WaitKey();
            }
        }

        private void TrackbarEvent(int pos)
        {
            //임시 이미지인 temp를 결과로 사용하기 위해서 src를 복제
            using (IplImage temp = src.Clone())
            {
                //pos(Point Of Scale)을 이용하여 값을 조정
                Cv.Threshold(src, temp, pos, 255, ThresholdType.Binary);
                window.ShowImage(temp); //Cv.Threshold()를 적용하고, 임시(결과) 이미지를 window에 띄움
            }
        }
    }
}
