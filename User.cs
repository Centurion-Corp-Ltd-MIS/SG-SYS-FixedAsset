using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for User
/// </summary>
public class User
{
    //******** User Details ***********
    private String gAutoID;
    private String gName;
    private String gEmail;
    private String gType;
    private String gSiteID;
    //**********************************

    public User()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void setGAutoID(String autoID)
    {
        gAutoID = autoID;
    }

    public String getGAutoID()
    {
        return gAutoID;
    }

    public void setGName(String name)
    {
        gName = name;
    }

    public String getGName()
    {
        return gName;
    }

    public void setGEmail(String email)
    {
        gEmail = email;
    }

    public String getGEmail()
    {
        return gEmail;
    }

    public void setGType(String viewType)
    {
        gType = viewType;
    }

    public String getGType()
    {
        return gType;
    }

    public void setGSiteID(String viewSiteID)
    {
        gSiteID = viewSiteID;
    }

    public String getGSiteID()
    {
        return gSiteID;
    }
}