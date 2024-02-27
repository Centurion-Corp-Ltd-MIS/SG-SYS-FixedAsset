using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Web.Security;
using System.Globalization;
using System.DirectoryServices.Protocols;
using System.DirectoryServices;

namespace FixedAssetSystem
{
    public partial class Dashboard : System.Web.UI.Page
    {
        dbConnection conn = new dbConnection();

        SqlCommand sqlcom = new SqlCommand();
        SqlDataAdapter sqlada = new SqlDataAdapter();
        SqlDataReader dr;
        DataTable dt = new DataTable();
        DataTable dtCategory = new DataTable();

        User user = new User();
        LoginScript loginScript = new LoginScript();

        public string[] pieLabels { get; set; }
        public int[] pieData { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            GeneratePieChart();
        }

        private void GeneratePieChart()
        {
            dtCategory.Clear();
            int total = 0;

            conn.Conn();
            conn.Open();

            string sqlstring = "SELECT category, COUNT(*) AS category_count FROM Assets GROUP BY category;";
            sqlcom = new SqlCommand();
            sqlcom.CommandText = sqlstring;
            sqlcom.Connection = conn.objConnection;
            dr = sqlcom.ExecuteReader();
            dtCategory.Load(dr);
            dr.Close();
            sqlcom.Dispose();

            sqlcom = new SqlCommand();
            sqlcom.CommandText = "SELECT COUNT(*) AS total_rows FROM Assets;";
            sqlcom.Connection = conn.objConnection;
            dr = sqlcom.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    total = dr.GetInt32(0);
                }
            }
            dr.Close();
            sqlcom.Dispose();

            conn.Close();



            List<int> listPieData = new List<int>();
            DataSet dsData = new DataSet();
            lblPieChartTitle.Text = "Nationality Profile - Pie Chart";
            List<string> labelList = new List<string>();
            string resultString = "";

            foreach (DataRow row in dtCategory.Rows)
            {
                Decimal value = ((decimal)Convert.ToInt32(row["category_count"]) / (decimal)total) * 100;
                string legend = value.ToString("0.00") + "%(" + row["category_count"].ToString() + ")";

                pieLabels = new string[] { "Switch :" + 20, "Laptop :" + 20, "Desktop :" + 20, "Monitor :" + 20, "Hark Disk :" + 20 };

                listPieData.Add(Convert.ToInt32(row["category_count"]));
            }


            lblPBDNAT.Text = total.ToString();
            pieData = listPieData.ToArray();


            string test12 = "";





























            //List<int> listPieData = new List<int>();
            //DataSet dsData = new DataSet();
            //lblPieChartTitle.Text = "Nationality Profile - Pie Chart";

            //Decimal PPRC = ((decimal)20 / (decimal)450) * 100;
            //Decimal PPH = ((decimal)40 / (decimal)450) * 100;
            //Decimal PBA = ((decimal)60 / (decimal)450) * 100;
            //Decimal PMY = ((decimal)100 / (decimal)450) * 100;
            //Decimal PMA = ((decimal)130 / (decimal)450) * 100;

            //string TPRC = PPRC.ToString("0.00") + "%(" + 20 + ")";
            //string TPH = PPH.ToString("0.00") + "%(" + 40 + ")";
            //string TBA = PBA.ToString("0.00") + "%(" + 60 + ")";
            //string TMY = PMY.ToString("0.00") + "%(" + 100 + ")";
            //string TMA = PMA.ToString("0.00") + "%(" + 130 + ")";

            //lblPBDNAT.Text = 450.ToString();
            //pieLabels = new string[] { "Chinese :" + 30, "Filipino: " + 40, "Bangladeshi: " + 50, "Myanmarese: " + 60, 
            //    "Malaysian: " + 70 };


            //listPieData.Add(20);
            //listPieData.Add(40);
            //listPieData.Add(60);
            //listPieData.Add(100);
            //listPieData.Add(130); ;
            //pieData = listPieData.ToArray();


        

        }
    }
}