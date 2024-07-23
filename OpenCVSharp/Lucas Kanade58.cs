using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharpEx1
{
    internal class Lucas_Kanade58 : IDisposable
    {
        // 카메라와 피사체의 상대 운동에 의하여 발생하는 피사체의 운동에 대한 패턴을 검출
        // LK(Lucas Kanade) 방법은 입력 이미지에 블록 크기를 기준으로 광학 흐름을 검출
        // 이전 프레임(Previous)과 현재 프레임(Current)은 영상이나 이미지를 사용
        IplImage gray;
        IplImage optical;

        public IplImage GrayScale(IplImage src)
        {
            gray = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, gray, ColorConversion.BgrToGray);
            return gray;
        }

        public IplImage OpticalFlowLK(IplImage previous, IplImage current)
        {
            //이전 프레임 previous와 현재 프레임 current를 매개변수로 사용하여 검출을 진행

            //광학 흐름 함수는 그레이스케일을 적용하여 검출을 진행
            //계산이미지로 사용할 prev와 curr 변수에 그레이스케일을 적용
            IplImage prev = this.GrayScale(previous);
            IplImage curr = this.GrayScale(current);
            optical = current;  //결과로 사용할 optical 필드에 현재 프레임을 사용

            //행과 열 설정
            int rows = optical.Height;
            int cols = optical.Width;

            //매트릭스는 행의 개수 x 열의 개수로 사용
            //X 방향 속도 벡터를 저장할 velx와 Y 방향 속도 벡터를 저장할 vely를 매트릭스 형식으로 생성
            //행의 개수는 높이의 크기와 같고, 열의 개수는 너비의 크기와 같음
            CvMat velx = Cv.CreateMat(rows, cols, MatrixType.F32C1);
            CvMat vely = Cv.CreateMat(rows, cols, MatrixType.F32C1);

            //매트릭스의 값을 0으로 초기화
            velx.SetZero();
            vely.SetZero();

            //Cv.CalcOpticalFlowLK(이전 프레임, 현재 프레임, 블록 크기, x 방향 속도 벡터, y 방향 속도 벡터)
            //블록 크기는 해당 블록 내에 픽셀들은 모두 같은 움직임으로 가정하는 크기
            //x 방향 속도 벡터와 y 방향 속도 벡터에 광학 흐름의 값이 담김
            Cv.CalcOpticalFlowLK(prev, curr, new CvSize(15, 15), velx, vely);

            //이중 for문을 사용하여 속도 벡터의 값을 출력
            //행과 열로 반복을 실행
            //변환식에서 값을 +=15로 두어, 화면을 15 픽셀마다 광학흐름을 검출
            for (int i = 0; i < cols; i += 15)
            {
                for(int j = 0; j < rows; j += 15)
                {
                    //매트릭스에 담겨있는 속도 벡터 성분 불러오기
                    //Cv.GetReal2D(matrix, index0, index1)
                    //index0은 행 방향(↓)
                    //index1은 열 방향(→)
                    int dx = (int)Cv.GetReal2D(velx, j, i);
                    int dy = (int)Cv.GetReal2D(vely, j, i);

                    //이미지의 15 간격마다 붉은색 지점을 표시
                    Cv.DrawCircle(optical, i, j, 1, CvColor.Red);

                    //if문과 Math.Abs()를 사용하여 일정 값 이상, 이하의 값을 무시하여 출력
                    if (Math.Abs(dx) < 30 && Math.Abs(dy) < 30)
                    {
                        //Cv.DrawLine()과 Cv.DrawCircle()을 사용하여 광학 흐름을 optical 필드에 표시
                        if (Math.Abs(dx) < 10 && Math.Abs(dy) < 10) continue;

                        Cv.DrawLine(optical, Cv.Point(i, j), Cv.Point(i + dx, j + dy), CvColor.Blue, 1, LineType.AntiAlias, 0);
                        Cv.DrawCircle(optical, new CvPoint(i + dx, j + dy), 3, CvColor.Blue, -1);
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
