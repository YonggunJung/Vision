using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharpEx1
{
    internal class Template_Matching52 : IDisposable
    {
        //영상이나 이미지에서 템플릿과 일치하는 오브젝트를 검출하는 함수
        IplImage match;
        
        public IplImage Templit(IplImage src, IplImage temp)
        {
            match = src;                //매칭 이미지
            IplImage templit = temp;    //템플릿 이미지
            //비교 결과
            //이미지 크기는 항상 W-w + 1 x H-h + 1로 고정적으로 사용
            //(W, H) = match 이미지의 너비와 높이, (w, h) = templit 이미지의 너비와 높이
            IplImage tm = new IplImage(new CvSize(match.Size.Width - templit.Size.Width + 1, match.Size.Height - templit.Size.Height + 1), BitDepth.F32, 1);

            //minloc은 검출된 위치의 최소 지점, maxloc은 검출된 위치의 최대 지점을 의미
            CvPoint minloc, maxloc;
            //minval은 검출된 위치의 최소 포인터, maxval은 검출된 위치의 최대 포인터를 의미
            Double minval, maxval;

            //Cv.MatchTemplate(매칭 이미지,템플릿 이미지 , 비교 결과 이미지, 연산방법)
            //MatchTemplateMethod.* : 연산방법입니다.R은 결과, T는 템플릿, I는 매칭 이미지를 의미
            //MatchTemplateMethod.SqDiff
            //MatchTemplateMethod.SqDiffNormed
            //MatchTemplateMethod.CCorr
            //MatchTemplateMethod.CCorrNormed
            //MatchTemplateMethod.CCoeff
            //MatchTemplateMethod.CCoeffNormed
            Cv.MatchTemplate(match, templit, tm, MatchTemplateMethod.SqDiffNormed);

            //Cv.MinMaxLoc()를 이용하여 비교 결과이미지에서 포인터와 지점을 검출
            //Cv.MinMaxLoc(최소 포인터, 최대 포인터, 최소 지점, 최대 지점)
            // out 키워드를 포함해야함
            Cv.MinMaxLoc(tm, out minval, out maxval, out minloc, out maxloc);

            //match 이미지에 최소 지점에서 템플릿 이미지 크기로 설정하여 템플릿 매칭 결과를 표시
            Cv.DrawRect(match, new CvRect(minloc.X, minloc.Y, templit.Width, templit.Height), CvColor.Red, 3);

            return match;
        }

        public void Dispose()
        {
            if (match != null) Cv.ReleaseImage(match);
        }
    }
}
