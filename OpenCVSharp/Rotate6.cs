using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharpEx1
{
    internal class Rotate6 : IDisposable
    {
        IplImage rotate;

        public IplImage Rotate(IplImage src, int angle)
        {
            rotate = new IplImage(src.Size, BitDepth.U8, 3);    //IplImage(크기, 정밀도, 채널)
            //Cv.GetRotationMatrix2D(중심점(x, y), 각도, 스케일)
            CvMat matrix = Cv.GetRotationMatrix2D(Cv.Point2D32f(src.Width / 2, src.Height / 2), angle, 1);
            //Cv.WarpAffine(원본, 결과, 배열, 보간법, 여백색상) 보통 Interpolation.Linear 이거 사용
            Cv.WarpAffine(src, rotate, matrix, Interpolation.Linear, CvScalar.ScalarAll(0));    
            //CvScalar.ScalarAll(0)는 여백 검은색
            return rotate;
        }

        public void Dispose()
        {
            if (rotate != null) Cv.ReleaseImage(rotate);
        }
    }
}
