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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            testingToolStripMenuItem.Enabled = false;
            searchToolStripMenuItem.Enabled = false;
            userManagementToolStripMenuItem.Enabled = false;

            //this.Width = 700;
            //this.Height = 500;
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            testingToolStripMenuItem.Enabled = true;
            searchToolStripMenuItem.Enabled = true;
            userManagementToolStripMenuItem.Enabled = true;
            login.Hide();
        }

        private void newTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewPatient newform = new AddNewPatient();
            newform.Show();

        }

        private void addExistingRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewRecord newform = new AddNewRecord();
            newform.Show();
        }

        private void mRNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchMRN newform = new SearchMRN();
            newform.Show();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void addTesterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tester newform = new Tester();
            newform.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Tester newform = new Tester();
            newform.Show();
        }

        private void adminLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void testerLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePassword a = new ChangePassword();
            a.Show();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            //this.Close();
            Form1 newform = new Form1();
            newform.Show();
            //this.Close();
            
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectExistingPatient a = new SelectExistingPatient();
            a.Show();
        }

        private void reportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportResult a = new ExportResult();
            a.Show();
        }

        private void patientNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchPatient a = new SearchPatient();
            a.Show();
        }

        private void doctorNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchTester a = new SearchTester();
            a.Show();
        }

        private void rangeOfDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchRangeDate a = new SearchRangeDate();
            a.Show();
        }

        private void createChangeUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdminLevel a = new AdminLevel();
            //a.Height = 600;
            //a.Width = 600;
            a.Show();
        }

        private void createNewGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewGroup a = new CreateNewGroup();
            a.Show();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string CurrentDatabasePath = Environment.CurrentDirectory + @"\quanlykho.mdf";

            FolderBrowserDialog fbd = new FolderBrowserDialog();     // Bat chon folder de Backup

            if (fbd.ShowDialog() == DialogResult.OK)
            {

                string PathtobackUp = fbd.SelectedPath.ToString();

                //System.IO.File.Copy(CurrentDatabasePath, PathtobackUp + @"\BackUp.mdf", true);
                MessageBox.Show("Data Saved Successfully", "Report");
            }
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string PathToRestoreDB = Environment.CurrentDirectory + @"\quanlykho.mdf";

                OpenFileDialog ofd = new OpenFileDialog();

                if (ofd.ShowDialog() == DialogResult.OK)
                {

                    string Filetorestore = ofd.FileName;

                    // Rename Current Database to .Bak

                    //System.IO.File.Move(PathToRestoreDB, PathToRestoreDB + " .bak");

                    //Restore the Databse From Backup Folder

                    //System.IO.File.Copy(Filetorestore, PathToRestoreDB, true);



                }
                MessageBox.Show("Data Restored Successfully", "Report");
            }
            catch
            {
               // MessageBox.Show("File đã được phục hồi!", "Báo cáo");
            }

        }
    }
}
