using OpenCvSharp;
using OpenCvSharp.CPlusPlus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenCVSharpEx1
{
    public partial class Bitwise_Mat42 : Form
    {
        public Bitwise_Mat42()
        {
            InitializeComponent();
        }

        IplImage bin;

        private void Bitwise_Mat42_Load(object sender, EventArgs e)
        {
            IplImage src = new IplImage(@"C:\Users\admin\source\repos\OpenCVSharpEx1\Images\츄8.jpg", LoadMode.Color);
            IplImage src_bin = this.Binary(src); // src에 이진화를 적용한 이미지

            //비트 연산에 사용할 이미지
            Mat m_src1 = new Mat(src);
            Mat m_src2 = new Mat(src_bin);  
            Mat bitwise = new Mat();    //비트 연산이 적용된 결과 이미지

            Window win_src1 = new Window("원본", WindowMode.StretchImage, m_src1);
            Window win_src2 = new Window("이진화", WindowMode.StretchImage, m_src2);

            //Cv2.BitwiseAnd(이미지1, 이미지2, 결과, 마스크)
            //이미지2가 흑백 이미지 일 경우, 이미지2의 흰색 부분만 출력
            Cv2.BitwiseAnd(m_src1, m_src2.CvtColor(ColorConversion.GrayToBgr), bitwise);
            Window win_and = new Window("And", WindowMode.StretchImage, bitwise);

            //Cv2.BitwiseOr(이미지1, 이미지2, 결과, 마스크)
            //이미지2가 흑백 이미지 일 경우, 이미지2의 검은색 부분만 출력
            //Cv2.BitwiseOr(m_src1, m_src2.CvtColor(ColorConversion.GrayToBgr), bitwise);
            //Window win_or = new Window("Or", WindowMode.StretchImage, bitwise);

            //Cv2.BitwiseXor(이미지1, 이미지2, 결과, 마스크)
            //이미지2가 흑백 이미지 일 경우, 이미지2의 검은색 부분만 출력하며, 흰색 부분은 반전 출력
            //Cv2.BitwiseXor(m_src1, m_src2.CvtColor(ColorConversion.GrayToBgr), bitwise);
            //Window win_Xor = new Window("Xor", WindowMode.StretchImage, bitwise);

            //Cv2.BitwiseNot(이미지, 결과, 마스크)
            //이미지가 흑백 이미지 일 경우, 반전 시켜 출력
            //Cv2.BitwiseNot(m_src2, bitwise);
            //Window win_Not = new Window("Not", WindowMode.StretchImage, bitwise);
            //Tip : 비트 연산에 사용되는 모든 이미지는 Mat 형식을 사용합니다.
            //Tip: 이미지1의 경우 채널이 3 이며, 이미지2의 경우 채널이 1 입니다.
            //Tip : 이미지2의 경우, 이미지1과 채널이 다르므로
            //m_src.CvtColor(ColorConversion.GrayToBgr)을 이용하여 3개의 채널을 가지는 이미지로 즉각 변환이 가능.
            //Tip : 마스크를 사용하지 않는 경우 생략이 가능합니다.
            //Tip: 이미지1, 이미지2, 결과, 마스크 이미지들의 크기는 모두 같아야합니다.
        }

        public IplImage Binary(IplImage src)        // 이진화 함수
        {
            bin = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, bin, ColorConversion.BgrToGray);
            Cv.Threshold(bin, bin, 50, 255, ThresholdType.Binary);
            return bin;
        }
    }
}
