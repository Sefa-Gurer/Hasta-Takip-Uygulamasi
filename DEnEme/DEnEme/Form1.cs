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
            veri_tabani.Open();
            
            int girilen_id= Convert.ToInt32(kullanici_adi.Text);
            string girilen_sifre = (sifre.Text);

            SqlCommand gercek_sifre = new SqlCommand("Select Sisteme_Giris_Bilgileri.Sifre from Sisteme_Giris_Bilgileri Where Personel=@prmPersonel", veri_tabani);
            gercek_sifre.Parameters.AddWithValue("@prmPersonel", girilen_id);
            var sonuc = gercek_sifre.ExecuteScalar();

            // Eğer sonuç null değilse ve girilen_sifre ile eşleşiyorsa
            if (sonuc != null && girilen_sifre == sonuc.ToString())
            {
                // Doğru şifre
                MessageBox.Show("Şifre doğru!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Şifre yanlış!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            veri_tabani.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
