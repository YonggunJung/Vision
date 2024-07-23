using OpenCvSharp;
using OpenCvSharp.CPlusPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharpEx1
{
    internal class Moments24 : IDisposable
    {
        IplImage bin;
        IplImage mom;

        public IplImage Binary(IplImage src)
        {
            bin = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, bin, ColorConversion.RgbToGray);
            Cv.Threshold(bin, bin, 150, 255, ThresholdType.Binary);
            return bin;
        }

        public IplImage Moment(IplImage src)
        {
            mom = new IplImage(src.Size, BitDepth.U8, 3);
            bin = new IplImage(src.Size, BitDepth.U8, 1);

            //원본 이미지를 복사한 mom과 Binary 이미지인 bin을 선언하고 적용
            Cv.Copy(src, mom);
            bin = this.Binary(src);

            CvMemStorage Storage = new CvMemStorage();
            CvSeq<CvPoint> contours;
            Cv.FindContours(bin, Storage, out contours, CvContour.SizeOf, ContourRetrieval.List, ContourChain.ApproxNone);

            CvSeq<CvPoint> apcon_seq = Cv.ApproxPoly(contours, CvContour.SizeOf, Storage, ApproxPolyMethod.DP, 3, true);

            //moments를 선언하여 중심점에 관한 정보
            CvMoments moments;
            int cX = 0, cY = 0;

            for (CvSeq<CvPoint> c = apcon_seq; c != null; c = c.HNext)
            {
                if(c.Total > 4)
                {
                    //Cv.Moments(물체의 코너점들, 중심점 데이터, Binary조건)
                    Cv.Moments(c, out moments, true);
                    //out moments를 통하여 검출된 moments를 저장하며,
                    //Binary조건은 참일 경우 0의 값이 아닌 이미지 픽셀은 1의 값으로 처리하게됩니다
                    //Spatial Moments : M00, M01, M02, M03, M10, M11, M12, M20, M21, M30
                    //Central Moments : Mu02, Mu03, Mu11, Mu12, Mu20, Mu21, Mu30
                    //Central Normalized Moments : Nu02, Nu03, Nu11, Nu12, Nu20, Nu21, Nu30
                    //Mu00 = M00, Mu01 = 0, Mu10 = 0
                    //Nu00 = 1, Nu01 = 0, Nu10 = 0

                    cX = Convert.ToInt32(moments.M10 / moments.M00);
                    cY = Convert.ToInt32(moments.M01 / moments.M00);

                    Cv.Circle(mom, new CvPoint(cX, cY), 5, CvColor.Red, -1);
                }
            }
            return mom;
        }
        public void Dispose()
        {
            if (bin != null) Cv.ReleaseImage(bin);
            if (mom != null) Cv.ReleaseImage(mom);
        }
    }
}
