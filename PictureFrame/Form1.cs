using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictuerFrame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int count = -1;
        private void button2_Click(object sender, EventArgs e)
        {
            if(count < 8)
            {
                count++;
            }
            label2.Text = (count+1).ToString();
            pictureBox1.Image = imageList1.Images[count];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (count > 0)
            {
                count--;
            }
            label2.Text = (count + 1).ToString();
            pictureBox1.Image = imageList1.Images[count];
        }
    }
}
