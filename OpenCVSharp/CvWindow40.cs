using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;

namespace OpenCVSharpEx1
{
    public partial class CvWindow40 : Form
    {
        public CvWindow40()
        {
            InitializeComponent();
        }
        

        private void CvWindow40_Load(object sender, EventArgs e)
        {
            IplImage src = new IplImage(@"C:\Users\admin\source\repos\OpenCVSharpEx1\Images\츄4.jpg", LoadMode.AnyColor);
            //CvWindow win = new CvWindow("제목", 윈도우 모드, 이미지)
            CvWindow win = new CvWindow("윈도우", WindowMode.StretchImage, src);
            //WindowMode.None : 플래그 없음
            //WindowMode.AutoSize : 이미지의 크기로 출력, 윈도우 창 크기 조정 불가
            //WindowMode.OpenGL : OpenGL을 지원하는 윈도우 창
            //WindowMode.ExpandedGui : 향상된 GUI 표시
            //WindowMode.NormalGui : 상태 표시줄 및 도구 모음이 없는 윈도우 창
            //WindowMode.StretchImage : 이미지를 윈도우 창 크기에 맞춤
            //WindowMode.Fullscreen : 전체 화면
            //WindowMode.FreeRatio : 가로 세로 비율 수정
            //WindowMode.KeepRatio : 이미지 비율 유지

            //win.Resize(640, 480); // 윈도우 창의 크기를 설정
            //win.Move(100, 100);   // 윈도우 창의 위치를 설정
            //win.ShowImage(src2);  // 윈도우 창의 이미지를 변경
            //win.Close();
        }
    }
}
