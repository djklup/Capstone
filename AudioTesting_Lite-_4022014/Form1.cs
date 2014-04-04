using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;

using System.Windows;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Media;

using FileHelpers;
using CSVFile;
using Microsoft.CSharp;
using Microsoft.Office;
namespace AudioTesting_Lite__3192014
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.TopMost = false;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.WindowState = FormWindowState.Maximized;

            //Initial selected item in the combobox in Azbio list

            //list_sel_az.SelectedIndex = 0; // List 1 is default

            list_sel_az.Text = string.Empty;
            CNCListNumber.Text = string.Empty;

            //CNCListNumber.SelectedIndex = 0;
            
           // tb.create_table_CNC_sumary();
            tb.create_table_CNC_ConductedTest();

            CNCconductedTest.DataSource = tb.CNCdt_ConductedTest;

            tb.create_table_Test_Results();
            TestResults.DataSource = tb.TestResults;

            tb.create_table_azconlist();
            az_conductedTest.DataSource = tb.az_conlist;
         }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            // Confirm user wants to close
            switch (MessageBox.Show(this, "Are you sure you want to exit the program?", "Closing Alert", MessageBoxButtons.YesNo))
            {
                case DialogResult.No:
                    e.Cancel = true;
                    break;
                default:
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabTest.SelectedTab = tabAZListSelect;
            CleanAZmainMenu();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CleanCNCmainMenu();
            tabTest.SelectedTab = tabCNCListSelect;
        }

        //Start azbio test------------------------------------------------------------------------------
        //datatable store final results of azbio
        //
        public DataTable dt_az = new DataTable();
        public Int16 click_az = 0;
        public SoundPlayer myPlayer;
        public int[] word12;
        public int numofword;
        public int N = 0;
        public int N_totalcorrect;
        public int N_total;
        public string[] fullsentence = {" ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " " };
        //public string link_audio_az = @"C:\Users\Ted\Dropbox\Soi's Projs\Capstone\Han\AZBio\List";
        public string link_audio_az = Path.Combine(Environment.CurrentDirectory, @"Source\azbio\audio\");
        public string link_text_az = Path.Combine(Environment.CurrentDirectory, @"Source\azbio\text\");
        public Tableclass tb = new Tableclass();
        public Int16[] Word_order_az = new Int16[20];
        //---- Reset values when begin azbio test
        private void button17_Click(object sender, EventArgs e)
        {
            bool OK = true;


            if (az_comboLeft.Text == "" || az_comboRight.Text == ""|| az_signallevel.Text == "" || list_sel_az.Text == "")
            {
                OK = false;
                MessageBox.Show("Must fill out all options", "Error");
            }


            if (OK)
            {
                azbutPrevious.Visible = false;
                azbutNext.Visible = false;
                CleanAZHisTest();
                CleanAZnewTest();
                tabTest.SelectedTab = tabAZAudio;


                if (AzRandomize.Checked == true)
                {
                    Word_order_az = CreateListOrder(0, 19);
                }
                else if (AzRandomize.Checked == false)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        Word_order_az[i] = Convert.ToInt16(i);
                    }
                }

                play_list_az.Text = list_sel_az.SelectedItem.ToString();
                play_word_az.Text = "0";

                tb.create_table_sen();
                tb.create_table_score();

                click_az = 0;
                N_totalcorrect = 0;
                N_total = 0; ;
            }
           
            
        }

        //
       
        private void Done_Click(object sender, EventArgs e)
        {
            Int16 list = 3;

            list = Convert.ToInt16(play_list_az.Text);
            //Save to database
            if (click_az > 0)
            {

                for (int i = 0; i < numofword; i++)
                {
                    if (i != -1)
                    {
                        fullsentence[i] = word12[i].ToString();
                    }

                }
                
                //N_totalcorrect = N_totalcorrect + N;
                //result_1sen_num.Text = N_totalcorrect.ToString() + "/" + N_total.ToString();
                //int percent = N_totalcorrect * 100 / N_total;
                //result_1sen_percent.Text = percent.ToString() + "%";
                //Save score
                tb.add_score_row(click_az, word12);
                //Display history
                displayAzHistory(click_az - 1);

                update_azscore();
                //TESTING 00000000
                //dataGridView6.DataSource = tb.dt;
                //dataGridView6.DataBindings();
                //dataGridView8.DataSource = tb.dt_score;
                //dataGridView8.DataBindings();
                // 0000000000000
            }
            //Initailize values for 12 words (button click
            word12 = new int[12] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
            s1.BackColor = DefaultBackColor;
            s2.BackColor = DefaultBackColor;
            s3.BackColor = DefaultBackColor;
            s4.BackColor = DefaultBackColor;
            s5.BackColor = DefaultBackColor;
            s6.BackColor = DefaultBackColor;
            s7.BackColor = DefaultBackColor;
            s8.BackColor = DefaultBackColor;
            s9.BackColor = DefaultBackColor;
            s10.BackColor = DefaultBackColor;
            s11.BackColor = DefaultBackColor;
            s12.BackColor = DefaultBackColor;
            N = 0;
            //

            //click = Convert.ToInt16(Text2.Text);
            //click--;

            string FileName = @link_audio_az+ "List" + list + ".wav";
           
            /// .............CHANGE LINK HERE OR ADD FILE TO PROJECT
            ///
            string AZBIOfilepath = Path.Combine(Environment.CurrentDirectory, @"Source\azbio\text\AZBIOdata.csv");
            //string[] files = File.ReadAllLines(path);
            //string AZBIOfilepath = @"C:\Users\Ted\Dropbox\Soi's Projs\Capstone\AZBIOdata.csv";
            /// -------------
            /// 
            StreamReader sr = new StreamReader(AZBIOfilepath);
            DataTable AZBIOtable = CSV.LoadDataTable(sr, false);
            sr.Close();


            Int32 position_start = 0;
            Int32 position_end = 0;

            if (click_az == 20)
            {

                Az_groupplaying.Enabled = false;
                azbutNext.Visible = true;
                azbutPrevious.Visible = true;
                azbutNext.Enabled = false;
                click_az--;
            }
            else
            {

                position_start = Convert.ToInt32(AZBIOtable.Rows[Word_order_az[click_az]][list - 1]); //<<==Note: different code from Word test
                position_end = (Convert.ToInt32(AZBIOtable.Rows[Word_order_az[click_az]+1][list - 1]) - 1) * 4;


                position_start = (position_start + 140000) * 4;


                //Text1.Text = Convert.ToString(list);
                //Text2.Text = Convert.ToString(click + 1);

                SplitWav_AZBIO(FileName, position_start, position_end);


                Int16 temp = Convert.ToInt16(Word_order_az[click_az] + 1);
                SplitSentence(list, temp);
               // play_word_az.Text = Convert.ToString(Word_order_az[click_az]);
               
                click_az++;
                play_word_az.Text = click_az.ToString();
                //Words choosens
                Done.Text = "Done";
            }
        }
        //---------Display history
        #region AZ_History

        private void wordchange(Button but, Label lab, int col)
        {
            if (Done.Text != "Start" && but.Text != " " && but.Text != "")
            {
                switch (MessageBox.Show("Are you sure you want to change?", "Alert", MessageBoxButtons.YesNo))
                {
                    case DialogResult.Yes:
                        try
                        {

                            string sen = lab.Text;
                            string[] word = sen.Split('#');
                            int num = Convert.ToInt16(word[1]);
                            //Console.WriteLine(num);
                            if (Convert.ToInt16(tb.dt_score.Rows[num - 1][col].ToString()) == 0)
                            {
                                tb.dt_score.Rows[num - 1][col] = 1;
                                but.BackColor = Color.MediumVioletRed;
                            }
                            else if (Convert.ToInt16(tb.dt_score.Rows[num - 1][col].ToString()) == 1)
                            {
                                tb.dt_score.Rows[num - 1][col] = 0;
                                but.BackColor = DefaultBackColor;
                            }
                            update_azscore();
                        }

                        catch { }

                        break;
                    case DialogResult.No:
                        break;
                    default:
                        break;
                }

            }
        }

        private void az_his1_1_Click(object sender, EventArgs e)
        {
            wordchange(az_his1_1, az_his1, 1);
        }

        private void az_his1_2_Click(object sender, EventArgs e)
        {
            wordchange(az_his1_2, az_his1, 2);
        }

        private void az_his1_3_Click(object sender, EventArgs e)
        {
            wordchange(az_his1_3, az_his1, 3);
        }

        private void az_his1_4_Click(object sender, EventArgs e)
        {
            wordchange(az_his1_4, az_his1, 4);
        }

        private void az_his1_5_Click(object sender, EventArgs e)
        {
            wordchange(az_his1_5, az_his1, 5);
        }

        private void az_his1_6_Click(object sender, EventArgs e)
        {
            wordchange(az_his1_6, az_his1, 6);
        }

        private void az_his1_7_Click(object sender, EventArgs e)
        {
            wordchange(az_his1_7, az_his1, 7);
        }

        private void az_his1_8_Click(object sender, EventArgs e)
        {
            wordchange(az_his1_8, az_his1, 8);
        }

        private void az_his1_9_Click(object sender, EventArgs e)
        {
            wordchange(az_his1_9, az_his1, 9);
        }

        private void az_his1_10_Click(object sender, EventArgs e)
        {
            wordchange(az_his1_10, az_his1, 10);
        }
        private void az_his1_11_Click(object sender, EventArgs e)
        {
            wordchange(az_his1_11, az_his1, 11);
        }

        private void button56_Click(object sender, EventArgs e)
        {
            wordchange(button56, az_his1, 12);
        }

        private void az_his2_12_Click(object sender, EventArgs e)
        {
            wordchange(az_his2_12, az_his2, 12);
        }

        private void az_his2_11_Click(object sender, EventArgs e)
        {
            wordchange(az_his2_11, az_his2, 11);
        }

        private void az_his2_10_Click(object sender, EventArgs e)
        {
            wordchange(az_his2_10, az_his2, 10);
        }

        private void az_his2_9_Click(object sender, EventArgs e)
        {
            wordchange(az_his2_9, az_his2, 9);
        }

        private void az_his2_8_Click(object sender, EventArgs e)
        {
            wordchange(az_his2_8, az_his2, 8);
        }

        private void az_his2_7_Click(object sender, EventArgs e)
        {
            wordchange(az_his2_7, az_his2, 7);
        }

        private void az_his2_6_Click(object sender, EventArgs e)
        {
            wordchange(az_his2_6, az_his2, 6);
        }

        private void az_his2_5_Click(object sender, EventArgs e)
        {
            wordchange(az_his2_5, az_his2, 5);
        }

        private void az_his2_4_Click(object sender, EventArgs e)
        {
            wordchange(az_his2_4, az_his2, 4);
        }

        private void az_his2_3_Click(object sender, EventArgs e)
        {
            wordchange(az_his2_3, az_his2, 3);
        }

        private void az_his2_2_Click(object sender, EventArgs e)
        {
            wordchange(az_his2_2, az_his2, 2);
        }

        private void az_his2_1_Click(object sender, EventArgs e)
        {
            wordchange(az_his2_1, az_his2, 1);
        }

        private void az_his3_12_Click(object sender, EventArgs e)
        {
            wordchange(az_his3_12, az_his3, 12);
        }

        private void az_his3_11_Click(object sender, EventArgs e)
        {
            wordchange(az_his3_11, az_his3, 11);
        }

        private void az_his3_10_Click(object sender, EventArgs e)
        {
            wordchange(az_his3_10, az_his3, 10);
        }

        private void az_his3_9_Click(object sender, EventArgs e)
        {
            wordchange(az_his3_9, az_his3, 9);
        }

        private void az_his3_8_Click(object sender, EventArgs e)
        {
            wordchange(az_his3_8, az_his3, 8);
        }

        private void az_his3_7_Click(object sender, EventArgs e)
        {
            wordchange(az_his3_7, az_his3, 7);
        }

        private void az_his3_6_Click(object sender, EventArgs e)
        {
            wordchange(az_his3_6, az_his3, 6);
        }

        private void az_his3_5_Click(object sender, EventArgs e)
        {
            wordchange(az_his3_5, az_his3, 5);
        }

        private void az_his3_4_Click(object sender, EventArgs e)
        {
            wordchange(az_his3_4, az_his3, 4);
        }

        private void az_his3_3_Click(object sender, EventArgs e)
        {
            wordchange(az_his3_3, az_his3, 3);
        }

        private void az_his3_2_Click(object sender, EventArgs e)
        {
            wordchange(az_his3_2, az_his3, 2);
        }

        private void az_his3_1_Click(object sender, EventArgs e)
        {
            wordchange(az_his3_1, az_his3, 1);
        }

        private void displayAzHistory(int i)
        {
            //1st line
            if (az_his.Text == "Hide History")
            {
                try
                {
                    az_his1.Text = "#" + (i+1).ToString();
                    az_his1_1.Text = tb.dt.Rows[i][2].ToString();
                    az_his1_2.Text = tb.dt.Rows[i][3].ToString();
                    az_his1_3.Text = tb.dt.Rows[i][4].ToString();
                    az_his1_4.Text = tb.dt.Rows[i][5].ToString();
                    az_his1_5.Text = tb.dt.Rows[i][6].ToString();
                    az_his1_6.Text = tb.dt.Rows[i][7].ToString();
                    az_his1_7.Text = tb.dt.Rows[i][8].ToString();
                    az_his1_8.Text = tb.dt.Rows[i][9].ToString();
                    az_his1_9.Text = tb.dt.Rows[i][10].ToString();
                    az_his1_10.Text = tb.dt.Rows[i][11].ToString();
                    az_his1_11.Text = tb.dt.Rows[i][12].ToString();
                    button56.Text = tb.dt.Rows[i][13].ToString();

                    //Color
                    if (Convert.ToInt16(tb.dt_score.Rows[i][1].ToString()) == 1)
                        az_his1_1.BackColor = Color.MediumVioletRed;
                    else
                        az_his1_1.BackColor = DefaultBackColor;

                    if (Convert.ToInt16(tb.dt_score.Rows[i][2].ToString()) == 1)
                        az_his1_2.BackColor = Color.MediumVioletRed;
                    else
                        az_his1_2.BackColor = DefaultBackColor;

                    if (Convert.ToInt16(tb.dt_score.Rows[i][3].ToString()) == 1)
                        az_his1_3.BackColor = Color.MediumVioletRed;
                    else
                        az_his1_3.BackColor = DefaultBackColor;

                    if (Convert.ToInt16(tb.dt_score.Rows[i][4].ToString()) == 1)
                        az_his1_4.BackColor = Color.MediumVioletRed;
                    else
                        az_his1_4.BackColor = DefaultBackColor;

                    if (Convert.ToInt16(tb.dt_score.Rows[i][5].ToString()) == 1)
                        az_his1_5.BackColor = Color.MediumVioletRed;
                    else
                        az_his1_5.BackColor = DefaultBackColor;

                    if (Convert.ToInt16(tb.dt_score.Rows[i][6].ToString()) == 1)
                        az_his1_6.BackColor = Color.MediumVioletRed;
                    else
                        az_his1_6.BackColor = DefaultBackColor;

                    if (Convert.ToInt16(tb.dt_score.Rows[i][7].ToString()) == 1)
                        az_his1_7.BackColor = Color.MediumVioletRed;
                    else
                        az_his1_7.BackColor = DefaultBackColor;

                    if (Convert.ToInt16(tb.dt_score.Rows[i][8].ToString()) == 1)
                        az_his1_8.BackColor = Color.MediumVioletRed;
                    else
                        az_his1_8.BackColor = DefaultBackColor;

                    if (Convert.ToInt16(tb.dt_score.Rows[i][9].ToString()) == 1)
                        az_his1_9.BackColor = Color.MediumVioletRed;
                    else
                        az_his1_9.BackColor = DefaultBackColor;

                    if (Convert.ToInt16(tb.dt_score.Rows[i][10].ToString()) == 1)
                        az_his1_10.BackColor = Color.MediumVioletRed;
                    else
                        az_his1_10.BackColor = DefaultBackColor;

                    if (Convert.ToInt16(tb.dt_score.Rows[i][11].ToString()) == 1)
                        az_his1_11.BackColor = Color.MediumVioletRed;
                    else
                        az_his1_11.BackColor = DefaultBackColor;

                    if (Convert.ToInt16(tb.dt_score.Rows[i][12].ToString()) == 1)
                        button56.BackColor = Color.MediumVioletRed;
                    else
                        button56.BackColor = DefaultBackColor;

                }
                catch { }

                //2nd line
                try
                {
                    az_his2.Text = "#" + (i).ToString();
                    az_his2_1.Text = tb.dt.Rows[i - 1][2].ToString();
                    az_his2_2.Text = tb.dt.Rows[i - 1][3].ToString();
                    az_his2_3.Text = tb.dt.Rows[i - 1][4].ToString();
                    az_his2_4.Text = tb.dt.Rows[i - 1][5].ToString();
                    az_his2_5.Text = tb.dt.Rows[i - 1][6].ToString();
                    az_his2_6.Text = tb.dt.Rows[i - 1][7].ToString();
                    az_his2_7.Text = tb.dt.Rows[i - 1][8].ToString();
                    az_his2_8.Text = tb.dt.Rows[i - 1][9].ToString();
                    az_his2_9.Text = tb.dt.Rows[i - 1][10].ToString();
                    az_his2_10.Text = tb.dt.Rows[i - 1][11].ToString();
                    az_his2_11.Text = tb.dt.Rows[i - 1][12].ToString();
                    az_his2_12.Text = tb.dt.Rows[i - 1][13].ToString();

                    //Color
                    if (Convert.ToInt16(tb.dt_score.Rows[i - 1][1].ToString()) == 1)
                        az_his2_1.BackColor = Color.MediumVioletRed;
                    else
                        az_his2_1.BackColor = DefaultBackColor;

                    if (Convert.ToInt16(tb.dt_score.Rows[i - 1][2].ToString()) == 1)
                        az_his2_2.BackColor = Color.MediumVioletRed;
                    else
                        az_his2_2.BackColor = DefaultBackColor;

                    if (Convert.ToInt16(tb.dt_score.Rows[i - 1][3].ToString()) == 1)
                        az_his2_3.BackColor = Color.MediumVioletRed;
                    else
                        az_his2_3.BackColor = DefaultBackColor;

                    if (Convert.ToInt16(tb.dt_score.Rows[i - 1][4].ToString()) == 1)
                        az_his2_4.BackColor = Color.MediumVioletRed;
                    else
                        az_his2_4.BackColor = DefaultBackColor;

                    if (Convert.ToInt16(tb.dt_score.Rows[i - 1][5].ToString()) == 1)
                        az_his2_5.BackColor = Color.MediumVioletRed;
                    else
                        az_his2_5.BackColor = DefaultBackColor;

                    if (Convert.ToInt16(tb.dt_score.Rows[i - 1][6].ToString()) == 1)
                        az_his2_6.BackColor = Color.MediumVioletRed;
                    else
                        az_his2_6.BackColor = DefaultBackColor;

                    if (Convert.ToInt16(tb.dt_score.Rows[i - 1][7].ToString()) == 1)
                        az_his2_7.BackColor = Color.MediumVioletRed;
                    else
                        az_his2_7.BackColor = DefaultBackColor;

                    if (Convert.ToInt16(tb.dt_score.Rows[i - 1][8].ToString()) == 1)
                        az_his2_8.BackColor = Color.MediumVioletRed;
                    else
                        az_his2_8.BackColor = DefaultBackColor;

                    if (Convert.ToInt16(tb.dt_score.Rows[i - 1][9].ToString()) == 1)
                        az_his2_9.BackColor = Color.MediumVioletRed;
                    else
                        az_his2_9.BackColor = DefaultBackColor;

                    if (Convert.ToInt16(tb.dt_score.Rows[i - 1][10].ToString()) == 1)
                        az_his2_10.BackColor = Color.MediumVioletRed;
                    else
                        az_his2_10.BackColor = DefaultBackColor;

                    if (Convert.ToInt16(tb.dt_score.Rows[i - 1][11].ToString()) == 1)
                        az_his2_11.BackColor = Color.MediumVioletRed;
                    else
                        az_his2_11.BackColor = DefaultBackColor;

                    if (Convert.ToInt16(tb.dt_score.Rows[i - 1][12].ToString()) == 1)
                        az_his2_12.BackColor = Color.MediumVioletRed;
                    else
                        az_his2_12.BackColor = DefaultBackColor;
                }
                catch { }

                //3nd line
                try
                {
                    
                    az_his3_1.Text = tb.dt.Rows[i - 2][2].ToString();
                    az_his3_2.Text = tb.dt.Rows[i - 2][3].ToString();
                    az_his3_3.Text = tb.dt.Rows[i - 2][4].ToString();
                    az_his3_4.Text = tb.dt.Rows[i - 2][5].ToString();
                    az_his3_5.Text = tb.dt.Rows[i - 2][6].ToString();
                    az_his3_6.Text = tb.dt.Rows[i - 2][7].ToString();
                    az_his3_7.Text = tb.dt.Rows[i - 2][8].ToString();
                    az_his3_8.Text = tb.dt.Rows[i - 2][9].ToString();
                    az_his3_9.Text = tb.dt.Rows[i - 2][10].ToString();
                    az_his3_10.Text = tb.dt.Rows[i - 2][11].ToString();
                    az_his3_11.Text = tb.dt.Rows[i - 2][12].ToString();
                    az_his3_12.Text = tb.dt.Rows[i - 2][13].ToString();

                    //Color
                    if (Convert.ToInt16(tb.dt_score.Rows[i - 2][1].ToString()) == 1)
                        az_his3_1.BackColor = Color.MediumVioletRed;
                    else
                        az_his3_1.BackColor = DefaultBackColor;

                    if (Convert.ToInt16(tb.dt_score.Rows[i - 2][2].ToString()) == 1)
                        az_his3_2.BackColor = Color.MediumVioletRed;
                    else
                        az_his3_2.BackColor = DefaultBackColor;

                    if (Convert.ToInt16(tb.dt_score.Rows[i - 2][3].ToString()) == 1)
                        az_his3_3.BackColor = Color.MediumVioletRed;
                    else
                        az_his3_3.BackColor = DefaultBackColor;

                    if (Convert.ToInt16(tb.dt_score.Rows[i - 2][4].ToString()) == 1)
                        az_his3_4.BackColor = Color.MediumVioletRed;
                    else
                        az_his3_4.BackColor = DefaultBackColor;

                    if (Convert.ToInt16(tb.dt_score.Rows[i - 2][5].ToString()) == 1)
                        az_his3_5.BackColor = Color.MediumVioletRed;
                    else
                        az_his3_5.BackColor = DefaultBackColor;

                    if (Convert.ToInt16(tb.dt_score.Rows[i - 2][6].ToString()) == 1)
                        az_his3_6.BackColor = Color.MediumVioletRed;
                    else
                        az_his3_6.BackColor = DefaultBackColor;

                    if (Convert.ToInt16(tb.dt_score.Rows[i - 2][7].ToString()) == 1)
                        az_his3_7.BackColor = Color.MediumVioletRed;
                    else
                        az_his3_7.BackColor = DefaultBackColor;

                    if (Convert.ToInt16(tb.dt_score.Rows[i - 2][8].ToString()) == 1)
                        az_his3_8.BackColor = Color.MediumVioletRed;
                    else
                        az_his3_8.BackColor = DefaultBackColor;

                    if (Convert.ToInt16(tb.dt_score.Rows[i - 2][9].ToString()) == 1)
                        az_his3_9.BackColor = Color.MediumVioletRed;
                    else
                        az_his3_9.BackColor = DefaultBackColor;

                    if (Convert.ToInt16(tb.dt_score.Rows[i - 2][10].ToString()) == 1)
                        az_his3_10.BackColor = Color.MediumVioletRed;
                    else
                        az_his3_10.BackColor = DefaultBackColor;

                    if (Convert.ToInt16(tb.dt_score.Rows[i - 2][11].ToString()) == 1)
                        az_his3_11.BackColor = Color.MediumVioletRed;
                    else
                        az_his3_11.BackColor = DefaultBackColor;

                    if (Convert.ToInt16(tb.dt_score.Rows[i - 2][12].ToString()) == 1)
                        az_his3_12.BackColor = Color.MediumVioletRed;
                    else
                        az_his3_12.BackColor = DefaultBackColor;

                    az_his3.Text = "#" + (i - 1).ToString();
                }
                catch { }
            }
        }


        #endregion



        //--------- SliptSentence
        private void SplitSentence(Int16 list, Int16 sentnum)
        {
            int counter = 0;
            string line1;
            string line2;
            string line3;
            // Read the file and display it line by line.
            System.IO.StreamReader file =
            new System.IO.StreamReader(@link_text_az + "List"+ list + ".txt");
            while ((line1 = file.ReadLine()) != null)
            {
                line2 = file.ReadLine();
                line3 = file.ReadLine();
                
                if (Convert.ToInt16(line1) == sentnum)
                {
                  
                    N_total = Convert.ToInt16(line3) + N_total;
                    string []fullsentence = {" ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " " };
                    string[] word = line2.Split(' ');
                    
                    for (int i = 0; i < Convert.ToInt16(line3); i++)
                    {
                        fullsentence[i] = word[i];
                        word12[i] = 0;
                    }
                    //Save sentence
                    tb.add_row(click_az, Convert.ToInt16(line3), fullsentence);
                    //
                    s1.Text = fullsentence[0];
                    s2.Text = fullsentence[1];
                    s3.Text = fullsentence[2];
                    s4.Text = fullsentence[3];
                    s5.Text = fullsentence[4];
                    s6.Text = fullsentence[5];
                    s7.Text = fullsentence[6];
                    s8.Text = fullsentence[7];
                    s9.Text = fullsentence[8];
                    s10.Text = fullsentence[9];
                    s11.Text = fullsentence[10];
                    s12.Text = fullsentence[11];
                    //Console.WriteLine(line2);
                    counter++;
                    break;
                }
            }
        }

        private void SplitWav_AZBIO(string FileName, Int32 position_start, Int32 position_end)
        {
            /*
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SoundPlayer s = new SoundPlayer(ofd.FileName);
                s.Play();
            } */

            Int32 sentenceLength = position_end - position_start;
            //Console.WriteLine(sentenceLength);
            byte[] wav = File.ReadAllBytes(FileName);

            var header = new byte[44];

            for (int i = 0; i < 44; i++)
            {
                header[i] = wav[i];
            }

            /////Change length in bytes of sentence to hexa
            byte p0 = Convert.ToByte(sentenceLength % 256);

            Int32 ptemp = sentenceLength / 256;
            byte p1 = Convert.ToByte(ptemp % 256);
            ptemp = ptemp / 256;
            byte p2 = Convert.ToByte(ptemp % 256);
            ptemp = ptemp / 256;
            byte p3 = Convert.ToByte(ptemp);
            ////length of data = 520000
            header[40] = p0;
            header[41] = p1;
            header[42] = p2;
            header[43] = p3;


            /////Change length+36 in bytes of sentence to hexa
            p0 = Convert.ToByte((sentenceLength + 36) % 256);
            ptemp = sentenceLength / 256;
            p1 = Convert.ToByte(ptemp % 256);
            ptemp = ptemp / 256;
            p2 = Convert.ToByte(ptemp % 256);
            ptemp = ptemp / 256;
            p3 = Convert.ToByte(ptemp);
            /////length of data(520000) + 36
            header[4] = p0;
            header[5] = p1;
            header[6] = p2;
            header[7] = p3;
            

            //Int32 wordLength = 520000;

            //Int32 position = 2841776;

            var sentence = new byte[sentenceLength];

            for (Int32 i = 0; i < sentenceLength; i++)
            {
                sentence[i] = wav[position_start + i];
            }

            var wavfile = new byte[Convert.ToInt32(header.Length + sentenceLength)];
            header.CopyTo(wavfile, 0);
            sentence.CopyTo(wavfile, header.Length);

            ////play sound from Byte array
            MemoryStream ms = new MemoryStream(wavfile);
            myPlayer = new SoundPlayer(ms);
            myPlayer.Play();

        }

        private void ShowDialog(int x)
        {
            throw new NotImplementedException();
        }

        private void Play(string name)
        {
            var sound = new System.Media.SoundPlayer(name);
            sound.Play();
        }

        private void repeat_Click(object sender, EventArgs e)
        {
            if (Done.Text != "Start")
            {
                try
                {
                    myPlayer.Play();
                }
                catch
                {
                    MessageBox.Show("Hit Play First", "Error!");
                }
            }
        }

        private void selALL_Click(object sender, EventArgs e)
        {
            try
            {
                if (Done.Text != "Start")
                {
                    N = cal_correctword();
                    if (N == 0)
                    {
                        for (int i = 0; i < 12; i++)
                        {
                            if (word12[i] != -1)
                            {
                                word12[i] = 1;
                            }
                            else break;
                        }
                        if (word12[0] == 1)
                        {
                            s1.BackColor = Color.MediumVioletRed;
                        }
                        if (word12[1] == 1)
                        {
                            s2.BackColor = Color.MediumVioletRed;
                        }
                        if (word12[2] == 1)
                        {
                            s3.BackColor = Color.MediumVioletRed;
                        }
                        if (word12[3] == 1)
                        {
                            s4.BackColor = Color.MediumVioletRed;
                        }
                        if (word12[4] == 1)
                        {
                            s5.BackColor = Color.MediumVioletRed;
                        }
                        if (word12[5] == 1)
                        {
                            s6.BackColor = Color.MediumVioletRed;
                        }
                        if (word12[6] == 1)
                        {
                            s7.BackColor = Color.MediumVioletRed;
                        }
                        if (word12[7] == 1)
                        {
                            s8.BackColor = Color.MediumVioletRed;
                        }
                        if (word12[8] == 1)
                        {
                            s9.BackColor = Color.MediumVioletRed;
                        }
                        if (word12[9] == 1)
                        {
                            s10.BackColor = Color.MediumVioletRed;
                        }
                        if (word12[10] == 1)
                        {
                            s11.BackColor = Color.MediumVioletRed;
                        }
                        if (word12[11] == 1)
                        {
                            s12.BackColor = Color.MediumVioletRed;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 12; i++)
                        {
                            if (word12[i] != -1)
                            {
                                word12[i] = 0;
                            }
                            else break;
                        }
                        s1.BackColor = DefaultBackColor;
                        s2.BackColor = DefaultBackColor;
                        s3.BackColor = DefaultBackColor;
                        s4.BackColor = DefaultBackColor;
                        s5.BackColor = DefaultBackColor;
                        s6.BackColor = DefaultBackColor;
                        s7.BackColor = DefaultBackColor;
                        s8.BackColor = DefaultBackColor;
                        s9.BackColor = DefaultBackColor;
                        s10.BackColor = DefaultBackColor;
                        s11.BackColor = DefaultBackColor;
                        s12.BackColor = DefaultBackColor;
                    }
                    N = cal_correctword();
                    string text = @"N = " + N.ToString();
                    Done.Text = text;
                }
            }
            catch
            {
            }
        }
        int cal_correctword()
        {
            int result = 0;
            for (int i = 0; i < 12; i++)
            {
                if (word12[i] != -1)
                {
                    result = result + word12[i];
                }
                else
                    break;
            }
            return result;
        }

        private void s1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Done.Text != "Start")
                {
                    if (word12[0] != -1)
                    {
                        word12[0] ^= 1;

                        if (word12[0] % 2 == 1) // the phoneme is selected
                        {
                            s1.BackColor = Color.MediumVioletRed;
                        }
                        else
                        {
                            s1.BackColor = DefaultBackColor;
                        }
                    }
                    N = cal_correctword();
                    string text = @"N = " + N.ToString();
                    Done.Text = text;
                }
            }
            catch { }
            
        }

        private void s2_Click(object sender, EventArgs e)
        {
            try
            {
                if (Done.Text != "Start")
                {
                    if (word12[1] != -1)
                    {
                        word12[1] ^= 1;

                        if (word12[1] % 2 == 1) // the phoneme is selected
                        {
                            s2.BackColor = Color.MediumVioletRed;
                        }
                        else
                        {
                            s2.BackColor = DefaultBackColor;
                        }
                    }
                    N = cal_correctword();
                    string text = @"N = " + N.ToString();
                    Done.Text = text;
                }
            }
            catch { }
            
        }

        private void s3_Click(object sender, EventArgs e)
        {
            try
            {
                if (Done.Text != "Start")
                {
                    if (word12[2] != -1)
                    {
                        word12[2] ^= 1;

                        if (word12[2] % 2 == 1) // the phoneme is selected
                        {
                            s3.BackColor = Color.MediumVioletRed;
                        }
                        else
                        {
                            s3.BackColor = DefaultBackColor;
                        }
                    }
                    N = cal_correctword();
                    string text = @"N = " + N.ToString();
                    Done.Text = text;
                }
            }
            catch { }
        }

        private void s4_Click(object sender, EventArgs e)
        {
            try
            {
                if (Done.Text != "Start")
                {
                    if (word12[3] != -1)
                    {
                        word12[3] ^= 1;

                        if (word12[3] % 2 == 1) // the phoneme is selected
                        {
                            s4.BackColor = Color.MediumVioletRed;
                        }
                        else
                        {
                            s4.BackColor = DefaultBackColor;
                        }
                    }
                    N = cal_correctword();
                    string text = @"N = " + N.ToString();
                    Done.Text = text;
                }
            }
            catch { }
        }

        private void s5_Click(object sender, EventArgs e)
        {
            try {
                if (Done.Text != "Start")
                {
                    if (word12[4] != -1)
                    {
                        word12[4] ^= 1;

                        if (word12[4] % 2 == 1) // the phoneme is selected
                        {
                            s5.BackColor = Color.MediumVioletRed;
                        }
                        else
                        {
                            s5.BackColor = DefaultBackColor;
                        }
                    }
                    N = cal_correctword();
                    string text = @"N = " + N.ToString();
                    Done.Text = text;
                }
            }
            catch { }
            
        }

        private void s6_Click(object sender, EventArgs e)
        {
            try
            {
                if (Done.Text != "Start")
                {
                    if (word12[5] != -1)
                    {
                        word12[5] ^= 1;

                        if (word12[5] % 2 == 1) // the phoneme is selected
                        {
                            s6.BackColor = Color.MediumVioletRed;
                        }
                        else
                        {
                            s6.BackColor = DefaultBackColor;
                        }
                    }
                    N = cal_correctword();
                    string text = @"N = " + N.ToString();
                    Done.Text = text;
                }
            }
            catch
            {
            }
        }

        private void s7_Click(object sender, EventArgs e)
        {
            try
            {
                if (Done.Text != "Start")
                {
                    if (word12[6] != -1)
                    {
                        word12[6] ^= 1;

                        if (word12[6] % 2 == 1) // the phoneme is selected
                        {
                            s7.BackColor = Color.MediumVioletRed;
                        }
                        else
                        {
                            s7.BackColor = DefaultBackColor;
                        }
                    }
                    N = cal_correctword();
                    string text = @"N = " + N.ToString();
                    Done.Text = text;
                }
            }
            catch { }
           
        }

        private void s8_Click(object sender, EventArgs e)
        {
            try
            {
                if (Done.Text != "Start")
                {
                    if (word12[7] != -1)
                    {
                        word12[7] ^= 1;

                        if (word12[7] % 2 == 1) // the phoneme is selected
                        {
                            s8.BackColor = Color.MediumVioletRed;
                        }
                        else
                        {
                            s8.BackColor = DefaultBackColor;
                        }
                    }
                    N = cal_correctword();
                    string text = @"N = " + N.ToString();
                    Done.Text = text;
                }
            }
            catch { }
            
        }

        private void s9_Click(object sender, EventArgs e)
        {
            try
            {
                if (Done.Text != "Start")
                {
                    if (word12[8] != -1)
                    {
                        word12[8] ^= 1;

                        if (word12[8] % 2 == 1) // the phoneme is selected
                        {
                            s9.BackColor = Color.MediumVioletRed;
                        }
                        else
                        {
                            s9.BackColor = DefaultBackColor;
                        }
                    }
                    N = cal_correctword();
                    string text = @"N = " + N.ToString();
                    Done.Text = text;
                }
            }
            catch { }
         
        }

        private void s10_Click(object sender, EventArgs e)
        {
            try
            {
                if (Done.Text != "Start")
                {
                    if (word12[9] != -1)
                    {
                        word12[9] ^= 1;

                        if (word12[9] % 2 == 1) // the phoneme is selected
                        {
                            s10.BackColor = Color.MediumVioletRed;
                        }
                        else
                        {
                            s10.BackColor = DefaultBackColor;
                        }
                    }
                    N = cal_correctword();
                    string text = @"N = " + N.ToString();
                    Done.Text = text;
                }
        }
            catch { }
        }
           
           

        private void s11_Click(object sender, EventArgs e)
        {
            try
            {
                if (Done.Text != "Start")
                {
                    if (word12[10] != -1)
                    {
                        word12[10] ^= 1;

                        if (word12[10] % 2 == 1) // the phoneme is selected
                        {
                            s11.BackColor = Color.MediumVioletRed;
                        }
                        else
                        {
                            s11.BackColor = DefaultBackColor;
                        }
                    }
                    N = cal_correctword();
                    string text = @"N = " + N.ToString();
                    Done.Text = text;
                }
            }
            catch { }
           
        }

        private void s12_Click(object sender, EventArgs e)
        {
            try
            {
                if (Done.Text != "Start")
                {
                    if (word12[11] != -1)
                    {
                        word12[11] ^= 1;

                        if (word12[11] % 2 == 1) // the phoneme is selected
                        {
                            s12.BackColor = Color.MediumVioletRed;
                        }
                        else
                        {
                            s12.BackColor = DefaultBackColor;
                        }
                    }
                    N = cal_correctword();
                    string text = @"N = " + N.ToString();
                    Done.Text = text;
                }
            }
            catch { }
           
        }
       

        private void button6_Click(object sender, EventArgs e)
        {
            if (cnc_button_showHis.Text == "Show History")
            {
                cnc_button_showHis.Text = "Hide History";
                cnc_groupHis.Visible = true;
                cnc_groupHis.Anchor = AnchorStyles.Top;
                cnc_groupPlaying.Anchor = AnchorStyles.Top;
                

            }
            else
            {

                cnc_button_showHis.Text = "Show History";
                cnc_groupHis.Visible = false;
                cnc_groupPlaying.Anchor = AnchorStyles.None;
            }
        }

        private void az_his_Click(object sender, EventArgs e)
        {
            if (az_his.Text == "Show History")
            {
                az_his.Text = "Hide History";
                az_group_his.Visible = true;
                displayAzHistory(click_az - 2);
            }
            else
            {

                az_his.Text = "Show History";
                az_group_his.Visible = false;
                
            }
        }

        private void CleanAZmainMenu()
        {
            list_sel_az.Text = string.Empty;
            az_comboLeft.Text = string.Empty;
            az_comboRight.Text = string.Empty;
        }

    
        private void CleanAZnewTest()
        {
            s1.Text = string.Empty;
            s2.Text = string.Empty;
            s3.Text = string.Empty;
            s4.Text = string.Empty;
            s5.Text = string.Empty;
            s6.Text = string.Empty;
            s7.Text = string.Empty;
            s8.Text = string.Empty;
            s9.Text = string.Empty;
            s10.Text = string.Empty;
            s11.Text = string.Empty;
            s12.Text = string.Empty;

            s1.BackColor = DefaultBackColor;
            s2.BackColor = DefaultBackColor;
            s3.BackColor = DefaultBackColor;
            s4.BackColor = DefaultBackColor;
            s5.BackColor = DefaultBackColor;
            s6.BackColor = DefaultBackColor;
            s7.BackColor = DefaultBackColor;
            s8.BackColor = DefaultBackColor;
            s9.BackColor = DefaultBackColor;
            s10.BackColor = DefaultBackColor;
            s11.BackColor = DefaultBackColor;
            s12.BackColor = DefaultBackColor;
            result_1sen_num.Text ="0/0";
            result_1sen_percent.Text = "0%";
            Done.Text = "Start";
        }

        private void CleanAZHisTest()
        {
            az_his1.Text = "#0";
            az_his2.Text = "#0";
            az_his3.Text = "#0";

            az_his1_1.Text = string.Empty;
            az_his1_1.BackColor = DefaultBackColor;
            az_his1_2.Text = string.Empty;
            az_his1_2.BackColor = DefaultBackColor;
            az_his1_3.Text = string.Empty;
            az_his1_3.BackColor = DefaultBackColor;
            az_his1_4.Text = string.Empty;
            az_his1_4.BackColor = DefaultBackColor;
            az_his1_5.Text = string.Empty;
            az_his1_5.BackColor = DefaultBackColor;
            az_his1_6.Text = string.Empty;
            az_his1_6.BackColor = DefaultBackColor;
            az_his1_7.Text = string.Empty;
            az_his1_7.BackColor = DefaultBackColor;
            az_his1_8.Text = string.Empty;
            az_his1_8.BackColor = DefaultBackColor;
            az_his1_9.Text = string.Empty;
            az_his1_9.BackColor = DefaultBackColor;
            az_his1_10.Text = string.Empty;
            az_his1_10.BackColor = DefaultBackColor;
            az_his1_11.Text = string.Empty;
            az_his1_11.BackColor = DefaultBackColor;
            button56.Text = string.Empty;
            button56.BackColor = DefaultBackColor;

            az_his2_1.Text = string.Empty;
            az_his2_1.BackColor = DefaultBackColor;
            az_his2_2.Text = string.Empty;
            az_his2_2.BackColor = DefaultBackColor;
            az_his2_3.Text = string.Empty;
            az_his2_3.BackColor = DefaultBackColor;
            az_his2_4.Text = string.Empty;
            az_his2_4.BackColor = DefaultBackColor;
            az_his2_5.Text = string.Empty;
            az_his2_5.BackColor = DefaultBackColor;
            az_his2_6.Text = string.Empty;
            az_his2_6.BackColor = DefaultBackColor;
            az_his2_7.Text = string.Empty;
            az_his2_7.BackColor = DefaultBackColor;
            az_his2_8.Text = string.Empty;
            az_his2_8.BackColor = DefaultBackColor;
            az_his2_9.Text = string.Empty;
            az_his2_9.BackColor = DefaultBackColor;
            az_his2_10.Text = string.Empty;
            az_his2_10.BackColor = DefaultBackColor;
            az_his2_11.Text = string.Empty;
            az_his2_11.BackColor = DefaultBackColor;
            az_his2_12.Text = string.Empty;
            az_his2_12.BackColor = DefaultBackColor;

            az_his3_1.Text = string.Empty;
            az_his3_1.BackColor = DefaultBackColor;
            az_his3_2.Text = string.Empty;
            az_his3_2.BackColor = DefaultBackColor;
            az_his3_3.Text = string.Empty;
            az_his3_3.BackColor = DefaultBackColor;
            az_his3_4.Text = string.Empty;
            az_his3_4.BackColor = DefaultBackColor;
            az_his3_5.Text = string.Empty;
            az_his3_5.BackColor = DefaultBackColor;
            az_his3_6.Text = string.Empty;
            az_his3_6.BackColor = DefaultBackColor;
            az_his3_7.Text = string.Empty;
            az_his3_7.BackColor = DefaultBackColor;
            az_his3_8.Text = string.Empty;
            az_his3_8.BackColor = DefaultBackColor;
            az_his3_9.Text = string.Empty;
            az_his3_9.BackColor = DefaultBackColor;
            az_his3_10.Text = string.Empty;
            az_his3_10.BackColor = DefaultBackColor;
            az_his3_11.Text = string.Empty;
            az_his3_11.BackColor = DefaultBackColor;
            az_his3_12.Text = string.Empty;
            az_his3_12.BackColor = DefaultBackColor;
        }

        //AzBio History button
        private void update_azscore()
        {
            int count1 = 0;
            int count0 = 0;
            foreach (DataRow dr in tb.dt_score.Rows)
            {
                for (int i = 1; i < 12; i++)
                {
                    if (Convert.ToInt16(dr[i].ToString()) == 1)
                    {
                        count1++;
                    }

                    else if (Convert.ToInt16(dr[i].ToString()) == 0)
                    {
                        count0++;
                    }
                }

            }
            N_totalcorrect = count1;
            N_total = count1 + count0;
            result_1sen_num.Text = N_totalcorrect.ToString() + "/" + N_total.ToString();
            int percent = N_totalcorrect * 100 / N_total;
            result_1sen_percent.Text = percent.ToString() + "%";

    
        }



       


        /// <summary>
        /// ===================CNC testing================================================

        public Int16[] Word_order = new Int16[50];
        public Int16 Word_playing = 0;
        public Int16 click_CNC = 0;
        public int startCNC = 0;
        public SoundPlayer CNCPlayer;

        public Int16 phon1_hit = 0;     //indicate the phoneme is selected.
        public Int16 phon2_hit = 0;
        public Int16 phon3_hit = 0;
        public Int16 extra_hit = 0;

        public Int16 CNCphonCorrect = 0;
        public Int16 CNCwordCorrect = 0;


        private void button3_Click(object sender, EventArgs e)      // Button Start Testing
        {
            bool OK = true;

            if (CNCleftEar.Text == "" || CNCrightEar.Text == "" | CNCListNumber.Text == "")
            {
                OK = false;
                MessageBox.Show("Must fill out all options", "Error");
            }


            if (OK)
            {

                CleanCNCnewTest();
                CleanCNChistory();

                tabTest.SelectedTab = tabCNCAudio;


                if (CNCRandomize.Checked == true)
                {
                    Word_order = CreateListOrder(0, 49);
                }
                else if (CNCRandomize.Checked == false)
                {
                    for (int i = 0; i < 50; i++)
                    {
                        Word_order[i] = Convert.ToInt16(i);
                    }
                }

                textBox8.Text = CNCListNumber.SelectedItem.ToString();

                textBox9.Text = "0";

                tb.create_table_CNC();
                tb.create_table_score_CNC();

                click_CNC = 0;
            }

       }


        private void CleanCNCnewTest()
        {
            Word_playing = 0;
            click_CNC = 0;
            startCNC = 0;
            CNCPlayer = null;
            phon1_hit = 0;
            phon2_hit = 0;
            phon3_hit = 0;
            CNCphonCorrect = 0;
            CNCwordCorrect = 0;

            CNCprevious.Visible = false;
            CNCnext.Visible = false;


            CNCstart.Text = "Start";

            cnc_groupPlaying.Enabled = true;


            phon1.Text = string.Empty;
            phon1.BackColor = DefaultBackColor;
            phon2.Text = string.Empty;
            phon2.BackColor = DefaultBackColor;
            phon3.Text = string.Empty;
            phon3.BackColor = DefaultBackColor;
            CNCExtra.BackColor = DefaultBackColor;

            labelCNCwordCorr.Text = "0/0";
            labelCNCwordCorrPercent.Text = "0%";

            labelCNCphonCorr.Text = "0/0";
            labelCNCphonCorrPercent.Text = "0%";

            CNChis1.Text = "#0";
            CNChis2.Text = "#0";
            CNChis3.Text = "#0";
            CNChis4.Text = "#0";
            CNChis5.Text = "#0";

            CNChis1_1.Text = string.Empty;
            CNChis1_2.Text = string.Empty;
            CNChis1_3.Text = string.Empty;
            CNChis1_1.BackColor = DefaultBackColor;
            CNChis1_2.BackColor = DefaultBackColor;
            CNChis1_3.BackColor = DefaultBackColor;
            CNChis1_extra.BackColor = DefaultBackColor;

            CNChis2_1.Text = string.Empty;
            CNChis2_2.Text = string.Empty;
            CNChis2_3.Text = string.Empty;
            CNChis2_1.BackColor = DefaultBackColor;
            CNChis2_2.BackColor = DefaultBackColor;
            CNChis2_3.BackColor = DefaultBackColor;
            CNChis2_extra.BackColor = DefaultBackColor;

            CNChis3_1.Text = string.Empty;
            CNChis3_2.Text = string.Empty;
            CNChis3_3.Text = string.Empty;
            CNChis3_1.BackColor = DefaultBackColor;
            CNChis3_2.BackColor = DefaultBackColor;
            CNChis3_3.BackColor = DefaultBackColor;
            CNChis3_extra.BackColor = DefaultBackColor;

            CNChis4_1.Text = string.Empty;
            CNChis4_2.Text = string.Empty;
            CNChis4_3.Text = string.Empty;
            CNChis4_1.BackColor = DefaultBackColor;
            CNChis4_2.BackColor = DefaultBackColor;
            CNChis4_3.BackColor = DefaultBackColor;
            CNChis4_extra.BackColor = DefaultBackColor;

            CNChis5_1.Text = string.Empty;
            CNChis5_2.Text = string.Empty;
            CNChis5_3.Text = string.Empty;
            CNChis5_1.BackColor = DefaultBackColor;
            CNChis5_2.BackColor = DefaultBackColor;
            CNChis5_3.BackColor = DefaultBackColor;
            CNChis5_extra.BackColor = DefaultBackColor;

        }

        private void CleanCNCmainMenu()
        {
            CNCleftEar.Text = string.Empty;
            CNCrightEar.Text = string.Empty;

            CNCListNumber.Text = string.Empty;
        }

        private void CleanCNChistory()
        {
            switch (CNChis1.Text)
            {
                case "#0":
                    CNChis1_1.Enabled = false;
                    CNChis1_2.Enabled = false;
                    CNChis1_3.Enabled = false;
                    CNChis1_extra.Enabled = false; 
                    break;
                default:
                    CNChis1_1.Enabled = true;
                    CNChis1_2.Enabled = true;
                    CNChis1_3.Enabled = true;
                    CNChis1_extra.Enabled = true; break;
            }

            switch (CNChis2.Text)
            {
                case "#0":
                    CNChis2_1.Enabled = false;
                    CNChis2_2.Enabled = false;
                    CNChis2_3.Enabled = false;
                    CNChis2_extra.Enabled = false;
                    break;
                default:
                    CNChis2_1.Enabled = true;
                    CNChis2_2.Enabled = true;
                    CNChis2_3.Enabled = true;
                    CNChis2_extra.Enabled = true; break;
            }

            switch (CNChis3.Text)
            {
                case "#0":
                    CNChis3_1.Enabled = false;
                    CNChis3_2.Enabled = false;
                    CNChis3_3.Enabled = false;
                    CNChis3_extra.Enabled = false;
                    break;
                default:
                    CNChis3_1.Enabled = true;
                    CNChis3_2.Enabled = true;
                    CNChis3_3.Enabled = true;
                    CNChis3_extra.Enabled = true; break;
            }

            switch (CNChis4.Text)
            {
                case "#0":
                    CNChis4_1.Enabled = false;
                    CNChis4_2.Enabled = false;
                    CNChis4_3.Enabled = false;
                    CNChis4_extra.Enabled = false;
                    break;
                default:
                    CNChis4_1.Enabled = true;
                    CNChis4_2.Enabled = true;
                    CNChis4_3.Enabled = true;
                    CNChis4_extra.Enabled = true; break;
            }

            switch (CNChis5.Text)
            {
                case "#0":
                    CNChis5_1.Enabled = false;
                    CNChis5_2.Enabled = false;
                    CNChis5_3.Enabled = false;
                    CNChis5_extra.Enabled = false;
                    break;
                default:
                    CNChis5_1.Enabled = true;
                    CNChis5_2.Enabled = true;
                    CNChis5_3.Enabled = true;
                    CNChis5_extra.Enabled = true; break;
            }
        }


        #region Called Functions: CreateListOrder, SplitWord, SplitWav, displayCNCHistory, updateScoreCNC

        public Int16[] CreateListOrder(int start, int end)
        {
            if (end < start)
            {
                throw new ArgumentException("Faulty parameter(s) passed: lower bound cannot be less than upper bound.");
            }
            List<int> returnList = new List<int>(end - start + 1);
            for (int i = start; i <= end; i++)
            {
                returnList.Add(i);
            }

            List<int> L = returnList;

            int No_item = end - start + 1;

            Int16[] word_or = new Int16[No_item];
            Random rd = new Random();

            for (int i = 0; i < No_item; i++)
            {
                int x = rd.Next(0, L.Count());
                word_or[i] = Convert.ToInt16(L[x]);
                L.RemoveAt(x);
            }
            return word_or;
        }

        private void SplitWord(Int16 list, Int16 sentnum)
        {
            //int counter = 0;
            list = Convert.ToInt16(list + 26);
            string line;
            // Read the file and display it line by line.

           // System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\Han Le\Dropbox\Winter2014\SoiProjs\Capstone\Han\MSTB_CD\Track" + list + ".txt");

            //System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\Ted\Dropbox\Soi's Projs\Capstone\Han\MSTB_CD\Track" + list + ".txt");
            
            System.IO.StreamReader file = new System.IO.StreamReader(Path.Combine(Environment.CurrentDirectory, @"Source\cnc\text\Track") + list + ".txt");
            //Console.Write(list);

            line = file.ReadLine();     // ignore the first line in the text file
            //Console.WriteLine(line);

            while ((line = file.ReadLine()) != null)
            {
                string[] fullsentence = { " ", " ", " ", " " };
                string[] word = line.Split(' ');
                //Console.WriteLine(word);

                if (Convert.ToInt16(word[0]) == sentnum)
                {
                    phon1.Text = word[1];
                    phon2.Text = word[2];
                    phon3.Text = word[3];
                    //Console.WriteLine(line);
                    //counter++;

                    string[] temp = new string[3];
                    temp[0] = word[1];
                    temp[1] = word[2];
                    temp[2] = word[3];
                    tb.add_row_CNC(click_CNC, temp);

                    break;
                }
            }
        }

        private void SplitWav(string FileName, Int32 position)
        {
            byte[] wav = File.ReadAllBytes(FileName);

            var header = new byte[44];

            for (int i = 0; i < 44; i++)
            {
                header[i] = wav[i];
            }

            /////length of data(520000) + 36
            header[4] = 100;
            header[5] = 239;
            header[6] = 7;
            header[7] = 0;

            ////length of data = 520000
            header[40] = 64;
            header[41] = 239;
            header[42] = 7;
            header[43] = 0;

            Int32 wordLength = 520000;

            //Int32 position = 2841776;

            var word = new byte[wordLength];

            for (Int32 i = 0; i < wordLength; i++)
            {
                word[i] = wav[position + i];
            }

            var wavfile = new byte[Convert.ToInt32(header.Length + word.Length)];
            header.CopyTo(wavfile, 0);
            word.CopyTo(wavfile, header.Length);

            ////play sound from Byte array
            MemoryStream ms = new MemoryStream(wavfile);

            CNCPlayer = new SoundPlayer(ms);

            CNCPlayer.Play();

        }

        //---------Display history
        private void displayCNCHistory(int i)
        {
            //1st line
            if (cnc_button_showHis.Text == "Hide History")
            {
                try
                {

                    CNChis1_1.Text = tb.CNCdt.Rows[i][1].ToString();
                    CNChis1_2.Text = tb.CNCdt.Rows[i][2].ToString();
                    CNChis1_3.Text = tb.CNCdt.Rows[i][3].ToString();
                    CNChis1.Text = "#" + (i + 1).ToString();

                    //Color
                    if (Convert.ToInt16(tb.CNCdt_score.Rows[i][1].ToString()) == 1)
                        CNChis1_1.BackColor = Color.MediumVioletRed;
                    else
                        CNChis1_1.BackColor = DefaultBackColor;


                    if (Convert.ToInt16(tb.CNCdt_score.Rows[i][2].ToString()) == 1)
                        CNChis1_2.BackColor = Color.MediumVioletRed;
                    else
                        CNChis1_2.BackColor = DefaultBackColor;


                    if (Convert.ToInt16(tb.CNCdt_score.Rows[i][3].ToString()) == 1)
                        CNChis1_3.BackColor = Color.MediumVioletRed;
                    else
                        CNChis1_3.BackColor = DefaultBackColor;

                    if (Convert.ToInt16(tb.CNCdt_score.Rows[i][4].ToString()) == 1)
                        CNChis1_extra.BackColor = Color.MediumBlue;
                    else
                        CNChis1_extra.BackColor = DefaultBackColor;

                }
                catch { }

                //2nd line
                try
                {

                    CNChis2_1.Text = tb.CNCdt.Rows[i - 1][1].ToString();
                    CNChis2_2.Text = tb.CNCdt.Rows[i - 1][2].ToString();
                    CNChis2_3.Text = tb.CNCdt.Rows[i - 1][3].ToString();
                    CNChis2.Text = "#" + (i).ToString();

                    //Color
                    if (Convert.ToInt16(tb.CNCdt_score.Rows[i - 1][1].ToString()) == 1)
                        CNChis2_1.BackColor = Color.MediumVioletRed;
                    else
                        CNChis2_1.BackColor = DefaultBackColor;


                    if (Convert.ToInt16(tb.CNCdt_score.Rows[i - 1][2].ToString()) == 1)
                        CNChis2_2.BackColor = Color.MediumVioletRed;
                    else
                        CNChis2_2.BackColor = DefaultBackColor;


                    if (Convert.ToInt16(tb.CNCdt_score.Rows[i - 1][3].ToString()) == 1)
                        CNChis2_3.BackColor = Color.MediumVioletRed;
                    else
                        CNChis2_3.BackColor = DefaultBackColor;


                    if (Convert.ToInt16(tb.CNCdt_score.Rows[i - 1][4].ToString()) == 1)
                        CNChis2_extra.BackColor = Color.MediumBlue;
                    else
                        CNChis2_extra.BackColor = DefaultBackColor;
                }
                catch { }

                //3nd line
                try
                {

                    CNChis3_1.Text = tb.CNCdt.Rows[i - 2][1].ToString();
                    CNChis3_2.Text = tb.CNCdt.Rows[i - 2][2].ToString();
                    CNChis3_3.Text = tb.CNCdt.Rows[i - 2][3].ToString();
                    CNChis3.Text = "#" + (i - 1).ToString();
                    //Color
                    if (Convert.ToInt16(tb.CNCdt_score.Rows[i - 2][1].ToString()) == 1)
                        CNChis3_1.BackColor = Color.MediumVioletRed;
                    else
                        CNChis3_1.BackColor = DefaultBackColor;


                    if (Convert.ToInt16(tb.CNCdt_score.Rows[i - 2][2].ToString()) == 1)
                        CNChis3_2.BackColor = Color.MediumVioletRed;
                    else
                        CNChis3_2.BackColor = DefaultBackColor;


                    if (Convert.ToInt16(tb.CNCdt_score.Rows[i - 2][3].ToString()) == 1)
                        CNChis3_3.BackColor = Color.MediumVioletRed;
                    else
                        CNChis3_3.BackColor = DefaultBackColor;

                    if (Convert.ToInt16(tb.CNCdt_score.Rows[i - 2][4].ToString()) == 1)
                        CNChis3_extra.BackColor = Color.MediumBlue;
                    else
                        CNChis3_extra.BackColor = DefaultBackColor;
                }
                catch { }

                // line 4
                try
                {

                    CNChis4_1.Text = tb.CNCdt.Rows[i - 3][1].ToString();
                    CNChis4_2.Text = tb.CNCdt.Rows[i - 3][2].ToString();
                    CNChis4_3.Text = tb.CNCdt.Rows[i - 3][3].ToString();
                    CNChis4.Text = "#" + (i - 2).ToString();
                    //Color
                    if (Convert.ToInt16(tb.CNCdt_score.Rows[i - 3][1].ToString()) == 1)
                        CNChis4_1.BackColor = Color.MediumVioletRed;
                    else
                        CNChis4_1.BackColor = DefaultBackColor;


                    if (Convert.ToInt16(tb.CNCdt_score.Rows[i - 3][2].ToString()) == 1)
                        CNChis4_2.BackColor = Color.MediumVioletRed;
                    else
                        CNChis4_2.BackColor = DefaultBackColor;


                    if (Convert.ToInt16(tb.CNCdt_score.Rows[i - 3][3].ToString()) == 1)
                        CNChis4_3.BackColor = Color.MediumVioletRed;
                    else
                        CNChis4_3.BackColor = DefaultBackColor;

                    if (Convert.ToInt16(tb.CNCdt_score.Rows[i - 3][4].ToString()) == 1)
                        CNChis4_extra.BackColor = Color.MediumBlue;
                    else
                        CNChis4_extra.BackColor = DefaultBackColor;
                }
                catch { }



                // line 5

                try
                {

                    CNChis5_1.Text = tb.CNCdt.Rows[i - 4][1].ToString();
                    CNChis5_2.Text = tb.CNCdt.Rows[i - 4][2].ToString();
                    CNChis5_3.Text = tb.CNCdt.Rows[i - 4][3].ToString();
                    CNChis5.Text = "#" + (i - 3).ToString();
                    //Color
                    if (Convert.ToInt16(tb.CNCdt_score.Rows[i - 4][1].ToString()) == 1)
                        CNChis5_1.BackColor = Color.MediumVioletRed;
                    else
                        CNChis5_1.BackColor = DefaultBackColor;


                    if (Convert.ToInt16(tb.CNCdt_score.Rows[i - 4][2].ToString()) == 1)
                        CNChis5_2.BackColor = Color.MediumVioletRed;
                    else
                        CNChis5_2.BackColor = DefaultBackColor;


                    if (Convert.ToInt16(tb.CNCdt_score.Rows[i - 4][3].ToString()) == 1)
                        CNChis5_3.BackColor = Color.MediumVioletRed;
                    else
                        CNChis5_3.BackColor = DefaultBackColor;

                    if (Convert.ToInt16(tb.CNCdt_score.Rows[i - 4][4].ToString()) == 1)
                        CNChis5_extra.BackColor = Color.MediumBlue;
                    else
                        CNChis5_extra.BackColor = DefaultBackColor;
                }
                catch { }

            }
        }

        public void updateScoreCNC(int order, int posi)
        {
            // order is the index of row in the table, posi indicates phonemem 1, 2, 3 or extra (4) which one is clicked to change

            int[] Oldscore = new int[4];
            Oldscore[0] = Convert.ToInt16(tb.CNCdt_score.Rows[order][1]);
            Oldscore[1] = Convert.ToInt16(tb.CNCdt_score.Rows[order][2]);
            Oldscore[2] = Convert.ToInt16(tb.CNCdt_score.Rows[order][3]);
            Oldscore[3] = Convert.ToInt16(tb.CNCdt_score.Rows[order][4]);


            int[] Newscore = new int[4];
            for (int i = 0; i < 4; i++)
            {
                Newscore[i] = Oldscore[i];
            }

            Newscore[posi - 1] ^= 1;

            //Console.Write(Newscore[posi - 1]);
            //Console.Write("\n");

            // update table and color 

            tb.CNCdt_score.Rows[order][posi] = Newscore[posi - 1];

            // update score

            int temp = Convert.ToInt16(-Oldscore[0] - Oldscore[1] - Oldscore[2] + Newscore[0] + Newscore[1] + Newscore[2]);

            //Console.Write("OldScore[]: ");
            //Console.Write(Oldscore[posi - 1]);
            //Console.Write("\n");

            //Console.Write("newscore[0]: ");
            //Console.Write(Newscore[posi - 1]);
            //Console.Write("\n");

            CNCphonCorrect = Convert.ToInt16(CNCphonCorrect - Oldscore[0] - Oldscore[1] - Oldscore[2] + Newscore[0] + Newscore[1] + Newscore[2]);

            if (Oldscore[3] == 0 && Newscore[3] == 1)
            {
                CNCwordCorrect--;
            }
            if (Oldscore[3] == 1 && Newscore[3] == 0)
            {
                CNCwordCorrect++;
            }


            if (Oldscore[0] + Oldscore[1] + Oldscore[2] == 3 && Newscore[0] + Newscore[1] + Newscore[2] < 3 && Oldscore[3] == Newscore[3])
            {
                CNCwordCorrect--;
            }

            if (Oldscore[0] + Oldscore[1] + Oldscore[2] < 3 && Newscore[0] + Newscore[1] + Newscore[2] == 3 && Oldscore[3] == Newscore[3])
            {
                CNCwordCorrect++;
            }

            int totalWord = tb.CNCdt_score.Rows.Count;

            //Console.Write(CNCwordCorrect);
            //Console.Write("\n");
            //Console.Write(CNCphonCorrect);
            //Console.Write("\n");

            labelCNCwordCorr.Text = @Convert.ToString(CNCwordCorrect) + "/" + @Convert.ToString(totalWord);
            labelCNCwordCorrPercent.Text = @Convert.ToString(100 * CNCwordCorrect / (totalWord)) + "%";


            labelCNCphonCorr.Text = @Convert.ToString(CNCphonCorrect) + "/" + @Convert.ToString(totalWord * 3);
            labelCNCphonCorrPercent.Text = @Convert.ToString(100 * CNCphonCorrect / (totalWord * 3)) + "%";

        }

        #endregion Called Functions: CreateListOrder, SplitWord, SplitWav, displayCNCHistory, updateScoreCNC


        #region Control buttons: Repeat, All, Start/Done

        private void button11_Click(object sender, EventArgs e)             // button Start/Done
        {
            if (click_CNC > 0)
            {
                if (phon1_hit + phon2_hit + phon3_hit == 3 && extra_hit != 1)
                {
                    CNCwordCorrect += 1;
                }
                labelCNCwordCorr.Text = @Convert.ToString(CNCwordCorrect) + "/" + @Convert.ToString(click_CNC);
                labelCNCwordCorrPercent.Text = @Convert.ToString(100 * CNCwordCorrect / (click_CNC)) + "%";


                CNCphonCorrect += Convert.ToInt16(phon1_hit + phon2_hit + phon3_hit);
                labelCNCphonCorr.Text = @Convert.ToString(CNCphonCorrect) + "/" + @Convert.ToString(click_CNC * 3);
                labelCNCphonCorrPercent.Text = @Convert.ToString(100 * CNCphonCorrect / (click_CNC * 3)) + "%";

                /// save data to table
                /// 

                int[] temp = new int[4];
                temp[0] = phon1_hit;
                temp[1] = phon2_hit;
                temp[2] = phon3_hit;
                temp[3] = extra_hit;

                tb.add_score_row_CNC(click_CNC - 1, temp);

                //dataGridView6.DataSource = tb.CNCdt;

                //dataGridView8.DataSource = tb.CNCdt_score;

                displayCNCHistory(click_CNC - 1);

                if (click_CNC < 6)
                    CleanCNChistory();


                if (click_CNC == 50)
                {
                    click_CNC = 51;             // for view history latter.

                    CNCprevious.Visible = true;
                    CNCnext.Visible = true;
                    CNCnext.Enabled = false;

                    cnc_groupPlaying.Enabled = false;
                }


            }
            //setup for phonemes button.

            if (click_CNC < 50)
            {
                phon1.BackColor = DefaultBackColor;
                phon2.BackColor = DefaultBackColor;
                phon3.BackColor = DefaultBackColor;
                CNCExtra.BackColor = DefaultBackColor;

                startCNC++;

                CNCstart.Text = "Done";

                Int16 list = Convert.ToInt16(CNCListNumber.SelectedItem.ToString());


                textBox8.Text = CNCListNumber.SelectedItem.ToString();

                //Console.Write(Word_order[click_CNC]);
                //Console.Write("\n");

                //textBox9.Text = Convert.ToString(Word_order[click_CNC] + 1);  // display the current word which is playing

                textBox9.Text = Convert.ToString(click_CNC + 1);  // display the current word which is playing

                //click_CNC = Convert.ToInt16(Text2.Text);

                //click_CNC--;
                //Path.Combine(Environment.CurrentDirectory, @"Source\azbio\text\");
                string FileName = Path.Combine(Environment.CurrentDirectory, @"Source\cnc\audio\Track") + (26 + list) + ".wav";
               // string FileName = @"C:\Users\Han Le\Dropbox\Winter2014\SoiProjs\Capstone\Han\MSTB_CD\Track" + (26 + list) + ".wav";

                string CNCfilepath = Path.Combine(Environment.CurrentDirectory, @"Source\cnc\text\CNCdata.csv");
                //string CNCfilepath = @"C:\Users\Han Le\Dropbox\Winter2014\SoiProjs\Capstone\CNCdata.csv";

                //string FileName = @"C:\Users\Ted\Dropbox\Soi's Projs\Capstone\Han\MSTB_CD\Track" + (26 + list) + ".wav";

                //string CNCfilepath = @"C:\Users\Ted\Dropbox\Soi's Projs\Capstone\CNCdata.csv";




                StreamReader sr = new StreamReader(CNCfilepath);
                DataTable CNCtable = CSV.LoadDataTable(sr, false);
                sr.Close();

                Int32 position = 0;

                position = Convert.ToInt32(CNCtable.Rows[Word_order[click_CNC]][list - 1]);


                position = (position - 90000) * 4;

                SplitWav(FileName, position);

                SplitWord(list, Convert.ToInt16(1 + Word_order[click_CNC]));            // display phonemes on the buttons

                //Console.WriteLine(phon1_hit + phon2_hit + phon3_hit);

                //click_CNC++;


                phon1_hit = 0;
                phon2_hit = 0;
                phon3_hit = 0;
                extra_hit = 0;

                click_CNC++;
            }

        }

        private void button10_Click(object sender, EventArgs e)     // Button All
        {
            if (startCNC > 0)
            {
                if (phon1_hit + phon2_hit + phon3_hit == 3)
                {
                    phon1_hit = 0;
                    phon1.BackColor = DefaultBackColor;
                    phon2_hit = 0;
                    phon2.BackColor = DefaultBackColor;
                    phon3_hit = 0;
                    phon3.BackColor = DefaultBackColor;
                }
                else
                {
                    phon1_hit = 1;
                    phon1.BackColor = Color.MediumVioletRed;
                    phon2_hit = 1;
                    phon2.BackColor = Color.MediumVioletRed;
                    phon3_hit = 1;
                    phon3.BackColor = Color.MediumVioletRed;
                }
                string N = @"N = " + (phon1_hit + phon2_hit + phon3_hit);
                CNCstart.Text = N;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                CNCPlayer.Play();
            }
            catch
            {
                MessageBox.Show("Hit Start First", "Error!");
            }
        }       // Button Repeat

        #endregion Control buttons: Repeat, All, Start/Done


        #region phoneme button clicked

        private void phon1_Click(object sender, EventArgs e)
        {
            if (startCNC > 0)
            {
                phon1_hit ^= 1;
                if (phon1_hit % 2 == 1) // the phoneme is selected
                {
                    phon1.BackColor = Color.MediumVioletRed;
                }
                else
                {
                    phon1.BackColor = DefaultBackColor;
                }

                string N = @"N = " + (phon1_hit + phon2_hit + phon3_hit);

                CNCstart.Text = N;
            }
        }

        private void phon2_Click(object sender, EventArgs e)
        {
            if (startCNC > 0)
            {
                phon2_hit ^= 1;
                if (phon2_hit % 2 == 1) // the phoneme is selected
                {
                    phon2.BackColor = Color.MediumVioletRed;
                }
                else
                {
                    phon2.BackColor = DefaultBackColor;
                }

                string N = @"N = " + (phon1_hit + phon2_hit + phon3_hit);
                CNCstart.Text = N;
            }
        }

        private void phon3_Click(object sender, EventArgs e)
        {
            if (startCNC > 0)
            {
                phon3_hit ^= 1;

                if (phon3_hit % 2 == 1) // the phoneme is selected
                {
                    phon3.BackColor = Color.MediumVioletRed;
                }
                else
                {
                    phon3.BackColor = DefaultBackColor;
                }

                string N = @"N = " + (phon1_hit + phon2_hit + phon3_hit);
                CNCstart.Text = N;
            }
        }

        private void CNCExtra_Click(object sender, EventArgs e)
        {
            if (startCNC > 0)
            {
                if ((phon1_hit + phon2_hit + phon3_hit) == 3 || (extra_hit == 1))
                {
                    extra_hit ^= 1;

                    if (extra_hit % 2 == 1) // the phoneme is selected
                    {
                        CNCExtra.BackColor = Color.MediumBlue;
                    }
                    else
                    {
                        CNCExtra.BackColor = DefaultBackColor;
                    }
                }


            }
        }

        #endregion phoneme button clicked


        #region change history


        private void phonHisChanged(Button butt, int posi, int offset)
        {
            switch (MessageBox.Show("Are you sure you want to change?", "Alert", MessageBoxButtons.YesNo))
            {
                case DialogResult.Yes:

                    Int16 num = Convert.ToInt16(click_CNC - offset);
                    try
                    {
                        updateScoreCNC(num, posi);

                        int score = Convert.ToInt16(tb.CNCdt_score.Rows[num][posi]);

                        if (score == 1)
                            butt.BackColor = Color.MediumVioletRed;
                        else
                            butt.BackColor = DefaultBackColor;
                    }
                    catch { }
                    break;
                case DialogResult.No:
                    break;
                default:
                    break;
            }
        }

        private void ExtraHisChanged(Button butt, int posi, int offset)
        {
            Int16 num = Convert.ToInt16(click_CNC - offset);

            int ExtraOldscore = Convert.ToInt16(tb.CNCdt_score.Rows[num][posi]);

            int OldPhon1 = Convert.ToInt16(tb.CNCdt_score.Rows[num][1]);
            int OldPhon2 = Convert.ToInt16(tb.CNCdt_score.Rows[num][2]);
            int OldPhon3 = Convert.ToInt16(tb.CNCdt_score.Rows[num][3]);

            if (ExtraOldscore == 1 || (OldPhon1 + OldPhon2 + OldPhon3) == 3)
            {

                switch (MessageBox.Show("Are you sure you want to change?", "Alert", MessageBoxButtons.YesNo))
                {
                    case DialogResult.Yes:

                        try
                        {
                            updateScoreCNC(num, posi);

                            int score = Convert.ToInt16(tb.CNCdt_score.Rows[num][posi]);

                            if (score == 1)
                                butt.BackColor = Color.MediumBlue;
                            else
                                butt.BackColor = DefaultBackColor;
                        }
                        catch { }
                        break;
                    case DialogResult.No:
                        break;
                    default:
                        break;
                }
            }
        }

        private void CNChis1_1_Click(object sender, EventArgs e)
        {
            phonHisChanged(CNChis1_1, 1, 2);
        }

        private void CNChis1_2_Click(object sender, EventArgs e)
        {
            phonHisChanged(CNChis1_2, 2, 2);
        }

        private void CNChis1_3_Click(object sender, EventArgs e)
        {
            phonHisChanged(CNChis1_3, 3, 2);
        }

        private void CNChis1_extra_Click(object sender, EventArgs e)
        {
            ExtraHisChanged(CNChis1_extra, 4, 2);
        }

        private void CNChis2_1_Click(object sender, EventArgs e)
        {
            phonHisChanged(CNChis2_1, 1, 3);
        }

        private void CNChis2_2_Click(object sender, EventArgs e)
        {
            phonHisChanged(CNChis2_2, 2, 3);
        }

        private void CNChis2_3_Click(object sender, EventArgs e)
        {
            phonHisChanged(CNChis2_3, 3, 3);
        }

        private void CNChis2_extra_Click(object sender, EventArgs e)
        {
            ExtraHisChanged(CNChis2_extra, 4, 3);
        }

        private void CNChis3_1_Click(object sender, EventArgs e)
        {
            phonHisChanged(CNChis3_1, 1, 4);
        }

        private void CNChis3_2_Click(object sender, EventArgs e)
        {
            phonHisChanged(CNChis3_2, 2, 4);
        }

        private void CNChis3_3_Click(object sender, EventArgs e)
        {
            phonHisChanged(CNChis3_3, 3, 4);
        }

        private void CNChis3_extra_Click(object sender, EventArgs e)
        {
            ExtraHisChanged(CNChis3_extra, 4, 4);
        }

        private void CNChis4_1_Click(object sender, EventArgs e)
        {
            phonHisChanged(CNChis4_1, 1, 5);
        }

        private void CNChis4_2_Click(object sender, EventArgs e)
        {
            phonHisChanged(CNChis4_2, 2, 5);
        }

        private void CNChis4_3_Click(object sender, EventArgs e)
        {
            phonHisChanged(CNChis4_3, 3, 5);
        }

        private void CNChis4_extra_Click(object sender, EventArgs e)
        {
            ExtraHisChanged(CNChis4_extra, 4, 5);
        }

        private void CNChis5_1_Click(object sender, EventArgs e)
        {
            phonHisChanged(CNChis5_1, 1, 6);
        }

        private void CNChis5_2_Click(object sender, EventArgs e)
        {
            phonHisChanged(CNChis5_2, 2, 6);
        }

        private void CNChis5_3_Click(object sender, EventArgs e)
        {
            phonHisChanged(CNChis5_3, 3, 6);
        }

        private void CNChis5_extra_Click(object sender, EventArgs e)
        {
            ExtraHisChanged(CNChis5_extra, 4, 6);
        }

        #endregion change history

        private void CNCfinish_Click(object sender, EventArgs e)
        {
            switch (MessageBox.Show("Are you sure you want to finish the Test?", "Alert", MessageBoxButtons.YesNo))
            {
                case DialogResult.Yes:

                    try
                    {

                        tabTest.SelectedTab = tabExistingPatient;

                        int listNo = Convert.ToInt16(CNCListNumber.SelectedItem.ToString());
                        string Quiet = "No";
                        //string LeftEar = CNCleftEar.SelectedItem.ToString();
                        //string RightEar = CNCrightEar.SelectedItem.ToString();

                        string LeftEar = CNCleftEar.Text;
                        string RightEar = CNCrightEar.Text;

                        if (CNCquiet.Checked)
                            Quiet = "Quiet";
                        else
                        {
                            Quiet = @CNC_snr.Text.ToString() + " dB";
                        }


                        //tb.add_row_CNC_sumary(listNo, Quiet, LeftEar, RightEar, CNCwordCorrect, CNCphonCorrect);
                        //dataGridView9.DataSource = tb.CNCdt_sumary;

                        int NoOfWordPlayed = tb.CNCdt_score.Rows.Count;

                        //int PhonCorrPercent = CNCphonCorrect * 100 / (NoOfWordPlayed * 3);
                        //int WordCorrPercent = CNCwordCorrect * 100 / NoOfWordPlayed;

                        string PhonCorrPercent = Convert.ToString(CNCphonCorrect * 100 / (NoOfWordPlayed * 3)) + @"%";
                        string  WordCorrPercent = Convert.ToString(CNCwordCorrect * 100 / NoOfWordPlayed) + @"%";

                        string PhonCorrDisp = Convert.ToString(CNCphonCorrect) + @"/" + Convert.ToString(NoOfWordPlayed*3);

                        string wordCorrDisp = Convert.ToString(CNCwordCorrect) + @"/" + Convert.ToString(NoOfWordPlayed);

                        tb.add_row_Test_Results("CNC", LeftEar, RightEar, listNo, Quiet, PhonCorrDisp, PhonCorrPercent, wordCorrDisp, WordCorrPercent);


                        tb.add_row_CNC_ConductedTest(listNo, LeftEar, RightEar);

                        CleanCNCmainMenu();


                        // change collection for CNCListNumber combobox.

                         int temp = 10 - tb.CNCdt_ConductedTest.Rows.Count;

                        string[] CNCList = new string[temp];
                        int index = 0;

                        for (int j = 1; j <= 10; j++)
                        {
                            int count = 0;

                            //Console.Write(tb.CNCdt_ConductedTest.Rows.Count);

                            for (int i = 0; i < tb.CNCdt_ConductedTest.Rows.Count; i++)
                            {
                                if (j == Convert.ToInt16(tb.CNCdt_ConductedTest.Rows[i][0].ToString()))
                                    count++;                                    
                            }

                            if (count == 0)
                            {
                                CNCList[index] = Convert.ToString(j);
                                index++;
                            }
                            
                        }

                        CNCListNumber.Items.Clear();

                        for (int i = 0; i < CNCList.Length; i++)
                            CNCListNumber.Items.Add(CNCList[i]);

                    }
                    catch { }
                    break;
                case DialogResult.No:
                    break;
                default:
                    break;
            }
        }

        private void CNCprevious_Click_1(object sender, EventArgs e)
        {
            if (click_CNC > 6)
            {
                CNCnext.Enabled = true;

                click_CNC = Convert.ToInt16(click_CNC - 5);
                displayCNCHistory(click_CNC - 2);

                //Console.Write(click_CNC);
                //Console.Write("\n");
            }
            if (click_CNC < 7)
            {
                CNCprevious.Enabled = false;
                CNCnext.Enabled = true;
            }
        }

        private void CNCnext_Click_1(object sender, EventArgs e)
        {
            if (click_CNC < 47)
            {
                CNCprevious.Enabled = true;

                click_CNC = Convert.ToInt16(click_CNC + 5);
                displayCNCHistory(click_CNC - 2);

                //Console.Write(click_CNC);
                //Console.Write("\n");
            }
            if (click_CNC > 46)
            {
                CNCnext.Enabled = false;
                CNCprevious.Enabled = true;
            }
        }


        #region using menu strip

        #endregion using menu strip

        private void button4_Click_1(object sender, EventArgs e)
        {
            SoundPlayer calib = new SoundPlayer();

            if (buttCali.Tag.ToString().Equals("1"))
            {
                buttCali.Tag = "0";

                buttCali.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.stop));

                string FileName = Path.Combine(Environment.CurrentDirectory, @"Source\cnc\audio\Track37.wav");

                byte[] wav = File.ReadAllBytes(FileName);

                MemoryStream ms = new MemoryStream(wav);

                calib = new SoundPlayer(ms);

                calib.Play();                

            }
            else
            {
                buttCali.Tag = "1";
                buttCali.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.play));

                calib.Stop();

            }
        }

        private void azBioTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabTest.SelectedTab = tabAZListSelect;
            CleanAZmainMenu();

        }

        private void cALIBRATEIONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabTest.SelectedTab = Calibration;
        }

        private void cNCTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabTest.SelectedTab = tabCNCListSelect;
            CleanCNCmainMenu();
        }

        private void rEPORTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabTest.SelectedTab = tabReportTest;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tabTest.SelectedTab = tabPlot;
        }

        private void CNCquiet_CheckedChanged(object sender, EventArgs e)
        {
            if (CNCquiet.Checked)
            {
                CNCgroupNoise.Enabled = false;
            }
            else
            {
                CNCgroupNoise.Enabled = true;
                CNCnoiseSource.SelectedIndex = 0;
                CNCnoiseSource.Text = CNCnoiseSource.SelectedItem.ToString();
            }

        }

        private void az_quiet_CheckedChanged(object sender, EventArgs e)
        {
            if (az_quiet.Checked)
            {
                az_quite_noise.Enabled = false;
            }
            else
            {
                az_quite_noise.Enabled = true;
                az_noise_source.SelectedIndex = 0;
                az_noise_source.Text = az_noise_source.SelectedItem.ToString();
            }
        }

        private void azbutPrevious_Click(object sender, EventArgs e)
        {
            CleanAZHisTest();
            if (click_az > 3)
            {
                azbutNext.Enabled = true;

                click_az = Convert.ToInt16(click_az - 3);
                displayAzHistory(click_az);
                Console.Write(click_az);
                Console.Write("\n");
            }
            if (click_az <= 3)
            {
                azbutPrevious.Enabled = false;
                azbutNext.Enabled = true;

            }
        }


        private void azbutNext_Click(object sender, EventArgs e)
        {
            if (click_az < 17)
            {
                azbutPrevious.Enabled = true;

                click_az = Convert.ToInt16(click_az + 3);

                displayAzHistory(click_az);
                Console.Write(click_az);
                Console.Write("\n");
            }
            if (click_az >= 19)
            {
                azbutPrevious.Enabled = true;
                azbutNext.Enabled = false;

            }
        }
        

        private void az_finish_Click(object sender, EventArgs e)
        {
            switch (MessageBox.Show("Are you sure you want to finish the Test?", "Alert", MessageBoxButtons.YesNo))
            {
                case DialogResult.Yes:

                    try
                    {

                        tabTest.SelectedTab = tabExistingPatient;

                        int listNo = Convert.ToInt16(list_sel_az.SelectedItem.ToString());
                        string Quiet = "No";
                        //string LeftEar = az_comboLeft.SelectedItem.ToString();
                        //string RightEar = az_comboRight.SelectedItem.ToString();

                        string LeftEar = az_comboLeft.Text;
                        string RightEar = az_comboRight.Text;

                        if (az_quiet.Checked)
                            Quiet = "Quiet";
                        else
                        {
                            Quiet = @az_SNR.Text.ToString() + " dB";
                        }

                        // int NoOfsentPlayed = tb.dt_score.Rows.Count;

                        int WordCorrPercent = N_totalcorrect * 100 / N_total;
                        string temp = @Convert.ToString(N_totalcorrect) + "/" + @Convert.ToString(N_total);

                        string wordCorrPerDisp = Convert.ToString(WordCorrPercent) + "%";

                        tb.add_row_Test_Results("AzBio", LeftEar, RightEar, listNo, Quiet, "N/A", "N/A", temp, wordCorrPerDisp);

                        az_comboLeft.Text = string.Empty;
                        az_comboRight.Text = string.Empty;
                        tb.add_row_azconlist(listNo, LeftEar, RightEar);


                        
                        // change collection for CNCListNumber combobox.

                        int azTemp = 15 - tb.az_conlist.Rows.Count;

                        string[] azList = new string[azTemp];
                        int index = 0;

                        for (int j = 1; j <= 15; j++)
                        {
                            int count = 0;

                            //Console.Write(tb.CNCdt_ConductedTest.Rows.Count);

                            for (int i = 0; i < tb.az_conlist.Rows.Count; i++)
                            {
                                if (j == Convert.ToInt16(tb.az_conlist.Rows[i][0].ToString()))
                                    count++;                                    
                            }

                            if (count == 0)
                            {
                                azList[index] = Convert.ToString(j);
                                index++;
                            }
                            
                        }

                        list_sel_az.Items.Clear();

                        for (int i = 0; i < azList.Length; i++)
                            list_sel_az.Items.Add(azList[i]);


                    }
                    catch { }
                    break;
                case DialogResult.No:
                    break;
                default:
                    break;
            }
        }

        private void button95_Click(object sender, EventArgs e)
        {
            DataTable cnc_result = null;
            DataTable az_result = null;

            try
            {
                DataRow[] drs = tb.TestResults.Select("Type = 'CNC'");
                cnc_result = drs.CopyToDataTable();
                cnc_result.Columns.RemoveAt(0);
            }
            catch { }

            try
            {
                DataRow[] drs_az = tb.TestResults.Select("Type = 'AzBio'");
                az_result = drs_az.CopyToDataTable();
                az_result.Columns.RemoveAt(0);
                az_result.Columns.RemoveAt(4);
                az_result.Columns.RemoveAt(4);
            }
            catch { }

            CreateTableInWordDocument(cnc_result, az_result);
        }

        private void CreateTableInWordDocument(DataTable dt_cnc, DataTable dt_az)
        {
            //////referecnce: http://www.authorcode.com/create-a-new-word-document-and-insert-a-table-in-c/
            object oMissing = System.Reflection.Missing.Value;
            object oEndOfDoc = "\\endofdoc";
            Microsoft.Office.Interop.Word._Application objWord;
            Microsoft.Office.Interop.Word._Document objDoc;
            objWord = new Microsoft.Office.Interop.Word.Application();
            objWord.Visible = true;
            objDoc = objWord.Documents.Add(ref oMissing, ref oMissing,
                ref oMissing, ref oMissing);
            Microsoft.Office.Interop.Word.Table objTable;
            Microsoft.Office.Interop.Word.Range wrdRng = objDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            Microsoft.Office.Interop.Word.Range rngCell;
             int i = 0;
            int j = 0;
            //Add a CNC table
            try
            {


                int cn_row = dt_cnc.Rows.Count + 1;
                int cn_col = 8;

                objTable = objDoc.Tables.Add(wrdRng, cn_row, cn_col, ref oMissing, ref oMissing);
                objTable.Range.ParagraphFormat.SpaceAfter = 6;

                //objTable.Rows[1].Range.Font.Bold = 1;
                //objTable.Rows[1].Range.Font.Italic = 1;
                //Object style = "Table Grid 8";
                //objTable.set_Style(ref style);
                objTable.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                objTable.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;

                objTable.Range.Font.Size = 8;
                objTable.Range.Font.Name = "Verdana";



                //Create Name row (1st row)
               

                rngCell = objTable.Cell(1, 1).Range;
                rngCell.Text = "Left Ear";
                rngCell.ParagraphFormat.Alignment =
                Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                rngCell.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorLightGreen;
                rngCell.Font.Bold = 1;

                rngCell = objTable.Cell(1, 2).Range;
                rngCell.Text = "Right Ear";
                rngCell.ParagraphFormat.Alignment =
                Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                rngCell.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorLightGreen;
                rngCell.Font.Bold = 1;

                rngCell = objTable.Cell(1, 3).Range;
                rngCell.Text = "List No.";
                rngCell.ParagraphFormat.Alignment =
                Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                rngCell.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorLightGreen;
                rngCell.Font.Bold = 1;

                rngCell = objTable.Cell(1, 4).Range;
                rngCell.Text = "S/N";
                rngCell.ParagraphFormat.Alignment =
                Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                rngCell.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorLightGreen;
                rngCell.Font.Bold = 1;

                rngCell = objTable.Cell(1, 5).Range;
                rngCell.Text = "Phoneme Score";
                rngCell.ParagraphFormat.Alignment =
                Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                rngCell.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorLightGreen;
                rngCell.Font.Bold = 1;

                rngCell = objTable.Cell(1, 6).Range;
                rngCell.Text = "Phoneme % correct";
                rngCell.ParagraphFormat.Alignment =
                Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                rngCell.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorLightGreen;
                rngCell.Font.Bold = 1;

                rngCell = objTable.Cell(1, 7).Range;
                rngCell.Text = "Word score";
                rngCell.ParagraphFormat.Alignment =
                Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                rngCell.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorLightGreen;
                rngCell.Font.Bold = 1;

                rngCell = objTable.Cell(1, 8).Range;
                rngCell.Text = "Word % correct";
                rngCell.ParagraphFormat.Alignment =
                Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                rngCell.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorLightGreen;
                rngCell.Font.Bold = 1;

                if (cn_row > 1)
                {
                    for (i = 2; i <= cn_row; i++) // row
                        for (j = 1; j <= cn_col; j++) // column
                        {
                            //strText = "Row" + i + " Coulmn" + j; // content each cell
                            objTable.Cell(i, j).Range.Text = dt_cnc.Rows[i - 2][j - 1].ToString(); // add the content to the table
                        }
                }

                //objTable.Borders.
                //this.Close();
                //Add some text after the table.

                Microsoft.Office.Interop.Word.Paragraph oPara4;
                wrdRng = objDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                oPara4 = objDoc.Content.Paragraphs.Add(wrdRng);
                oPara4.Range.InsertParagraphBefore();
                oPara4.Range.Text = "";
                oPara4.Format.SpaceAfter = 6;
                oPara4.Range.InsertParagraphAfter();
            }
            catch { }
            //AzBio
            try
            {

                int az_row = dt_az.Rows.Count + 1;
                int az_col = 6;
                //Microsoft.Office.Interop.Word.Table objTable;
                wrdRng = objDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                objTable = objDoc.Tables.Add(wrdRng, az_row, az_col, ref oMissing, ref oMissing);
                //objTable.Range.ParagraphFormat.SpaceAfter = 7;

                //objTable.Rows[1].Range.Font.Bold = 1;
                //objTable.Rows[1].Range.Font.Italic = 1;
                //Object style = "Table Grid 8";
                //objTable.set_Style(ref style);
                objTable.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                objTable.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;

                objTable.Range.Font.Size = 8;
                objTable.Range.Font.Name = "Verdana";

                //Create Name row (1st row)
                // Microsoft.Office.Interop.Word.Range rngCell;

                rngCell = objTable.Cell(1, 1).Range;
                rngCell.Text = "Left Ear";
                rngCell.ParagraphFormat.Alignment =
                Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                rngCell.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorLightGreen;
                rngCell.Font.Bold = 1;

                rngCell = objTable.Cell(1, 2).Range;
                rngCell.Text = "Right Ear";
                rngCell.ParagraphFormat.Alignment =
                Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                rngCell.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorLightGreen;
                rngCell.Font.Bold = 1;

                rngCell = objTable.Cell(1, 3).Range;
                rngCell.Text = "List No.";
                rngCell.ParagraphFormat.Alignment =
                Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                rngCell.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorLightGreen;
                rngCell.Font.Bold = 1;

                rngCell = objTable.Cell(1, 4).Range;
                rngCell.Text = "S/N";
                rngCell.ParagraphFormat.Alignment =
                Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                rngCell.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorLightGreen;
                rngCell.Font.Bold = 1;


                rngCell = objTable.Cell(1, 5).Range;
                rngCell.Text = "Word score";
                rngCell.ParagraphFormat.Alignment =
                Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                rngCell.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorLightGreen;
                rngCell.Font.Bold = 1;

                rngCell = objTable.Cell(1, 6).Range;
                rngCell.Text = "Word % correct";
                rngCell.ParagraphFormat.Alignment =
                Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                rngCell.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorLightGreen;
                rngCell.Font.Bold = 1;

               
                    for (i = 2; i <= az_row; i++) // row
                        for (j = 1; j <= az_col; j++) // column
                        {
                            //strText = "Row" + i + " Coulmn" + j; // content each cell
                            objTable.Cell(i, j).Range.Text = dt_az.Rows[i - 2][j - 1].ToString(); // add the content to the table
                        }
                
            }
            catch { }
            
        }
    }
}
