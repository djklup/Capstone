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
    public partial class SearchPatient : Form
    {
        public SearchPatient()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //SearchPatientResult a = new SearchPatientResult();
            //a.Show();
            //this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SearchResult a = new SearchResult();
            a.Show();
            this.Close();
        }
    }
}
