namespace Machine_Vision_Dashboard
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cvsInSightDisplay1 = new Cognex.InSight.Controls.Display.CvsInSightDisplay();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.State1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.OnLine = new System.Windows.Forms.Button();
            this.Connect = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.CA1 = new System.Windows.Forms.Label();
            this.CY1 = new System.Windows.Forms.Label();
            this.CX1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.QRRes = new System.Windows.Forms.Label();
            this.Trigger = new System.Windows.Forms.Button();
            this.OKNGBox = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cvsInSightDisplay1
            // 
            this.cvsInSightDisplay1.DefaultTextScaleMode = Cognex.InSight.Controls.Display.CvsInSightDisplay.TextScaleModeType.Proportional;
            this.cvsInSightDisplay1.DialogIcon = null;
            this.cvsInSightDisplay1.Location = new System.Drawing.Point(12, 12);
            this.cvsInSightDisplay1.Name = "cvsInSightDisplay1";
            this.cvsInSightDisplay1.PreferredCropScaleMode = Cognex.InSight.Controls.Display.CvsInSightDisplayCropScaleMode.Default;
            this.cvsInSightDisplay1.Size = new System.Drawing.Size(783, 534);
            this.cvsInSightDisplay1.TabIndex = 0;
            this.cvsInSightDisplay1.ResultsChanged += new System.EventHandler(this.cvsInSightDisplay1_ResultsChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.State1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.OnLine);
            this.groupBox1.Controls.Add(this.Connect);
            this.groupBox1.Font = new System.Drawing.Font("굴림", 13F);
            this.groupBox1.Location = new System.Drawing.Point(801, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(253, 109);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connect";
            // 
            // State1
            // 
            this.State1.AutoSize = true;
            this.State1.Font = new System.Drawing.Font("굴림", 12F);
            this.State1.Location = new System.Drawing.Point(88, 37);
            this.State1.Name = "State1";
            this.State1.Size = new System.Drawing.Size(29, 16);
            this.State1.TabIndex = 8;
            this.State1.Text = "Off";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("굴림", 12F);
            this.label2.Location = new System.Drawing.Point(17, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Vision Mode";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 12F);
            this.label1.Location = new System.Drawing.Point(17, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Vision";
            // 
            // OnLine
            // 
            this.OnLine.Font = new System.Drawing.Font("굴림", 12F);
            this.OnLine.Location = new System.Drawing.Point(137, 67);
            this.OnLine.Name = "OnLine";
            this.OnLine.Size = new System.Drawing.Size(100, 23);
            this.OnLine.TabIndex = 5;
            this.OnLine.Text = "OnLine";
            this.OnLine.UseVisualStyleBackColor = true;
            this.OnLine.Click += new System.EventHandler(this.OnLine_Click);
            // 
            // Connect
            // 
            this.Connect.Font = new System.Drawing.Font("굴림", 12F);
            this.Connect.Location = new System.Drawing.Point(137, 33);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(100, 23);
            this.Connect.TabIndex = 4;
            this.Connect.Text = "Connect";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.CA1);
            this.groupBox2.Controls.Add(this.CY1);
            this.groupBox2.Controls.Add(this.CX1);
            this.groupBox2.Font = new System.Drawing.Font("굴림", 13F);
            this.groupBox2.Location = new System.Drawing.Point(801, 127);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(253, 145);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Match";
            // 
            // CA1
            // 
            this.CA1.AutoSize = true;
            this.CA1.Font = new System.Drawing.Font("굴림", 12F);
            this.CA1.Location = new System.Drawing.Point(17, 113);
            this.CA1.Name = "CA1";
            this.CA1.Size = new System.Drawing.Size(62, 16);
            this.CA1.TabIndex = 10;
            this.CA1.Text = "Angle : ";
            // 
            // CY1
            // 
            this.CY1.AutoSize = true;
            this.CY1.Font = new System.Drawing.Font("굴림", 12F);
            this.CY1.Location = new System.Drawing.Point(17, 72);
            this.CY1.Name = "CY1";
            this.CY1.Size = new System.Drawing.Size(32, 16);
            this.CY1.TabIndex = 9;
            this.CY1.Text = "Y : ";
            // 
            // CX1
            // 
            this.CX1.AutoSize = true;
            this.CX1.Font = new System.Drawing.Font("굴림", 12F);
            this.CX1.Location = new System.Drawing.Point(17, 31);
            this.CX1.Name = "CX1";
            this.CX1.Size = new System.Drawing.Size(33, 16);
            this.CX1.TabIndex = 8;
            this.CX1.Text = "X : ";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.QRRes);
            this.groupBox3.Font = new System.Drawing.Font("굴림", 13F);
            this.groupBox3.Location = new System.Drawing.Point(801, 278);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(253, 68);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "QRCode";
            // 
            // QRRes
            // 
            this.QRRes.AutoSize = true;
            this.QRRes.Font = new System.Drawing.Font("굴림", 18F);
            this.QRRes.Location = new System.Drawing.Point(86, 26);
            this.QRRes.Name = "QRRes";
            this.QRRes.Size = new System.Drawing.Size(73, 24);
            this.QRRes.TabIndex = 11;
            this.QRRes.Text = "Result";
            // 
            // Trigger
            // 
            this.Trigger.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Trigger.Location = new System.Drawing.Point(870, 473);
            this.Trigger.Name = "Trigger";
            this.Trigger.Size = new System.Drawing.Size(117, 46);
            this.Trigger.TabIndex = 6;
            this.Trigger.Text = "Trigger";
            this.Trigger.UseVisualStyleBackColor = true;
            this.Trigger.Click += new System.EventHandler(this.Trigger_Click);
            // 
            // OKNGBox
            // 
            this.OKNGBox.AutoSize = true;
            this.OKNGBox.Font = new System.Drawing.Font("돋움", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.OKNGBox.Location = new System.Drawing.Point(850, 387);
            this.OKNGBox.Name = "OKNGBox";
            this.OKNGBox.Size = new System.Drawing.Size(153, 40);
            this.OKNGBox.TabIndex = 12;
            this.OKNGBox.Text = "OK/NG";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1066, 558);
            this.Controls.Add(this.OKNGBox);
            this.Controls.Add(this.Trigger);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cvsInSightDisplay1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Cognex.InSight.Controls.Display.CvsInSightDisplay cvsInSightDisplay1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button OnLine;
        private System.Windows.Forms.Button Connect;
        private System.Windows.Forms.Label CA1;
        private System.Windows.Forms.Label CY1;
        private System.Windows.Forms.Label CX1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label QRRes;
        private System.Windows.Forms.Button Trigger;
        private System.Windows.Forms.Label OKNGBox;
        private System.Windows.Forms.Label State1;
    }
}

