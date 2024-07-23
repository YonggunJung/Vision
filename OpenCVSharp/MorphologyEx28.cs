using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace OpenCVSharpEx1
{
    internal class MorphologyEx28 : IDisposable
    {
        IplImage morp;

        public IplImage Morphology(IplImage src)
        {
            morp = new IplImage(src.Size, BitDepth.U8, 3);

            //모폴로지(Morphology)의 형태학적 작업을 위해 IplConvKernel을 이용하여 지정된 크기와 구조 요소를 반환
            IplConvKernel element = new IplConvKernel(3, 3, 1, 1, ElementShape.Ellipse);
            // Cv.MorphologyEx(원본, 결과, 임시, 요소, 연산 방법, 반복횟수)
            //MorphologyOperation.Open : 열기 연산
            //MorphologyOperation.Close : 닫기 연산
            //MorphologyOperation.Gradient : 그라디언트 연산
            //MorphologyOperation.TopHat : 탑햇 연산
            //MorphologyOperation.BlackHat : 블랙햇 연산
            //Cv.MorphologyEx(src, morp, src, element, MorphologyOperation.Open, 3);
            //Cv.MorphologyEx(src, morp, src, element, MorphologyOperation.Close, 3);
            //Cv.MorphologyEx(src, morp, src, element, MorphologyOperation.Gradient, 3);
            //Cv.MorphologyEx(src, morp, src, element, MorphologyOperation.TopHat, 3);
            Cv.MorphologyEx(src, morp, src, element, MorphologyOperation.BlackHat, 3);
            //열기 연산
            //침식(Erode) 후, 팽창(Dilate) 적용. Open = Dilate(Erode(src))와 동일
            //영역의 크기를 유지하며 밝은 영역을 감소시킴
            //닫기 연산
            //팽창(Dilate) 후, 침식(Erode) 적용. Close = Erode(Dilate(src))와 동일
            //영역의 크기를 유지하며 어두운 영역을 감소시킴.
            //그라디언트 연산
            //팽창(Dilate)에서 침식(Erode)을 제외함. Gradient = Dilate(src) - Erode(src)와 동일
            //탑햇 연산
            //원본에서 열기 연산을 제외함. TopHat = src - Open와 동일
            //블랙햇 연산
            //닫기 연산에서 원본을 제외함. BlackHat = Close - src와 동일함

            return morp;
        }

        public void Dispose()
        {
            if (morp != null) Cv.ReleaseImage(morp);
        }
    }
}
