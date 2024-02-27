﻿using System;
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
    public partial class SetupSite : System.Web.UI.Page
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

            conn.Conn();
            conn.Open();

            GenerateTable();

            conn.Close();
        }

        public void GenerateTable()
        {
            String TodayDate = DateTime.Now.ToString();
            timeLabel.Text = TodayDate.ToString();

            DataTable Table = new DataTable();

            string sqlstring = "SELECT * from dbo.Site; ";

            sqlcom = new SqlCommand();
            sqlcom.CommandText = sqlstring;
            sqlcom.Connection = conn.objConnection;
            dr = sqlcom.ExecuteReader();
            Table.Load(dr);
            dr.Close();
            sqlcom.Dispose();

            gvTable.DataSource = Table;
            gvTable.DataBind();
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            lblErrorMsg.Visible = false;
            span1.Visible = false;

            GridViewRow gvRow = (GridViewRow)((Control)sender).Parent.Parent;
            int index = gvRow.RowIndex;

            txtID.Text = gvTable.Rows[index].Cells[0].Text.ToString();
            txtSite.Text = gvTable.Rows[index].Cells[1].Text.ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            conn.Conn();
            conn.Open();

            if (txtSite.Text == "")
            {
                lblErrorMsg.Visible = true;
                span1.Visible = true;
            }
            else
            {
                lblErrorMsg.Visible = false;
                span1.Visible = false;
            }

            if (txtSite.Text != "")
            {
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = new SqlCommand();
                sqlcmd.CommandText = "select * from Site where Site = @SiteName and AutoID != @Auto_ID";
                sqlcmd.Connection = conn.objConnection;
                sqlcmd.Parameters.AddWithValue("@SiteName", SqlDbType.NVarChar).Value = txtSite.Text.Trim();
                sqlcmd.Parameters.AddWithValue("@Auto_ID", txtID.Text);
                dr = sqlcmd.ExecuteReader();

                if (dr.HasRows)
                {
                    lblErrorMsgDup.Visible = true;
                    span1.Visible = true;
                }
                else
                {
                    lblErrorMsgDup.Visible = false;
                    span1.Visible = false;


                    if (txtID.Text == "-")
                    {
                        // Insert Record
                        sqlcom.CommandText = "INSERT INTO dbo.Site (Site) VALUES " +
                            "(@Site) ";


                        sqlcom.Parameters.AddWithValue("@Site", SqlDbType.Int).Value = txtSite.Text;

                        sqlcom.Connection = conn.objConnection;
                        sqlcom.ExecuteNonQuery();
                        sqlcom.Dispose();

                        txtID.Text = "-";
                        txtSite.Text = "";

                    }
                    else
                    {
                        //update record
                        sqlcom.CommandText = "UPDATE dbo.Site SET " +
                            "Site = @Site " +
                            " WHERE AutoID = @ID";

                        sqlcom.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = txtID.Text;
                        sqlcom.Parameters.AddWithValue("@Site", SqlDbType.VarChar).Value = txtSite.Text;

                        sqlcom.Connection = conn.objConnection;
                        sqlcom.ExecuteNonQuery();
                        sqlcom.Dispose();

                        txtID.Text = "-";
                        txtSite.Text = "";
                    }

                    this.reload2();
                }
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            lblErrorMsg.Visible = false;
            span1.Visible = false;
            txtID.Text = "-";
            txtSite.Text = "";
        }

        protected void reload2()
        {
            Response.Redirect(Request.RawUrl);
        }
    }
}