using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharpEx1
{
    internal class Hough_Transform_Lines25 : IDisposable
    {
        IplImage bin;
        IplImage canny;
        IplImage houline;

        public IplImage Binary(IplImage src)
        {
            bin = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, bin, ColorConversion.RgbToGray);
            Cv.Threshold(bin, bin, 120, 255, ThresholdType.Binary);
            return bin;
        }

        public IplImage CannyEdge(IplImage src)
        {
            canny = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.Canny(src, canny, 50, 100);
            return canny;
        }

        public IplImage HoughLines(IplImage src)
        {
            houline = new IplImage(src.Size, BitDepth.U8, 3);
            canny = new IplImage(src.Size, BitDepth.U8, 1);

            canny = this.CannyEdge(this.Binary(src));
            Cv.CvtColor(canny, houline, ColorConversion.GrayToBgr);

            CvMemStorage Storage  = new CvMemStorage();

            // Standard 방법
            //HoughLines2(메모리 저장소, 변환 방법, rho, theta, 임계값, 매개변수1, 매개변수2)
            //rho는 누산기의 거리 분해능. theta는 누산기의 각도 분해능. 단위는 라디안이므로 각도 단위로 입력
            //매개변수1
            //Standard 사용 시 : 사용 안함
            //Probabilistic 사용 시: 최소 선 길이
            //MultiScale 사용 시 : rho에 대한 약수
            //매개변수2
            //Standard 사용 시: 사용 안함
            //Probabilistic 사용 시: 최대 선 간격
            //MultiScale 사용 시 : theta에 대한 약수
            CvSeq lines = canny.HoughLines2(Storage, HoughLinesMethod.Standard, 1, Math.PI / 180, 50, 0, 0);

            for (int i = 0; i<Math.Min(lines.Total, 3); i++)
            {
                //Math.Min(lines.Total, 3)은 lines의 값이 3보다 낮은 값만 사용하게끔 하여 반복

                CvLineSegmentPolar element = lines.GetSeqElem<CvLineSegmentPolar>(i).Value;

                float r = element.Rho;
                float theta = element.Theta;

                double a = Math.Cos(theta);
                double b = Math.Sin(theta);
                double x0 = r * a;
                double y0 = r * b;
                int scale = src.Size.Width + src.Size.Height;

                CvPoint pt1 = new CvPoint(Convert.ToInt32(x0 - scale * b), Convert.ToInt32(y0 + scale * a));
                CvPoint pt2 = new CvPoint(Convert.ToInt32(x0 + scale * b), Convert.ToInt32(y0 - scale * a));

                houline.Circle(new CvPoint((int)x0, (int)y0), 5, CvColor.Yellow, -1);
                houline.Line(pt1, pt2, CvColor.Red, 1, LineType.AntiAlias);

                //Probabilistic 방법
                //CvSeq lines = canny.HoughLines2(Storage, HoughLinesMethod.Probabilistic, 1, Math.PI / 180, 140, 50, 10);

                //for (int i = 0; i < lines.Total; i++)
                //{
                //    CvLineSegmentPoint element = lines.GetSeqElem<CvLineSegmentPoint>(i).Value;
                //    houline.Line(element.P1, element.P2, CvColor.Yellow, 1, LineType.AntiAlias);
                //}
            }
            return houline;
        }
        public void Dispose()
        {
            if (bin != null) Cv.ReleaseImage(bin);
            if (canny != null) Cv.ReleaseImage(canny);
            if (houline != null) Cv.ReleaseImage(houline);
        }
    }
}
