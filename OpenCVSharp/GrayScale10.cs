using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharpEx1
{
    internal class GrayScale10 : IDisposable
    {
        IplImage gray;
        IplImage gray2;
        IplImage gray3;
        IplImage gray4;
        IplImage gray5;

        public IplImage GrayScale(IplImage src)
        {
            // 그레이스케일
            gray = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, gray, ColorConversion.BgrToGray);

            gray2 = new IplImage(src.Size, BitDepth.U8, 3);
            Cv.CvtColor(src, gray2, ColorConversion.BgrToCrCb);

            gray3 = new IplImage(src.Size, BitDepth.U8, 3);
            Cv.CvtColor(src, gray3, ColorConversion.BgrToLab);

            gray4 = new IplImage(src.Size, BitDepth.U8, 3);
            Cv.CvtColor(src, gray4, ColorConversion.BgrToLuv);

            gray5 = new IplImage(src.Size, BitDepth.U8, 3);
            Cv.CvtColor(src, gray5, ColorConversion.BgrToXyz);

            return gray;
        }
        public void Dispose()
        {
            if (gray != null) Cv.ReleaseImage(gray);
        }
    }
}
