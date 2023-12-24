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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            dateTimePicker1.MaxDate = DateTime.Today;
            comboBox1.Items.Add("0-");
            comboBox1.Items.Add("0+");
            comboBox1.Items.Add("A-");
            comboBox1.Items.Add("A+");
            comboBox1.Items.Add("B-");
            comboBox1.Items.Add("B+");
            comboBox1.Items.Add("AB-");
            comboBox1.Items.Add("AB+");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(id);
            this.Hide(); // Form4'ü gizle
            form2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                veri_tabani.Open();
                string textbox1 = (textBox1.Text.Trim());
                string girilen_ad = (textBox2.Text.Trim());
                string girilen_soyad = (textBox3.Text.Trim());
                string girilen_mail = (textBox4.Text.Trim());
                string textbox2 = (textBox5.Text.Trim());
                DateTime girilen_dogum_gunu = dateTimePicker1.Value;
                //string girilen_dogum_gunu = (textBox6.Text.Trim());
                //|| girilen_dogum_gunu == ""
                //string girilen_kan_grubu = (textBox7.Text.Trim());
                //|| girilen_kan_grubu == ""
                if (comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Lütfen kan grubu girin.");
                }
                else
                {
                    string girilen_kan_grubu = comboBox1.SelectedItem.ToString();
                    if (textbox1 == "" || girilen_ad == "" || girilen_soyad == "" || textbox2 == "")
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
                            if (Int64.TryParse(textbox1, out Int64 girilen_tc) && Int64.TryParse(textbox2, out Int64 girilen_telefon))
                            {
                                if (girilen_tc < 99999999999 && girilen_tc > 10000000000)
                                {
                                    if (girilen_telefon < 5999999999 && girilen_telefon > 5000000000)
                                    {
                                        using (SqlCommand cmd = new SqlCommand("INSERT INTO Hastalar(Hasta_tc, Hasta_Ad, Hasta_Soyad, Hasta_Email, Hasta_Tel, Hasta_Dogum_Tarihi, Kan_Grubu) VALUES(@tc, @ad, @soyad, @mail, @telefon,@dogum_tarihi, @kan_grubu)", veri_tabani))
                                        {
                                            // Parametreleri ekleyin
                                            cmd.Parameters.AddWithValue("@tc", girilen_tc);
                                            cmd.Parameters.AddWithValue("@ad", girilen_ad);
                                            cmd.Parameters.AddWithValue("@soyad", girilen_soyad);
                                            cmd.Parameters.AddWithValue("@mail", girilen_mail);
                                            cmd.Parameters.AddWithValue("@telefon", girilen_telefon);
                                            cmd.Parameters.AddWithValue("@dogum_tarihi", girilen_dogum_gunu);
                                            cmd.Parameters.AddWithValue("@kan_grubu", girilen_kan_grubu);

                                            // Sorguyu çalıştırın
                                            int affectedRows = cmd.ExecuteNonQuery();
                                            if (affectedRows > 0)
                                            {
                                                MessageBox.Show("Hasta başarıyla eklendi.");
                                                textBox1.Text = string.Empty;
                                                textBox2.Text = string.Empty;
                                                textBox3.Text = string.Empty;
                                                textBox4.Text = string.Empty;
                                                textBox5.Text = string.Empty;
                                                dateTimePicker1.Value = DateTime.Now.Date;
                                                comboBox1.SelectedIndex = -1; // ComboBox'ı sıfırla
                                            }
                                            else
                                            {
                                                MessageBox.Show("Hasta eklenirken bir hata oluştu.");
                                            }
                                        }
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
                            else
                            {
                                MessageBox.Show("Geçerli bir T.C. ve telefon numarası giriniz", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Hay Aksi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            veri_tabani.Close();
        }
    }
}