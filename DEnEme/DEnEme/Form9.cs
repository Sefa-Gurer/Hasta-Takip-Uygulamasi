using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DEnEme
{
    public partial class Form9 : Form
    {
        int gelenid;
        string girilen_sonuc_tipi;
        public Form9(int id)
        {
            gelenid = id;
            InitializeComponent();
        }
        SqlConnection veri_tabani = new SqlConnection(@"Server = DESKTOP-8RQP2FE\SQLEXPRESS;Database=HastaTakip; Trusted_Connection=True");
        private void Form9_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("DEMİR");
            comboBox1.Items.Add("GLUKOZ");
            comboBox1.Items.Add("İNSÜLİN");
            comboBox1.Items.Add("KOLESTEROL");
            comboBox1.Items.Add("TSH");
            comboBox1.Items.Add("ÜRE");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form8 form8 = new Form8(gelenid);
            this.Hide();
            form8.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //try
            //{
                veri_tabani.Open();
                DateTime tarih = DateTime.Now;
                string textbox1 = (textBox1.Text.Trim());
                string girilen_sonuc = (textBox2.Text.Trim());

                if (comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Lütfen sonuç tipi giriniz.");
                }
                else
                {
                    if (textbox1 == "" || girilen_sonuc == "")
                    {
                        MessageBox.Show("Doldurulması zorunlu alanlar boş", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (Int64.TryParse(textbox1, out Int64 girilen_tc))
                        {
                            if (girilen_tc < 99999999999 && girilen_tc > 10000000000)
                            {
                                using (SqlCommand cmd = new SqlCommand("INSERT INTO Tahliller(tc, Sonuc_Tipi, Sonuc, Tarih) VALUES(@tc, @sonuc_tipi, @sonuc, @tarih)", veri_tabani))
                                {
                                    // Parametreleri ekleyin
                                    cmd.Parameters.AddWithValue("@tc", girilen_tc);
                                    cmd.Parameters.AddWithValue("@sonuc_tipi", girilen_sonuc_tipi);
                                    cmd.Parameters.AddWithValue("@sonuc", girilen_sonuc);
                                    cmd.Parameters.AddWithValue("@tarih", tarih);

                                    // Sorguyu çalıştırın
                                    int affectedRows = cmd.ExecuteNonQuery();
                                    if (affectedRows > 0)
                                    {
                                        MessageBox.Show("Sonuç başarıyla eklendi.");
                                        textBox1.Text = string.Empty;
                                        textBox2.Text = string.Empty;
                                        comboBox1.SelectedIndex = -1; // ComboBox'ı sıfırla
                                        label5.Text = "Ortalama değer aralığı";
                                    }
                                    else
                                    {
                                        MessageBox.Show("Sonuç eklenirken bir hata oluştu.");
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Geçerli bir T.C. giriniz", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Geçerli bir T.C. giriniz", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            //}
            //catch
            //{
            //    MessageBox.Show("Hay Aksi!(Sonucu '.' ile girmeyi deneyin)", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            veri_tabani.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Items.Count > 0)
            {
                object selecteditem = comboBox1.SelectedItem;
                if (selecteditem != null)
                {
                    veri_tabani.Open();
                    string sonuc_tipi = comboBox1.SelectedItem.ToString();
                    girilen_sonuc_tipi = sonuc_tipi;

                    SqlCommand sonucmax = new SqlCommand("Select Tahlil_Aralikleri.Sonuc_Max from Tahlil_Aralikleri Where Sonuc_Tipi=@prmsonuctipi", veri_tabani);
                    sonucmax.Parameters.AddWithValue("@prmsonuctipi", girilen_sonuc_tipi);
                    var sonuc_max = sonucmax.ExecuteScalar();

                    SqlCommand sonucmin = new SqlCommand("Select Tahlil_Aralikleri.Sonuc_Min from Tahlil_Aralikleri Where Sonuc_Tipi=@prmsonuctipi", veri_tabani);
                    sonucmin.Parameters.AddWithValue("@prmsonuctipi", girilen_sonuc_tipi);
                    var sonuc_min = sonucmin.ExecuteScalar();

                    label5.Text = Convert.ToString($"({sonuc_min} - {sonuc_max})");
                }
            }
            veri_tabani.Close();
        }
    }
}
