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
    public partial class Form6 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=MSI\SQLEXPRESS;Initial Catalog=kutuphane;Integrated Security=True");
        SqlCommand cmd;
        public void uyeListele()
        {
            string uyeListele = "SELECT u.uye_id AS [Üye No], u.uye_ad AS Ad, u.uye_soyad AS Soyad, u.uye_mail AS Mail, a.il AS İl, a.ilce AS İlçe, a.mahalle AS Mahalle, a.sokak AS Sokak, a.bina AS Bina, a.kapi AS Kapi \r\nFROM Uye u JOIN Adres a \r\nON u.adres_id = a.adres_id\r\nORDER BY u.uye_id;";
            SqlDataAdapter da = new SqlDataAdapter(uyeListele, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            //Temizle();
        }
        public void kitapListele()
        {
            string kitapListele = "SELECT k.kitap_id AS Barkod, k.kitap_ad AS [Kitap Adı], t.tur_ad AS Tür, y.yazar_ad AS [Yazar Adı], y.yazar_soyad AS [Yazar Soyad], k.kitap_basimyil AS [Basım Yılı], k.kitap_yayinci AS Yayınevi, kn.kat AS Kat, kn.dolap AS Dolap, kn.raf AS Raf\r\nFROM Konum kn\r\nJOIN Kitap k ON kn.kitap_id = k.kitap_id\r\nJOIN Yazar y ON k.yazar_id = y.yazar_id\r\nJOIN Tur t ON t.tur_id = k.tur_id\r\nORDER BY k.kitap_id;";
            SqlDataAdapter da = new SqlDataAdapter(kitapListele, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            //Temizle();
        }
        public void emanetListele()
        {
            string kitapListele = "SELECT o.odunc_id AS [Emanet No], u.uye_ad AS [Üye Ad], u.uye_soyad AS [Üye Soyad], u.uye_mail AS [Üye Mail], k.kitap_ad AS [Kitap], o.odunc_baslangic AS [Teslim Tarihi], o.odunc_bitis AS [İade Tarihi], CASE WHEN o.odunc_kontrol = 1 THEN 'Evet' ELSE 'Hayır' END AS [İade Edildi]\r\nFROM Uye u\r\nJOIN Odunc o ON u.uye_id = o.uye_id\r\nJOIN Kitap k ON o.kitap_id = k.kitap_id\r\nORDER BY o.odunc_id;";
            SqlDataAdapter da = new SqlDataAdapter(kitapListele, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView3.DataSource = dt;
            //Temizle();
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
    }
}
