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

namespace DEnEme
{
    public partial class Form6 : Form
    {
        int id;
        public Form6(int gelenid)
        {
            id = gelenid;
            InitializeComponent();
        }
        SqlConnection veri_tabani = new SqlConnection(@"Server = DESKTOP-8RQP2FE\SQLEXPRESS;Database=HastaTakip; Trusted_Connection=True");
        private void Form6_Load(object sender, EventArgs e)
        {
            veri_tabani.Open();
            try
            {
                tüm_verileri_listeleme();
                gridAyari();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri yükleme hatası: " + ex.Message);
            }
            finally
            {
                veri_tabani.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5(id);
            this.Hide();
            form5.ShowDialog();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewCell clickedCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];

                if (clickedCell.Value == null || string.IsNullOrEmpty(clickedCell.Value.ToString()))
                {
                }
                else
                {
                    veri_tabani.Open();

                    string tcValue = dataGridView1.Rows[e.RowIndex].Cells["tc"].Value.ToString();
                    DateTime tarihValue = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells["Tarih"].Value);

                    using (SqlCommand komut = new SqlCommand("SELECT Sonuc FROM Radyolojik_Goruntuler WHERE tc = @tc AND Tarih = @tarih", veri_tabani))
                    {
                        komut.Parameters.AddWithValue("@tc", tcValue);
                        komut.Parameters.AddWithValue("@tarih", tarihValue);

                        using (SqlDataReader dr = komut.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                if (dr["Sonuc"] != DBNull.Value)
                                {
                                    byte[] resim = (byte[])dr["Sonuc"];

                                    if (resim != null && resim.Length > 0)
                                    {
                                        using (MemoryStream memorystream = new MemoryStream(resim))
                                        {
                                            pictureBox1.Image = Image.FromStream(memorystream);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Resim verisi boş veya geçersiz format.");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Resim verisi bulunamadı.");
                                }
                            }
                        }
                    }
                    veri_tabani.Close();
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Int64 girilen_tc = Convert.ToInt64(textBox1.Text);

                string query = "SELECT Radyolojik_Goruntuler.tc," +
                               "       Hastalar.Hasta_Ad," +
                               "       Hastalar.Hasta_Soyad," +
                               "       Radyolojik_Goruntuler.Sonuc_Tipi," +
                               "       Radyolojik_Goruntuler.Tarih " +
                               "FROM Radyolojik_Goruntuler " +
                               "LEFT JOIN Hastalar ON Radyolojik_Goruntuler.tc = Hastalar.Hasta_tc " +
                               "WHERE Hastalar.Hasta_tc = @tcKimlik"; // Kişinin TC'sine göre filtreleme

                // SqlCommand ve SqlParameter kullanarak sorguyu çalıştırma
                using (SqlCommand cmd = new SqlCommand(query, veri_tabani))
                {
                    // Parametreyi ekleyin
                    cmd.Parameters.AddWithValue("@tcKimlik", girilen_tc);

                    // Verileri çekmek için bir SqlDataAdapter kullanın
                    SqlDataAdapter hasta_bilgileri = new SqlDataAdapter(cmd);

                    // Verileri içerecek bir DataTable oluşturun
                    DataTable dataTable = new DataTable();

                    // DataTable'a verileri doldurun
                    hasta_bilgileri.Fill(dataTable);

                    // DataGridView'e DataTable'ı bağlayın
                    dataGridView1.DataSource = dataTable;
                    gridAyari();
                }
            }
            catch
            {
                MessageBox.Show("Geçerli bir değer giriniz", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tüm_verileri_listeleme();
            gridAyari();
            textBox1.Text = "";
        }
        void gridAyari()
        {
            dataGridView1.ReadOnly = true; // sadece okunabilir olması yani veri düzenleme kapalı
            dataGridView1.AllowUserToDeleteRows = false; // satırların silinmesi engelleniyor
        }
        void tüm_verileri_listeleme()
        {



            string query = "SELECT Radyolojik_Goruntuler.tc," +
                           "       Hastalar.Hasta_Ad," +
                           "       Hastalar.Hasta_Soyad," +
                           "       Radyolojik_Goruntuler.Sonuc_Tipi," +
                           "       Radyolojik_Goruntuler.Tarih " +
                           "FROM Radyolojik_Goruntuler " +
                           "LEFT JOIN Hastalar ON Radyolojik_Goruntuler.tc = Hastalar.Hasta_tc ";

            // Verileri çekmek için bir SqlDataAdapter kullanın
            SqlDataAdapter hasta_bilgileri = new SqlDataAdapter(query, veri_tabani);

            // Verileri içerecek bir DataTable oluşturun
            DataTable dataTable = new DataTable();

            // DataTable'a verileri doldurun
            hasta_bilgileri.Fill(dataTable);

            // DataGridView'e DataTable'ı bağlayın
            dataGridView1.DataSource = dataTable;
        }
    }
}