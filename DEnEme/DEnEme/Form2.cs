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
            gelenid = id;
            InitializeComponent();
        }
        SqlConnection veri_tabani = new SqlConnection(@"Server = SGURER\SQLEXPRESS;Database=HastaTakip; Trusted_Connection=True");
        private void Form2_Load(object sender, EventArgs e)
        {
            veri_tabani.Open();

            SqlCommand gercek_sifre = new SqlCommand("Select Personeller.Personel_Ad from Personeller Where PersonelID=@prmPersonel", veri_tabani);
            gercek_sifre.Parameters.AddWithValue("@prmPersonel", gelenid);
            var ad = gercek_sifre.ExecuteScalar();

            label1.Text = "Hoş geldin " + ad;

            DateTime tarih = DateTime.Now;
            label2.Text = " " + tarih.DayOfWeek + " "  + tarih;
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
