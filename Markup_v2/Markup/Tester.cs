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
    public partial class Tester : Form
    {
        public Tester()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DeleteTester newform = new DeleteTester();
            newform.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DeleteTester newform = new DeleteTester();
            newform.Show();
        }
    }
}
