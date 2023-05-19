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
    public partial class Form5 : Form
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
            SqlDataAdapter da = new SqlDataAdapter("SELECT k.kitap_id AS Barkod, k.kitap_ad AS [Kitap Adı], t.tur_ad AS Tür, y.yazar_ad AS [Yazar Adı], y.yazar_soyad AS [Yazar Soyad], k.kitap_basimyil AS [Basım Yılı], k.kitap_yayinci AS Yayınevi, kn.kat AS Kat, kn.dolap AS Dolap, kn.raf AS Raf\r\nFROM Konum kn\r\nJOIN Kitap k ON kn.kitap_id = k.kitap_id\r\nJOIN Yazar y ON k.yazar_id = y.yazar_id\r\nJOIN Tur t ON t.tur_id = k.tur_id\r\nORDER BY k.kitap_id;", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            Temizle();
        }
        public Form5()
        {
            InitializeComponent();
        }
        private void pictureBox1_Click(object sender, EventArgs e) // Geri git, ana menu
        {
            Form3 anaMenu = new Form3();
            this.Hide();
            anaMenu.Show();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Temizle();
        }
    }
}
