using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharpEx1
{
    internal class Distance_Transform44 : IDisposable
    {
        private  IplImage dist;

        public  IplImage DistTransform(IplImage src)
        {
            //결과를 표시할 이미지인 dist
            dist = new IplImage(src.Size, BitDepth.F32, 1); //이미지는 정밀도를 F32
            IplImage bin = new IplImage(src.Size, BitDepth.U8, 1);  //이진화 이미지 bin을 선언

            //bin 이미지에 이진화를 적용
            Cv.CvtColor(src, bin, ColorConversion.BgrToGray);
            Cv.Threshold(bin, bin, 225, 255, ThresholdType.BinaryInv);

            //팽창과 침식을 이용하여 이진화 이후 나타나는 노이즈를 제거
            Cv.Dilate(bin, bin, null, 2);
            Cv.Erode(bin, bin, null, 2);


            //Cv.DistTransform()를 사용하여 가장 가까운 흑색 픽셀까지의 거리를 계산
            //Cv.DistTransform(이진화 이미지, 결과, 거리 유형, 마스크 크기)
            Cv.DistTransform(bin, dist, DistanceType.L2, 3);    //DistanceType.User : 사용자 지정 거리

            //거리 변환을 통해 얻어낸 이미지를 이용하여 다시 이진화를 적용해 붙어있는 이미지를 구분
            Cv.Threshold(dist, dist, 20, 150, ThresholdType.Binary);

            return dist;
        }

        public void Dispose()
        {
            if (dist != null) Cv.ReleaseImage(dist);
        }
    }
}
