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
    public partial class SearchRangeDate : Form
    {
        public SearchRangeDate()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SearchResultDetail a = new SearchResultDetail();
            a.Show();
        }
    }
}