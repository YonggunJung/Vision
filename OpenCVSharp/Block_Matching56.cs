using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace OpenCVSharpEx1
{
    internal class Block_Matching56 : IDisposable
    {
        //Optical Flow BM
        //카메라와 피사체의 상대 운동에 의하여 발생하는 피사체의 운동에 대한 패턴을 검출
        //BM(Block Matching) 방법은 블록 단위로 이미지를 나누며 이전 프레임과 현재 프레임을 매칭하여 광학 흐름을 검출
        //이전 프레임(Previous)과 현재 프레임(Current)은 영상이나 이미지를 사용

        IplImage gray;
        IplImage optical;

        public IplImage GrayScale(IplImage src)
        {
            gray = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, gray, ColorConversion.BgrToGray);
            return gray;
        }

        public IplImage OpticalFlowBM(IplImage previous, IplImage current)
        {
            //이전 프레임 previous와 현재 프레임 current를 매개변수로 사용하여 검출을 진행

            //광학 흐름 함수는 그레이스케일을 적용하여 검출을 진행
            //계산이미지로 사용할 prev와 curr 변수에 그레이스케일을 적용
            IplImage prev = this.GrayScale(previous);
            IplImage curr = this.GrayScale(current);
            //결과로 사용할 optical 필드에 현재 프레임을 사용
            optical = current;
            //그레이스케일을 사용하여 검출하므로 급격한 밝기 변화나 노이즈에는 정확한 검출을 얻어낼 수 없음

            //함수에 사용되는 주요 상수인 BlockSize, ShiftSize, Range를 선언하고 값을 적용
            int BlockSize = 16;     //검출에 사용할 블록의 크기
            int ShiftSize = 32;     //블록의 이격 거리를 의미합니다. 값이 낮을 수록 검출 간격이 촘촘해짐
            int MaxRange = 10;      //블록 주변의 인접한 블록 크기를 의미
            //BlockSize와 MaxRange의 값이 높을 수록, ShiftSize의 값이 낮을 수록 연산 시간이 길어짐

            //광학 흐름 함수는 CvSize 형식을 인수로 사용하므로 각각의 변수에 값을 적용
            CvSize blockSize = new CvSize(BlockSize, BlockSize);
            CvSize shiftSize = new CvSize(ShiftSize, ShiftSize);
            CvSize maxrange = new CvSize(MaxRange, MaxRange);

            //광학 흐름에서 검출된 X 방향 속도 벡터와 Y 방향 속도 벡터의 값을 저장하기 위해 VelSize
            //블록 매칭 방식은 블록 크기에 대하여 광학 흐름을 계산하므로 원본 이미지의 크기보다 작음
            CvSize VelSize = new CvSize
            {
                //VelSize의 크기는 (프레임 크기 - 블록 크기 + 이격 거리) / 이격 거리
                Width = (optical.Width - blockSize.Width + shiftSize.Width) / shiftSize.Width,
                Height = (optical.Height - blockSize.Height + shiftSize.Height) / shiftSize.Height
            };

            //X 방향 속도 벡터를 저장할 velx와 Y 방향 속도 벡터를 저장할 vely를 매트릭스 형식
            //매트릭스는 행의 개수 x 열의 개수로 사용
            //행의 개수는 높이의 크기와 같으며, 열의 개수는 너비의 크기와 같습
            CvMat velx = Cv.CreateMat(VelSize.Height, VelSize.Width, MatrixType.F32C1);
            CvMat vely = Cv.CreateMat(VelSize.Height, VelSize.Width, MatrixType.F32C1);

            //매트릭스를 생성하였으므로, SetZero()를 통하여 매트릭스의 값을 0으로 초기화
            Cv.SetZero(velx);
            Cv.SetZero(vely);

            //광학 흐름구하기
            //Cv.CalcOpticalFlowBM(이전 프레임, 현재 프레임, 블록 크기, 이격 거리, 인접한 블록 크기, 초기 근사값 속도 필드 사용 유/무, x 방향 속도 벡터, y 방향 속도 벡터)
            //초기 근사값 속도 필드 사용 유/ 무는 초기 근사값으로 입력 속도를 사용할지 여부를 결정
            //x 방향 속도 벡터와 y 방향 속도 벡터에 광학 흐름의 값이 담김
            Cv.CalcOpticalFlowBM(prev, curr, blockSize, shiftSize, maxrange, false, velx, vely);
            //이중 for문을 사용하여 속도 벡터의 값을 출력
            for (int i = 0; i < velx.Rows; i++)
            {
                for (int j = 0; j < vely.Cols; j++)
                {
                    //Cv.GetReal2D(matrix, index0, index1)
                    //index0은 행 방향(↓)을 의미
                    //index1은 열 방향(→)을 의미
                    int dx = (int)Cv.GetReal2D(velx, i, j);
                    int dy = (int)Cv.GetReal2D(vely, i, j);

                    //Cv.DrawLine()을 사용하여 광학 흐름을 optical 필드에 표시
                    Cv.DrawLine(optical, new CvPoint(j * ShiftSize, i * ShiftSize), new CvPoint(j * ShiftSize + dx, i * ShiftSize + dy), CvColor.Red, 3, LineType.AntiAlias, 0);
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
