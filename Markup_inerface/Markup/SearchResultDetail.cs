﻿using System;
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
    public partial class SearchResultDetail : Form
    {
        public SearchResultDetail()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SearchResultDetailCNC newform = new SearchResultDetailCNC();
            newform.Show();

            SearchResultDetailAzBio newform2 = new SearchResultDetailAzBio();
            newform2.Show();
                      
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PlotSingleResult newform = new PlotSingleResult();
            newform.Show();
        }
    }
}
