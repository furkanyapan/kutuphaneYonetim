using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kutuphaneYonetim
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 uyeIslem = new Form4();
            this.Hide();
            uyeIslem.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form5 kitapIslem = new Form5();
            this.Hide();
            kitapIslem.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form6 emanetIslem = new Form6();
            this.Hide();
            emanetIslem.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form7 adminIslem = new Form7();
            this.Hide();
            adminIslem.Show();
        }
    }
}
