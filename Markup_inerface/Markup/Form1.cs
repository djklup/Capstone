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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            testingToolStripMenuItem.Enabled = false;
            searchToolStripMenuItem.Enabled = false;
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            testingToolStripMenuItem.Enabled = true;
            searchToolStripMenuItem.Enabled = true;
            login.Hide();
        }

        private void newTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewTest newform = new NewTest();
            newform.Show();
        }

        private void addExistingRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewRecord newform = new AddNewRecord();
            newform.Show();
        }

        private void mRNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchMRN newform = new SearchMRN();
            newform.Show();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
