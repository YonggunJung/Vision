using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharpEx1
{
    internal class Slice9 : IDisposable
    {
        IplImage slice;

        public IplImage Slice(IplImage src)
        {
            slice = new IplImage(Cv.Size(165, 35), BitDepth.U8, 3);
            //Cv.SetImageROI(소스, new CvRect(x좌표 시작점, y좌표 시작점, 넓이, 높이))
            Cv.SetImageROI(src, new CvRect(240, 280, slice.Width, slice.Height));
            Cv.Resize(src, slice);  //잘라진 src를 넣기
            Cv.ResetImageROI(src);  //잘라진 src를 초기 상태의 src로 변경
            return slice;
        }

        public void Dispose()
        {
            if (slice != null) Cv.ReleaseImage(slice);
        }
    }
}
