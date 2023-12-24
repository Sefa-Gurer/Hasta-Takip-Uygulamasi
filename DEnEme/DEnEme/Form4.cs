using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DEnEme
{
    public partial class Form4 : Form
    {
        int id;
        public Form4(int gelenid)
        {
            id = gelenid;
            InitializeComponent();
        }
        SqlConnection veri_tabani = new SqlConnection(@"Server = SGURER\SQLEXPRESS;Database=HastaTakip; Trusted_Connection=True");
        private void Form4_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(id);
            this.Hide(); // Form4'ü gizle
            form2.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string textbox1 = (textBox1.Text.Trim());
            string girilen_ad = (textBox2.Text.Trim());
            string girilen_soyad = (textBox3.Text.Trim());
            string girilen_mail = (textBox4.Text.Trim());
            string textbox2 = (textBox5.Text.Trim());
            string girilen_dogum_gunu = (textBox6.Text.Trim());
            string girilen_kan_grubu = (textBox7.Text.Trim());

            if (textbox1 == "" || girilen_ad == "" || girilen_soyad == "" || textbox2 == "" || girilen_dogum_gunu == "" || girilen_kan_grubu == "")
            {
                MessageBox.Show("Doldurulması zorunlu alanlar boş", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (girilen_mail != "" && !girilen_mail.Contains("@"))
                {
                    MessageBox.Show("Geçerli bir mail giriniz", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Int64 girilen_tc = Convert.ToInt64(textbox1);
                    Int64 girilen_telefon = Convert.ToInt64(textbox2);
                    if (girilen_tc < 99999999999 && girilen_tc > 10000000000)
                    {
                        if (girilen_telefon < 5999999999 && girilen_telefon > 5000000000)
                        {
                            MessageBox.Show("tc ve telefon doğru", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Geçerli bir telefon numarası giriniz", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Geçerli bir T.C. giriniz", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}
