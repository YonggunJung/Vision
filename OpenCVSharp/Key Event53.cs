using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharpEx1
{
    internal class Key_Event53 : IDisposable
    {
        IplImage gray;
        IplImage key;

        public IplImage GrayScale(IplImage src)
        {
            gray = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, gray, ColorConversion.BgrToGray);
            return gray;
        }

        public IplImage KeyEvent(IplImage src)
        {
            key = src.Clone();      //결과 이미지인 key에 원본 이미지 src를 복제
            //윈도우 창 win을 생성하고 초기 이미지를 key로 사용
            CvWindow win = new CvWindow("Window", WindowMode.StretchImage, key);

            bool repeat = true;
            //while()문을 이용하여 repeat이 false가 될 때까지 반복
            while (repeat)
            {
                //switch()문을 이용하여 키 입력값을 판단
                switch (CvWindow.WaitKey(0))
                {
                    case 'r':                       //r 키가 입력됬을 때 해당 구문을 실행
                        key = this.GrayScale(src);  //key 필드에 그레이스케일을 적용
                        win.ShowImage(key);         //win 윈도우 창에 표시
                        break;

                    case '\r':                      //Enter 키가 입력되었을 때
                        key = src;                  //key 필드를 src로 다시 초기화
                        win.ShowImage(key);         //win 윈도우 창에 표시
                        break;

                    case (char)27:                  //Esc 키가 입력되었을 때
                        win.Close();                //win 윈도우 창을 닫고
                        repeat = false;             //반복을 종료
                        break;
                }
            }
            return key;
        }

        public void Dispose()
        {
            if (gray != null) Cv.ReleaseImage(gray);
            if (key != null) Cv.ReleaseImage(key);
        }
    }
}
