using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OpenCvSharp.CPlusPlus.SimpleBlobDetector;

namespace OpenCVSharpEx1
{
    internal class Blur13 : IDisposable
    {
        IplImage blur;

        public IplImage Blur(IplImage src)
        {
            blur = new IplImage(src.Size, BitDepth.U8, 3);
            //Cv.Smooth(원본, 결과, 효과종류, param1, param2, param3, param4), param들은 생략가능
            //SmoothType.Gaussian - param1* param2 크기 픽셀들의 가중치 합,
            //가로 방향 표준편차(param3), 세로 방향 표준 편차(parma4)
            Cv.Smooth(src, blur, SmoothType.Gaussian);

            //SmoothType.Blur - 단순 블러: param1* param2 크기 픽셀들의 평균
            //Cv.Smooth(src, blur, SmoothType.Blur);

            //SmoothType.BlurNoScale - 스케일링이 없는 단순 블러: param1* param2 크기 픽셀들의 합
            //Cv.Smooth(src, blur, SmoothType.BlurNoScale);

            // SmoothType.Median - 중간값 블러 : param1 * param2 크기 픽셀들의 중간값
            //Cv.Smooth(src, blur, SmoothType.Median);
            return blur;
        }
        public void Dispose()
        {
            if (blur != null) Cv.ReleaseImage(blur);
        }
    }
}
