using OpenCvSharp;
using OpenCvSharp.Dnn;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using Timer = System.Windows.Forms.Timer;

namespace Yolov3
{
    public partial class Form1 : Form
    {
        bool isCameraOn;
        Thread thread;
        Mat mat;
        VideoCapture videoCapture;
        public Form1()
        {
            InitializeComponent();
            Timer_OP();
            this.CenterToScreen();
            yolov3_Ready();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            recode_button.Text = "녹화";
            isCameraOn = false;
        }
        Net net;
        

        
        private void yolov3_Ready()
        {
            string cfg_Path = Application.StartupPath + @"\running\yolov3_testing.cfg";
            string model_Path = Application.StartupPath + @"\running\yolov3_training_last.weights";
            net = CvDnn.ReadNetFromDarknet(cfg_Path, model_Path);

            //dnn_backend 설정으로 무엇을 기반으로 둘기 정하는 설정값?인듯
            // 0 => Defalt : 사용하지 않음
            // 1 => HALIDE : ??
            // 2 =>     INFERENCE_ENGINE : 지식베이스 검색
            // 3 => OPENCV : 실시간 이미지 프로세싱에 중점을 둔 라이브러리
            net.SetPreferableBackend((Backend)3);

            // DNN_TARGET 어떤 걸로 할것인가?
            // 0 => CPU
            // 1 => OPENCI : 개방형 범용 병렬 컴퓨팅 프레임워크(GPU, CPU같이 돌림)
            // 2 => OPENcL_FP16 : 위에 거에서 16비트 쓰는것? 보통 이거씀
            // 3 => FPGA : 설계 가능 논리 소자와 프로그래밍이 가능한 내부 회로가 포함된 반도체 소자를 예를 들어
            // AND, OR 그런 연산하는 반도체 소자
            net.SetPreferableTarget((Target)2);
        }

        private void yolov3_Start()
        {
            // Mat => 입력 영상
            // 1.0 / 255 => 입력 영상 필셀 값에 곱할 값. 기본값은 1.
            // (딥러닝 학습을 진행할 때 입력 영상을 0~255 픽셀값을 이용 했는지,
            // 0~1로 정규화해서 이용했는지에 맞게 지정해줘야 함.
            // 0~1로 정규화하여 학습을 진행했으며 1/255를 입력해 줘야 함.
            Mat blob = CvDnn.BlobFromImage(new Mat(filename), 1.0 / 255 , new OpenCvSharp.Size(416, 416), new Scalar(), true, false);

            net.SetInput(blob); //위에서 입력받은 정보 계산

            var outName = net.GetUnconnectedOutLayersNames();       //출력 레이어 이름 얻기
            var outs = outName.Select(_ => new Mat()).ToArray();    //출력 레이어를 위한 패드 생성

            net.Forward(outs, outName);
            GetResult(outs, true, pictureBox1, null, richTextBox1);
        }

