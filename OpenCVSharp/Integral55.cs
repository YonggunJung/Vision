using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharpEx1
{
    internal class Integral55 : IDisposable
    {
        IplImage gray;
        IplImage integral;  

        public IplImage GrayScale(IplImage src)
        {
            gray = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, gray, ColorConversion.BgrToGray);
            return gray;
        }

        public IplImage IntegralImage(IplImage src)
        {
            //계산 이미지로 사용할 integral을 생성, 정밀도는 F32 또는 F64의 단일 채널 형식만 사용
            integral = new IplImage(src.Size, BitDepth.F64, 1); 
            integral = this.GrayScale(src);     //단일 채널로 변경하기 위해 그레이스케일을 적용

            //결과 이미지인 sum, sqsum, tiltedsum을 생성
            //이미지 크기는 너비 + 1, 높이 + 1을 사용, 정밀도는 F32 또는 F64의 단일 채널 형식만 사용
            //적분 이미지
            IplImage sum = new IplImage(new CvSize(src.Width + 1, src.Height + 1), BitDepth.F64, 1);
            //제곱된 적분 이미지
            IplImage sqsum = new IplImage(new CvSize(src.Width + 1, src.Height + 1), BitDepth.F64, 1);  
            //45° 기울어진 적분 이미지
            IplImage tiltedsum = new IplImage(new CvSize(src.Width + 1, src.Height + 1), BitDepth.F64, 1);

            //적분 이미지를 구하기
            //Cv.Integral(계산 이미지, 적분 이미지, 제곱된 적분 이미지, 45° 기울어진 적분 이미지)
            Cv.Integral(integral, sum, sqsum, tiltedsum);

            CvMat src_mat = new CvMat(integral.Height, integral.Width, MatrixType.F64C1);
            CvMat sum_mat = new CvMat(sum.Height, sum.Width, MatrixType.F64C1);

            for(int i = 0; i< integral.Width; i++)
            {
                for (int j = 0; j < integral.Height; j++)
                {
                    src_mat[j, i] = integral[j, i].Val0;
                }
            }

            for(int i = 0; i < sum.Width; i++)
            {
                for (int j = 0;j < sum.Height; j++)
                {
                    sum_mat[j, i] = sum[j, i].Val0;
                }
            }

            Console.WriteLine(src_mat);
            Console.WriteLine(sum_mat);
            return sum;
        }

        public void Dispose()
        {
            if (gray != null) Cv.ReleaseImage(gray);
            if (integral != null) Cv.ReleaseImage(integral);
        }
    }
}
