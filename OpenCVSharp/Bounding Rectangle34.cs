using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharpEx1
{
    internal class Bounding_Rectangle34 : IDisposable
    {
        IplImage bound;

        public IplImage BoundingRecTangle(IplImage src)
        {
            bound = new IplImage(src.Size, BitDepth.U8, 3);

            int num = 100;  //공간 안에 사용될 점의 개수
            //CvRNG를 이용하여 난수를 발생
            CvRNG rng = new CvRNG(DateTime.Now);    // DateTime.Now를 이용해 시간 데이터를 현재 시간으로 초기화
            // CvPoint[]를 이용해 점이 저장될 배열을 선언
            CvPoint[] points = new CvPoint[num];

            //for문을 이용해 100개의 점에 임의의 좌표로 점들의 위치를 지정
            for (int i = 0; i < num; i++)
            {
                points[i] = new CvPoint()
                {
                    X = (int)(rng.RandInt() % (bound.Width)),
                    Y = (int)(rng.RandInt() % (bound.Height))
                };  //너비와 높이의 크기를 넘어가지 않게 나머지 연산
                //원을 그려 시각화
                bound.Circle(points[i], 3, new CvColor(0, 255, 0), Cv.FILLED);
            }

            //Cv.BoundingRect(점이 저장된 배열)
            CvRect rect = Cv.BoundingRect(points);
            bound.Rectangle(new CvPoint(rect.X, rect.Y), new CvPoint(rect.X + rect.Width, rect.Y + rect.Height), new CvColor(255, 0, 0), 2);

            return bound;
        }

        public void Dispose()
        {
            if (bound != null) Cv.ReleaseImage(bound);
        }
    }
}
