using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharpEx1
{
    internal class Pyramid_LK59 : IDisposable
    {
        //카메라와 피사체의 상대 운동에 의하여 발생하는 피사체의 운동에 대한 패턴을 검출
        //PyrLK(Pyramid Lucas Kanade) 방법은 입력 이미지와 피라미드 이미지를 사용하여 코너를 기준으로 광학 흐름을 검출
        //이전 프레임(Previous)과 현재 프레임(Current)은 영상이나 이미지를 사용
        IplImage gray;
        IplImage optical;

        public IplImage GrayScale(IplImage src)
        {
            gray = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, gray, ColorConversion.BgrToGray);
            return gray;
        }

        public IplImage OpticalFlowPyrLK(IplImage previous, IplImage current)
        {
            //이전 프레임 previous와 현재 프레임 current를 매개변수로 사용하여 검출을 진행

            //광학 흐름 함수는 그레이스케일을 적용하여 검출을 진행
            //계산이미지로 사용할 prev와 curr 변수에 그레이스케일을 적용
            IplImage prev = this.GrayScale(previous);
            IplImage curr = this.GrayScale(current);
            optical = current;      //결과로 사용할 optical 필드에 현재 프레임을 사용

            //LK 방법은 피라미드 이미지를 사용하므로 이전 프레임과 현재 프레임에 피라미드 이미지를 저장할 변수를 생성
            //피라미드 이미지의 크기는 너비는 8만큼 크며, 높이는 1/3값
            IplImage prev_pyramid = new IplImage(new CvSize(optical.Width + 8, optical.Height / 3), BitDepth.U8, 1);
            IplImage curr_pyramid = new IplImage(new CvSize(optical.Width + 8, optical.Height / 3), BitDepth.U8, 1);

            //코너 검출 함수인 GoodFeaturesToTrack()을 사용할 예정이므로 eigImg와 tempImg를 저장
            IplImage eigImg = new IplImage(optical.Size, BitDepth.U8, 1);
            IplImage tempImg = new IplImage(optical.Size, BitDepth.U8, 1);

            //GoodFeaturesToTrack()에서 검출한 코너를 저장할 corners와 광학 흐름으로 이동한 코너인 corners2를 생성
            CvPoint2D32f[] corners;
            CvPoint2D32f[] corners2;

            int cornerCount = 600;      //반환할 코너의 최대 개수. 값이 너무 높을 경우 연산 속도가 느려짐
            sbyte[] status;             //광학 흐름의 발생 유/무를 저장. 1이 저장될 경우 광학 흐름이 발생
            CvTermCriteria criteria = new CvTermCriteria(100, 0.01);    //종료 기준을 설정

            //이전 프레임에 대하여 코너를 검출
            Cv.GoodFeaturesToTrack(prev, eigImg, tempImg, out corners, ref cornerCount, 0.01, 15);

            //광학 흐름 구하기
            //Cv.CalcOpticalFlowPyrLK(이전 프레임, 현재 프레임, 피라미드 이전 프레임, 피라미드 현재 프레임, 이전 프레임 코너 검출점, 계산된 현재 프레임 코너 검출점, 블록 크기, 레벨, 상태, 종결 기준, 플래그)
            //계산된 현재 프레임 코너 검출점에는 광학 흐름이 발생한 종료점의 위치를 반환
            //상태는 광학흐름의 발생 유/무를 반환
            //플래그

            //PyrAReady: 이전 프레임의 피라미드를 사전에 계산
            //PyrBReady : 현재 프레임의 피라미드를 사전에 계산
            //InitialGuesses : 함수가 호출되기 전에 초기 추정 좌표를 포함(이전 프레임의 코너 검출점이 현재 프레임의 코너 검출점)
            //InitialFlow: 함수가 호출되기 전에 초기 추정 좌표를 포함(이전 프레임의 코너 검출점이 현재 프레임의 코너 검출점)
            //GetMinEigenVals: 최소 고유 값을 오류 측정 값으로 사용
            //오류 측정값 (trackError) - 계산된 값이 주변 움직임에 비해서 값이 너무 튀는 경우 제거하는 용도로 사용
            Cv.CalcOpticalFlowPyrLK(prev, curr, prev_pyramid, curr_pyramid, corners, out corners2, new CvSize(20, 20), 5, out status, criteria, LKFlowFlag.PyrAReady);

            //검출된 코너의 개수만큼 반복
            for (int i = 0; i < cornerCount; i++)
            {
                if (status[i] == 1)
                {
                    //상태값을 사용하여 광학 흐름이 발생하였을 때 값을 출력
                    //Cv.DrawLine()과 Cv.DrawCircle()을 사용하여 광학 흐름을 optical 필드에 표시
                    //dx와 dy를 생성하여 일정 속도 이상, 이하의 값을 무시하거나 출력할 수 있음
                    Cv.DrawLine(optical, corners[i], corners2[i], CvColor.Red, 1, LineType.AntiAlias, 0);
                    Cv.DrawCircle(optical, corners2[i], 3, CvColor.Red, -1);
                }
            }
            return optical;
        }
        public void Dispose()
        {
            if (gray != null) Cv.ReleaseImage(gray);
            if (optical != null) Cv.ReleaseImage(optical);
        }
    }
}
