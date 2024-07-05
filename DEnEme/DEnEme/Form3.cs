using System;
using System.Collections;
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
    public partial class Form3 : Form
    {
        int id;
        public Form3(int gelenid)
        {
            id = gelenid;
            InitializeComponent();
        }
        SqlConnection veri_tabani = new SqlConnection(@"Server = DESKTOP-8RQP2FE\SQLEXPRESS;Database=HastaTakip; Trusted_Connection=True");
        private void Form3_Load(object sender, EventArgs e)
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
            Form2 form2 = new Form2(id);
            this.Hide(); // Form1'i gizle
            form2.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Int64 girilen_tc = Convert.ToInt64(textBox1.Text);

                using (SqlCommand hasta_tc = new SqlCommand("SELECT * FROM Hastalar WHERE Hasta_tc = @TcNo", veri_tabani))
                {
                    // Parametre ekleyin
                    hasta_tc.Parameters.AddWithValue("@TcNo", girilen_tc);

                    SqlDataAdapter hasta_bilgileri = new SqlDataAdapter(hasta_tc);

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
            // Verileri çekmek için bir SqlDataAdapter kullanın
            SqlDataAdapter hasta_bilgileri = new SqlDataAdapter("SELECT * FROM Hastalar", veri_tabani);

            // Verileri içerecek bir DataTable oluşturun
            DataTable dataTable = new DataTable();

            // DataTable'a verileri doldurun
            hasta_bilgileri.Fill(dataTable);

            // DataGridView'e DataTable'ı bağlayın
            dataGridView1.DataSource = dataTable;
        }
    }
}
