using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharpEx1
{
    internal class Hough_Transform_Circles26 : IDisposable
    {
        IplImage houcircle;

        public IplImage HoughCircles(IplImage src)
        {
            houcircle = new IplImage(src.Size, BitDepth.U8, 3);
            IplImage gray = new IplImage(src.Size, BitDepth.U8, 1);

            Cv.Copy(src, houcircle);
            Cv.CvtColor(src, gray, ColorConversion.BgrToGray);
            Cv.Smooth(gray, gray, SmoothType.Gaussian, 9);

            CvMemStorage Storage = new CvMemStorage();
            CvSeq<CvCircleSegment> circles = Cv.HoughCircles(gray, Storage, HoughCirclesMethod.Gradient, 1, 100, 150, 50, 0, 0);
            foreach (CvCircleSegment item in circles)
            {
                Cv.Circle(houcircle, item.Center, (int)item.Radius, CvColor.Blue, 3);
            }
            return houcircle;
        }

        public void Dispose()
        {
            if (houcircle != null) Cv.ReleaseImage(houcircle);
        }
    }
}
