using OpenCvSharp;
using OpenCvSharp.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp.Blob;
using OpenCvSharp.CPlusPlus;

namespace OpenCVSharpEx1
{
    internal class Blob_Labeling32 : IDisposable
    {
        IplImage bin;
        IplImage blob;

        public IplImage Binary(IplImage src)
        {
            bin = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, bin, ColorConversion.BgrToGray);
            Cv.Threshold(bin, bin, 50, 255, ThresholdType.Binary);
            return bin;
        }

        public IplImage BlobImage(IplImage src)
        {
            blob = new IplImage(src.Size, BitDepth.U8, 3);
            bin = this.Binary(src); //라벨링에 사용될 이미지

            CvBlobs blobs = new CvBlobs();
            blobs.Label(bin);   //라벨링을 진행. 이진화 이미지를 사용

            //blobs.RenderBlobs(원본, 결과)
            blobs.RenderBlobs(src, blob);   // 라벨링 결과 확인

            foreach (KeyValuePair<int, CvBlob> item in blobs)   //라벨링 정보를 확인
            {
                CvBlob b = item.Value;

                //라벨링 번호 : b.Label
                //중심점: b.Centroid
                //면적(moment 00) : b.Area
                //등고선 각도: b.Angle() / Math.PI * 180
                //사각형 정보 : b.Rect
                //라벨링의 시작 위치 : b.Contour.StartingPoint
                blob.PutText(Convert.ToString(b.Label), b.Centroid, new CvFont(FontFace.HersheyComplex, 1, 1), CvColor.Red);

                //라벨링은 좌측 상단부터 우측 상단방향으로 이동해가면서 파악
            }

            return blob;
        }
        public void Dispose()
        {
            if (bin != null) Cv.ReleaseImage(bin);
            if (blob != null) Cv.ReleaseImage(blob);
        }
    }
}
