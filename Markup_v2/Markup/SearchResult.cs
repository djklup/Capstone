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
    public partial class SearchResult : Form
    {
        public SearchResult()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.Close();
            SearchResultDetail newform = new SearchResultDetail();
            newform.Show();
        }

        private void SearchMRNresult_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            PlotSingleResult newform = new PlotSingleResult();
            newform.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PlotSingleResult newform = new PlotSingleResult();
            newform.Show();
        }
    }
}
