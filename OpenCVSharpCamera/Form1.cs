using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;
using OpenCvSharp;
using OpenCvSharp.Extensions;
namespace OpenCV
{
    delegate void dele(Mat m);

    public partial class Form1 : Form
    {
        bool isCameraOn;

        dele filter;    // 카메라에 적용할 필터(효과) 델리게이트
        Thread thread;
        Mat mat;
        VideoCapture videoCapture;


        public Form1()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            button1.Text = "카메라 시작";
            isCameraOn = false;
            filter = null;
            radioButton1.Checked = true;
        }

        private void CameraCallback()
        {
            mat = new Mat();
            videoCapture = new VideoCapture(1);

            if (!videoCapture.IsOpened())
            {
                Text = "카메라 연결 실패!";
                MessageBox.Show("카메라를 열 수 없습니다. 연결 상태를 확인 해 주세요.");

                return;
            }

            while (true)
            {
                videoCapture.Read(mat);

                if (!mat.Empty() && filter != null)
                {
                    filter(mat);    // 선택된 라디오 버튼에 따른 필터 적용.                    
                }

                if (!mat.Empty())
                {
                    // 로고를 디스플레이하기 위해 그레이 이미지(1채널)는 컬러 포맷(3채널)으로 변환
                    if (mat.Channels() == 1)
                    {
                        Cv2.CvtColor(mat, mat, ColorConversionCodes.GRAY2BGR);
                    }
                    Cv2.PutText(mat, "CAM1", new OpenCvSharp.Point(550, 470), HersheyFonts.HersheyDuplex, 1, new Scalar(0, 0, 255), 2);

                    // 이 전 프레임에서 PictureBox에 로드된 비트맵 이미지를 Dispose하지 않으면 메모리 사용량 크게 증가
                    if (pictureBox1.Image != null)
                    {
                        pictureBox1.Image.Dispose();
                    }
                    pictureBox1.Image = BitmapConverter.ToBitmap(mat);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isCameraOn == false)
            {
                videoCapture = new VideoCapture(1);
                thread = new Thread(new ThreadStart(CameraCallback));

                thread.Start();
                isCameraOn = true;
                button1.Text = "카메라 정지";
            }
            else
            {
                if (videoCapture.IsOpened())
                {
                    thread.Abort();
                    if (pictureBox1.Image != null)
                        pictureBox1.Image.Dispose();

                    videoCapture.Release();
                    mat.Release();
                }
                isCameraOn = false;
                button1.Text = "카메라 시작";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://s-engineer.tistory.com/");
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (thread != null && thread.IsAlive && videoCapture.IsOpened())
            {
                thread.Abort();
                if (pictureBox1.Image != null)
                    pictureBox1.Image.Dispose();

                videoCapture.Release();
                mat.Release();
            }
        }

        // 필터 함수들
        private void ToGray(Mat mat)
        {
            Cv2.CvtColor(mat, mat, ColorConversionCodes.BGR2GRAY);
        }

        private void ToEmboss(Mat mat)
        {
            float[] data = { -1.0f, -1.0f, 0.0f, -1.0f, 0f, 1.0f, 0.0f, 1.0f, 1.0f };
            Mat emboss = new Mat(3, 3, MatType.CV_32FC1, data);

            Cv2.CvtColor(mat, mat, ColorConversionCodes.BGR2GRAY);
            Cv2.Filter2D(mat, mat, -1, emboss, new OpenCvSharp.Point(-1, -1), 128);
            emboss.Release();
        }

        private void ToBlur(Mat mat)
        {
            Cv2.GaussianBlur(mat, mat, new OpenCvSharp.Size(), (double)3);
        }

        private void ToSharpen(Mat mat)
        {
            Mat blurred = new Mat();
            Cv2.GaussianBlur(mat, blurred, new OpenCvSharp.Size(), (double)3);

            // 아래 연산이 반복 되면 메모리 사용량이 크게 증가.
            float alpha = 2.0f;
            ((1 + alpha) * mat - alpha * blurred).ToMat().CopyTo(mat);
            // mat = (1 + alpha) * mat - alpha * blurred;

            blurred.Release();
        }

        private void ToEdge(Mat mat)
        {
            Cv2.CvtColor(mat, mat, ColorConversionCodes.BGR2GRAY);
            Cv2.Canny(mat, mat, 50, 70);
        }

        // 라디오 버튼 이벤트 핸들러들

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                filter = null;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                filter = ToGray;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                filter = ToEmboss;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                filter = ToBlur;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                filter = ToSharpen;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                filter = ToEdge;
        }
    }
}
