using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kutuphaneYonetim
{
    public partial class Form4 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=MSI\SQLEXPRESS;Initial Catalog=kutuphane;Integrated Security=True");
        SqlCommand cmd;
        public void Temizle()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
        }
        public void Listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT u.uye_id, u.uye_ad, u.uye_soyad, u.uye_mail, a.adres_id, a.il, a.ilce, a.mahalle, a.sokak, a.bina, a.kapi\r\nFROM Uye u\r\nJOIN Adres a ON u.adres_id = a.adres_id", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        public Form4()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form3 anaMenu = new Form3();
            this.Hide();
            anaMenu.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            Listele();
        }
    }
}
