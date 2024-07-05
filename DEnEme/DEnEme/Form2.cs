using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DEnEme
{
    public partial class Form2 : Form
    {
        int gelenid;
        public Form2(int id)
        {
            InitializeComponent();
            gelenid = id;
            timer1.Start();
        }
        SqlConnection veri_tabani = new SqlConnection(@"Server = DESKTOP-8RQP2FE\SQLEXPRESS;Database=HastaTakip; Trusted_Connection=True");
        private void Form2_Load(object sender, EventArgs e)
        {
            veri_tabani.Open();

            SqlCommand gercek_sifre = new SqlCommand("Select Personeller.Personel_Ad from Personeller Where PersonelID=@prmPersonel", veri_tabani);
            gercek_sifre.Parameters.AddWithValue("@prmPersonel", gelenid);
            var ad = gercek_sifre.ExecuteScalar();

            SqlCommand punvanıd = new SqlCommand("Select Personeller.Unvan from Personeller Where PersonelID=@prmPersonel", veri_tabani);
            punvanıd.Parameters.AddWithValue("@prmPersonel", gelenid);
            var unvanıd = punvanıd.ExecuteScalar();

            SqlCommand ppoliknilikıd = new SqlCommand("Select Personeller.Poliklinik from Personeller Where PersonelID=@prmPersonel", veri_tabani);
            ppoliknilikıd.Parameters.AddWithValue("@prmPersonel", gelenid);
            var poliknilikıd = ppoliknilikıd.ExecuteScalar();

            SqlCommand ppoliknilik = new SqlCommand("Select Poliklinikler.Poliklinik_adi from Poliklinikler Where PoliklinikID=@prmPoliklinik", veri_tabani);
            ppoliknilik.Parameters.AddWithValue("@prmPoliklinik", poliknilikıd);
            var poliknilik = ppoliknilik.ExecuteScalar();

            SqlCommand punvan = new SqlCommand("Select Unvanlar.Unvan_adi from Unvanlar Where UnvanID=@prmUnvan", veri_tabani);
            punvan.Parameters.AddWithValue("@prmUnvan", unvanıd);
            var unvan = punvan.ExecuteScalar();

            label1.Text = "Hoş geldin " + ad;
            //label2.Text = " " + tarih.DayOfWeek + " "  + tarih;

            label3.Text = " " + unvan;
            label4.Text = " " + poliknilik + " Polikliniği";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 hasta_bilgileri= new Form3(gelenid);
            this.Hide();
            hasta_bilgileri.ShowDialog();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form4 hasta_ekleme = new Form4(gelenid);
            this.Hide();
            hasta_ekleme.ShowDialog();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Form5 sonuclar = new Form5(gelenid);
            this.Hide();
            sonuclar.ShowDialog();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime tarih = DateTime.Now;
            label2.Text = " " + tarih.DayOfWeek + " " + tarih;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form6 sonuc_ekleme = new Form6(gelenid);
            this.Hide();
            sonuc_ekleme.ShowDialog();
        }
    }
}
