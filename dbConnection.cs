using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for dbConnection
/// </summary>
public class dbConnection
{
    private static string str = "";
    public SqlConnection objConnection = new SqlConnection(str);
    private static string instance = "cent-sql\\DEV";
    private static string instance2 = "";

    private static string user = "sa";
    private static string password = "P@ssw0rd";

    private static string gconnectionString = "FixedAssetSystem";
    private static string gconnectionString2 = "master";

    public static string gcon = gconnectionString;

    public dbConnection()
    {

    }

    public SqlConnection Conn()
    {
        str = "Server=" + instance + ";user id=" + user + ";password=" + password + ";initial catalog=" + gconnectionString + ";MultipleActiveResultSets=true";
        objConnection = new SqlConnection(str);
        return objConnection;
    }

    public SqlConnection Conn2()
    {
        str = "Server=" + instance + ";user id=" + user + ";password=" + password + ";initial catalog=" + gconnectionString2 + ";MultipleActiveResultSets=true";
        objConnection = new SqlConnection(str);
        return objConnection;
    }



    public void Open()
    {
        objConnection.Open();
    }

    public void Close()
    {
        objConnection.Close();
    }

    public void Path(string path)
    {
        str = path;
    }

    public void ReadDB()
    {
        String line = "";

        System.IO.StreamReader file = new System.IO.StreamReader(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "/DB.INI");
        while ((line = file.ReadLine()) != null)
        {
            instance = line;
        }

        file.Close();
    }

    public String GetDBInstance()
    {
        return instance;
    }

    public String GetCompanyName(String connection)
    {
        String company = "";

        return company;
    }

    public void SetGconnectionstring(String gconnection)
    {
        gconnectionString = gconnection;
    }
}