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
    public partial class ExistingPatientNewTest : Form
    {
        public ExistingPatientNewTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CNCform newform = new CNCform();
            newform.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AzBioForm newform = new AzBioForm();
            newform.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SearchResultDetail newform = new SearchResultDetail();
            newform.Show();

            this.Close();
        }

    }
}
