using System.Data.SqlClient;

namespace kutuphaneYonetim
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=MSI\SQLEXPRESS;Initial Catalog=kutuphane;Integrated Security=True");
        SqlCommand cmd;
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 kayitOl = new Form2();
            this.Hide();
            kayitOl.Show();
        }

        private void button1_Click(object sender, EventArgs e) //GIRIS
        {
            try
            {
                con.Open();
                cmd = new SqlCommand(" select * from Admin", con);
                SqlDataReader dr = cmd.ExecuteReader();
                bool sonuc = false;
                while (dr.Read())
                {
                    if (dr[3].ToString().Trim() == textBox1.Text && dr[4].ToString().Trim() == textBox2.Text)
                    {
                        sonuc = true;
                        MessageBox.Show("Giri� Ba�ar�l� ", "Giri� Ekran�");
                        break;
                    }
                }

                if (sonuc)
                {
                    sonuc = false;
                    Form3 form3 = new Form3();
                    this.Hide();
                    form3.Show();
                }
                else
                {
                    MessageBox.Show("Hatal� Kullan�c� Ad� ya da Parola ! ", "Hatal� Giri� Ekran� ");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hatal� i�lem ", "Hata ekran� ");
            }
            finally 
            { 
                con.Close(); 
            } 
        }
    }
}