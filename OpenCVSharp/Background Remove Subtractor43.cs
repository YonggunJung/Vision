using OpenCvSharp.CPlusPlus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenCVSharpEx1
{
    public partial class Background_Remove_Subtractor43 : Form
    {
        public Background_Remove_Subtractor43()
        {
            InitializeComponent();
        }

        public void BackgroundSubtractor()
        {
            using (VideoCapture video = new VideoCapture(1))    //Mat 형식의 영상을 생성
            // 3가지의 함수를 이용하여 배경을 삭제 가능.
            using (BackgroundSubtractorGMG GMG = new BackgroundSubtractorGMG())
            using (BackgroundSubtractorMOG MOG = new BackgroundSubtractorMOG())
            using (BackgroundSubtractorMOG2 MOG2 = new BackgroundSubtractorMOG2())
            
            using (Mat frame = new Mat())   //video의 프레임을 저장할 frame
            using (Mat remove = new Mat())  //배경이 삭제된 후 출력할 remove를 생성

            //Window창을 선언하여 결과를 표시할 윈도우 창을 생성
            using (Window win_GMG = new Window("GMG"))
            using (Window win_MOG = new Window("MOG"))
            using (Window win_MOG2 = new Window("MOG2"))
            {
                video.FrameWidth = 640;
                video.FrameHeight = 480;

                while(Cv2.WaitKey(1) < 0)   //키 입력이 있을 때 까지 반복
                {
                    //frame에 비디오 장치에서 읽어온 영상을 저장
                    video.Read(frame);

                    //*.Run(원본, 결과)
                    //ShowImage(출력 이미지)를 사용하여 결과를 표시
                    //Window 창을 사용하지 않고 Form에 띄울 경우 pictureBoxIpl1.ImageIpl = remove.ToIplImage();를 사용
                    GMG.Run(frame, remove);
                    win_GMG.ShowImage(remove);

                    MOG.Run(frame, remove);
                    win_MOG.ShowImage(remove);

                    MOG2.Run(frame, remove);
                    win_MOG2.ShowImage(remove);
                }
            }
        }

        private void Background_Remove_Subtractor43_Load(object sender, EventArgs e)
        {
            BackgroundSubtractor();
        }
    }
}
