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
    public partial class AzBioForm : Form
    {
        public AzBioForm()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            AzBioAudioTest newform = new AzBioAudioTest();
            newform.Show();

            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AzbioResult newform = new AzbioResult();
            newform.Show();
        }
    }
}
