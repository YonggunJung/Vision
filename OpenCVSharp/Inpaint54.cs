using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Ink;

namespace OpenCVSharpEx1
{
    internal class Inpaint54 : IDisposable
    {
        //이미지에서 불필요한 부분이나 영역을 제거한 후, 주변의 화소값으로 대체
        IplImage inpaint;

        public IplImage InpaintImage(IplImage src)
        {
            inpaint = new IplImage(src.Size, BitDepth.U8, 3);   //결과 이미지로 사용할 inpaint의 속성을 설정
            IplImage paint = src.Clone();       //계산 이미지로 사용할 paint를 생성하고 원본을 복제
            IplImage mask = new IplImage(src.Size, BitDepth.U8, 1); //마스크로 사용할 mask를 생성하고 속성을 설정

            //계산 이미지위에 마스크를 그릴 수 있게 윈도우 창을 생성
            CvWindow win_Paint = new CvWindow("Paint", WindowMode.AutoSize, paint);

            //이전 마우스 좌표인 prevPt를 생성하고 초기 위치를 (-1, -1)로 초기화
            CvPoint prevPt = new CvPoint(-1, -1);
            //마우스 콜백 함수를 적용하여 윈도우 창위에 마스크를 직접 생성
            win_Paint.OnMouseCallback += delegate (MouseEvent eve, int x, int y, MouseEvent flag)
            {
                //마우스가 이동하는 동안 계산 이미지와 마스크에 선을 그림
                //계산 이미지에는 시각적으로 마스크가 어떻게 그려지는지 확인
                if (eve == MouseEvent.LButtonDown)
                    prevPt = new CvPoint(x, y);

                else if (eve == MouseEvent.LButtonUp || (flag & MouseEvent.FlagLButton) == 0)
                    prevPt = new CvPoint(-1, -1);

                else if (eve == MouseEvent.MouseMove && (flag & MouseEvent.FlagLButton) != 0)
                {
                    CvPoint pt = new CvPoint(x, y);

                    Cv.DrawLine(mask, prevPt, pt, CvColor.White, 5, LineType.AntiAlias, 0);
                    Cv.DrawLine(paint, prevPt, pt, CvColor.White, 5, LineType.AntiAlias, 0);
                    prevPt = pt;
                    win_Paint.ShowImage(paint);
                }
            };

            bool repeat = true;
            while (repeat)
            {
                //키 이벤트 함수를 적용하여 윈도우 창에서 서로 다른 함수를 적용
                switch (CvWindow.WaitKey(0))
                {
                    case 'r':       //r 키가 눌렸을 때 마스크와 계산 이미지를 초기화
                        mask.SetZero();
                        Cv.Copy(src, paint);
                        win_Paint.ShowImage(paint);
                        break;

                    case '\r':      //Enter 키가 눌렸을 때 개체 제거함수를 적용하고, 새로운 윈도우 창에 결과를 표시
                        CvWindow win_Inpaint = new CvWindow("Inpainted", WindowMode.AutoSize);
                        //Cv.Inpaint()를 사용하여 마스크 위치에 해당하는 개체를 제거
                        //Cv.Inpaint(계산 이미지, 마스크, 결과, 반지름, 알고리즘)
                        //반지름 : 마스크 내부 픽셀의 색상을 결정하기 위한 주변 영역의 반지름
                        //알고리즘
                        //InpaintMethod.NS : Navier - Stokes 방식
                        //InpaintMethod.Telea : Alexandru Telea 방식
                        Cv.Inpaint(paint, mask, inpaint, 3, InpaintMethod.NS);
                        win_Inpaint.ShowImage(inpaint);
                        break;

                    case (char)27:      //Esc 키가 눌렸을 때 반복을 종료하고 결과를 반환
                        CvWindow.DestroyAllWindows();
                        repeat = false;
                        break;
                }
            }
            return inpaint;
        }
        public void Dispose()
        {
            if (inpaint != null) Cv.ReleaseImage(inpaint);
        }
    }
}
