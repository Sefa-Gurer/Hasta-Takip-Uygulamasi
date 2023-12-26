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
    public partial class Form6 : Form
    {
        int id;
        public Form6(int gelenid)
        {
            id = gelenid;
            InitializeComponent();
        }
        SqlConnection veri_tabani = new SqlConnection(@"Server = SGURER\SQLEXPRESS;Database=HastaTakip; Trusted_Connection=True");
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
            Form2 ana_ekran = new Form2(id);
            this.Hide();
            ana_ekran.ShowDialog();
        }
        void gridAyari()
        {
            dataGridView1.ReadOnly = true; // sadece okunabilir olması yani veri düzenleme kapalı
            dataGridView1.AllowUserToDeleteRows = false; // satırların silinmesi engelleniyor
        }
        void tüm_verileri_listeleme()
        {
            // Verileri çekmek için bir SqlDataAdapter kullanın
            SqlDataAdapter hasta_bilgileri = new SqlDataAdapter("SELECT * FROM Radyolojik_Goruntuler", veri_tabani);

            // Verileri içerecek bir DataTable oluşturun
            DataTable dataTable = new DataTable();

            // DataTable'a verileri doldurun
            hasta_bilgileri.Fill(dataTable);

            // DataGridView'e DataTable'ı bağlayın
            dataGridView1.DataSource = dataTable;
        }
    }
}
