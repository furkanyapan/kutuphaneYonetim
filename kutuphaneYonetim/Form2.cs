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
    public partial class Form2 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=MSI\SQLEXPRESS;Initial Catalog=kutuphane;Integrated Security=True");
        SqlCommand cmd;
        public Form2()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 uyeGiris = new Form1();
            this.Hide();
            uyeGiris.Show();
        }

        private void button1_Click(object sender, EventArgs e)   // Yeni Kayit
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("insert into Admin (uye_id, admin_gorev, admin_username, admin_password) values ('" + int.Parse(textBox1.Text) + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "')", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Admin kayit işlemi başarılı", "Kayıt Ekranı");

            }
            catch
            {
                MessageBox.Show("Hatalı İşlem Yaptınız", "Hata Ekranı");
            }
            finally
            {
                con.Close();
            }
        }
    }
}
