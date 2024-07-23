using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharpEx1
{
    internal class Flip5 : IDisposable
    {
        IplImage symm;

        public IplImage Symmetry(IplImage src)
        {
            symm = new IplImage(src.Size, BitDepth.U8, 3);
            Cv.Flip(src, symm, FlipMode.X); //상하 대칭
            Cv.Flip(src, symm, FlipMode.Y); // 좌우 대칭
            return symm;
        }

        public void Dispose()
        {
            if (symm != null) Cv.ReleaseImage(symm);
        }
    }
}
