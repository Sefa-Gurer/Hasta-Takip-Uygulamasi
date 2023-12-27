using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DEnEme
{
    public partial class Form10 : Form
    {
        int id;
        string imagepath;
        int secim;
        public Form10(int gelenid)
        {
            id = gelenid;
            InitializeComponent();
        }
        SqlConnection veri_tabani = new SqlConnection(@"Server = SGURER\SQLEXPRESS;Database=HastaTakip; Trusted_Connection=True");
        private void button1_Click(object sender, EventArgs e)
        {
            Form8 form8 = new Form8(id);
            this.Hide(); // Form4'ü gizle
            form8.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Sonuç seç";
            openFileDialog1.Filter = "png(*.png)|*.png|jpg(*.jpg)|*.jpg|jpeg(*.jpeg)|*.jpeg";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = System.Drawing.Image.FromFile(openFileDialog1.FileName);
                imagepath = openFileDialog1.FileName.ToString();
                secim = 1;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                veri_tabani.Open();
                if (secim == 1)
                {
                    FileStream filestream = new FileStream(imagepath, FileMode.Open, FileAccess.Read);
                    BinaryReader binaryreader = new BinaryReader(filestream);
                    byte[] goruntu = binaryreader.ReadBytes((int)filestream.Length);
                    binaryreader.Close();
                    filestream.Close();

                    string textbox1 = (textBox1.Text.Trim());
                    if (comboBox1.SelectedIndex == -1)
                    {
                        MessageBox.Show("Lütfen sonuç tipi girin.");
                    }
                    else
                    {
                        string girilen_sonuc_tipi = comboBox1.SelectedItem.ToString();
                        if (textbox1 == "")
                        {
                            MessageBox.Show("Doldurulması zorunlu alanlar boş", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            if (Int64.TryParse(textbox1, out Int64 girilen_tc))
                            {
                                if (girilen_tc < 99999999999 && girilen_tc > 10000000000)
                                {
                                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Radyolojik_Goruntuler(tc,Sonuc_Tipi,Sonuc,Tarih) VALUES(@tc, @tip, @sonuc, @tarih)", veri_tabani))
                                    {
                                        // Parametreleri ekleyin
                                        cmd.Parameters.AddWithValue("@tc", girilen_tc);
                                        cmd.Parameters.AddWithValue("@tip", girilen_sonuc_tipi);
                                        cmd.Parameters.Add("@sonuc", SqlDbType.Image, goruntu.Length).Value= goruntu;
                                        DateTime tarih = DateTime.Now;
                                        cmd.Parameters.AddWithValue("@tarih", tarih);


                                        // Sorguyu çalıştırın
                                        int affectedRows = cmd.ExecuteNonQuery();
                                        if (affectedRows > 0)
                                        {
                                            MessageBox.Show("Sonuç başarıyla eklendi.");
                                            textBox1.Text = string.Empty;
                                            comboBox1.SelectedIndex = -1; // ComboBox'ı sıfırla
                                            pictureBox1.Image = null;
                                            secim = 0;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Hasta eklenirken bir hata oluştu.");
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
                }
                else
                {
                    MessageBox.Show("Sonuç seçimi yapılmadı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("Hay Aksi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            veri_tabani.Close();
        }
        private void Form10_Load(object sender, EventArgs e)
        {
            secim = 0;
            comboBox1.Items.Add("MR");
            comboBox1.Items.Add("RÖNTGEN");
        }
    }
}
