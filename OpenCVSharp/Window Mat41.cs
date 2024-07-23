using OpenCvSharp.CPlusPlus;
using OpenCvSharp;
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
    public partial class Window_Mat41 : Form
    {
        public Window_Mat41()
        {
            InitializeComponent();
        }
        IplImage src;
        Mat m_src;
        private void Window_Mat41_Load(object sender, EventArgs e)
        {
            src = new IplImage(@"C:\Users\admin\source\repos\OpenCVSharpEx1\Images\츄7.jpg", LoadMode.Color);
            m_src = new Mat(src);
            //Window win = new Window("제목", 윈도우 모드, 이미지)
            Window win = new Window("츄에영", WindowMode.StretchImage, m_src);
            //윈도우 모드 : 윈도우 창의 크기 모드를 의미합니다.
            //WindowMode.None : 플래그 없음
            //WindowMode.AutoSize : 이미지의 크기로 출력, 윈도우 창 크기 조정 불가
            //WindowMode.OpenGL : OpenGL을 지원하는 윈도우 창
            //WindowMode.ExpandedGui : 향상된 GUI 표시
            //WindowMode.NormalGui : 상태 표시줄 및 도구 모음이 없는 윈도우 창
            //WindowMode.StretchImage : 이미지를 윈도우 창 크기에 맞춤
            //WindowMode.Fullscreen : 전체 화면
            //WindowMode.FreeRatio : 가로 세로 비율 수정
            //WindowMode.KeepRatio : 이미지 비율 유지

            //win.Resize(640, 480);     //윈도우 창의 크기를 설정
            //win.Move(100, 100);       //윈도우 창의 위치를 설정
            //win.ShowImage(m_src2);    //윈도우 창의 이미지를 변경
            //win.Close();
        }
    }
}
