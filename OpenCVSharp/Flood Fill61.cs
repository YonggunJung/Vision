using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharpEx1
{
    internal class Flood_Fill61 : IDisposable
    {
        //영상이나 이미지에서 지정된 색으로 연결된 객체의 내부를 채우는데 사용
        IplImage fill;

        public IplImage FloodFill(IplImage src)
        {
            // 원본을 복사한 이미지
            fill = new IplImage(src.Size, BitDepth.U8, 3);
            fill = src.Clone();     // 복제하여 같은 이미지로 변경

            //윈도우 창 win을 생성하고 초기 이미지를 fill로 사용
            CvWindow win = new CvWindow("Window", WindowMode.StretchImage, fill);
            //마우스 포인터의 위치로 사용할 Pt를 -1, -1의 좌표로 초기화
            CvPoint Pt = new CvPoint(-1, -1);
            //함수를 구조화하기위한 포인터인 Comp를 생성
            //Comp에 내부 채우기를 실행했을 때 생성되는 정보가 담겨있음
            CvConnectedComp Comp = new CvConnectedComp();

            //마우스 콜백 이벤트를 delegate 형식으로 적용하여 클래스 내부에서도 함수가 작동하게 생성
            win.OnMouseCallback += delegate (MouseEvent eve, int x, int y, MouseEvent flag)
            {
                if (eve == MouseEvent.LButtonDown)
                {
                    //Pt에 현재 마우스 좌표를 저장
                    Pt = new CvPoint(x, y);
                    //Cv.FloodFill()를 이용하여 내부 채우기
                    //Cv.FloodFill(계산 이미지, 내부 채우기 색상, 하한 값, 상한 값, 연결 요소, 연결성)
                    //하한 값은 Pt 위치에서의(해당 색상 값 -하한 값)의 색상까지는 같은 색상으로 간주.
                    //상한 값은 Pt 위치에서의(해당 색상 값 +상한 값)의 색상까지는 같은 색상으로 간주.
                    //연결 요소는 내부 채우기를 실행하였을 때의 정보가 담겨있음.
                    //연결성은 픽셀의 어떠한 이웃 값이 고려될지를 설정.
                    //FloodFillFlag.*
                    //Link4: 이웃한 4 픽셀을 고려.
                    //Link8: 이웃한 8 픽셀을 고려.
                    //FixedRange: 시드 픽셀간의 차이를 고려.
                    //MaskOnly: 이미지를 변경하지 않고, 마스크를 채움.
                    //마스크의 매개변수는 연결성 매개변수 이후에 쉼표(,)를 추가하여 값을 할당할 수 있음
                    Cv.FloodFill(fill, Pt, CvColor.Black, Cv.ScalarAll(50), Cv.ScalarAll(50), out Comp, FloodFillFlag.Link8);
                    win.ShowImage(fill);
                    Console.WriteLine(Comp.Area);
                }
                else if (eve == MouseEvent.RButtonDown)
                {
                    Pt = new CvPoint(x, y);
                    Cv.FloodFill(fill, Pt, CvColor.White, Cv.ScalarAll(50), Cv.ScalarAll(50), out Comp, FloodFillFlag.Link8);
                    win.ShowImage(fill);
                    Console.WriteLine(Comp.Area);
                }
            };

            //키 이벤트를 사용하여 r키가 눌러졌을 때, 이미지를 초기화하며 q키가 눌러졌을 때, 종료
            while (true)
            {
                int key = Cv.WaitKey(0);
                if (key == 'r')
                {
                    fill = src.Clone();
                    win.ShowImage(fill);
                }
                else if(key == 'q')
                {
                    Cv.DestroyAllWindows();
                    break;
                }
            }
            return fill;
        }
        public void Dispose()
        {
            if (fill != null) Cv.ReleaseImage(fill);
        }
    }
}
