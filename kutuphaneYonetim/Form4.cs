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
                MessageBox.Show("Admin kayit işlemi başarılı", "Kayıt Ekranı");

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

        private void Form4_MouseClick(object sender, MouseEventArgs e)
        {

        }
    }
}
