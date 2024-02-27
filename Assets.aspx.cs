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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System.Runtime.InteropServices.ComTypes;
using System.Xml.Linq;
using System.Drawing.Printing;

namespace FixedAssetSystem
{
    public partial class Assets : System.Web.UI.Page
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

        private void loadData()
        {
            lblAssetID.Text = "";
            ddlCategory.ClearSelection();
            ddlFrom.ClearSelection();
            ddlTo.ClearSelection();

            conn.Conn();
            conn.Open();

            //Load Site
            sqlcom = new SqlCommand();
            sqlcom.CommandText = "SELECT Site from Site";
            sqlcom.Connection = conn.objConnection;
            dr = sqlcom.ExecuteReader();
            ddlFrom.Items.Add(new ListItem("--- SELECT Site ---"));
            ddlTo.Items.Add(new ListItem("--- SELECT Site ---"));
            while (dr.Read())
            {
                ddlFrom.Items.Add(new ListItem(dr[0].ToString()));
                ddlTo.Items.Add(new ListItem(dr[0].ToString()));
            }
            dr.Close();
            sqlcom.Dispose();

            //Load Department
            sqlcom = new SqlCommand();
            sqlcom.CommandText = "SELECT Department from Department";
            sqlcom.Connection = conn.objConnection;
            dr = sqlcom.ExecuteReader();
            ddlFromDe.Items.Add(new ListItem("--- SELECT Department ---"));
            ddlToDe.Items.Add(new ListItem("--- SELECT Department ---"));
            while (dr.Read())
            {
                ddlFromDe.Items.Add(new ListItem(dr[0].ToString()));
                ddlToDe.Items.Add(new ListItem(dr[0].ToString()));
            }
            dr.Close();
            sqlcom.Dispose();

            //Load Brand
            sqlcom = new SqlCommand();
            sqlcom.CommandText = "SELECT Brand from Brand";
            sqlcom.Connection = conn.objConnection;
            dr = sqlcom.ExecuteReader();
            ddlBrand.Items.Add(new ListItem("--- SELECT Brand ---"));
            while (dr.Read())
            {
                ddlBrand.Items.Add(new ListItem(dr[0].ToString()));
            }
            dr.Close();
            sqlcom.Dispose();


            //Load Category
            sqlcom = new SqlCommand();
            sqlcom.CommandText = "SELECT Category from Category";
            sqlcom.Connection = conn.objConnection;
            dr = sqlcom.ExecuteReader();
            ddlCategory.Items.Add(new ListItem("--- SELECT Category ---"));
            ddlCategoryMain.Items.Add(new ListItem("--- SELECT Category ---"));
            while (dr.Read())
            {
                ddlCategory.Items.Add(new ListItem(dr[0].ToString()));
                ddlCategoryMain.Items.Add(new ListItem(dr[0].ToString()));
            }
            dr.Close();
            sqlcom.Dispose();

            conn.Close();
        }

