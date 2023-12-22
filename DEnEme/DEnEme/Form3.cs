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
    public partial class Form3 : Form
    {
        int id;
        public Form3(int gelenid)
        {
            id = gelenid;
            InitializeComponent();
        }
        SqlConnection veri_tabani = new SqlConnection(@"Server = SGURER\SQLEXPRESS;Database=HastaTakip; Trusted_Connection=True");
        private void Form3_Load(object sender, EventArgs e)
        {
            try
            {
                veri_tabani.Open();

                // Verileri çekmek için bir SqlDataAdapter kullanın
                SqlDataAdapter hasta_bilgileri = new SqlDataAdapter("SELECT * FROM Hastalar", veri_tabani);

                // Verileri içerecek bir DataTable oluşturun
                DataTable dataTable = new DataTable();

                // DataTable'a verileri doldurun
                hasta_bilgileri.Fill(dataTable);

                // DataGridView'e DataTable'ı bağlayın
                dataGridView1.DataSource = dataTable;
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
    }
}
