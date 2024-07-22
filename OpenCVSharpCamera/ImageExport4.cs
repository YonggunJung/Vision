using OpenCvSharp;
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
    public partial class ImageExport : Form
    {
        public ImageExport()
        {
            InitializeComponent();
        }

        private void ImageExport_Load(object sender, EventArgs e)
        {
            using (IplImage ipl = new IplImage(@"C:\Users\admin\source\repos\OpenCVSharpEx1\Images\30.png", LoadMode.AnyColor)) // 4장
            //pictureBoxIpl1.ImageIpl = ipl;// 4장  이미지 출력

            // 31장 이진화 메서드 - Binarizer
            using (Gamma_Correction30 Convert = new Gamma_Correction30())
            {
                pictureBoxIpl1.ImageIpl = Convert.GammaCorrect(ipl);
            }

            // 30장 감마 보정 - Gamma Correction
            //using (Gamma_Correction30 Convert = new Gamma_Correction30())
            //{
            //    pictureBoxIpl1.ImageIpl = Convert.GammaCorrect(ipl);
            //}

            // 29장 얼굴 검출 - Haar Classifier Cascade
            //using (Haar_Classifier_Cascade29 Convert = new Haar_Classifier_Cascade29())
            //{
            //    pictureBoxIpl1.ImageIpl = Convert.FaceDetection(ipl);
            //}

            // 28장 모폴로지 연산 - MorphologyEx
            //using (MorphologyEx28 Convert = new MorphologyEx28())
            //{
            //    pictureBoxIpl1.ImageIpl = Convert.Morphology(ipl);
            //}

            // 27장 팽창 및 침식 - Dilate & Erode
            //팽창 - Dilate
            //using (Dilate_Erode27 Convert = new Dilate_Erode27())
            //{
            //    pictureBoxIpl1.ImageIpl = Convert.DilateImage(ipl);
            //}
            //침식 - Erode
            //using (Dilate_Erode27 Convert = new Dilate_Erode27())
            //{
            //    pictureBoxIpl1.ImageIpl = Convert.ErodeImage(ipl);
            //}

            // 26장 원 검출 - Hough Transform Circles
            //using (Hough_Transform_Circles26 Convert = new Hough_Transform_Circles26())
            //{
            //    pictureBoxIpl1.ImageIpl = Convert.HoughCircles(ipl);
            //}

            // 25장 직선 검출 - Hough Transform Lines
            //using (Hough_Transform_Lines25 Convert = new Hough_Transform_Lines25())
            //{
            //    pictureBoxIpl1.ImageIpl = Convert.HoughLines(ipl);
            //}

            // 24장 중심점 - Moments
            //using (Moments24 Convert = new Moments24())
            //{
            //    pictureBoxIpl1.ImageIpl = Convert.Moment(ipl);
            //}

            // 23장 블록 껍질 - ConvexHull
            //using (ConvexHull23 Convert = new ConvexHull23())
            //{
            //    pictureBoxIpl1.ImageIpl = Convert.ConvexHull(ipl);
            //}

            //22장 코너검출2
            //ApproxPoly_Contour
            //using (FindConer21_22 Convert = new FindConer21_22())
            //{
            //    pictureBoxIpl1.ImageIpl = Convert.ApproxPoly_Contour(ipl);
            //}

            //21장 코너검출1
            //GoodFeaturesToTrack
            //using (FindConer21 Convert = new FindConer21())
            //{
            //    pictureBoxIpl1.ImageIpl = Convert.GoodFeaturesToTrack(ipl);
            //}
            //GoodFeaturesToTrack_HarrisCornerDetector
            //using (FindConer21 Convert = new FindConer21())
            //{
            //    pictureBoxIpl1.ImageIpl = Convert.GoodFeaturesToTrack_HarrisCornerDetector(ipl);
            //}
            //HarrisCorner
            //using (FindConer21 Convert = new FindConer21())
            //{
            //    pictureBoxIpl1.ImageIpl = Convert.HarrisCorner(ipl);
            //}

            //20장 윤곽선 - Contour
            //FindContour
            //using (FindContour20 Convert = new FindContour20())
            //{
            //    pictureBoxIpl1.ImageIpl = Convert.Contour(ipl);
            //}
            //ContourScanner
            //using (ContourScanner20 Convert = new ContourScanner20())
            //{
            //    pictureBoxIpl1.ImageIpl = Convert.Contour(ipl);
            //}

            // 19장 캡쳐 및 녹화 - Capture & Record
            // 단일 캡쳐
            //private void button1_Click(object sender, EventArgs e)
            //{
            //    Cv.SaveImage("경로및이름", 저장영상), 연속적으로 캡쳐된 이미지는 capture.jpg에 덮어씌워짐
            //    Cv.SaveImage(@"C:\Users\admin\source\repos\OpenCVSharpEx1\capture.jpg", ipl);
            //}
            // 다중 캡쳐
            //private void button1_Click(object sender, EventArgs e)
            //{
            //    Button1을 클릭하면 그 시점의 영상이 캡쳐되어 지정된 경로에 현재시간의 제목으로 저장
            //    string save_name = DateTime.Now.ToString("yyyy-MM-dd-hh시mm분ss초");
            //    Cv.SaveImage("../../" + save_name + ".jpg", src);
            //}
            // 단일 녹화
            //CvVideoWriter("경로및이름", "FourCC", fps, 영상크기)
            //FourCC(Four Character Code) : 디지털 미디어 포맷 코드.즉,코덱의 인코딩 방식을 의미
            //CvVideoWriter OpenCV_video = new CvVideoWriter("../../Record.avi", "XVID", 15, Cv.Size(640, 480));
            //timer2를 켰다가 끄는 방식으로 원하는 지점에서의 영상을 시작
            //private void timer2_Tick(object sender, EventArgs e)
            //{
            //    OpenCV_video.WriteFrame(src);
            //}
            //private void button2_Click(object sender, EventArgs e)
            //{
            //    timer2.Enabled = true;
            //}
            //private void button3_Click(object sender, EventArgs e)
            //{
            //    timer2.Enabled = false;
            //}
            // 다중 녹화
            //CvVideoWriter OpenCV_video
            //private void timer2_Tick(object sender, EventArgs e)
            //{
            //    OpenCV_video.WriteFrame(src);
            //}
            //private void button2_Click(object sender, EventArgs e)
            //{
            //    string save_name = DateTime.Now.ToString("yyyy-MM-dd-hh시mm분ss초");
            //    OpenCV_video = new CvVideoWriter("../../" + save_name + ".avi", "XVID", 15, Cv.GetSize(src));
            //    timer2.Enabled = true;
            //}
            //private void button3_Click(object sender, EventArgs e)
            //{
            //    timer2.Enabled = false;
            //}

            //18장 기하학적 변환 - Warp Perspective
            //using (Warp_Perspective Convert = new Warp_Perspective())
            //{
            //    pictureBoxIpl1.ImageIpl = Convert.PerspectiveTransform(ipl);
            //}

            //17장 그리기 - Drawing
            //using (Drawing17 Convert = new Drawing17())
            //{
            //    pictureBoxIpl1.ImageIpl = Convert.DrawingImage(ipl);
            //}

            //16장 분리, 병합 - Split, Merge
            //분리
            //using (SplitMerge16 Convert = new SplitMerge16())
            //{
            //    pictureBoxIpl1.ImageIpl = Convert.Split(ipl);
            //}
            //병합
            //using (SplitMerge16 Convert = new SplitMerge16())
            //{
            //    pictureBoxIpl1.ImageIpl = Convert.Merge(ipl);
            //}

            // 15장 HSV(Hue, Saturation, Value)
            //using (HSV15 Convert = new HSV15())
            //{
            //    pictureBoxIpl1.ImageIpl = Convert.HSV(ipl);
            //}

            // 14장 가장자리 검출 - Edge
            //Canny Edge
            //using (Edge14 Convert = new Edge14())
            //{
            //    pictureBoxIpl1.ImageIpl = Convert.CannyEdge(ipl);
            //}
            //// Sobel Edge
            //using (Edge14 Convert = new Edge14())
            //{
            //    pictureBoxIpl1.ImageIpl = Convert.SobelEdge(ipl);
            //}
            //// Laplace Edge
            //using (Edge14 Convert = new Edge14())
            //{
            //    pictureBoxIpl1.ImageIpl = Convert.LaplaceEdge(ipl);
            //}

            // 13장 흐림 효과 - Blur
            //using (Blur13 Convert = new Blur13())
            //{
            //    pictureBoxIpl1.ImageIpl = Convert.Blur(ipl);
            //}

            // 12장 이진화 - Binary
            //using (Binary12 Convert = new Binary12())
            //{
            //    pictureBoxIpl1.ImageIpl = Convert.Binary(ipl);
            //}

            // 11장 역상 - Reverse Image
            //using (ReverseImage11 Convert = new ReverseImage11())
            //{
            //    pictureBoxIpl1.ImageIpl = Convert.ReverseImage(ipl);
            //}

            // 10장 그레이스케일 - GrayScale
            //using (GrayScale10 Convert = new GrayScale10())
            //{
            //    pictureBoxIpl1.ImageIpl = Convert.GrayScale(ipl);
            //}

            // 9장 Slice 자르기
            //using (Slice9 Convert = new Slice9())
            //{
            //    pictureBoxIpl1.ImageIpl = Convert.Slice(ipl);
            //}

            // 8장 크기 조절
            //using (Resize8 Convert = new Resize8())
            //{
            //    pictureBoxIpl1.ImageIpl = Convert.ResizeImage(ipl);
            //}

            // 7장 확대 축소
            //using (ImagePyramid7 Convert = new ImagePyramid7())
            //{
            //    pictureBoxIpl1.ImageIpl = Convert.ZoomIn(ipl);
            //}
            //using (ImagePyramid7 Convert = new ImagePyramid7())
            //{
            //    pictureBoxIpl1.ImageIpl = Convert.ZoomOut(ipl);
            //}

            //6장 회전
            //using (Rotate6 Convert = new Rotate6())
            //{
            //    pictureBoxIpl1.ImageIpl = Convert.Rotate(ipl, 90);
            //}

            //5장 대칭(상하, 좌우)
            //using (Flip5 Convert = new Flip5())  
            //{
            //    pictureBoxIpl1.ImageIpl = Convert.Symmetry(ipl);
            //}
        }

    }
}
