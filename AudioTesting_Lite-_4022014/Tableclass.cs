using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Media;
using FileHelpers;
using CSVFile;

namespace AudioTesting_Lite__3192014
{
    public class Tableclass
    {
        public DataTable dt; //save sentence
        public DataTable dt_score; // save score
        public DataTable TestResults;
        public void create_table_sen()
        {
            dt = new DataTable();
            dt.Columns.Add("Order", typeof(int));
            dt.Columns.Add("Numword", typeof(int));
            dt.Columns.Add("w1", typeof(string));
            dt.Columns.Add("w2", typeof(string));
            dt.Columns.Add("w3", typeof(string));
            dt.Columns.Add("w4", typeof(string));
            dt.Columns.Add("w5", typeof(string));
            dt.Columns.Add("w6", typeof(string));
            dt.Columns.Add("w7", typeof(string));
            dt.Columns.Add("w8", typeof(string));
            dt.Columns.Add("w9", typeof(string));
            dt.Columns.Add("w10", typeof(string));
            dt.Columns.Add("w11", typeof(string));
            dt.Columns.Add("w12", typeof(string));
        }

        public void add_row(int order, int numword, string[] sentence)
        {
            DataRow dtrow = dt.NewRow();
            dtrow[0] = order;
            dtrow[1] = numword;
            for (int i = 0; i < 12; i++)
            {
                dtrow[i + 2] = sentence[i];
            }
            dt.Rows.Add(dtrow);
        }

        public void create_table_score()
        {
            dt_score = new DataTable();
            dt_score.Columns.Add("Order", typeof(int));
            dt_score.Columns.Add("w1", typeof(string));
            dt_score.Columns.Add("w2", typeof(string));
            dt_score.Columns.Add("w3", typeof(string));
            dt_score.Columns.Add("w4", typeof(string));
            dt_score.Columns.Add("w5", typeof(string));
            dt_score.Columns.Add("w6", typeof(string));
            dt_score.Columns.Add("w7", typeof(string));
            dt_score.Columns.Add("w8", typeof(string));
            dt_score.Columns.Add("w9", typeof(string));
            dt_score.Columns.Add("w10", typeof(string));
            dt_score.Columns.Add("w11", typeof(string));
            dt_score.Columns.Add("w12", typeof(string));
        }

        public void add_score_row(int order, int[] score)
        {
            DataRow dtrow = dt_score.NewRow();
            dtrow[0] = order;
            for (int i = 0; i < 12; i++)
            {
                dtrow[i + 1] = score[i];
            }
            dt_score.Rows.Add(dtrow);
        } 
  
        //// for CNC testing
        public DataTable CNCdt; //save sentence
        public DataTable CNCdt_score; // save score
        public DataTable CNCdt_sumary;
        public DataTable CNCdt_ConductedTest;

        public void create_table_CNC()
        {
            CNCdt = new DataTable();
            CNCdt.Columns.Add("Order", typeof(int));
            CNCdt.Columns.Add("P1", typeof(string));
            CNCdt.Columns.Add("P2", typeof(string));
            CNCdt.Columns.Add("P3", typeof(string));
        }

        public void add_row_CNC(int order, string[] word)
        {
            DataRow dtrow = CNCdt.NewRow();
            dtrow[0] = order;
            for (int i = 0; i < 3; i++)
            {
                dtrow[i + 1] = word[i];
            }
            CNCdt.Rows.Add(dtrow);
        }

        public void create_table_score_CNC()
        {
            CNCdt_score = new DataTable();
            CNCdt_score.Columns.Add("Order", typeof(int));
            CNCdt_score.Columns.Add("P1", typeof(string));
            CNCdt_score.Columns.Add("P2", typeof(string));
            CNCdt_score.Columns.Add("P3", typeof(string));
            CNCdt_score.Columns.Add("Extra", typeof(string));
        }

        public void add_score_row_CNC(int order, int[] score)
        {
            DataRow dtrow = CNCdt_score.NewRow();
            dtrow[0] = order;
            for (int i = 0; i < 4; i++)
            {
                dtrow[i + 1] = score[i];
            }
            CNCdt_score.Rows.Add(dtrow);
        }

        public void create_table_CNC_ConductedTest()
        {
            CNCdt_ConductedTest = new DataTable();
            CNCdt_ConductedTest.Columns.Add("ListNo", typeof(int));
            CNCdt_ConductedTest.Columns.Add("LeftEar", typeof(string));
            CNCdt_ConductedTest.Columns.Add("RightEar", typeof(string));
        }

        public void add_row_CNC_ConductedTest(int ListNo, string LeftEar, string RightEar)
        {
            DataRow dtrow = CNCdt_ConductedTest.NewRow();
            dtrow[0] = ListNo;
            dtrow[1] = LeftEar;
            dtrow[2] = RightEar;
            CNCdt_ConductedTest.Rows.Add(dtrow);
        }

        public void create_table_Test_Results()
        {
            TestResults = new DataTable();
            TestResults.Columns.Add("Type", typeof(string));
            TestResults.Columns.Add("LeftEar", typeof(string));
            TestResults.Columns.Add("RightEar", typeof(string));
            TestResults.Columns.Add("ListNo", typeof(int));
            TestResults.Columns.Add("Quiet", typeof(string));
            TestResults.Columns.Add("PhonCorr", typeof(string));
            TestResults.Columns.Add("PhonCorrPercent", typeof(string));
            TestResults.Columns.Add("WordCorr", typeof(string));
            TestResults.Columns.Add("WordCorrPercent", typeof(string));
        }

        public void add_row_Test_Results(string type, string LeftEar, string RightEar, int ListNo, string Quiet, string PhonCorr, string PhonCorrPercent, string WordCorr, string WordCorrPercent)
        {
            DataRow dtrow = TestResults.NewRow();

            dtrow[0] = type;
            dtrow[1] = LeftEar;
            dtrow[2] = RightEar;
            dtrow[3] = ListNo;
            dtrow[4] = Quiet;
            dtrow[5] = PhonCorr;
            dtrow[6] = PhonCorrPercent;
            dtrow[7] = WordCorr;
            dtrow[8] = WordCorrPercent;

            TestResults.Rows.Add(dtrow);
        }

        public DataTable az_conlist;

        public void create_table_azconlist()
        {
            az_conlist = new DataTable();
            az_conlist.Columns.Add("ListNo", typeof(int));
            az_conlist.Columns.Add("LeftEar", typeof(string));
            az_conlist.Columns.Add("RightEar", typeof(string));
        }

        public void add_row_azconlist(int ListNo, string LeftEar, string RightEar)
        {
            DataRow dtrow = az_conlist.NewRow();

            dtrow[0] = ListNo; 
            dtrow[1] = LeftEar;
            dtrow[2] = RightEar;
            az_conlist.Rows.Add(dtrow);
        }
    }
}
