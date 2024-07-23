using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharpEx1
{
    internal class ImagePyramid7 : IDisposable
    {
        IplImage zoomin;
        IplImage zoomout;

        public IplImage ZoomIn(IplImage src)
        {
            zoomin = new IplImage(Cv.Size(src.Width * 2, src.Height * 2), BitDepth.U8, 3);
            Cv.PyrUp(src, zoomin, CvFilter.Gaussian5x5);
            return zoomin;
        }

        public IplImage ZoomOut(IplImage src)
        {
            zoomout = new IplImage(Cv.Size(src.Width / 2, src.Height / 2), BitDepth.U8, 3);
            Cv.PyrDown(src, zoomout, CvFilter.Gaussian5x5);
            return zoomout;
        }

        public void Dispose()
        {
            if (zoomin != null) Cv.ReleaseImage(zoomin);
            if (zoomout != null) Cv.ReleaseImage(zoomout);
        }
    }
}
