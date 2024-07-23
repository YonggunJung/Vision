using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharpEx1
{
    internal class ReverseImage11 : IDisposable
    {
        IplImage reverse;

        public IplImage ReverseImage(IplImage src)
        {
            reverse = new IplImage(src.Size, BitDepth.U8, 3);
            Cv.Not(src, reverse); // 반전, Cv.Not(원본, 결과)
            return reverse;
        }

        public void Dispose()
        {
            if (reverse != null) Cv.ReleaseImage(reverse);
        }
    }
}
