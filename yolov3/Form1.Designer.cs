namespace Yolov3
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.START_button = new System.Windows.Forms.Button();
            this.END_button = new System.Windows.Forms.Button();
            this.SP_Label = new System.Windows.Forms.Label();
            this.END_Label = new System.Windows.Forms.Label();
            this.window_button = new System.Windows.Forms.Button();
            this.Dis_textbox = new System.Windows.Forms.TextBox();
            this.recode_button = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(479, 426);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("한컴 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button1.Location = new System.Drawing.Point(706, 366);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 72);
            this.button1.TabIndex = 1;
            this.button1.Text = "불러오기";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(507, 366);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(193, 72);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // START_button
            // 
            this.START_button.BackColor = System.Drawing.Color.Blue;
            this.START_button.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.START_button.ForeColor = System.Drawing.Color.White;
            this.START_button.Location = new System.Drawing.Point(507, 12);
            this.START_button.Name = "START_button";
            this.START_button.Size = new System.Drawing.Size(82, 61);
            this.START_button.TabIndex = 1;
            this.START_button.Text = "시작점";
            this.START_button.UseVisualStyleBackColor = false;
            this.START_button.Click += new System.EventHandler(this.START_button_Click);
            this.START_button.KeyDown += new System.Windows.Forms.KeyEventHandler(this.START_button_KeyDown);
            // 
            // END_button
            // 
            this.END_button.BackColor = System.Drawing.Color.Yellow;
            this.END_button.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.END_button.ForeColor = System.Drawing.Color.Black;
            this.END_button.Location = new System.Drawing.Point(507, 79);
            this.END_button.Name = "END_button";
            this.END_button.Size = new System.Drawing.Size(82, 55);
            this.END_button.TabIndex = 1;
            this.END_button.Text = "마침점";
            this.END_button.UseVisualStyleBackColor = false;
            this.END_button.Click += new System.EventHandler(this.END_button_Click);
            // 
            // SP_Label
            // 
            this.SP_Label.Font = new System.Drawing.Font("한컴 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.SP_Label.Location = new System.Drawing.Point(609, 12);
            this.SP_Label.Name = "SP_Label";
            this.SP_Label.Size = new System.Drawing.Size(128, 61);
            this.SP_Label.TabIndex = 3;
            this.SP_Label.Text = "label1";
            this.SP_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // END_Label
            // 
            this.END_Label.Font = new System.Drawing.Font("한컴 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.END_Label.Location = new System.Drawing.Point(609, 79);
            this.END_Label.Name = "END_Label";
            this.END_Label.Size = new System.Drawing.Size(120, 55);
            this.END_Label.TabIndex = 3;
            this.END_Label.Text = "label1";
            this.END_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // window_button
            // 
            this.window_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.window_button.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.window_button.ForeColor = System.Drawing.Color.White;
            this.window_button.Location = new System.Drawing.Point(507, 313);
            this.window_button.Name = "window_button";
            this.window_button.Size = new System.Drawing.Size(168, 47);
            this.window_button.TabIndex = 1;
            this.window_button.Text = "윈도우 캡쳐중";
            this.window_button.UseVisualStyleBackColor = false;
            this.window_button.Click += new System.EventHandler(this.window_button_Click);
            this.window_button.KeyDown += new System.Windows.Forms.KeyEventHandler(this.START_button_KeyDown);
            // 
            // Dis_textbox
            // 
            this.Dis_textbox.Font = new System.Drawing.Font("굴림", 11F);
            this.Dis_textbox.Location = new System.Drawing.Point(681, 313);
            this.Dis_textbox.Multiline = true;
            this.Dis_textbox.Name = "Dis_textbox";
            this.Dis_textbox.Size = new System.Drawing.Size(107, 46);
            this.Dis_textbox.TabIndex = 4;
            this.Dis_textbox.Text = "100";
            this.Dis_textbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // recode_button
            // 
            this.recode_button.BackColor = System.Drawing.Color.Red;
            this.recode_button.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.recode_button.ForeColor = System.Drawing.Color.White;
            this.recode_button.Location = new System.Drawing.Point(507, 140);
            this.recode_button.Name = "recode_button";
            this.recode_button.Size = new System.Drawing.Size(85, 57);
            this.recode_button.TabIndex = 1;
            this.recode_button.Text = "녹화";
            this.recode_button.UseVisualStyleBackColor = false;
            this.recode_button.Click += new System.EventHandler(this.recode_button_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 33;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Dis_textbox);
            this.Controls.Add(this.END_Label);
            this.Controls.Add(this.SP_Label);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.recode_button);
            this.Controls.Add(this.END_button);
            this.Controls.Add(this.window_button);
            this.Controls.Add(this.START_button);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button START_button;
        private System.Windows.Forms.Button END_button;
        private System.Windows.Forms.Label SP_Label;
        private System.Windows.Forms.Label END_Label;
        private System.Windows.Forms.Button window_button;
        private System.Windows.Forms.TextBox Dis_textbox;
        private System.Windows.Forms.Button recode_button;
        public System.Windows.Forms.Timer timer1;
    }
}

