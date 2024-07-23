using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharpEx1
{
    internal class Binary12 : IDisposable
    {
        IplImage bin;
        public IplImage Binary(IplImage src)
        {
            bin = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, bin, ColorConversion.RgbToGray); //그레이스케일 변환
            //Cv.Threshold(원본, 결과, 임계값, 최댓값, 임계값종류)
            Cv.Threshold(bin, bin, 100, 255, ThresholdType.Binary);
            //Cv.Threshold(bin, bin, 100, 255, ThresholdType.BinaryInv);
            //Cv.Threshold(bin, bin, 100, 255, ThresholdType.Otsu);
            //Cv.Threshold(bin, bin, 100, 255, ThresholdType.ToZero);
            //Cv.Threshold(bin, bin, 100, 255, ThresholdType.ToZeroInv);
            //Cv.Threshold(bin, bin, 100, 255, ThresholdType.Truncate);

            //Cv.Threshold(bin, bin, 50, 255, ThresholdType.Binary);
            //Cv.Threshold(bin, bin, 150, 255, ThresholdType.Binary);
            //Cv.Threshold(bin, bin, 50, 200, ThresholdType.Binary);
            //Cv.Threshold(bin, bin, 100, 200, ThresholdType.Binary);
            return bin;
        }

        public void Dispose()
        {
            if (bin != null) Cv.ReleaseImage(bin);
        }
    }
}
