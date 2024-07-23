using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace OpenCVSharpEx1
{
    internal class Warp_Perspective : IDisposable
    {
        // 기하학적으로 변환하기 위해 사용
        // 영상이나 이미지를 펼치거나 좁힐 수 있음
        // WarpPerspective는 4개의 점 매핑(4개의 점을 이용한 변환)
        // WarpAffine는 3개의 점을 매핑(3개의 점을 이용한 변환)
        IplImage perspective;

        public IplImage PerspectiveTransform(IplImage src)
        {
            perspective = new IplImage(src.Size, BitDepth.U8, 3);

            //CvPoint2D32f()가 float형 2D형식으로 값을 받기 때문에 float로 선언
            float width = src.Size.Width;
            float height = src.Size.Height;

            //Cv.GetPerspectiveTransform()가 CvPoint2D32f형식으로 값을 받기 때문에 CvPoint2D32f로 선언
            CvPoint2D32f[] srcPoint = new CvPoint2D32f[4];
            CvPoint2D32f[] dstPoint = new CvPoint2D32f[4];

            //포인트 순서 0:좌상, 1:좌하, 2:우상, 3:우하
            //srcPoint[] 에서 변환될 4개의 임의의 지점을 선택
            srcPoint[0] = new CvPoint2D32f(195.0f, 200.0f);
            srcPoint[1] = new CvPoint2D32f(45.0f, 400.0f);
            srcPoint[2] = new CvPoint2D32f(435.0f, 200.0f);
            srcPoint[3] = new CvPoint2D32f(575.0f, 400.0f);

            //dstPoint[] 에서 출력될 화면 크기에 맞게 설정
            dstPoint[0] = new CvPoint2D32f(0.0f, 0.0f);
            dstPoint[1] = new CvPoint2D32f(0.0f, height);
            dstPoint[2] = new CvPoint2D32f(width, 0.0f);
            dstPoint[3] = new CvPoint2D32f(width, height);

            //CvMat을 이용하여 mapMatrix를 선언해 변환된 이미지의 값을 계산
            //Cv.GetPerspectiveTransform(변환될 지점, 변환된 지점)
            CvMat mapMatrix = Cv.GetPerspectiveTransform(srcPoint, dstPoint);
            //Cv.WarpPerspective()로 변환 시킴
            //Cv.WarpPerspective(원본, 결과, Matrix, 보간법, 여백색상), 보간은 선형 보간사용
            Cv.WarpPerspective(src, perspective, mapMatrix, Interpolation.Linear, CvScalar.ScalarAll(0));
            //CvScalar.ScalarAll(0)는 여백을 검은색으로 채움

            return perspective;
        }
        public void Dispose()
        {
            if (perspective != null) Cv.ReleaseImage(perspective);
        }
    }
}
