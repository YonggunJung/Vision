using OpenCvSharp;
using OpenCvSharp.CPlusPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharpEx1
{
    internal class HSV15 : IDisposable
    {
        IplImage hsv;

        public IplImage HSV(IplImage src)
        {
            hsv = new IplImage(src.Size, BitDepth.U8, 3);
            
            IplImage h = new IplImage(src.Size, BitDepth.U8, 1);
            IplImage s = new IplImage(src.Size, BitDepth.U8, 1);
            IplImage v = new IplImage(src.Size, BitDepth.U8, 1);

            Cv.CvtColor(src, hsv, ColorConversion.BgrToHsv);
            Cv.Split(hsv, h, s, v, null);
            hsv.SetZero();

            // HSV(Hue, Saturation, Value) 공간은 색상을 표현하기에 간편한 색상 
            // 색상(Hue) - 시각적 감각의 속성,  0 ~ 179의 범위
            // Cv.InRangeS(원본, 최소, 최대, 결과)
            Cv.InRangeS(h, 90, 135, h);
            Cv.Copy(src, hsv, h);

            // 채도(Saturation) - 이미지의 색상 깊이로, 색상이 얼마나 선명한(순수한) 색인지를 의미
            // 0%에 가까울수록 무채색, 100%에 가까울수록 가장 선명한(순수한)색,  0 ~ 255의 범위
            // Cv.InRangeS(원본, 최소, 최대, 결과)
            Cv.InRangeS(s, 100, 255, s);
            Cv.Copy(src, hsv, s);

            // 명도(Value) - 색의 밝고 어두운 정도를 의미
            //높을수록 색상이 밝아지며, 명도가 낮을수록 색상이 어두워짐
            // Cv.InRangeS(원본, 최소, 최대, 결과)
            Cv.InRangeS(v, 50, 200, v);
            Cv.Copy(src, hsv, v);

            return hsv;
        }

        public void Dispose()
        {
            if (hsv != null) Cv.ReleaseImage(hsv);
        }

    }
}
