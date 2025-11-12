using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace e94131114_practice_6_1
{
    public partial class FormPalse : Form
    {
        Form form1 = new Form1();
        int countp = Form1.count;

        public FormPalse()
        {
            InitializeComponent();
        }

        private void FormPalse_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Image.FromFile(@"..\..\..\..\images\background.png");
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            labelCount.Text = $"你有 {countp} 個歐防風";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonNothing_Click(object sender, EventArgs e) //返回遊戲
        {
            Form1.palseCoice = 0;
            this.Close();
        }

        private void buttonRead_Click(object sender, EventArgs e) //讀檔
        {
            Form1.palseCoice = 2;
            this.Close();
        }

        private void buttonSafe_Click(object sender, EventArgs e) //存檔
        {
            Form1.palseCoice = 1;
            this.Close();
        }
    }
}
