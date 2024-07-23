using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharpEx1
{
    internal class Dilate_Erode27 : IDisposable
    {
        IplImage dil;
        IplImage ero;

        public IplImage DilateImage(IplImage src)
        {
            dil = new IplImage(src.Size, BitDepth.U8, 3);

            //IplConvKernel(너비, 높이, X좌표, Y좌표, 형태, 커스텀형태)
            //ElementShape.Cross : 십자형 구조 요소
            //ElementShape.Custom : 사용자 정의 구조 요소
            //ElementShape.Ellipse : 타원형(직사각형에 채워진 타원) 구조 요소
            //ElementShape.Rect : 직사각형 구조 요소
            IplConvKernel element = new IplConvKernel(4, 4, 2, 2, ElementShape.Custom, new int[3, 3]);
            //Cv.* (원본, 결과, 구조 요소, 반복횟수)
            Cv.Dilate(src, dil, element, 3);
            return dil;
        }

        public IplImage ErodeImage(IplImage src)
        {
            ero = new IplImage(src.Size, BitDepth.U8, 3);

            IplConvKernel element = new IplConvKernel(4, 4, 2, 2, ElementShape.Custom, new int[3, 3]);
            //Cv.* (원본, 결과, 구조 요소, 반복횟수)
            Cv.Erode(src, ero, element, 3);
            return ero;
        }

        public void Dispose()
        {
            if (dil != null) Cv.ReleaseImage(dil);
            if (ero != null) Cv.ReleaseImage(ero);
        }
    }
}
