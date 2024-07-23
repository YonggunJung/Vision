using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace OpenCVSharpEx1
{
    internal class Drawing17 : IDisposable
    {
        IplImage draw;

        public IplImage DrawingImage(IplImage src)
        {
            draw = new IplImage(src.Size, BitDepth.U8, 3);
            //원본을 복사해 작업할 공간을 새로 만듦
            Cv.Copy(src, draw);

            // 선 그리기
            //Cv.DrawLine(원본, x1, y1, x2, y2, 색상, 두께)
            Cv.DrawLine(draw, 10, 10, 630, 10, CvColor.Blue, 10);
            //Cv.DrawLine(원본, new CvPoint(x1, y1), new CvPoint(x2, y2), new CvColor(R, G, B), 두께)
            Cv.DrawLine(draw, new CvPoint(10, 40), new CvPoint(630, 40), new CvColor(255, 100, 100), 5);

            // 원 그리기
            //Cv.Cricle(원본, x, y, 반지름, 색상, 두께)
            Cv.DrawCircle(draw, 60, 150, 50, CvColor.Orange, 2);
            //Cv.DrawCircle(원본, new CvPoint(x, y), new CvColor(R, G, B), 두께)
            Cv.DrawCircle(draw, new CvPoint(200, 150), 50, CvColor.Plum, -1);
            //두께를 - 1로 할 경우 내부가 채워짐

            //사각형 그리기
            //Cv.DrawRect(원본, x1, y1, x2, y2, 색상, 두께)
            Cv.DrawRect(draw, 300, 100, 400, 200, CvColor.Green, 2);
            //Cv.DrawRect(원본, new CvPoint(x1, y1), new CvPoint(x2, y2), new CvColor(R, G, B), 두께)
            Cv.DrawRect(draw, new CvPoint(450, 100), new CvPoint(550, 200), CvColor.Red, -1);
            //두께를 - 1로 할 경우 내부가 채워짐

            // 타원이나 호 그리기
            //Cv.DrawEllipse(원본, new CvPoint(x, y), new CvSize(width, height), 기준각도, 시작각도, 종료각도, 색상)
            //각도의 범위는 0 ~360,  0°는 3시 방향으로 반시계방향(CCW)으로 각도가 커집니다.
            Cv.DrawEllipse(draw, new CvPoint(100, 300), new CvSize(50, 50), 0, 45, 360, CvColor.Beige);

            //Cv.PutText(원본, new CvPoint(x, y), new CvFont(FontFace.*, hscale, vscale), 색상)
            //FontFace는 글자모양을 의미, hscale, vscale을 이용하여 글자의 크기를 설정
            Cv.PutText(draw, "Open CV", new CvPoint(200, 300), new CvFont(FontFace.HersheyComplex, 0.7, 0.7), new CvColor(15, 255, 100));
            Cv.PutText(draw, "Open CV", new CvPoint(350, 300), new CvFont(FontFace.HersheyComplex, 0.1, 3.0), new CvColor(15, 255, 100));

            return draw;
        }

        public void Dispose()
        {
            if (draw != null) Cv.ReleaseImage(draw);
        }
    }
}
