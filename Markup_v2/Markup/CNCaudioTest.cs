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
    public partial class CNCaudioTest : Form
    {
        public CNCaudioTest()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            CNCresult newform = new CNCresult();
            newform.Show();
            this.Close();
        }
    }
}
