using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices.Protocols;
using System.DirectoryServices;
using System.Net;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Web.Security;
namespace FixedAssetSystem
{
    public partial class Login : System.Web.UI.Page
    {
        dbConnection conn = new dbConnection();
        SqlCommand sqlcom = new SqlCommand();
        SqlDataAdapter sqlada = new SqlDataAdapter();
        SqlDataReader dr;
        User user = new User();
        LoginScript loginScript = new LoginScript();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (tbxUsername.Text.Trim() == "" || tbxPassword.Text.Trim() == "")
            {
                tbxUsername.Focus();
                lblPromptTitle.Text = "Login";
                lblDisplayError.Text = "Please enter your user ID, password and choose a site!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { openErrorModal(); });", true);
            }
            else
            {
                bool emailFormat = Regex.IsMatch(tbxUsername.Text.Trim(),
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase);
                if (emailFormat == false)
                {
                    lblPromptTitle.Text = "Login";
                    lblDisplayError.Text = "Please login using your email address and password.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { openErrorModal(); });", true);
                }
                else
                {
                    bool result = loginScript.CheckLoginSMSummit(tbxUsername.Text.Trim(), tbxPassword.Text);
                    if (result == true) //AD Login
                    {
                        user = new User();
                        user = loginScript.CheckLoginSMSummit2(tbxUsername.Text.Trim(), tbxPassword.Text);
                        bool userExist = false;
                        string username = "";
                        string userEmail = "";
                        string userType = "";
                        string userActive = "";
                        int dormID = 0;
                        int userAutoID = 0;

                        conn.Conn();
                        conn.Open();
                        sqlcom = new SqlCommand();
                        sqlcom.CommandText = "select * from User_Table where UserEmail = @EmailAddress";
                        sqlcom.Parameters.AddWithValue("@EmailAddress", SqlDbType.NVarChar).Value = tbxUsername.Text.Trim();
                        sqlcom.Connection = conn.objConnection;
                        dr = sqlcom.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                userExist = true;
                                userAutoID = dr.GetInt32(0);
                                userEmail = dr.GetString(2);
                                username = dr.GetString(1);
                                userType = dr.GetString(3);
                                userActive = dr.GetString(4);
                            }
                        }

                        dr.Close();
                        sqlcom.Dispose();

                        conn.Close();

                        //Check active status
                        if (userActive == "ACTIVE")
                        {
                            Session["User"] = user;
                            user.setGAutoID(Convert.ToString(userAutoID));
                            user.setGEmail(tbxUsername.Text);
                            user.setGName(username);
                            user.setGType(userType);
                            user.setGSiteID(Convert.ToString(dormID));

                            Response.Redirect("~/Dashboard.aspx", true);
                        }
                        else
                        {
                            lblPromptTitle.Text = "Login : Access Denied.";
                            lblDisplayError.Text = "Your account has been deactivated or you do not have access to this site";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { openErrorModal(); });", true);
                        }
                    }
                    else
                    {
                        lblPromptTitle.Text = "Login fail";
                        lblDisplayError.Text = "Please check your ID and password.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { openErrorModal(); });", true);
                    }
                }


            }
        }

    }
}