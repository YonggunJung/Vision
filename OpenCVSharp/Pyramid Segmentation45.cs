using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Activation;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharpEx1
{
    internal class Pyramid_Segmentation45 : IDisposable
    {
        IplImage pyrseg;

        public IplImage PyrSegmentation(IplImage src)
        {
            //계산에 사용할 srcROI를 생성하여 src를 복사하여 저장
            IplImage srcROI = src.Clone();
            pyrseg = new IplImage(src.Size, BitDepth.U8, 3);    //결과에 사용할 pyrseg를 생성

            //주요 매개변수인 레벨과 임계값1, 임계값2를 선언
            int level = 5;
            double threshold1 = 255.0;
            double threshold2 = 50.0;

            CvRect roi = new CvRect()
            {
                X = 0,
                Y = 0,
                //너비와 높이를 AND연산을 통하여 좌측으로 쉬프트
                //2의 보수법을 사용
                Width = srcROI.Width & -(1 << level),
                Height = srcROI.Height & -(1 << level)
            };

            srcROI.ROI = roi;           // srcROI에 관심 영역을 적용
            pyrseg = srcROI.Clone();    // 관심 영역이 적용된 srcROI를 pyrseg에 복사

            //PyrSegmentation(원본, 결과, 레벨값, 임계값1, 임계값2)
            Cv.PyrSegmentation(srcROI, pyrseg, level, threshold1, threshold2);

            return pyrseg;
            
        }

        public void Dispose()
        {
            if (pyrseg != null) Cv.ReleaseImage(pyrseg);
        }
    }
}
