using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharpEx1
{
    internal class Converter35 : IDisposable
    {
        public Bitmap ConvertToBitmap(IplImage src)
        {
            Bitmap bitmap = src.ToBitmap();
            return bitmap;
        }

        public IplImage ConvertToIplImage(Bitmap src)
        {
            //*.ToMat()을 사용하여 Mat형식으로도 변환이 가능
            IplImage iplimage = src.ToIplImage();
            return iplimage;
        }
        //BitmapConverter를 이용하여 변환이 가능합니다.
        //Bitmap → IplImage : bitmap = BitmapConverter.ToIplImage(src);
        //IplImage → Bitmap : iplimage = BitmapConverter.ToBitmap(src);
        //IplImage → Mat : iplimage = BitmapConverter.ToMat(src);
        public void Dispose()
        {

        }
    }
}
