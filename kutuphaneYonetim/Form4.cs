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
            Temizle();
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

        private void button4_Click(object sender, EventArgs e) //Temizle Butonu
        {
            Temizle();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void button1_Click(object sender, EventArgs e)  //Ekle
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("INSERT INTO Adres (il, ilce, mahalle, sokak, bina, kapi) values ('" + textBox4.Text + "', '" + textBox5.Text + "', '" + textBox6.Text + "', '" + textBox7.Text + "', '" + int.Parse(textBox8.Text) + "', '" + int.Parse(textBox9.Text) + "'); DECLARE @adres_id int; SET @adres_id = SCOPE_IDENTITY(); INSERT INTO Uye (uye_ad, uye_soyad, uye_mail, adres_id) VALUES ('" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', @adres_id)", con);
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
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            textBox8.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            textBox9.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)  //Guncelle
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("UPDATE Adres SET il='" + textBox4.Text + "', ilce = '" + textBox5.Text + "', mahalle='" + textBox6.Text + "', sokak='" + textBox7.Text + "', bina= '" + int.Parse(textBox8.Text) + "', kapi='" + int.Parse(textBox9.Text) + "' WHERE adres_id = '" + int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()) + "'; UPDATE Uye SET uye_ad='" + textBox1.Text + "', uye_soyad='" + textBox2.Text + "', uye_mail= '" + textBox3.Text + "' WHERE uye_id = '" + int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()) + "'      ", con);
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("DELETE FROM Uye WHERE uye_id = '" + int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()) + "';DELETE FROM Adres WHERE adres_id = '" + int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()) + "'      ", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Üye silme işlemi başarılı", "Silme Ekranı");

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
