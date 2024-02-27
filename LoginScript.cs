using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices.Protocols;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Net;

/// <summary>
/// Summary description for Login
/// </summary>
public class LoginScript
{
    public LoginScript()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public String CheckLogin(String name, String pw)
    {
        String result = "";
        try
        {
            String server = "LDAP://smsummit";
            DirectoryEntry entry = new DirectoryEntry(server, name, pw);
            object nativeObject = entry.NativeObject;
            UserPrincipal up = null;

            result = entry.Name;
        }
        catch (DirectoryServicesCOMException cex)
        {
            result = cex.Message;
        }
        catch (Exception ex)
        {
            result = ex.ToString();
        }

        return result;
    }

    public User CheckLoginSMSummit2(String name, String pw)
    {
        User user = new User();
        String result = "";
        try
        {
            PrincipalContext pc = new PrincipalContext(ContextType.Domain, "smsummit.com.sg");
            bool valid = pc.ValidateCredentials(name, pw, ContextOptions.Negotiate);
            UserPrincipal up = null;
            up = UserPrincipal.FindByIdentity(pc, name);

            result = up.DisplayName;
            user.setGEmail(up.EmailAddress);
            user.setGName(up.DisplayName);
        }
        catch (DirectoryServicesCOMException cex)
        {
            result = cex.Message;
        }
        catch (Exception ex)
        {
            result = ex.ToString();
        }

        return user;
    }

    public Boolean CheckLoginSMSummit(String name, String pw)
    {
        bool result = false;
        try
        {
            PrincipalContext pc = new PrincipalContext(ContextType.Domain, "smsummit.com.sg");
            result = pc.ValidateCredentials(name, pw, ContextOptions.Negotiate);

        }
        catch (DirectoryServicesCOMException cex)
        {
            result = false;
        }
        catch (Exception ex)
        {
            result = false;
        }

        return result;
    }

    public User CheckLoginCP2(String name, String pw)
    {
        User user = new User();
        String result = "";
        try
        {
            PrincipalContext pc = new PrincipalContext(ContextType.Domain, "192.168.1.201", "centurion\administrator", "Cen88turion");
            bool valid = pc.ValidateCredentials(name, pw, ContextOptions.Negotiate);
            UserPrincipal up = null;
            //up = UserPrincipal.FindByIdentity(pc, name);

            //result = up.DisplayName;
            //user.setGEmail(up.EmailAddress);
            user.setGEmail(name);
            //user.setGUser(up.DisplayName);
        }
        catch (DirectoryServicesCOMException cex)
        {
            result = cex.Message;
        }
        catch (Exception ex)
        {
            result = ex.ToString();
        }

        return user;
    }

    public Boolean CheckLoginCP(String name, String pw)
    {
        bool result = false;
        try
        {
            PrincipalContext pc = new PrincipalContext(ContextType.Domain, "192.168.1.201", "centurion\administrator", "Cen88turion");
            result = pc.ValidateCredentials(name, pw, ContextOptions.Negotiate);

        }
        catch (DirectoryServicesCOMException cex)
        {
            result = false;
        }
        catch (Exception ex)
        {
            result = false;
        }

        return result;
    }

    public String GetUserName(String name)
    {
        User user = new User();
        String result = "";
        try
        {
            PrincipalContext pc = new PrincipalContext(ContextType.Domain, "smsummit.com.sg");
            UserPrincipal up = null;
            up = UserPrincipal.FindByIdentity(pc, name);

            result = up.DisplayName;
        }
        catch (DirectoryServicesCOMException cex)
        {
            result = cex.Message;
        }
        catch (Exception ex)
        {
            result = ex.ToString();
        }

        return result;
    }
}