        public void GenerateTable()
        {
            conn.Conn();
            conn.Open();

            String TodayDate = DateTime.Now.ToString();
            //timeLabel.Text = TodayDate.ToString();

            DataTable Table = new DataTable();

            string sqlstring = "SELECT * from dbo.Assets order by autoID desc; ";

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

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            changeDiv.Visible = true;

            txtAssetTag.Enabled = false;
            txtAssetTag.BackColor = System.Drawing.Color.AliceBlue;
            txtSN.Enabled = false;
            txtSN.BackColor = System.Drawing.Color.AliceBlue;
            ddlFrom.Enabled = false;
            ddlFrom.BackColor = System.Drawing.Color.AliceBlue;
            ddlCategory.Enabled = false;
            ddlCategory.BackColor = System.Drawing.Color.AliceBlue;
            txtPF.Enabled = false;
            txtPF.BackColor = System.Drawing.Color.AliceBlue;
            txtPO.Enabled = false;
            txtPO.BackColor = System.Drawing.Color.AliceBlue;
            txtDescription.Enabled = false;
            txtDescription.BackColor = System.Drawing.Color.AliceBlue;
            ddlBrand.Enabled = false;
            ddlBrand.BackColor = System.Drawing.Color.AliceBlue;
            txtModel.Enabled = false;
            txtModel.BackColor = System.Drawing.Color.AliceBlue;
            ddlFromDe.Enabled = false;
            ddlFromDe.BackColor = System.Drawing.Color.AliceBlue;


            GridViewRow gvRow = (GridViewRow)((Control)sender).Parent.Parent;
            int index = gvRow.RowIndex;

            lblAssetID.Text = gvTable.Rows[index].Cells[0].Text.ToString();

            conn.Conn();
            conn.Open();

            sqlcom = new SqlCommand();
            sqlcom.CommandText = "Select * From dbo.Assets where AutoID= @ID";
            sqlcom.Connection = conn.objConnection;
            sqlcom.Parameters.AddWithValue("@ID", lblAssetID.Text);
            dr = sqlcom.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    txtAssetTag.Text = dr.GetString(1);
                    txtSN.Text = dr.GetString(2);
                    ddlFrom.SelectedValue = dr.GetString(3);
                    ddlCategory.SelectedValue = dr.GetString(4);
                    txtPerson.Text = dr.GetString(5);
                    txtPF.Text = dr.GetString(6);
                    txtPO.Text = dr.GetString(7);
                    txtDescription.Text = dr.GetString(8);
                    ddlFromDe.SelectedValue = dr.GetString(9);
                    ddlBrand.SelectedValue = dr.GetString(10);
                    txtModel.Text = dr.GetString(11);
                }
            }
            dr.Close();
            sqlcom.Dispose();

            conn.Close();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "MyControls", "$('#controls').modal();", true);
        }

        protected void btnMasterGen_Click(object sender, EventArgs e)
        {
            changeDiv.Visible = false;  
            txtAssetTag.Enabled = true;
            txtAssetTag.BackColor = System.Drawing.Color.White;
            txtSN.Enabled = true;
            txtSN.BackColor = System.Drawing.Color.White;
            ddlFrom.Enabled = true;
            ddlFrom.BackColor = System.Drawing.Color.White;
            ddlFromDe.Enabled = true;
            ddlFromDe.BackColor = System.Drawing.Color.White;
            ddlCategory.Enabled = true;
            ddlCategory.BackColor = System.Drawing.Color.White;
            txtPF.Enabled = true;
            txtPF.BackColor = System.Drawing.Color.White;
            txtPO.Enabled = true;
            txtPO.BackColor = System.Drawing.Color.White;
            txtDescription.Enabled = true;
            txtDescription.BackColor = System.Drawing.Color.White;
            ddlBrand.Enabled = true;
            ddlBrand.BackColor = System.Drawing.Color.White;
            txtModel.Enabled = true;
            txtModel.BackColor = System.Drawing.Color.White;

            lblAssetID.Text = "";
            txtAssetTag.Text = "";
            txtSN.Text = "";
            ddlFrom.SelectedIndex = 0;
            ddlCategory.SelectedIndex = 0;
            txtPerson.Text = "";
            txtPF.Text = "";
            txtPO.Text = "";
            txtDescription.Text = "";
            ddlBrand.SelectedIndex = 0;
            txtModel.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "MyControls", "$('#controls').modal();", true);
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtPerson.Text == "" || ddlBrand.SelectedIndex == 0 || txtModel.Text == "" || ddlFrom.SelectedIndex == 0 || ddlFromDe.SelectedIndex == 0 || ddlCategory.SelectedIndex == 0 || txtAssetTag.Text == "" || txtSN.Text == "" || txtPO.Text == "" || txtPF.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "MyControls", "$('.modal-backdrop').remove();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { openErrorModal(); });", true);
            }
            else
            {
                conn.Conn();
                conn.Open();

                string Person = "";

                sqlcom = new SqlCommand();
                sqlcom.CommandText = "Select Personnel From dbo.Assets where AutoID= @ID5";
                sqlcom.Connection = conn.objConnection;
                sqlcom.Parameters.AddWithValue("@ID5", lblAssetID.Text);
                dr = sqlcom.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Person = dr.GetString(0);
                    }
                }
                dr.Close();
                sqlcom.Dispose();

                if (lblAssetID.Text == "" || lblAssetID.Text == "-")
                {
                    //insert stuff
                    sqlcom.CommandText = "INSERT INTO dbo.Assets (AssetTag, SerialNum, Site, Category, Personnel, PurchaseFrom, PurchaseOrder, Description, Department, Brand, Model) VALUES " +
                                "(@AssetTag ,@SerialNum ,@Site ,@Category ,@Personnel ,@PurchaseFrom ,@PurchaseOrder ,@Description, @Department, @Brand, @Model) ";


                    sqlcom.Parameters.AddWithValue("@AssetTag", txtAssetTag.Text);
                    sqlcom.Parameters.AddWithValue("@SerialNum", txtSN.Text);
                    sqlcom.Parameters.AddWithValue("@Site", ddlFrom.SelectedValue);
                    sqlcom.Parameters.AddWithValue("@Category", ddlCategory.SelectedValue);
                    sqlcom.Parameters.AddWithValue("@Personnel", txtPerson.Text);
                    sqlcom.Parameters.AddWithValue("@PurchaseFrom", txtPF.Text);
                    sqlcom.Parameters.AddWithValue("@PurchaseOrder", txtPO.Text);
                    sqlcom.Parameters.AddWithValue("@Description", txtDescription.Text);
                    sqlcom.Parameters.AddWithValue("@Department", ddlFromDe.SelectedValue);
                    sqlcom.Parameters.AddWithValue("@Brand", ddlBrand.SelectedValue);
                    sqlcom.Parameters.AddWithValue("@Model", txtModel.Text);

                    sqlcom.Connection = conn.objConnection;
                    sqlcom.ExecuteNonQuery();
                    sqlcom.Dispose();

                    //find the newest(highest Auto ID)
                    int AutoIDs = 0;
                    sqlcom = new SqlCommand();
                    sqlcom.CommandText = "SELECT MAX(AutoID) AS highest_value FROM Assets;";
                    sqlcom.Connection = conn.objConnection;
                    dr = sqlcom.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            AutoIDs = dr.GetInt32(0);
                        }
                    }
                    dr.Close();
                    sqlcom.Dispose();

                    //insert creation
                    sqlcom.CommandText = "INSERT INTO dbo.Action_Log (DateAction, Event, FieldChanged, ChangedFrom, ChangedTo, ActionBy, AssetID) VALUES " +
                                "(@DateAction ,@Event ,@FieldChanged ,@ChangedFrom ,@ChangedTo ,@ActionBy ,@AssetID) ";

                    sqlcom.Parameters.AddWithValue("@DateAction", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
                    sqlcom.Parameters.AddWithValue("@Event", "Asset Create");
                    sqlcom.Parameters.AddWithValue("@FieldChanged", "");
                    sqlcom.Parameters.AddWithValue("@ChangedFrom", "");
                    sqlcom.Parameters.AddWithValue("@ChangedTo", "");
                    sqlcom.Parameters.AddWithValue("@ActionBy", user.getGName());
                    sqlcom.Parameters.AddWithValue("@AssetID", AutoIDs);

                    sqlcom.Connection = conn.objConnection;
                    sqlcom.ExecuteNonQuery();
                    sqlcom.Dispose();
                }
                else
                {
                    if (Person == txtPerson.Text && ddlTo.SelectedIndex == 0 && ddlTo.SelectedValue == ddlFrom.SelectedValue)
                    {
                        //nth has changed 
                    }
                    else
                    {
                        if (Person != txtPerson.Text)
                        {
                            //insert to history
                            sqlcom.CommandText = "INSERT INTO dbo.Action_Log (DateAction, Event, FieldChanged, ChangedFrom, ChangedTo, ActionBy, AssetID) VALUES " +
                                "(@DateAction ,@Event ,@FieldChanged ,@ChangedFrom ,@ChangedTo ,@ActionBy ,@AssetID) ";


                            sqlcom.Parameters.AddWithValue("@DateAction", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
                            sqlcom.Parameters.AddWithValue("@Event", "Asset Edit");
                            sqlcom.Parameters.AddWithValue("@FieldChanged", "Personnel");
                            sqlcom.Parameters.AddWithValue("@ChangedFrom", Person);
                            sqlcom.Parameters.AddWithValue("@ChangedTo", txtPerson.Text);
                            sqlcom.Parameters.AddWithValue("@ActionBy", user.getGName());
                            sqlcom.Parameters.AddWithValue("@AssetID", Convert.ToInt32(lblAssetID.Text));

                            sqlcom.Connection = conn.objConnection;
                            sqlcom.ExecuteNonQuery();
                            sqlcom.Dispose();

                            sqlcom.CommandText = "UPDATE dbo.Assets SET " +
                                "Personnel = @Personnel4" +
                                " WHERE AutoID = @AutoID4";

                            sqlcom.Parameters.AddWithValue("@Personnel4", txtPerson.Text);
                            sqlcom.Parameters.AddWithValue("@AutoID4", lblAssetID.Text);

                            sqlcom.Connection = conn.objConnection;
                            sqlcom.ExecuteNonQuery();
                            sqlcom.Dispose();
                        }

                        if (ddlTo.SelectedIndex != 0)
                        {
                            if (ddlTo.SelectedValue != ddlFrom.SelectedValue)
                            {
                                //insert to history
                                sqlcom.CommandText = "INSERT INTO dbo.Action_Log (DateAction, Event, FieldChanged, ChangedFrom, ChangedTo, ActionBy, AssetID) VALUES " +
                                "(@DateAction2 ,@Event2 ,@FieldChanged2 ,@ChangedFrom2 ,@ChangedTo2 ,@ActionBy2 ,@AssetID2) ";


                                sqlcom.Parameters.AddWithValue("@DateAction2", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
                                sqlcom.Parameters.AddWithValue("@Event2", "Asset Edit");
                                sqlcom.Parameters.AddWithValue("@FieldChanged2", "Site");
                                sqlcom.Parameters.AddWithValue("@ChangedFrom2", ddlFrom.SelectedValue);
                                sqlcom.Parameters.AddWithValue("@ChangedTo2", ddlTo.SelectedValue);
                                sqlcom.Parameters.AddWithValue("@ActionBy2", user.getGName());
                                sqlcom.Parameters.AddWithValue("@AssetID2", Convert.ToInt32(lblAssetID.Text));

                                sqlcom.Connection = conn.objConnection;
                                sqlcom.ExecuteNonQuery();
                                sqlcom.Dispose();

                                sqlcom.CommandText = "UPDATE dbo.Assets SET " +
                                " Site = @Site3" +
                                " WHERE AutoID = @AutoID3";

                                sqlcom.Parameters.AddWithValue("@Site3", ddlTo.SelectedValue);
                                sqlcom.Parameters.AddWithValue("@AutoID3", lblAssetID.Text);

                                sqlcom.Connection = conn.objConnection;
                                sqlcom.ExecuteNonQuery();
                                sqlcom.Dispose();
                            }
                        }

                        if (ddlToDe.SelectedIndex != 0)
                        {
                            if (ddlToDe.SelectedValue != ddlFrom.SelectedValue)
                            {
                                //insert to history
                                sqlcom.CommandText = "INSERT INTO dbo.Action_Log (DateAction, Event, FieldChanged, ChangedFrom, ChangedTo, ActionBy, AssetID) VALUES " +
                                "(@DateAction3 ,@Event3 ,@FieldChanged3 ,@ChangedFrom3 ,@ChangedTo3 ,@ActionBy3 ,@AssetID3) ";


                                sqlcom.Parameters.AddWithValue("@DateAction3", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
                                sqlcom.Parameters.AddWithValue("@Event3", "Asset Edit");
                                sqlcom.Parameters.AddWithValue("@FieldChanged3", "Department");
                                sqlcom.Parameters.AddWithValue("@ChangedFrom3", ddlFromDe.SelectedValue);
                                sqlcom.Parameters.AddWithValue("@ChangedTo3", ddlToDe.SelectedValue);
                                sqlcom.Parameters.AddWithValue("@ActionBy3", user.getGName());
                                sqlcom.Parameters.AddWithValue("@AssetID3", Convert.ToInt32(lblAssetID.Text));

                                sqlcom.Connection = conn.objConnection;
                                sqlcom.ExecuteNonQuery();
                                sqlcom.Dispose();

                                sqlcom.CommandText = "UPDATE dbo.Assets SET " +
                                " Department = @Department5" +
                                " WHERE AutoID = @AutoID5";

                                sqlcom.Parameters.AddWithValue("@Department5", ddlToDe.SelectedValue);
                                sqlcom.Parameters.AddWithValue("@AutoID5", lblAssetID.Text);

                                sqlcom.Connection = conn.objConnection;
                                sqlcom.ExecuteNonQuery();
                                sqlcom.Dispose();
                            }
                        }
                        //update stuff


                    }



                }
                conn.Close();

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "MyControls", "$('.modal-backdrop').remove();", true);

                this.reload2();
            }
        }

        protected void reload2()
        {
            Response.Redirect(Request.RawUrl);
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

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)((Control)sender).Parent.Parent;
            int index = gvRow.RowIndex;

            string Autoid = gvTable.Rows[index].Cells[0].Text.ToString();
            string AssetTag = gvTable.Rows[index].Cells[1].Text.ToString();
            string SerialNum = gvTable.Rows[index].Cells[2].Text.ToString();
            string ModalName = "";

            conn.Conn();
            conn.Open();

            
            sqlcom = new SqlCommand();
            sqlcom.CommandText = "Select model From dbo.Assets where AutoID= @ID5";
            sqlcom.Connection = conn.objConnection;
            sqlcom.Parameters.AddWithValue("@ID5", Autoid);
            dr = sqlcom.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ModalName = dr.GetString(0);
                }
            }
            dr.Close();
            sqlcom.Dispose();

            conn.Close();

            ReportDocument myReport = new ReportDocument();

            myReport.Load(Server.MapPath("Reports\\AssetCrystal.rpt"));

            //ParameterFieldDefinitions paramFieldDefs = myReport.DataDefinition.ParameterFields;
            //foreach (ParameterFieldDefinition paramFieldDef in paramFieldDefs)
            //{
            //    ParameterValues currentValues = new ParameterValues();
            //    ParameterDiscreteValue paramDiscreteValue = new ParameterDiscreteValue();
            //    paramDiscreteValue.Value = "YourParameterValue"; // Replace with actual value
            //    currentValues.Add(paramDiscreteValue);
            //    paramFieldDef.ApplyCurrentValues(currentValues);
            //}

            ParameterFieldDefinition PFD;
            ParameterValues PValues;
            ParameterDiscreteValue Parm;



            myReport.Refresh();
            //myReport.DataSourceConnections[0].SetConnection(conn.GetDBInstance(), "FixedAssetSystem", "sa", "P@ssw0rd");

            string filename = Server.MapPath("Report\\") + Autoid + ".pdf";
            myReport.SetParameterValue("ModalName", ModalName);
            myReport.SetParameterValue("SN", SerialNum);
            myReport.SetParameterValue("AssetID", AssetTag);
            myReport.SetParameterValue("AssetBar", "*"+ AssetTag + "*");
            myReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filename);
            myReport.Dispose();


            //myReport.PrintOptions.PrinterName = "TSC TE210";
            //PrintDocument PD = new PrintDocument();
            //for (int i = 0; i <= PD.PrinterSettings.PaperSizes.Count - 1; i++)
            //{
            //    if (PD.PrinterSettings.PaperSizes[i].PaperName == "test01")
            //    {
            //        myReport.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)PD.PrinterSettings.PaperSizes[i].RawKind;
            //        break;
            //    }
            //}

            //rpt.Refresh();

            string url = "Report/" + Autoid + ".pdf"; // Adjust the path as per your application structure

            // Create the script to open the new window with the PDF
            string script = "window.open('" + ResolveUrl("~/" + url) + "', '_blank');";

            // Register the script with the page
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenPDFScript", script, true);
        }
    }
}