        private static void GetResult(Mat[] outs, bool nms, PictureBox pictureBox1, Image image, RichTextBox richTextBox1, bool morethreshold = true)
        {
            float threshold = 0.01f;//정확성 0~ 1
            float nmsThreshold = 0.01f;//상자 곂치는 거 필터작용

            try
            {
                if (outs != null)
                {
                    List<int> classed = new List<int>();//해당 객체가 무엇인지
                    List<float> confidences = new List<float>();//전체 정확성
                    List<float> probilities = new List<float>();//정확성
                    List<Rect2d> boxes = new List<Rect2d>();//박스 그리기 
                    //IEnumerable 은 사용자 정의에 한한 것을 반복하게 하는데 쓰인다. 즉 외부 Mat 같은것의 배열을 반복하는데 좋음.
                    int w = 0;
                    int h = 0;

                    if (image != null)
                    {
                        pictureBox1.Image = image;
                        w = image.Width;//이미지의 가로 길이 담기
                        h = image.Height;//이미지의 세로 길이 담기
                    }
                    else
                    {
                        w = pictureBox1.Image.Width;//이미지의 가로 길이 담기
                        h = pictureBox1.Image.Height;//이미지의 세로 길이 담기
                    }

                    const int prefix = 5; //134번 줄에 설명있음
                                          //const 2트째지만 고정 값이다. 변경 불가
                                          // int classnumber = -1;

                    richTextBox1.Text = "";
                    foreach (Mat mat in outs)
                    {

                        // classnumber++;
                        // richTextBox1.AppendText(classnumber.ToString());
                        //foreach는 for 문으로  output의 배열 길이 만큼 돌리면서 해당 카운트의 Mat을 Mat count 넣음.
                        //예를 들어 첫번째 돌때는 Mat mat = output[0], 두번째는  Mat mat = output[1] 이런식으로 돌아간다.
                        for (int i = 0; i < mat.Rows; i++)
                        {
                            float confidence = mat.At<float>(i, 4);//해당 객체의 정확성 담기
                            if (confidence > threshold)
                            { //confidence의 값이 threshold(0.5)보다 높으면
                                Cv2.MinMaxLoc(mat.Row(i).ColRange(prefix, mat.Cols), out _, out OpenCvSharp.Point max);
                                int classes = max.X;//발견한 클래스의 넘버 값.
                                float probability = mat.At<float>(i, classes + prefix);//5번째 이후부터 검사한 클래스의 확률이기 때문에 5를 더한다.  
                                if (probability > threshold)
                                {
                                    //mat 에 배열에 따라 들어가있는 값이 다름. 
                                    //0은 이미지의 중앙 x축 위치 정보값
                                    //1 은 이미지의 중앙 y축 위치 정보값
                                    //2 는 객체 상자의 가로 길이(w)
                                    //3 은 객체 상자의 세로 길이(h)
                                    //4 는 객체의 정확성 0~ 1사이로 뜬다.
                                    //5 이상 부터는 여러 클래스 박스 확률이 담겼다,

                                    float centerX = mat.At<float>(i, 0) * w;
                                    float centerY = mat.At<float>(i, 1) * h;
                                    float width = mat.At<float>(i, 2) * w;
                                    float height = mat.At<float>(i, 3) * h;
                                    //richTextBox1.AppendText(i.ToString() + " "+ classes.ToString() + Label[classes] +" " +mat.At<float>(i, classes+1 + prefix).ToString() + "%끝\n");
                                    //그리기 작업문
                                    //image 와 도구상자의 중앙 위치, 가로 세로 길이 보내기
                                    classed.Add(classes);
                                    confidences.Add(confidence);
                                    probilities.Add(probability);
                                    boxes.Add(new Rect2d(centerX, centerY, width, height));

                                }
                            }


                        }
                    }
                    if (boxes.Count == 0)
                    {

                        if (richTextBox1.Text != "관측되지 않음\n") richTextBox1.Text = "관측되지 않음\n";
                    }
                    else if (nms)
                    {
                        CvDnn.NMSBoxes(boxes, confidences, threshold, nmsThreshold, out int[] indices);
                        foreach (int i in indices)
                        {
                            Rect2d box = boxes[i];
                            richTextBox1.AppendText($"{Label[classed[i]]} {probilities[i] * 100:0.0}%\n");
                            richTextBox1.AppendText("x,y = " + ((int)box.X).ToString() + "/" + ((int)box.Y).ToString() + "\n" + "w,h = " + ((int)box.Width).ToString() + "/" + ((int)box.Height).ToString() + "\n\n");
                            Draw(classed[i], probilities[i], (float)box.X, (float)box.Y, (float)box.Width, (float)box.Height, pictureBox1);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < boxes.Count; i++)
                        {
                            Rect2d box = boxes[i];
                            richTextBox1.AppendText($"{Label[classed[i]]} {probilities[i] * 100:0.0}% \n"); ;
                            richTextBox1.AppendText(((int)box.X).ToString() + "/" + ((int)box.Y).ToString() + "\n" + ((int)box.Width).ToString() + "/" + ((int)box.Height).ToString() + "\n\n");
                            Draw(classed[i], probilities[i], (float)box.X, (float)box.Y, (float)box.Width, (float)box.Height, pictureBox1);
                        }
                    }
                }

            }
            catch
            {
                richTextBox1.AppendText("GetResult 문에서 실패");
            }
        }

