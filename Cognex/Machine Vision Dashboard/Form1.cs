using Cognex.InSight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Machine_Vision_Dashboard
{
    // Cognex Machine Vision Dashboard 만들기
    // 코그넥스 머신비전 대시보드 만들기 ㅋㅋㅋ
    public partial class Form1 : Form
    {
        CvsInSight insight = new CvsInSight();      //Insight 객체 생성
        bool IsConnected1 = false;                  // 연결 상태 
        bool OnLineST1;                             // 온/오프 상태
        bool Result1TF;                             // 테스트 합불 표현
        public Form1()
        {
            InitializeComponent();
        }

        private void cvsInSightDisplay1_ResultsChanged(object sender, EventArgs e)
        {
            // 카메라 결과가 변경시 발생하는 이벤트(트리거 등)
            if (IsConnected1)
            {
                // 카메라가 촬영한 데이터를 result1Set에 할당
                CvsResultSet resultSet1 = cvsInSightDisplay1.Results;

                if (resultSet1.Image != null)       //이미지 결과가 null이 아니면
                {
                    try
                    {
                        if (resultSet1.HasNewlyAcquiredImage) //새 이미지 결과를 획득했으면
                        {
                            Bitmap img1 = new Bitmap(cvsInSightDisplay1.GetBitmap());
                        }
                        RefreshData();  // 모니터링 결과 새로고침
                    }
                    catch { }
                }
                // 변경된 데이터를 업데이트
                cvsInSightDisplay1.AcceptUpdate();
            }
        }

        private void OnLine_Click(object sender, EventArgs e)
        {
            if (IsConnected1)       //카메라 연결 되면
            {
                if (cvsInSightDisplay1.SoftOnline)      //카메라가 온라인 상태이면
                {
                    cvsInSightDisplay1.SoftOnline = !cvsInSightDisplay1.SoftOnline;
                    OnLine.BackColor = Color.Red;
                }
                else if (!cvsInSightDisplay1.SoftOnline)    //카메라가 온프라인 상태이면
                {
                    cvsInSightDisplay1.SoftOnline = !cvsInSightDisplay1.SoftOnline;
                    OnLine.BackColor = Color.Green;
                }
            }
        }

        private void Connect_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(IsConnected1))    // 비연결
                {
                    //아이피 주소의 카메라에 접속
                    cvsInSightDisplay1.Connect("192.168.3.21", "admin", "", false);

                    IsConnected1 = true;
                    Connect.Text = "Disconnect";
                    State1.Text = "ON";
                    State1.BackColor = Color.Green;

                    cvsInSightDisplay1.ImageScale = 0.84;   // 촬영중인 이미지 배율 설정
                    cvsInSightDisplay1.ShowImage = true;    // 카메라가 받은 이미지를 보여줌
                    cvsInSightDisplay1.ShowGraphics = true;

                    Online_Check();
                }
                else        // 연결
                {
                    insight.Disconnect();       // 접속 해제
                    IsConnected1 = false;
                    Connect.Text = "Connect";
                    State1.Text = "Off";
                    State1.BackColor = Color.Red;
                    cvsInSightDisplay1.ShowImage = false;       // 카메라가 취득한 이미지 가림
                    cvsInSightDisplay1.ShowGraphics = false;
                    OnLine.BackColor = Color.White;

                    ResetFormText_1();
                }
            }
            catch { }
        }

        void Online_Check()     // 카메라가 online상태인지 체크
        {
            //OnLineST1에 카메라의 온라인 상태 여부를 할당
            OnLineST1 = cvsInSightDisplay1.SoftOnline;

            if (OnLineST1 == true)  // 카메라가 온라인이면
            {
                OnLine.BackColor = Color.Green;
                Trigger.Enabled = false;        // 온라인 일 때 트리거 비활성화
            }
            else
            {
                OnLine.BackColor = Color.Red;
                Trigger.Enabled = true;         // 오프라인 일 때 트리거 활성화
            }
        }

        private void ResetFormText_1()          // 모니터링 결과 새로고침
        {
            QRRes.Text = "";
            CX1.Text = "X : ";
            CY1.Text = "Y : ";
            CA1.Text = "Angle : ";
            OKNGBox.Visible = false;
        }

        private void Trigger_Click(object sender, EventArgs e)
        {
            cvsInSightDisplay1.InSight.ManualAcquire(wait: true);
        }

        private void RefreshData()      //모니터링 값을 새로고침 할 때
        {
            try
            {
                // 카메라의 스프레드시트에서 각 셀의 값을 변수에 할당
                var XY_X = cvsInSightDisplay1.Results.Cells["B27"].ToString();
                var XY_Y = cvsInSightDisplay1.Results.Cells["C27"].ToString();
                var angle = cvsInSightDisplay1.Results.Cells["D27"].ToString();

                CX1.Text = "X : " + XY_X.Substring(0, 7);
                CY1.Text = "Y : " + XY_Y.Substring(0, 7);
                CA1.Text = "Angle : " + angle;

                if (angle.Length > 7)   // 각도값의 길이가 7보다 크면
                    angle = angle.Substring(0, 7);
                else
                    angle = angle.Substring(0, 6);

                CX1.Text = "X : " + XY_X.Substring(0, 7);
                CY1.Text = "Y : " + XY_Y.Substring(0, 7);
                CA1.Text = "Angle : " + angle;

                // 카메라의 스프레드시트에서 해당셀의 데이터를 개체에 할당
                string Result1Value = cvsInSightDisplay1.Results.Cells["B53"].ToString();   //QRCode 값
                QRRes.Text = Result1Value;

                // QR코드의 값이 ChunCheon 이거나 Ploytechnics 일 때
                // 이 주석이 무슨뜻이지 모르겠음 한 번 봐야 겠음

                OKNGBox.Visible = true;

                //QR코드의 값이 "YEL" 일 때(개인마다 다를수 있음)
                if (Result1Value == "YEL")
                {
                    Result1TF = true;

                    OKNGBox.BackColor = Color.Green;
                    OKNGBox.Text = "OK";
                }
                else
                {
                    Result1TF = false;

                    OKNGBox.BackColor = Color.Red;
                    OKNGBox.Text = "NG";
                }
            }
            catch { }
        }
    }
}
