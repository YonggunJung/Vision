using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenCVSharpEx1
{
    internal class Haar_Classifier_Cascade29 : IDisposable
    {
        IplImage haarface;

        public IplImage FaceDetection(IplImage src)
        {
            haarface = new IplImage(src.Size, BitDepth.U8, 3);
            //haarface는 원본을 복사한 이미지
            Cv.Copy(src, haarface);

            const double scale = 0.9;   // 검출되는 이미지의 비율
            const double scaleFactor = 1.139;
            const int minNeighbors = 1;

            //검출되는 이미지인 Detected_image를 scale의 비율에 맞게 재조정
            using (IplImage Detected_image = new IplImage(new CvSize(Cv.Round(src.Width / scale), Cv.Round(src.Height / scale)), BitDepth.U8, 1))
            {
                using (IplImage gray = new IplImage(src.Size, BitDepth.U8, 1))
                {
                    //Cv.CvtColor와 Cv.Resize를 통하여 이미지의 크기를 조정
                    Cv.CvtColor(src, gray, ColorConversion.BgrToGray);
                    Cv.Resize(gray, Detected_image, Interpolation.Linear);
                    //Cv.EqualizeHist(원본, 결과)를 통하여 GrayScale 이미지의 화상을 평탄화
                    //매우 어둡거나 매우 밝은 부분들이 일정하게 조정
                    Cv.EqualizeHist(Detected_image, Detected_image);
                }


                using (CvHaarClassifierCascade cascade = CvHaarClassifierCascade.FromFile(@"C:\Users\admin\source\repos\OpenCVSharpEx1\data\haarcascade_frontalface_alt.xml"))
                using (CvMemStorage storage = new CvMemStorage())
                {
                //Cv.HaarDetectObjects(탐지이미지, 객체 감지 파일, 메모리 저장소, 스케일팩터, 이웃수, 작동 모드, 최소 크기, 최대 크기)Detected_image : 탐지할 이미지입니다.
                //cascade: 객체를 감지할 파일의 경로가 저장된 HaarClassifierCascade.
                //storage: 매모리가 저장될 저장소.
                //scaleFactor : 이미지 크기가 감소되는 양.
                //minNeighbors: 검출 시 유지해야하는 인접한 얼굴의 수. 0일 경우 중복해서 찾거나,
                //너무 높을 경우 가까운 얼굴은 찾지 못함.
                //HaarDetectionType.* : 작동 모드.
                //HaarDetectionType.Zero : 최적화를 수행하지 않습니다.
                //HaarDetectionType.DoCannyPruning : Canny Edge detector를 사용하여 가장자리가 너무 적거나 많은 경우 탐색X
                //HaarDetectionType.ScaleImage : 확대 / 축소를 하지 않고 downscale을 통하여 cascade에서 수행.
                //HaarDetectionType.FindBiggestObject : 가장 큰 객체 1명만 검출.
                //HaarDetectionType.DoRoughSearch : 객체를 충분히 찾으면 작은 크기의 객체는 검출X.
                //MinSize: 검출할 얼굴의 최소 크기를 설정.
                //MaxSize : 검출할 얼굴의 최대 크기를 설정.
                //Tip : CvSize(0, 0)으로 설정 시 제한 크기를 설정하지 않고 찾음
                    CvSeq<CvAvgComp> faces = Cv.HaarDetectObjects(Detected_image, cascade, storage, scaleFactor, minNeighbors, HaarDetectionType.ScaleImage, new CvSize(90, 90), new CvSize(0, 0));

                    //for문을 이용하여 검출된 얼굴의 위치에 Circle을 그림
                    for (int i = 0; i < faces.Total; i++)
                    {
                        CvRect r = faces[i].Value.Rect;
                        CvPoint center = new CvPoint
                        {
                            X = Cv.Round((r.X + r.Width * 0.5) * scale),
                            Y = Cv.Round((r.Y + r.Height * 0.5) * scale)
                        };
                        int radius = Cv.Round((r.Width + r.Height) * 0.25 * scale);
                        haarface.Circle(center, radius, CvColor.Black, 3, LineType.AntiAlias, 0);
                    }
                }
                return haarface;
            }
        }
        public void Dispose()
        {
            if (haarface != null) Cv.ReleaseImage(haarface);
        }
    }
}
