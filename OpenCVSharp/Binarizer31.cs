using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp.Extensions;

namespace OpenCVSharpEx1 
{
    internal class Binarizer31 : IDisposable
    {
        IplImage gray;
        IplImage bina;

        public IplImage GrayScale(IplImage src)
        {
            gray = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, gray, ColorConversion.BgrToGray);
            return gray;
        }

        public IplImage BinarizerMethod(IplImage src)
        {
            bina = new IplImage(src.Size, BitDepth.U8, 1);
            gray = this.GrayScale(src);

            //Binarizer.Nick(그레이스케일, 결과, 커널의 크기, 계수)
            //커널의 크기는 홀수만 가능, 크기가 클수록 이미지가 깔끔
            //계수는 커널의 크기와 이미지를 고려하여 적절한 값을 대입
            //Binarizer.Nick(gray, bina, 61, 0.3);
            //Binarizer.Niblack(gray, bina, 61, -0.5);
            //Binarizer.NiblackFast(gray, bina, 61, -0.5);

            //Binarizer.Sauvola(그레이스케일, 결과, 커널의 크기, 계수1, 계수2)
            // 계수는 적절한 값을 대입
            //Binarizer.Sauvola(gray, bina, 77, 0.2, 64);
            //Binarizer.SauvolaFast(gray, bina, 77, 0.2, 64);
            Binarizer.Bernsen(gray, bina, 51, 60, 150);

            return bina;
        }

        public void Dispose()
        {
            if (gray != null) Cv.ReleaseImage(gray);
            if (bina != null) Cv.ReleaseImage(bina);
        }
    }
}
