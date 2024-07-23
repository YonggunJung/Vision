using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OpenCVSharpEx1
{
    internal class HDC36 : IDisposable
    {
        IplImage hdcgraphics;

        public IplImage DrawToHdc(IplImage src)
        {
            //CvRect를 이용하여 관심 영역을 설정
            //new CvRect(x좌표 시작점, y좌표 시작점, 넓이, 높이)
            CvRect roi = new CvRect(250, 250, 640, 480);
            hdcgraphics = new IplImage(roi.Size, BitDepth.U8, 3);   //hdcgraphics에 roi 크기로 설정

            src.ROI = roi;  //ROI(Region Of Interest), src의 관심영역을 roi로 설정
            //Graphics는 Bitmap에서 작업하므로 bitmap과 grp를 선언
            //PixelFormat을 사용하여 색 데이터의 형식을 설정
            using (Bitmap bitmap = new Bitmap(roi.Width, roi.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
            using (Graphics grp = Graphics.FromImage(bitmap))
            {
                //hdc에 Graphics와 관련된 장치 컨텍스트에 대한 핸들을 가져옵
                IntPtr hdc = grp.GetHdc();
                //BitmapConverter.DrawToHdc(원본, hdc, 크기)
                BitmapConverter.DrawToHdc(src, hdc, new CvRect(new CvPoint(0, 0), roi.Size));
                grp.ReleaseHdc(hdc);

                grp.DrawString("076923.github.io", new Font("굴림체", 12), Brushes.Red, 5, 5);

                hdcgraphics.CopyFrom(bitmap);   //픽셀 데이터를 IplImage인 hdcgraphics에 적용
            }

            src.ResetROI();

            return hdcgraphics;
        }
        public void Dispose()
        {
            if (hdcgraphics != null) Cv.ReleaseImage(hdcgraphics);
        }
    }
}
