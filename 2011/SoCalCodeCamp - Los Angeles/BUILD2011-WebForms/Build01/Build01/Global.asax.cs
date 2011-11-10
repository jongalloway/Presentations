using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using Build01.Model;

namespace Build01
{
    public class Global : System.Web.HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Set the EF database initializer
            Database.SetInitializer(new IssuesDbInitializer());

            // Overwrite the default jQuery script definition
            ScriptManager.ScriptResourceMapping.AddDefinition("jquery", typeof(Page).Assembly, new ScriptResourceDefinition
            {
                Path = "~/Scripts/jquery-1.6.3.min.js",
                DebugPath = "~/Scripts/jquery-1.6.3.js",
                CdnPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.6.3.min.js",
                CdnDebugPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.6.3.js",
                CdnSupportsSecureConnection = true
            });
        }
    }
}