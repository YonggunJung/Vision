using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharpEx1
{
    internal class Horn_Schunck57 : IDisposable
    {
        //카메라와 피사체의 상대 운동에 의하여 발생하는 피사체의 운동에 대한 패턴을 검출
        //HS(Horn Schunck) 방법은 입력 이미지의 모든 픽셀에 대하여 광학 흐름을 검출
        //이전 프레임(Previous)과 현재 프레임(Current)은 영상이나 이미지

        IplImage gray;
        IplImage optical;

        public IplImage GrayScale(IplImage src)
        {
            gray = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, gray, ColorConversion.BgrToGray);
            return gray;
        }

        public IplImage OpticalFlowHS(IplImage previous, IplImage current)
        {
            IplImage prev = this.GrayScale(previous);
            IplImage curr = this.GrayScale(current);
            optical = current;

            int rows = optical.Height;
            int cols = optical.Width;

            CvMat velx = Cv.CreateMat(rows, cols, MatrixType.F32C1);
            CvMat vely = Cv.CreateMat(rows, cols, MatrixType.F32C1);

            Cv.SetZero(velx);
            Cv.SetZero(vely);

            CvTermCriteria criteria = Cv.TermCriteria(CriteriaType.Iteration | CriteriaType.Epsilon, 10, 0.01);

            Cv.CalcOpticalFlowHS(prev, curr, false, velx, vely, 150.0, criteria);

            for(int i = 0; i < rows; i += 15)
            {
                for(int j = 0; j < cols; j += 15)
                {
                    int dx = (int)Cv.GetReal2D(velx, i, j);
                    int dy = (int)Cv.GetReal2D(vely, i, j);

                    Cv.DrawLine(optical, Cv.Point(j, i), Cv.Point(j + dx, i + dy), CvColor.Red, 3, LineType.AntiAlias);
                } 
            }
            return optical;
        }

        public void Dispose()
        {
            if (gray != null) Cv.ReleaseImage(gray);
            if (optical != null) Cv.ReleaseImage(optical);
        }
    }
}
