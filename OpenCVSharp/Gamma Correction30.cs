using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharpEx1
{
    internal class Gamma_Correction30 : IDisposable
    {
        IplImage gamma;

        public IplImage GammaCorrect(IplImage src)
        {
            gamma = new IplImage(src.Size, BitDepth.U8, 3);
            //double gamma_value = 0.5;   //gamma_value는 감마 보정에 사용될 값
            //double gamma_value = 0.0;
            //double gamma_value = 1.0;
            //double gamma_value = 2.0;
            double gamma_value = 4.0;

            //LUT를 진행하기 위해서 사용되는 공식.
            //LUT란 LookUp Table의 약어로 배열 색인화 과정으로 대체하는 데 사용
            byte[] lut = new byte[256];
            for (int i = 0; i < lut.Length; i++)
            {
                lut[i] = (byte)(Math.Pow(i / 255.0, 1.0 / gamma_value) * 255.0);
            }

            //Cv.LUT(원본, 결과, LUT 계산식). 감마 보정 실행
            Cv.LUT(src, gamma, lut);

            return gamma;
        }

        public void Dispose()
        {
            if (gamma != null) Cv.ReleaseImage(gamma);
        }
    }
}
