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
    public partial class AzbioResult : Form
    {
        public AzbioResult()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AzBioForm newform = new AzBioForm();
            newform.Show();
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
