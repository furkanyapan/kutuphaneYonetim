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
            SqlDataAdapter da = new SqlDataAdapter("SELECT k.kitap_id AS Barkod, k.kitap_ad AS [Kitap Adı], t.tur_ad AS Tür, y.yazar_ad AS [Yazar Adı], y.yazar_soyad AS [Yazar Soyad], k.kitap_basimyil AS [Basım Yılı], k.kitap_yayinci AS Yayınevi, kn.kat AS Kat, kn.dolap AS Dolap, kn.raf AS Raf, y.yazar_id, t.tur_id\r\nFROM Konum kn\r\nJOIN Kitap k ON kn.kitap_id = k.kitap_id\r\nJOIN Yazar y ON k.yazar_id = y.yazar_id\r\nJOIN Tur t ON t.tur_id = k.tur_id\r\nORDER BY k.kitap_id;", con);
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

        private void button1_Click(object sender, EventArgs e) // Ekle 
        {
            try
            {
                con.Open();
                //cmd = new SqlCommand("INSERT INTO Yazar(yazar_ad, yazar_soyad) VALUES ('"+textBox5.Text+"', '"+textBox6.Text+"'); DECLARE @yazar_id int; SET @yazar_id = SCOPE_IDENTITY(); INSERT INTO Tur(tur_ad) VALUES ('"+textBox2.Text+"'); DECLARE @tur_id int; SET @tur_id = SCOPE_IDENTITY(); INSERT INTO Kitap(kitap_ad, kitap_basimyil, kitap_yayinci, tur_id, yazar_id) VALUES ('"+textBox1.Text +"', '"+textBox4.Text+"', '"+textBox3.Text+"', @tur_id, @yazar_id); DECLARE @kitap_id int; SET @kitap_id = SCOPE_IDENTITY(); INSERT INTO Konum(kitap_id, kat, dolap, raf) VALUES (@kitap_id, '"+int.Parse(textBox7.Text)+"', '"+int.Parse(textBox8.Text)+"', '"+int.Parse(textBox9.Text)+"');    ", con);
                cmd = new SqlCommand("INSERT INTO Yazar(yazar_ad, yazar_soyad) VALUES (@yazar_ad, @yazar_soyad); DECLARE @yazar_id int; SET @yazar_id = SCOPE_IDENTITY(); INSERT INTO Tur(tur_ad) VALUES (@tur_ad); DECLARE @tur_id int; SET @tur_id = SCOPE_IDENTITY(); INSERT INTO Kitap(kitap_ad, kitap_basimyil, kitap_yayinci, tur_id, yazar_id) VALUES (@kitap_ad, @kitap_basimyil, @kitap_yayinci, @tur_id, @yazar_id); DECLARE @kitap_id int; SET @kitap_id = SCOPE_IDENTITY(); INSERT INTO Konum(kitap_id, kat, dolap, raf) VALUES (@kitap_id, @kat, @dolap, @raf);", con);

                cmd.Parameters.AddWithValue("@yazar_ad", textBox5.Text);
                cmd.Parameters.AddWithValue("@yazar_soyad", textBox6.Text);
                cmd.Parameters.AddWithValue("@tur_ad", textBox2.Text);
                cmd.Parameters.AddWithValue("@kitap_ad", textBox1.Text);
                cmd.Parameters.AddWithValue("@kitap_basimyil", textBox4.Text);
                cmd.Parameters.AddWithValue("@kitap_yayinci", textBox3.Text);
                cmd.Parameters.AddWithValue("@kat", int.Parse(textBox7.Text));
                cmd.Parameters.AddWithValue("@dolap", int.Parse(textBox8.Text));
                cmd.Parameters.AddWithValue("@raf", int.Parse(textBox9.Text));

                cmd.ExecuteNonQuery();
                MessageBox.Show("Üye kayit işlemi başarılı", "Kayıt Ekranı");

            }
            catch
            {
                MessageBox.Show("Hatalı İşlem Yaptınız", "Hata Ekranı");
            }
            finally
            {
                Listele();
                con.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            textBox8.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            textBox9.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)  //Guncelle
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("UPDATE Kitap SET kitap_ad='" + textBox1.Text + "', kitap_yayinci='" + textBox3.Text + "', kitap_basimyil='" + textBox4.Text + "' WHERE kitap_id='" + int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()) + "'; UPDATE Tur SET tur_ad='" + textBox2.Text + "' WHERE tur_id='" + int.Parse(dataGridView1.CurrentRow.Cells[11].Value.ToString()) + "'; UPDATE Yazar SET yazar_ad='" + textBox5.Text + "', yazar_soyad='" + textBox6.Text + "' WHERE yazar_id='" + int.Parse(dataGridView1.CurrentRow.Cells[10].Value.ToString()) + "'; UPDATE Konum SET kat='" + int.Parse(textBox7.Text) + "', dolap='" + int.Parse(textBox8.Text) + "', raf='" + int.Parse(textBox9.Text) + "' WHERE kitap_id='" + int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()) + "'", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Üye güncelleme işlemi başarılı", "Güncelleme Ekranı");

            }
            catch
            {
                MessageBox.Show("Hatalı İşlem Yaptınız", "Hata Ekranı");
            }
            finally
            {
                Listele();
                con.Close();
            }
        }
    }
}
