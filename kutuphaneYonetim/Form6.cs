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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace kutuphaneYonetim
{
    public partial class Form6 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=MSI\SQLEXPRESS;Initial Catalog=kutuphane;Integrated Security=True");
        SqlCommand cmd;

        public void Temizle()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            dateTimePicker1.Text = DateTime.Now.ToString("yyyy-MM-dd");
            dateTimePicker2.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        public void uyeListele()
        {
            string uyeListele = "SELECT u.uye_id AS [Üye No], u.uye_ad AS Ad, u.uye_soyad AS Soyad, u.uye_mail AS Mail, a.il AS İl, a.ilce AS İlçe, a.mahalle AS Mahalle, a.sokak AS Sokak, a.bina AS Bina, a.kapi AS Kapi \r\nFROM Uye u JOIN Adres a \r\nON u.adres_id = a.adres_id\r\nORDER BY u.uye_id;";
            SqlDataAdapter da = new SqlDataAdapter(uyeListele, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            Temizle();
        }
        public void kitapListele()
        {
            string kitapListele = "SELECT k.kitap_id AS Barkod, k.kitap_ad AS [Kitap Adı], t.tur_ad AS Tür, y.yazar_ad AS [Yazar Adı], y.yazar_soyad AS [Yazar Soyad], k.kitap_basimyil AS [Basım Yılı], k.kitap_yayinci AS Yayınevi, kn.kat AS Kat, kn.dolap AS Dolap, kn.raf AS Raf\r\nFROM Konum kn\r\nJOIN Kitap k ON kn.kitap_id = k.kitap_id\r\nJOIN Yazar y ON k.yazar_id = y.yazar_id\r\nJOIN Tur t ON t.tur_id = k.tur_id\r\nORDER BY k.kitap_id;";
            SqlDataAdapter da = new SqlDataAdapter(kitapListele, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            Temizle();
        }
        public void emanetListele()
        {
            string kitapListele = "SELECT o.odunc_id AS [Emanet No], u.uye_ad AS [Üye Ad], u.uye_soyad AS [Üye Soyad], u.uye_mail AS [Üye Mail], k.kitap_ad AS [Kitap], o.odunc_baslangic AS [Teslim Tarihi], o.odunc_bitis AS [İade Tarihi], CASE WHEN o.odunc_kontrol = 1 THEN 'Evet' ELSE 'Hayır' END AS [İade Edildi]\r\nFROM Uye u\r\nJOIN Odunc o ON u.uye_id = o.uye_id\r\nJOIN Kitap k ON o.kitap_id = k.kitap_id\r\nORDER BY o.odunc_id;";
            SqlDataAdapter da = new SqlDataAdapter(kitapListele, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView3.DataSource = dt;
            Temizle();
        }
        public Form6()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e) // Geri git, ana menu
        {
            Form3 anaMenu = new Form3();
            this.Hide();
            anaMenu.Show();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            uyeListele();
            kitapListele();
            emanetListele();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void button1_Click(object sender, EventArgs e) // Odunc Ver
        {
            try
            {
                con.Open();
                cmd = new SqlCommand(" INSERT INTO Odunc (uye_id, kitap_id, odunc_baslangic, odunc_bitis, odunc_kontrol) VALUES (@uye_id, @kitap_id, @odunc_baslangic, @odunc_bitis, @odunc_kontrol);", con);

                cmd.Parameters.AddWithValue("@uye_id", textBox1.Text);
                cmd.Parameters.AddWithValue("@kitap_id", textBox2.Text);
                cmd.Parameters.AddWithValue("@odunc_baslangic", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@odunc_bitis", dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@odunc_kontrol", '0');

                cmd.ExecuteNonQuery();
                MessageBox.Show("Odunç verme işlemi başarılı", "Odünç verme Ekranı");

            }
            catch
            {
                MessageBox.Show("Hatalı İşlem Yaptınız", "Hata Ekranı");
            }
            finally
            {
                emanetListele();
                con.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e) // geri al
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("UPDATE Odunc SET odunc_kontrol=1 WHERE odunc_id = '" + int.Parse(textBox3.Text) + "' ; ", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Geri alma işlemi başarılı", "Geri ALma Ekranı");

            }
            catch
            {
                MessageBox.Show("Hatalı İşlem Yaptınız", "Hata Ekranı");
            }
            finally
            {
                emanetListele();
                con.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) //uye cell click
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e) // kitap cell click
        {
            textBox2.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e) //emenet cell click
        {
            textBox3.Text = dataGridView3.CurrentRow.Cells[0].Value.ToString();
        }

        private void textBox4_TextChanged(object sender, EventArgs e) // Uye uye ismi ile arama
        {
            SqlDataAdapter da = new SqlDataAdapter(" SELECT u.uye_id AS [Üye No], u.uye_ad AS Ad, u.uye_soyad AS Soyad, u.uye_mail AS Mail, a.il AS İl, a.ilce AS İlçe, a.mahalle AS Mahalle, a.sokak AS Sokak, a.bina AS Bina, a.kapi AS Kapi \r\nFROM Uye u JOIN Adres a \r\nON u.adres_id = a.adres_id\r\nWHERE u.uye_ad LIKE '" + textBox4.Text + "%' ", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            Temizle();
        }

        private void textBox5_TextChanged(object sender, EventArgs e) // Kitap ad ile ara
        {
            SqlDataAdapter da = new SqlDataAdapter(" SELECT k.kitap_id AS Barkod, k.kitap_ad AS [Kitap Adı], t.tur_ad AS Tür, y.yazar_ad AS [Yazar Adı], y.yazar_soyad AS [Yazar Soyad], k.kitap_basimyil AS [Basım Yılı], k.kitap_yayinci AS Yayınevi, kn.kat AS Kat, kn.dolap AS Dolap, kn.raf AS Raf\r\nFROM Konum kn\r\nJOIN Kitap k ON kn.kitap_id = k.kitap_id\r\nJOIN Yazar y ON k.yazar_id = y.yazar_id\r\nJOIN Tur t ON t.tur_id = k.tur_id\r\nWHERE k.kitap_ad LIKE '" + textBox5.Text + "%' ", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            Temizle();
        }

        private void textBox6_TextChanged(object sender, EventArgs e) // Emanaet uye ad ile arama
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT o.odunc_id AS [Emanet No], u.uye_ad AS [Üye Ad], u.uye_soyad AS [Üye Soyad], u.uye_mail AS [Üye Mail], k.kitap_ad AS [Kitap], o.odunc_baslangic AS [Teslim Tarihi], o.odunc_bitis AS [İade Tarihi], CASE WHEN o.odunc_kontrol = 1 THEN 'Evet' ELSE 'Hayır' END AS [İade Edildi]\r\nFROM Uye u\r\nJOIN Odunc o ON u.uye_id = o.uye_id\r\nJOIN Kitap k ON o.kitap_id = k.kitap_id\r\nWHERE u.uye_ad LIKE '" + textBox6.Text + "%' ", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView3.DataSource = dt;
            Temizle();
        }

        private void button3_Click(object sender, EventArgs e) //iade tarihi azalan sira
        {
            SqlDataAdapter da = new SqlDataAdapter(" SELECT o.odunc_id AS [Emanet No], u.uye_ad AS [Üye Ad], u.uye_soyad AS [Üye Soyad], u.uye_mail AS [Üye Mail], k.kitap_ad AS [Kitap], o.odunc_baslangic AS [Teslim Tarihi], o.odunc_bitis AS [İade Tarihi], CASE WHEN o.odunc_kontrol = 1 THEN 'Evet' ELSE 'Hayır' END AS [İade Edildi]\r\nFROM Uye u\r\nJOIN Odunc o ON u.uye_id = o.uye_id\r\nJOIN Kitap k ON o.kitap_id = k.kitap_id\r\nORDER BY o.odunc_bitis DESC; ", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView3.DataSource = dt;
            Temizle();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter(" SELECT o.odunc_id AS [Emanet No], u.uye_ad AS [Üye Ad], u.uye_soyad AS [Üye Soyad], u.uye_mail AS [Üye Mail], k.kitap_ad AS [Kitap], o.odunc_baslangic AS [Teslim Tarihi], o.odunc_bitis AS [İade Tarihi], CASE WHEN o.odunc_kontrol = 1 THEN 'Evet' ELSE 'Hayır' END AS [İade Edildi]\r\nFROM Uye u\r\nJOIN Odunc o ON u.uye_id = o.uye_id\r\nJOIN Kitap k ON o.kitap_id = k.kitap_id\r\nORDER BY o.odunc_bitis; ", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView3.DataSource = dt;
            Temizle();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter(" SELECT o.odunc_id AS [Emanet No], u.uye_ad AS [Üye Ad], u.uye_soyad AS [Üye Soyad], u.uye_mail AS [Üye Mail], k.kitap_ad AS [Kitap], o.odunc_baslangic AS [Teslim Tarihi], o.odunc_bitis AS [İade Tarihi], CASE WHEN o.odunc_kontrol = 1 THEN 'Evet' ELSE 'Hayır' END AS [İade Edildi]\r\nFROM Uye u\r\nJOIN Odunc o ON u.uye_id = o.uye_id\r\nJOIN Kitap k ON o.kitap_id = k.kitap_id\r\nWHERE o.odunc_kontrol=0\r\nORDER BY o.odunc_id ASC;", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView3.DataSource = dt;
            Temizle();
        }
    }
}
