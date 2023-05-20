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
    public partial class Form7 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=MSI\SQLEXPRESS;Initial Catalog=kutuphane;Integrated Security=True");
        SqlCommand cmd;

        public void Toplam1()
        {
            con.Open();
            cmd = new SqlCommand("SELECT COUNT(*) FROM Uye WHERE adres_id IN (SELECT adres_id FROM Adres WHERE il LIKE '" + textBox1.Text + "%')", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                label11.Text = dr[0].ToString();
            }
            dr.Close();
            con.Close();

        }
        public void Toplam2()
        {
            con.Open();
            cmd = new SqlCommand("SELECT COUNT(*) FROM Kitap WHERE tur_id IN (SELECT tur_id FROM Tur WHERE tur_ad LIKE '" + textBox2.Text + "%' )", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                label13.Text = dr[0].ToString();
            }
            dr.Close();
            con.Close();

        }
        public void Toplam3()
        {
            con.Open();
            cmd = new SqlCommand("SELECT COUNT(*) FROM Kitap WHERE yazar_id IN (SELECT yazar_id FROM Yazar WHERE yazar_ad LIKE '" + textBox3.Text + "%' )", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                label14.Text = dr[0].ToString();
            }
            dr.Close();
            con.Close();

        }

        public void uyelistele()
        {
            string uyeListele = "SELECT u.uye_id AS [Üye No], u.uye_ad AS Ad, u.uye_soyad AS Soyad, u.uye_mail AS Mail FROM Uye u ";
            SqlDataAdapter da = new SqlDataAdapter(uyeListele, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }
        public void kitapTurListele()
        {
            string kitapTurListele = "SELECT k.kitap_id AS Barkod, k.kitap_ad AS [Kitap Adı], k.kitap_yayinci AS Yayınevi, k.kitap_basimyil AS [Basım Yılı] FROM Kitap k";
            SqlDataAdapter da = new SqlDataAdapter(kitapTurListele, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;

        }

        public void kitapYazarListele()
        {
            string kitapYazarListele = "SELECT k.kitap_id AS Barkod, k.kitap_ad AS [Kitap Adı], k.kitap_yayinci AS Yayınevi, k.kitap_basimyil AS [Basım Yılı] FROM Kitap k";
            SqlDataAdapter da = new SqlDataAdapter(kitapYazarListele, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView3.DataSource = dt;

        }
        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            uyelistele();
            kitapTurListele();
            kitapYazarListele();
            Toplam1();
            Toplam2();
            Toplam3();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) //Girilen İldeki Kayıtlı Üyeler
        {
            string uyeilListele = "SELECT u.uye_id AS [Üye No], u.uye_ad AS Ad, u.uye_soyad AS Soyad, u.uye_mail AS Mail FROM Uye u WHERE adres_id IN (SELECT adres_id FROM Adres WHERE il LIKE '" + textBox1.Text + "%')";
            SqlDataAdapter da = new SqlDataAdapter(uyeilListele, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            cmd = new SqlCommand("SELECT COUNT(*) FROM Uye WHERE adres_id IN (SELECT adres_id FROM Adres WHERE il = 'İstanbul'", con);
            Toplam1();
        }

        private void pictureBox1_Click(object sender, EventArgs e) //ana menu, geri git
        {
            Form3 anaMenu = new Form3();
            this.Hide();
            anaMenu.Show();
        }

        private void textBox2_TextChanged(object sender, EventArgs e) // Girilen Türdeki Kayıtlı Kitaplar
        {
            string kitapTurListele = "SELECT k.kitap_id AS Barkod, k.kitap_ad AS [Kitap Adı], k.kitap_yayinci AS Yayınevi, k.kitap_basimyil AS [Basım Yılı] FROM Kitap k WHERE tur_id IN (SELECT tur_id FROM Tur WHERE tur_ad LIKE '" + textBox2.Text + "%')";
            SqlDataAdapter da = new SqlDataAdapter(kitapTurListele, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            Toplam2();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)//Girilen Yazarın Kayıtlı Kitapları
        {
            string kitapYazarListele = "SELECT k.kitap_id AS Barkod, k.kitap_ad AS [Kitap Adı], k.kitap_yayinci AS Yayınevi, k.kitap_basimyil AS [Basım Yılı] FROM Kitap k WHERE yazar_id IN (SELECT yazar_id FROM Yazar WHERE yazar_ad LIKE '" + textBox3.Text + "%')";
            SqlDataAdapter da = new SqlDataAdapter(kitapYazarListele, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView3.DataSource = dt;
            Toplam3();
        }

    }
}
