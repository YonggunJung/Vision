using OpenCvSharp;
using OpenCvSharp.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OpenCVSharpEx1
{
    internal class Blob_Contour33 : IDisposable
    {
        IplImage bin;
        IplImage blobcontour;

        public IplImage Binary(IplImage src)
        {
            bin = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, bin, ColorConversion.BgrToGray);
            Cv.Threshold(bin, bin, 50, 255, ThresholdType.Binary);

            return bin;
        }

        public IplImage BlobContourImage(IplImage src)
        {
            blobcontour = new IplImage(src.Size, BitDepth.U8, 3);
            bin = this.Binary(src);

            CvBlobs blobs = new CvBlobs();
            blobs.Label(bin);

            foreach(KeyValuePair<int, CvBlob> item in blobs)
            {
                CvBlob b = item.Value;

                //CvContourChainCode를 이용하여 b에서 Contour값을 받아옴
                CvContourChainCode cc = b.Contour;
                //cc.Render(blobcontour)를 이용하여 blobcontour에 윤곽선(컨투어)를 그림
                cc.Render(blobcontour);

                //CvContourPolygon을 이용하여 폴리곤형태로 변환
                CvContourPolygon polygon = cc.ConvertToPolygon();
                foreach(CvPoint p in polygon)   //blobcontour에 폴리곤을 그림
                {
                    blobcontour.Circle(p, 1, CvColor.Red, -1);
                }
            }
            return blobcontour;
        }

        public void Dispose()
        {
            if (bin != null) Cv.ReleaseImage(bin);
            if (blobcontour != null) Cv.ReleaseImage(blobcontour);
        }
    }
}
