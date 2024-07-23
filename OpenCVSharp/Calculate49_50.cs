using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharpEx1
{
    internal class Calculate49_50 : IDisposable
    {
        //49강 - 다양한 연산을 활용하여 영상이나 이미지를 변환을 할 수 있습니다
        IplImage bin;
        IplImage calc;

        public IplImage Binary(IplImage src)
        {
            bin = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, bin, ColorConversion.BgrToGray);
            Cv.Threshold(bin, bin, 100, 255, ThresholdType.Binary);
            return bin;
        }

        public IplImage  Calculate_1(IplImage src)
        {
            //src_bin을 생성하여 src의 이미지를 복제
            IplImage src_bin = src.Clone();
            calc = new IplImage(src.Size, BitDepth.U8, 3);  //calc를 결과 이미지로 사용

            //src_bin에 이진화를 적용한 후, 색상 형식으로 즉각 변환
            this.Binary(src_bin).CvtColor(src_bin, ColorConversion.GrayToBgr);

            //Cv.And(이미지1, 이미지2, 결과, 마스크)
            //이미지2가 흑백 이미지 일 경우, 이미지2의 흰색 부분만 출력
            Cv.And(src, src_bin, calc);
            CvWindow And = new CvWindow("And", WindowMode.StretchImage, calc);

            //Cv.And(이미지1, 이미지2, 결과, 마스크)
            //이미지2가 흑백 이미지 일 경우, 이미지2의 검은색 부분만 출력
            Cv.Or(src, src_bin, calc);
            CvWindow Or = new CvWindow("Or", WindowMode.StretchImage, calc);

            //Cv.Xor(이미지1, 이미지2, 결과, 마스크)
            //이미지2가 흑백 이미지 일 경우, 이미지2의 검은색 부분만 출력, 흰색 부분은 반전 출력
            Cv.Xor(src, src_bin, calc);
            CvWindow Xor = new CvWindow("Xor", WindowMode.StretchImage, calc);

            //Cv.Not(이미지, 결과, 마스크)
            //이미지가 흑백 이미지 일 경우, 반전 시켜 출력
            Cv.Not(src, calc);
            CvWindow Not = new CvWindow("Not", WindowMode.StretchImage, calc);

            Cv.WaitKey();
            {
                And.Close();
                Or.Close();
                Xor.Close();
                Not.Close();
            }
            return calc;
        }
        // ===============================================================================
        //50강 - 다양한 연산을 활용하여 영상이나 이미지를 변환을 할 수 있습니다.
        IplImage symm;
        IplImage calc2;

        public IplImage Symmetry(IplImage src)
        {
            symm = new IplImage(src.Size, BitDepth.U8, 3);
            Cv.Flip(src, symm, FlipMode.Y);
            return symm;
        }

        public IplImage Calculate_2(IplImage src)
        {
            IplImage src_symm = src.Clone();    //rc_symm을 생성하여 src의 이미지를 복제
            calc2 = new IplImage(src.Size, BitDepth.U8, 3);  //calc2를 결과 이미지로 사용

            src_symm = this.Symmetry(src);  //src_symm을 좌우대칭한 이미지로 변경

            //Cv.Add(이미지1, 이미지2, 결과, 마스크)
            //합산
            Cv.Add(src, src_symm, calc2);
            CvWindow Add = new CvWindow("Add", WindowMode.StretchImage, calc2);

            //Cv.Sub(이미지1, 이미지2, 결과, 마스크)
            //이미지1에서 이미지2를 감산
            Cv.Sub(src, src_symm, calc2);
            CvWindow Sub = new CvWindow("Sub", WindowMode.StretchImage, calc2);

            //Cv.Mul(이미지1, 이미지2, 결과, 스케일)
            //이미지1에서 이미지2를 곱합
            Cv.Mul(src, src_symm, calc2);
            CvWindow Mul = new CvWindow("Mul", WindowMode.StretchImage, calc2);

            //Cv.Div(이미지1, 이미지2, 결과, 스케일)
            //이미지1에서 이미지2를 나눕
            Cv.Div(src, src_symm, calc2);
            CvWindow Div = new CvWindow("Div", WindowMode.StretchImage, calc2);

            //Cv.Max(이미지1, 이미지2, 결과)
            //이미지1와 이미지2에서 최댓값을 찾음
            Cv.Max(src, src_symm, calc2);
            CvWindow Max = new CvWindow("Max", WindowMode.StretchImage, calc2);

            //Cv.Min(이미지1, 이미지2, 결과)
            //이미지1와 이미지2에서 최솟값을 찾음
            Cv.Min(src, src_symm, calc2);
            CvWindow Min = new CvWindow("Min", WindowMode.StretchImage, calc2);

            //Cv.AbsDiff(이미지1, 이미지2, 결과)
            //이미지1에서 이미지2의 절댓값 차이를 계산
            Cv.AbsDiff(src, src_symm, calc2);
            CvWindow AbsDiff = new CvWindow("AbsDiff", WindowMode.StretchImage, calc2);

            Cv.WaitKey();
            {
                Add.Close();
                Sub.Close();
                Mul.Close();
                Div.Close();
                Max.Close();
                Min.Close();
                AbsDiff.Close();
            }
            return calc2;
        }

        public void Dispose()
        {
            if (bin != null) Cv.ReleaseImage(bin);
            if (calc != null) Cv.ReleaseImage(calc);
            if (symm != null) Cv.ReleaseImage(symm);
            if (calc2 != null) Cv.ReleaseImage(calc2);
        }
    }
}
