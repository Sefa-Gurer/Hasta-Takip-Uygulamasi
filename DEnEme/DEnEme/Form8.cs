using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DEnEme
{
    public partial class Form8 : Form
    {
        int gelenid;
        public Form8(int id)
        {
            gelenid = id;
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(gelenid);
            this.Hide(); // Form4'ü gizle
            form2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form9 form9 = new Form9(gelenid);
            this.Hide(); // Form4'ü gizle
            form9.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form10 form10 = new Form10(gelenid);
            this.Hide(); // Form4'ü gizle
            form10.Show();
        }
    }
}
