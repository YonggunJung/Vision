using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenCVSharpEx1
{
    internal class Pyramid_Mean_Shift_Filtering46 : IDisposable
    {
        //이미지 피라미드에 의한 평균 이동 분할을 진행
        //레벨을 지정하여 이미지 피라미드를 만들고 이 정보를 이용하여 이미지 분할을 실행합니다.
        //공간 윈도우 반경과 색상 윈도우 반경을 사용하여 평균 공간 값과 평균 색 벡터로 최종 값이 설정됩니다.
        //피라미드 평균 이동 분할은 매우 높은 계산 시간을 요구
        IplImage pyrmean;
        public IplImage PyrMeanShiftFiltering(IplImage src)
        {
            //계산에 사용할 srcROI를 생성하여 src를 복사하여 저장
            IplImage srcROI = src.Clone();
            pyrmean = new IplImage(src.Size, BitDepth.U8, 3); //결과에 사용할 pyrmean를 생성

            int level = 2;                  //레벨
            double space_radius = 30.0;     //공간 윈도우 반경
            double color_radius = 30.0;     //색상 윈도우 반경

            //관심 영역으로 사용할 roi를 생성
            CvRect roi = new CvRect
            {
                X = 0,
                Y = 0,
                //너비와 높이를 AND연산을 통해 좌측으로 쉬프트 연산
                Width = srcROI.Width & -(1 << level),
                Height = srcROI.Height & -(1 << level)
            };

            srcROI.ROI = roi;           //srcROI에 관심 영역
            pyrmean = srcROI.Clone();   //관심 영역이 적용된 srcROI를 pyrmean에 복사

            //Cv.PyrMeanShiftFiltering(원본, 결과, 공간 윈도우 반경, 공간 색상 반경, 레벨, 종결기준)
            Cv.PyrMeanShiftFiltering(srcROI, pyrmean, space_radius, color_radius, level, new CvTermCriteria(3, 1));
            //종결기준은 new CvTermCriteria(최대반복횟수, 정확성)

            return pyrmean;
        }

        public void Dispose()
        {
            if (pyrmean != null) Cv.ReleaseImage(pyrmean);
        }
    }
}