        private static void Draw(int classes, float probability, float centerX, float centerY, float width, float height, PictureBox pictureBox1)
        {
            string text = $"{Label[classes]} ({probability * 100:0.0}%)";
            //https://bananamandoo.tistory.com/30 여기 사이트 참고 위에건 소수점 한자리 숫자만 나타내겠다는 뜻
            float x = centerX - width / 2;//중앙점에서 가로의 반을 빼줘야 그리는 점의 시작 포인트가 됨
            float y = centerY - height / 2;//중앙점에서 세로의 반을 빼줘야 그리는 점의 시작 포인트가 됨

            //그리는 도화지를 pictureBox1.Image로 설정
            using (Graphics thumbnailGraphic = Graphics.FromImage(pictureBox1.Image))
            {
                thumbnailGraphic.CompositingQuality = CompositingQuality.HighQuality;
                thumbnailGraphic.SmoothingMode = SmoothingMode.HighQuality;
                thumbnailGraphic.InterpolationMode = InterpolationMode.HighQualityBicubic;

                // Define Text Options
                Font drawFont = new Font("Arial", 12, FontStyle.Bold);//폰트는 Arial 에 크기는 12, 굵기는 굵게
                SizeF size = thumbnailGraphic.MeasureString(text, drawFont);
                SolidBrush fontBrush = new SolidBrush(Color.Black);
                System.Drawing.Point atPoint = new System.Drawing.Point((int)x, (int)y - (int)size.Height - 1); ;
                // Define BoundingBox options
                //color[classes]  객체마다 보여주는 색을 정함 
                Pen pen = new Pen(color[classes], 3.2f);
                //color[classes]  객체마다 보여주는 색을 정함 
                SolidBrush colorBrush = new SolidBrush(color[classes]);

                thumbnailGraphic.FillRectangle(colorBrush, (int)x, (int)(y - size.Height - 1), (int)size.Width, (int)size.Height);
                //사각형 그리기
                thumbnailGraphic.DrawString(text, drawFont, fontBrush, atPoint);
                //해당 객체 글자 그리기

                // Draw bounding box on image
                thumbnailGraphic.DrawRectangle(pen, x, y, width, height);
            }
        }

        private static string[] Label = new string[]
        {
            //static은 생성자부터 프로그램 종료시까지 존재하여 바로 쓰기 가능
            //public는 다른 문에서 쓸 때 그때 그때 생성
            //즉 메모리에 넣어서 쓰는 거기 때문에 자주는 쓰지만 값을 반환하지 않으면 static을 쓰는게 유리
            "사과",
            "오렌지"
        };

        private static Color[] color = new Color[]
        {
            Color.Red,
            Color.Orange
        };

