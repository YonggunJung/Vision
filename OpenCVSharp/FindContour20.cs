using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharpEx1
{
    internal class FindContour20 : IDisposable
    {
        IplImage bin;   // 검색할 이미지인 bin
        IplImage con;   // 출력할 이미지인 con
        public IplImage Binary(IplImage src)
        {
            bin = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, bin, ColorConversion.RgbToGray);
            Cv.Threshold(bin, bin, 150, 255, ThresholdType.Binary);
            return bin;
        }

        public IplImage Contour(IplImage src)
        {
            //컨투어는 8Bit 단일 채널, Binary 영상으로 검출
            con = new IplImage(src.Size, BitDepth.U8, 3);
            bin = new IplImage(src.Size, BitDepth.U8, 1);

            //con과 bin을 만들고 이미지를 복사하고 덮어씌움
            Cv.Copy(src, con);
            bin = this.Binary(src);

            //Storage는 윤곽선(컨투어)의 메모리를 저장
            CvMemStorage Storage = new CvMemStorage();
            CvSeq<CvPoint> contours;    //contours는 윤곽선(컨투어)의 정보와 정수의 2D 좌표를 저장

            //모든 윤곽선(컨투어)를 검색
            //검색 방법(ContourRetrieval.*)
            //ContourRetrieval.CComp : 모든 윤곽선을 검색하여 2 단계 계층 구조로 구성합니다.
            //최상위 레벨은 구성 요소의 외곽(외부) 경계이고, 두 번째 레벨은 내곽(홀)의 경계입니다.
            //ContourRetrieval.External : 외곽 윤곽선만 검출합니다.
            //ContourRetrieval.List : 모든 윤곽선을 검출하여 list에 넣습니다.
            //ContourRetrieval.Tree : 모든 윤곽선을 검출하여 Tree계층 구조로 만듭니다.

            //근사화 방법(ContourChain.*)
            //ContourChain.ApproxNone : 윤곽점들의 모든 점을 반환합니다.
            //ContourChain.ApproxSimple : 윤곽점들 단순화 수평, 수직 및 대각선 요소를 압축하고 끝점만 남겨 둡니다.
            //ContourChain.Code : 프리먼 체인 코드에서의 윤곽선으로 적용합니다.
            //ContourChain.ApproxTC89KCOS, ContourChain.ApproxTC89L1 : Teh - chin 알고리즘 적용합니다.
            //ContourChain.LinkRuns : 하나의 수평 세그먼트를 연결하여 완전히 다른 윤곽선 검색 알고리즘을 사용합니다.
            //Cv,FindContours(이진화 이미지, 메모리 저장소, 윤곽선 저장, 자료구조의 크기, 검색 방법, 근사화 방법)
            Cv.FindContours(bin, Storage, out contours, CvContour.SizeOf, ContourRetrieval.List, ContourChain.ApproxNone);

            //Cv.DrawContours()를 이용하여 컨투어를 그림
            //Cv.DrawContours(결과, 윤곽선, 외곽윤곽색상, 내곽윤곽색상, 최대레벨, 두께, 선형타입)
            Cv.DrawContours(con, contours, CvColor.Yellow, CvColor.Red, 1, 4, LineType.AntiAlias);

            Cv.ClearSeq(contours);
            Cv.ReleaseMemStorage(Storage);

            return con;
        }

        public void Dispose()
        {
            if (bin != null) Cv.ReleaseImage(bin);
            if (con != null) Cv.ReleaseImage(con);
        }
    }
}
