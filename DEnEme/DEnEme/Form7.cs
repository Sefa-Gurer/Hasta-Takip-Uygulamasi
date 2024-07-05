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

namespace DEnEme
{
    public partial class Form7 : Form
    {
        int id;
        public Form7(int gelenid)
        {
            id = gelenid;
            InitializeComponent();
        }
        SqlConnection veri_tabani = new SqlConnection(@"Server = DESKTOP-8RQP2FE\SQLEXPRESS;Database=HastaTakip; Trusted_Connection=True");
        private void Form7_Load(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Int64 girilen_tc = Convert.ToInt64(textBox1.Text);

                string query = "SELECT Hastalar.Hasta_tc," +
                               "       Hastalar.Hasta_Ad," +
                               "       Hastalar.Hasta_Soyad," +
                               "       Tahliller.Sonuc_Tipi," +
                               "       Tahliller.Sonuc," +
                               "       Tahlil_Aralikleri.Sonuc_Min, " +
                               "       Tahlil_Aralikleri.Sonuc_Max, " +
                               "       Tahliller.Tarih " +
                               "FROM Tahliller " +
                               "LEFT JOIN Hastalar ON Tahliller.tc = Hastalar.Hasta_tc " +
                               "LEFT JOIN Tahlil_Aralikleri ON Tahliller.Sonuc_Tipi = Tahlil_Aralikleri.Sonuc_Tipi " +
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

            string query = "SELECT Hastalar.Hasta_tc," +
                "           Hastalar.Hasta_Ad," +
                "           Hastalar.Hasta_Soyad," +
                "           Tahliller.Sonuc_Tipi," +
                "           Tahliller.Sonuc," +
                "           Tahlil_Aralikleri.Sonuc_Min, " +
                "           Tahlil_Aralikleri.Sonuc_Max, " +
                "           Tahliller.Tarih " +
                           "FROM Tahliller " +
                           "LEFT JOIN Hastalar ON Tahliller.tc = Hastalar.Hasta_tc " +
                           "LEFT JOIN Tahlil_Aralikleri ON Tahliller.Sonuc_Tipi = Tahlil_Aralikleri.Sonuc_Tipi ";

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
