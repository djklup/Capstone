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
    public partial class AzBioAudioTest : Form
    {
        public AzBioAudioTest()
        {
            InitializeComponent();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            AzbioResult newform = new AzbioResult();
            newform.Show();
            this.Close();
        }
    }
}
