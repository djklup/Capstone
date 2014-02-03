using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Markup
{
    public partial class CNCform : Form
    {
        public CNCform()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CNCresult newform = new CNCresult();
            newform.Show();

        }

        private void Start_Click(object sender, EventArgs e)
        {
            CNCaudioTest newform = new CNCaudioTest();
            newform.Show();
            this.Close();
        }
    }
}
