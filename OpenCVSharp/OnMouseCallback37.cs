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
    public partial class OnMouseCallback37 : Form
    {
        public OnMouseCallback37()
        {
            InitializeComponent();
        }
        CvWindow win;
        IplImage src;

        private void OnMouseCallback37_Load(object sender, EventArgs e)
        {
            // 이미지를 파일에서 로드하거나 새로 생성.
            src = new IplImage(@"C:\Users\admin\source\repos\OpenCVSharpEx1\Images\츄6.jpg", LoadMode.Color);
            //"OpenCV"라는 제목의 CvWindow를 생성하고, 초기화되지 않은 src 이미지를 사용하여 연결
            win = new CvWindow("OpenCV", src);
            //CvWindow의 OnMouseCallback 이벤트에 click 메서드를 등록
            win.OnMouseCallback += new CvMouseCallback(click);
        }
        private void  click(MouseEvent eve, int x, int y, MouseEvent flag)
        {
            // 왼쪽 마우스 버튼을 눌렀을 때
            if (eve == MouseEvent.LButtonDown)
            {
                // 클릭한 좌표에 텍스트를 그립니다.
                string text = "X : " + x.ToString() + " Y : " + y.ToString();
                Cv.PutText(src, text, new CvPoint(x, y), new CvFont(FontFace.HersheyComplex, 0.5, 0.5), CvColor.Red);
                win.Image = src;    // 변경된 이미지를 CvWindow에 반영.
            }
            //플래그 키를 사용할 경우(flag &MouseEvent.FlagCtrlKey) != 0을 사용해야 클릭한다는 것을 의미.
            //== 을 사용 시 클릭하지 않을 경우
            // 오른쪽 마우스 버튼을 누르고 Ctrl 키를 눌렀을 때
            if (eve == MouseEvent.RButtonDown && (flag & MouseEvent.FlagCtrlKey) != 0)
            {
                // 클릭한 좌표를 중심으로 반지름 15인 초록색 원을 그립니다.
                Cv.Circle(src, x, y, 15, CvColor.GreenYellow);
                win.Image = src;    // 변경된 이미지를 CvWindow에 반영합니다.
            }
            //마우스
            //LButtonDown : 마우스 왼쪽 버튼을 누를 때
            //LButtonUp : 마우스 왼쪽 버튼을 뗄 때
            //LButtonDoubleClick : 마우스 왼쪽 버튼을 더블 클릭할 때
            //MButtonDown: 마우스 휠 버튼을 누를 때
            //MButtonUp : 마우스 휠 버튼을 뗄 때
            //MButtonDoubleClick : 마우스 휠 버튼을 더블 클릭할 때
            //RButtonDown: 마우스 오른쪽 버튼을 누를 때
            //RButtonUp : 마우스 오른쪽 버튼을 뗄 때
            //RButtonDoubleClick : 마우스 오른쪽 버튼을 더블 클릭할 때
            //MouseMove: 마우스를 움직일 때

            //플래그 키
            //FlagLButton: 마우스 왼쪽 버튼을 누른 상태로 드래그 할 때
            //FlagMButton: 마우스 휠 버튼을 누른 상태로 드래그 할 때
            //FlagRButton: 마우스 오른쪽 버튼을 누른 상태로 드래그 할 때
            //FlagShiftKey: Shift 키를 눌렀을 때
            //FlagCtrlKey: Ctrl 키를 눌렀을 때
            //FlagAltKey: Alt 키를 눌렀을 때
        }

        
    }
}
