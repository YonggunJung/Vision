using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharpEx1
{
    internal class SplitMerge16 : IDisposable
    {
        //영상이나 이미지의 채널을 나누고 합치기 위해 사용
        IplImage b;
        IplImage g;
        IplImage r;

        IplImage merge;

        public IplImage Split(IplImage src)
        {
            //bgr 각각은 단색이기 때문에 채널은 1
            b = new IplImage(src.Size, BitDepth.U8, 1);
            g = new IplImage(src.Size, BitDepth.U8, 1);
            r = new IplImage(src.Size, BitDepth.U8, 1);
            //Cv,Split(원본, 채널1, 채널2, 채널3, 채널4)
            Cv.Split(src, b, g, r, null);

            //return r;
            //return g;
            return b;
        }

        public IplImage Merge(IplImage src)
        {
            //Cv.Merge()를 이용하여 각 채널을 합침
            //b, g ,r과 채널 순서를 이용하여 특정 색상 채널을 다른 색상 채널 계열로 혼합 및 제거 가능
            merge = new IplImage(src.Size, BitDepth.U8, 3);
            b = new IplImage(src.Size, BitDepth.U8, 1);
            g = new IplImage(src.Size, BitDepth.U8, 1);
            r = new IplImage(src.Size, BitDepth.U8, 1);

            //Cv.Split(src, b, g, r, null);
            //Cv.Merge(b, null, null, null, merge);
            //Cv.Merge(g, null, null, null, merge);
            //Cv.Merge(r, null, null, null, merge);
            //Cv.Merge(null, b, null, null, merge);
            //Cv.Merge(null, null, b, null, merge);
            //Cv.Merge(b, b, b, null, merge);
            //Cv.Merge(r, g, b, null, merge);
            //Cv.Merge(b, g, r, null, merge);

            return merge;
        }

        public void Dispose()
        {
            if (b != null) Cv.ReleaseImage(b);
            if (g != null) Cv.ReleaseImage(g);
            if (r != null) Cv.ReleaseImage(r);
            if (merge != null) Cv.ReleaseImage(merge);
        }
    }
}
