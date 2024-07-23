using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharpEx1
{
    internal class Resize8 : IDisposable
    {
        IplImage resize;

        public IplImage ResizeImage(IplImage src)
        {
            // 원본 넓이의 절반, 원본 높이의 +100
            resize = new IplImage(Cv.Size(src.Width / 2, src.Height + 100), BitDepth.U8, 3);
            Cv.Resize(src, resize, Interpolation.Linear);   //Cv.Reisze(원본, 결과, 보간법)
            return resize;
        }

        public void Dispose()
        {
            if (resize != null) Cv.ReleaseImage(resize);
        }
    }
}