        string filename = "";
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filename = ofd.FileName;
                pictureBox1.Image = new Bitmap(filename);
                yolov3_Start();
            }
        }

        Timer point_Timer = new Timer();
        string point_string = "";
        System.Drawing.Point SPoint = new System.Drawing.Point();
        System.Drawing.Point EPoint = new System.Drawing.Point();
        private void Timer_OP()
        {
            point_Timer.Interval = 100;     // 1000이 1초, 즉 0.1초마다
            point_Timer.Tick += new EventHandler(point_Timer_Tick);

            window_timer.Interval = 100;     // 1000이 1초, 즉 0.1초마다
            window_timer.Tick += new EventHandler(window_timer_Tick);

        }
        private void point_Timer_Tick(object sender, EventArgs e)
        {
            // 시스템 디스플레이 비율이 125%이거나 다르면 이 방법으로 맞춰줌
            int dsi = 100;
            try
            {
                dsi = Convert.ToInt32(Dis_textbox.Text);
            }
            catch
            {
                Dis_textbox.Text = "숫자가 아닙니다.";
            }
            START_button.Focus();
            if(point_string == "시작점")
            {
                SPoint = new System.Drawing.Point(MousePosition.X * dsi / 100, MousePosition.Y * dsi /100);
                SP_Label.Text = $"X-{SPoint.X}\nY-{SPoint.Y}";
            }
            else if (point_string == "마침점")
            {
                EPoint = new System.Drawing.Point(MousePosition.X * dsi / 100, MousePosition.Y * dsi / 100);
                END_Label.Text = $"X-{EPoint.X}\nY-{EPoint.Y}";
            }

        }
        
        private void START_button_Click(object sender, EventArgs e)
        {
            if (!point_Timer.Enabled)
            {
                point_string = "시작점";
                point_Timer.Start();
            }
            
        }

        private void START_button_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.S & point_Timer.Enabled)
            {
                point_Timer.Stop();
            }
        }

        private void END_button_Click(object sender, EventArgs e)
        {
            if (!point_Timer.Enabled)
            {
                point_string = "마침점";
                point_Timer.Start();
            }
                
        }

        System.Drawing.Point DrawPoint = new System.Drawing.Point(0, 0);
        Bitmap windowbitmap;
        Timer window_timer = new Timer();
        System.Threading.Thread windowing_Thread;
        bool windowing_bool = false;
        private void window_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (!window_timer.Enabled)
                {
                    if (SPoint.X > EPoint.X) DrawPoint = new System.Drawing.Point(EPoint.X, 0);
                    else if (SPoint.X <= EPoint.X) DrawPoint = new System.Drawing.Point(SPoint.X, 0);

                    if (SPoint.Y > EPoint.Y) DrawPoint = new System.Drawing.Point(DrawPoint.X, EPoint.Y);
                    else if (SPoint.Y <= EPoint.Y) DrawPoint = new System.Drawing.Point(DrawPoint.X, SPoint.Y);

                    windowing_bool = true;

                    windowbitmap = new Bitmap(Math.Abs(SPoint.X - EPoint.X), Math.Abs(SPoint.Y - EPoint.Y), System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                    window_timer.Start();
                    //A 작업을 하며서 windowing이라는 B작업 병행
                    windowing_Thread = new System.Threading.Thread(new System.Threading.ThreadStart(windowing));
                    windowing_Thread.Start();
                }
                else
                {
                    window_timer.Stop();
                    pictureBox1.Image = null;
                }
            }
            catch
            {
                MessageBox.Show("시작점과 마침점을 정해주세요");
            }
            
        }

        Bitmap windowClone;
        Mat[] resultouts;
        private void window_timer_Tick(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromImage(windowbitmap);
            g.CopyFromScreen(DrawPoint, new System.Drawing.Point(0, 0), windowbitmap.Size);
            windowClone = windowbitmap.Clone(new Rectangle(new System.Drawing.Point(0, 0), windowbitmap.Size), System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            pictureBox1.Image = windowbitmap;
            GetResult(resultouts, true, pictureBox1, null, richTextBox1);
        }
        
        private void windowing()
        {
            while(windowing_bool)
            {
                window_yolov3_Start();
            }
        }

        private void window_yolov3_Start()
        {
            
            if(pictureBox1.Image != null)
            {
                // Mat => 입력 영상
                // 1.0 / 255 => 입력 영상 필셀 값에 곱할 값. 기본값은 1.
                // (딥러닝 학습을 진행할 때 입력 영상을 0~255 픽셀값을 이용 했는지,
                // 0~1로 정규화해서 이용했는지에 맞게 지정해줘야 함.
                // 0~1로 정규화하여 학습을 진행했으며 1/255를 입력해 줘야 함.
                Mat blob = CvDnn.BlobFromImage(BitmapConverter.ToMat(windowClone), 1.0 / 255, new OpenCvSharp.Size(416, 416), new Scalar(), true, false);

                net.SetInput(blob); //위에서 입력받은 정보 계산

                var outName = net.GetUnconnectedOutLayersNames();       //출력 레이어 이름 얻기
                var outs = outName.Select(_ => new Mat()).ToArray();    //출력 레이어를 위한 패드 생성

                net.Forward(outs, outName);
                resultouts = outs;
            }
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

        private void recode_button_Click(object sender, EventArgs e)
        {
            if (isCameraOn == false)
            {
                videoCapture = new VideoCapture(1);
                thread = new Thread(new ThreadStart(CameraCallback));

                thread.Start();
                isCameraOn = true;
                recode_button.Text = "녹화중";
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
                recode_button.Text = "녹화";
            }
        }
    }
}
