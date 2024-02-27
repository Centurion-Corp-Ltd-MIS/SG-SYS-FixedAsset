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

namespace FixedAssetSystem
{
    public partial class History : System.Web.UI.Page
    {
        dbConnection conn = new dbConnection();

        SqlCommand sqlcom = new SqlCommand();
        SqlDataAdapter sqlada = new SqlDataAdapter();
        SqlDataReader dr;

        User user = new User();
        LoginScript loginScript = new LoginScript();

        protected void Page_Load(object sender, EventArgs e)
        {
            user = (User)Session["User"];
            if (user == null)
            {
                Response.Redirect("~/login.aspx", true);
            }

            if (!IsPostBack)
            {
                loadData(); 

                GenerateTable();
            }
            
        }

        public void GenerateTable()
        {
            conn.Conn();
            conn.Open();

            String TodayDate = DateTime.Now.ToString();
            //timeLabel.Text = TodayDate.ToString();

            DataTable Table = new DataTable();

            string sqlstring = "SELECT * from dbo.Assets; ";

            sqlcom = new SqlCommand();
            sqlcom.CommandText = sqlstring;
            sqlcom.Connection = conn.objConnection;
            dr = sqlcom.ExecuteReader();
            Table.Load(dr);
            dr.Close();
            sqlcom.Dispose();

            gvTable.DataSource = Table;
            gvTable.DataBind();

            conn.Close();
        }

        private void loadData()
        {
            ddlCategoryMain.ClearSelection();

            conn.Conn();
            conn.Open();

            //Load Category
            sqlcom = new SqlCommand();
            sqlcom.CommandText = "SELECT Category from Category";
            sqlcom.Connection = conn.objConnection;
            dr = sqlcom.ExecuteReader();
            ddlCategoryMain.Items.Add(new ListItem("--- SELECT Category ---"));
            while (dr.Read())
            {
                ddlCategoryMain.Items.Add(new ListItem(dr[0].ToString()));
            }
            dr.Close();
            sqlcom.Dispose();

            conn.Close();
        }

        protected void lnkView_Click(object sender, EventArgs e)
        {
            DataTable logsTable = new DataTable();

            GridViewRow gvRow = (GridViewRow)((Control)sender).Parent.Parent;
            int index = gvRow.RowIndex;

            conn.Conn();
            conn.Open();

            string sqlstring = "SELECT * from Action_Log where AssetID = @AssetID order by AssetID";

            sqlcom = new SqlCommand();
            sqlcom.CommandText = sqlstring;
            sqlcom.Connection = conn.objConnection;
            sqlcom.Parameters.AddWithValue("@AssetID", gvTable.Rows[index].Cells[0].Text.ToString());
            dr = sqlcom.ExecuteReader();
            logsTable.Load(dr);
            dr.Close();
            sqlcom.Dispose();

            logTable.DataSource = logsTable;
            logTable.DataBind();

            conn.Close();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "MyControls", "$('#controls').modal();", true);
        }

        protected void ddlCategoryMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCategoryMain.SelectedIndex == 0)
            {
                GenerateTable();
            }
            else
            {
                conn.Conn();
                conn.Open();

                DataTable Table = new DataTable();

                string sqlstring = "SELECT * from dbo.Assets where Category = @Category; ";

                sqlcom = new SqlCommand();
                sqlcom.CommandText = sqlstring;
                sqlcom.Connection = conn.objConnection;
                sqlcom.Parameters.AddWithValue("@Category", ddlCategoryMain.SelectedValue);
                dr = sqlcom.ExecuteReader();
                Table.Load(dr);
                dr.Close();
                sqlcom.Dispose();

                gvTable.DataSource = Table;
                gvTable.DataBind();

                conn.Close();
            }
        }
    }
}