using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharpEx1
{
    internal class Edge14 : IDisposable
    {
        IplImage canny;
        IplImage sobel;
        IplImage laplace;

        public IplImage CannyEdge(IplImage src)
        {
            canny = new IplImage(src.Size, BitDepth.U8, 1); // Canny Edge는 단색이기 때문에 채널은 1
            /*Cv.Canny(src, canny, 0, 100);*/   //Cv.Canny(원본, 결과, 임계값1, 임계값2)
            //Cv.Canny(src, canny, 100, 255);
            // 임계값1 : 임계값1 이하에 포함된 가장자리는 가장자리에서 제외
            // 임계값2 : 임계값2 이상에 포함된 가장자리는 가장자리로 간주
            return canny;
        }

        public IplImage SobelEdge(IplImage src)
        {
            // Sobel Edge는 x방향 미분값과 y방향 미분값을 이용하여 가장자리를 검출
            sobel = new IplImage(src.Size, BitDepth.U8, 3);
            Cv.Copy(src, sobel);
            // Cv.Sobel(원본, 결과, x방향 미분, y방향 미분, 커널의 크기)
            //Cv.Sobel(sobel, sobel, 0, 1, ApertureSize.Size3);
            Cv.Sobel(sobel, sobel, 1, 0, ApertureSize.Size3);
            return sobel;
        }

        public IplImage LaplaceEdge(IplImage src)
        {
            laplace = new IplImage(src.Size, BitDepth.U8, 3);
            Cv.Laplace(src, laplace);   // Cv.Laplace(원본, 결과)
            return laplace;
        }

        public void Dispose()
        {
            if (canny != null) Cv.ReleaseImage(canny);
            if (sobel != null) Cv.ReleaseImage(sobel);
            if (laplace != null) Cv.ReleaseImage(laplace);
        }
    }
}
