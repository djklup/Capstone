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
    public partial class AddNewRecord : Form
    {
        public AddNewRecord()
        {
            InitializeComponent();
            //tabControl1.TabPages.Remove(tabPage2);
            //tabControl1.TabPages.Remove(tabPage3);
            //tabControl1.TabPages.Remove(tabPage4);

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AddNewRecord_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage4;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AddRecordDetailCNC newform = new AddRecordDetailCNC();
            newform.Show();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            AddRecordDetailAzBio newform = new AddRecordDetailAzBio();
            newform.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //tabControl1.TabPages.Remove(tabPage1);
            //tabControl1.TabPages.Add(tabPage2);
            tabControl1.SelectedTab = tabPage2;
            textBox4.Text = textBox1.Text;
                                            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
