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
    public partial class SelectExistingPatient : Form
    {
        public SelectExistingPatient()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            AddNewPatient a = new AddNewPatient();
            a.Show();
            
        }
    }
}
