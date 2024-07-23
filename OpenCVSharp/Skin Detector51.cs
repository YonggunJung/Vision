using OpenCvSharp;
using OpenCvSharp.CPlusPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace OpenCVSharpEx1
{
    internal class Skin_Detector51 : IDisposable
    {
        // 피부색과 흡사한 픽셀들을 검출하는 알고리즘
        IplImage skin;

        public IplImage SkinDetection(IplImage src)
        {
            skin = new IplImage(src.Size, BitDepth.U8, 3);      //결과용 이미지인 skin
            IplImage output = new IplImage(src.Size, BitDepth.U8, 1);   //계산용 이미지인 output

            Cv.Copy(src, skin);

            //detector를 선언하여 피부색을 검출하기 위해 생성자를 만듦
            //CvAdaptiveSkinDetector(1, 모폴로지 연산 방법)
            //첫 번째 인수는 samplingDivdier를 의미하며, 1의 값을 고정적으로 사용
            //MorphingMethod.None : 모폴로지 연산을 수행하지 않음
            //MorphingMethod.Erode : 모폴로지 침식만 적용
            //MorphingMethod.ErodeDilate : 모폴로지 침식 후 팽창 적용
            //MorphingMethod.ErodeErode : 모포롤지 침식 후 침식 적용
            CvAdaptiveSkinDetector detector = new CvAdaptiveSkinDetector(1, MorphingMethod.ErodeDilate);
            //피부색 검출 알고리즘을 실행
            //detector.Process(원본, 결과)
            //결과이미지에 검출 결과를 저장
            detector.Process(src, output);

            //이중 for문을 이용하여 이미지의 너비와 높이만큼 반복하여 모든 픽셀에 대해 검사
            for (int x = 0; x < src.Width; x++)
            {
                for (int y = 0; y < src.Height; y++)
                {
                    //검출용 이미지인 output의 (x, y)의 픽셀의 값이 흑색이 아니라면, 피부색으로 가정
                    if (output[y, x].Val0 != 0)         //Val0는 첫 번째 엘리먼트 요소를 의미
                        skin[y, x] = CvColor.Green;     //if문에 부합하면 결과이미지 (x, y) 좌표의 색상을 초록색으로 변경
                }
            }
            return skin;
        }
        public void Dispose()
        {
            if (skin != null) Cv.ReleaseImage(skin);
        }
    }
}
