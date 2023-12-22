using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DEnEme
{
    public partial class Form1 : Form
    {    
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection veri_tabani = new SqlConnection(@"Server = SGURER\SQLEXPRESS;Database=HastaTakip; Trusted_Connection=True");
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (kullanici_adi.Text == "")
            {
                MessageBox.Show("ID giriniz", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                veri_tabani.Open();
                int girilen_id = Convert.ToInt32(kullanici_adi.Text);
                string girilen_sifre = (sifre.Text);

                SqlCommand gercek_sifre = new SqlCommand("Select Sisteme_Giris_Bilgileri.Sifre from Sisteme_Giris_Bilgileri Where Personel=@prmPersonel", veri_tabani);
                gercek_sifre.Parameters.AddWithValue("@prmPersonel", girilen_id);
                var sonuc = gercek_sifre.ExecuteScalar();

                // Eğer sonuç null değilse ve girilen_sifre ile eşleşiyorsa
                try
                {
                    if (sonuc == null)
                    {
                        MessageBox.Show("Kullanıcı Bulunamadı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (girilen_sifre == sonuc.ToString())
                        {
                            DateTime tarih = DateTime.Now;
                            SqlCommand giris_bilgisi = new SqlCommand("INSERT INTO Sisteme_Giris_Zamani (Personel, Giris_Zamani) VALUES (@prmPersonel, @prmTarih)", veri_tabani);
                            giris_bilgisi.Parameters.AddWithValue("@prmPersonel", girilen_id);
                            giris_bilgisi.Parameters.AddWithValue("@prmTarih", tarih);
                            giris_bilgisi.ExecuteNonQuery();

                            // Doğru şifre
                            Form2 ana_ekran = new Form2(girilen_id);
                            this.Hide();
                            ana_ekran.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Şifre yanlış!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch { MessageBox.Show("Hay Aksi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                veri_tabani.Close();
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
