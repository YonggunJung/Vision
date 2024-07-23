using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharpEx1
{
    internal class Farneback60 : IDisposable
    {
        //카메라와 피사체의 상대 운동에 의하여 발생하는 피사체의 운동에 대한 패턴을 검출
        //Farneback 방법은 Gunnar Farneback의 알고리즘을 사용하여 밀도가 높은 광학 흐름을 계산
        IplImage gray;
        IplImage optical;

        public IplImage GrayScale(IplImage src)
        {
            gray = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, gray, ColorConversion.BgrToGray);
            return gray;
        }

        public IplImage OpticalFlowFarneback(IplImage previous, IplImage current)
        {
            //이전 프레임 previous와 현재 프레임 current를 매개변수로 사용하여 검출을 진행

            //광학 흐름 함수는 그레이스케일을 적용하여 검출을 진행
            //계산이미지로 사용할 prev와 curr 변수에 그레이스케일을 적용

            IplImage prev = this.GrayScale(previous);
            IplImage curr = this.GrayScale(current);
            optical = current;      //결과로 사용할 optical 필드에 현재 프레임을 사용

            //행과 열을 설정
            int rows = optical.Height;
            int cols = optical.Width;

            //광학 흐름 검출 함수에 사용할 인수들을 설정
            //flow는 광학 흐름에 대한 정보를 저장합니다. 비트 깊이는 F32, 채널은 2를 사용
            IplImage flow = new IplImage(optical.Size, BitDepth.F32, 2);
            // 프레임의 피라미드를 만들기 위한 이미지 크기를 설정. 값은 0~1의 값을 사용. 0.5는 고전적인 피라미드의 크기
            double pyrScale = 0.5;
            //피라미드 이미지의 레벨값을 설정. 1로 사용할 경우, 원본 이미지로 사용
            int level = 3;
            //윈도우 창의 크기를 의미
            //값이 클수록 노이즈의 영향과 처리속도가 짧아지지만, 검출 결과가 흐릿
            int winSize = 15;
            //각 피라미드에서 알고리즘이 반복 수행할 횟수
            int iterations = 3;
            //인접 픽셀 영역 크기. 값이 클수록 매끄러워지고, 검출 결과가 흐릿해짐. 값은 5~7의 값을 가장 많이 사용
            int polyN = 5;
            //가우시안의 표준 편차
            //olyN의 값이 5일 경우, 1.1의 값을 주로 사용, polyN의 값이 7일 경우, 1.5의 값을 주로 사용
            double polySigma = 1.1;

            // 광학 흐림 구하기
            //Cv.CalcOpticalFlowFarneback(이전 프레임, 현재 프레임, 광학 흐름 저장 변수, 피라미드 스케일, 레벨, 윈도우 창 크기, 반복 횟수, 인접 픽셀 영역 크기, 가우시안 표준 편차, 플래그)
            Cv.CalcOpticalFlowFarneback(prev, curr, flow, pyrScale, level, winSize, iterations, polyN, polySigma, LKFlowFlag.PyrAReady);

            //이중 for문을 사용하여 윈도우 창 크기의 간격 만큼 반복
            for (int i = 0; i < cols;i += winSize)
            {
                for (int j = 0; j < rows; j+= winSize)
                {
                    //flow에 저장되어있는 광학 흐름에 대한 값 받기
                    //(j ,i)지점에서 index 0과 index 1 값을 dx, dy에 저장
                    //index 0은 x좌표, index 1은 y좌표
                    int dx = (int)flow[j, i][0];
                    int dy = (int)flow[j, i][1];

                    //if문을 이용하여 광학 흐름이 발생되지 않았을 때는 표시
                    if (dx != 0 || dy != 0)
                    {
                        //Cv.DrawLine()과 Cv.DrawCircle()을 사용하여 광학 흐름을 optical 필드에 표시
                        Cv.DrawLine(optical, Cv.Point(i, j), Cv.Point(i + dx, j + dy), CvColor.Blue, 1, LineType.AntiAlias, 0);
                        Cv.DrawCircle(optical, new CvPoint(i + dx, j + dy), 3, CvColor.Blue, - 1);
                    }
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
