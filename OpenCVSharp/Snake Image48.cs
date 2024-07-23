using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenCVSharpEx1
{
    internal class Snake_Image48 : IDisposable
    {
        IplImage snake;

        public IplImage SnakeImage(IplImage src)
        {
            //계산에 사용할 snake_calc
            IplImage snake_calc = new IplImage(src.Size, BitDepth.U8, 1);
            snake = new IplImage(src.Size, BitDepth.U8, 3);     //결과에 사용할 snake

            Cv.CvtColor(src, snake_calc, ColorConversion.BgrToGray);                //그레이스케일
            Cv.Threshold(snake_calc, snake_calc, 150, 255, ThresholdType.Binary);   //이진화
            //가우시안 블러를 적용하여 이미지를 단순화
            Cv.Smooth(snake_calc, snake_calc, SmoothType.Gaussian, 9); 

            int contour_num = 2000;     //윤곽의 개수로 사용할 contour_num
            CvPoint[] contour = new CvPoint[contour_num];   //contour를 생성하여 contour_num의 개수만큼 생성
            //center를 생성하여 윤곽 추적을 위한 이미지의 중심점을 생성
            CvPoint center = new CvPoint(snake_calc.Width / 2, snake_calc.Height / 2);

            //for문을 사용하여 중심점을 기준으로 coutour_num 개수만큼 둘러싸게 함
            for (int i = 0; i < contour.Length; i++)
            {
                contour[i].X = (int)(center.X * Math.Cos(2 * Math.PI * i / contour.Length) + center.X);
                contour[i].Y = (int)(center.Y * Math.Sin(2 * Math.PI * i / contour.Length) + center.Y);
            }

            //윤곽 추적을 보여줄 window를 생성
            CvWindow window = null;

            int k = 0;
            //윤곽 추적을 위하여 contour_num의 절반만큼 반복
            while (k < contour_num / 2)
            {
                k++;

                //window를 생성하고 크기 모드를 StretchImage로 사용하며 크기를 적절하게 변경
                window = new CvWindow("SnakeImage", WindowMode.StretchImage);
                window.Resize(640, 480);

                //Cv.SnakeImage(이미지, 윤곽선, 알파, 베타, 감마, 이웃 크기, 종료 기준, 그라디언트 플래그)
                //알파              : 연속성의 가중치.
                //베타              : 곡률의 가중치.
                //감마              : 이미지의 가중치.
                //이웃크기          : 최솟값을 검색하는데 사용되는 모든 이웃 점의 크기.
                //종료 기준         : 반복 알고리즘의 종료 기준입니다. 최대 반복 횟수 의미.
                //그라디언트 플래그 : 모든 이미지 픽셀에 대한 그래디언트 크기를 계산하고이를 에너지 필드로 간주 유 / 무.
                Cv.SnakeImage(snake_calc, contour, 0.50f, 0.40f, 0.30f, new CvSize(15, 15), new CvTermCriteria(1), true);

                //그려진 윤곽선을 지우고 새롭게 그리기 위하여 원본을 덧씌움
                Cv.Copy(src, snake);

                //for문에서는 첫 번째 윤곽점과 마지막 윤곽점을 잇지 못하므로 for문이 모두 반복된 후 남은 점을 이음
                for (int i = 0; i < contour.Length - 1; i++)
                {
                    Cv.Line(snake, contour[i], contour[i + 1], CvColor.Red, 5);
                }
                Cv.Line(snake, contour[contour.Length - 1], contour[0], CvColor.Red, 5);

                //window에 윤곽 추적 결과를 표시
                window.Image = snake;
                // while문이 반복중에도 윈도우 창이 업데이트
                Application.DoEvents(); //using System.Windows.Forms; 필요
            }
            window.Close();
            return snake;
        }

        public void Dispose()
        {
            if (snake != null) Cv.ReleaseImage(snake);
        }
    }
}
