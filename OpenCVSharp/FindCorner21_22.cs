using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenCVSharpEx1
{
    internal class FindConer21_22 : IDisposable
    {
        IplImage gray;
        IplImage corner;

        public IplImage GrayScale(IplImage src)
        {
            gray = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, gray, ColorConversion.BgrToGray);
            return gray;
        }

        public IplImage GoodFeaturesToTrack(IplImage src)
        {
            corner = new IplImage(src.Size, BitDepth.U8, 3);    //8Bit 단일 채널, GrayScale 영상 검출
            gray = new IplImage(src.Size, BitDepth.U8, 1);  //출력할 이미지인 corner와 검색할 이미지인 gray를 만듦
            //GoodFeaturesToTrack에 사용하기 위한 매개변수 이미지 eigImg, tempImg를 만듦
            IplImage eigImg = new IplImage(src.Size, BitDepth.U8, 1);
            IplImage tempImg = new IplImage(src.Size, BitDepth.U8, 1);

            //corner에 원본 이미지를 복사하여 덮음
            Cv.Copy(src, corner);
            gray = this.GrayScale(src); // 그레이 스케일 적용

            CvPoint2D32f[] corners;     //corners는 검색된 코너의 벡터 값
            int cornerCount = 150;      //cornerCount는 반환할 코너의 최대 개수

            // 코너 검출
            //Cv.GoodFeaturesToTrack(그레이스케일, eigImg, tempImg, 코너점, 코너의 개수, 퀄리티 레벨, 최소 거리)
            //qualityLevel(퀄리티 레벨)은 코너 품질 설정
            Cv.GoodFeaturesToTrack(gray, eigImg, tempImg, out corners, ref cornerCount, 0.01, 5);

            //Cv.FindCornerSubPix()를 이용하여 코너점들의 위치를 수정
            //Cv.FindCornerSubPix(그레이스케일, 코너점, 코너의 개수, win Size, zeroZone Size, 기준)
            //win Size는 검출하려는 부분의 절반 크기
            //zeroZone Size는 검출에서 제외하려는 부분의 절반 크기
            //(-1, -1)은 검출에서 제외하려는 부분이 없음
            //Criteria(기준)은 코너 정밀화 반복작업
            //new CvTermCriteria(maxlter, epsilon)입니다.
            //maxlter는 입력된 수치 만큼 반복작업하며, epsilon보다 값이 낮아지면 종료
            Cv.FindCornerSubPix(gray, corners, cornerCount, new CvSize(3, 3), new CvSize(-1, -1), new CvTermCriteria(20, 0.03));

            //for문과 Cv.Circle을 이용하여 검출된 코너점들을 corner이미지에 그림
            for (int i = 0; i < cornerCount; i++)
            {
                Cv.Circle(corner, corners[i], 3, CvColor.Black, 2);
            }

            return corner;
        }
        public IplImage GoodFeaturesToTrack_HarrisCornerDetector(IplImage src)
        {
            corner = new IplImage(src.Size, BitDepth.U8, 3);    //8Bit 단일 채널, GrayScale 영상 검출
            gray = new IplImage(src.Size, BitDepth.U8, 1);  //출력할 이미지인 corner와 검색할 이미지인 gray를 만듦
            //GoodFeaturesToTrack에 사용하기 위한 매개변수 이미지 eigImg, tempImg를 만듦
            IplImage eigImg = new IplImage(src.Size, BitDepth.U8, 1);
            IplImage tempImg = new IplImage(src.Size, BitDepth.U8, 1);

            //corner에 원본 이미지를 복사하여 덮음
            Cv.Copy(src, corner);
            gray = this.GrayScale(src); // 그레이 스케일 적용

            CvPoint2D32f[] corners;     //corners는 검색된 코너의 벡터 값
            int cornerCount = 150;      //cornerCount는 반환할 코너의 최대 개수

            // 코너 검출
            //Cv.GoodFeaturesToTrack(그레이스케일, eigImg, tempImg, 코너점, 코너의 개수, 퀄리티 레벨, 최소 거리, 마스크, 블록 크기, Harris 방법 사용 유/무, ksize)
            //qualityLevel(퀄리티 레벨)은 코너 품질 설정
            //mask(마스크)는 코너가 감지되는 영역을 지정
            //blockSize(블록 크기)는 코너 계산을 위한 평균 블록의 크기
            //useHarrisDetector(Haris 방법 사용 유 / 무)는 Harris 방법을 사용할지에 대한 bool값
            //ksize는 Harris 방법의 매개 변수
            Cv.GoodFeaturesToTrack(gray, eigImg, tempImg, out corners, ref cornerCount, 0.01, 5, null, 3, true, 0.01);

            //Cv.FindCornerSubPix()를 이용하여 코너점들의 위치를 수정
            //Cv.FindCornerSubPix(그레이스케일, 코너점, 코너의 개수, win Size, zeroZone Size, 기준)
            //win Size는 검출하려는 부분의 절반 크기
            //zeroZone Size는 검출에서 제외하려는 부분의 절반 크기
            //(-1, -1)은 검출에서 제외하려는 부분이 없음
            //Criteria(기준)은 코너 정밀화 반복작업
            //new CvTermCriteria(maxlter, epsilon)입니다.
            //maxlter는 입력된 수치 만큼 반복작업하며, epsilon보다 값이 낮아지면 종료
            Cv.FindCornerSubPix(gray, corners, cornerCount, new CvSize(3, 3), new CvSize(-1, -1), new CvTermCriteria(20, 0.03));

            //for문과 Cv.Circle을 이용하여 검출된 코너점들을 corner이미지에 그림
            for (int i = 0; i < cornerCount; i++)
            {
                Cv.Circle(corner, corners[i], 3, CvColor.Black, 2);
            }

            return corner;
        }

        public IplImage HarrisCorner(IplImage src)
        {
            gray = new IplImage(src.Size, BitDepth.U8, 1);
            corner = new IplImage(src.Size, BitDepth.F32, 1);

            gray = this.GrayScale(src);

            // CvCornerHarris(그레이스케일, 반환이미지, ApertureSize, kisze)
            Cv.CornerHarris(gray, corner, 3, ApertureSize.Size3, 0.05);
            //이미지에서 이웃한 화소들 중 최대 화소값으로 대체하여 선명하게 만듦
            Cv.Dilate(corner, corner);

            return corner;
        }
        // ==================================================================================================
        // 코너 검출 2
        IplImage bin;
        IplImage apcon;
        public IplImage Binary(IplImage src)
        {
            bin = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, bin, ColorConversion.RgbToGray);
            Cv.Threshold(bin, bin, 200, 255, ThresholdType.Binary);
            return bin;
        }

        public IplImage ApproxPoly_Contour(IplImage src)
        {
            apcon = new IplImage(src.Size, BitDepth.U8, 3);
            bin = new IplImage(src.Size, BitDepth.U8, 1);

            //원본 이미지를 복사한 apcon과 Binary 이미지인 bin을 선언하고 적용
            Cv.Copy(src, apcon);
            bin = this.Binary(src);

            CvMemStorage Storage = new CvMemStorage(); ;
            CvSeq<CvPoint> contours;
            Cv.FindContours(bin, Storage, out contours, CvContour.SizeOf, ContourRetrieval.List, ContourChain.ApproxNone);

            // 다각형
            //Cv.ApproxPoly(시퀸스, 자료구조의 크기, 메모리 저장소, 근사방법, 근사정확도, 시퀀스결정)
            CvSeq<CvPoint> apcon_seq = Cv.ApproxPoly(contours, CvContour.SizeOf, Storage, ApproxPolyMethod.DP, 1, true);

            for(CvSeq<CvPoint> c = apcon_seq; c != null; c = c.HNext)
            {
                if (c.Total > 4)    //4개보다 적으면 무시
                {
                    for (int i = 0; i < c.Total; i++)
                    {
                        CvPoint? p = Cv.GetSeqElem(c, i);
                        CvPoint conpt;
                        conpt.X = p.Value.X;
                        conpt.Y = p.Value.Y;

                        Cv.Circle(apcon, conpt, 3, CvColor.Black, -1);
                    }
                }
            }
            return apcon;
        }
        public void Dispose()
        {
            if (gray != null) Cv.ReleaseImage(gray);
            if (corner != null) Cv.ReleaseImage(corner);
            if (bin != null) Cv.ReleaseImage(bin);
            if (apcon != null) Cv.ReleaseImage(apcon);
        }
    }
}
