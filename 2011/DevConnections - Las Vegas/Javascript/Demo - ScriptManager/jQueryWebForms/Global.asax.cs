using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;

namespace jQueryWebForms {
    public class Global : System.Web.HttpApplication {

        void Application_Start(object sender, EventArgs e) {
            
            ScriptManager.ScriptResourceMapping.AddDefinition("jquery", new ScriptResourceDefinition {
                Path = "~/Scripts/jquery-1.6.4.min.js",
                DebugPath = "~/Scripts/jquery-1.6.4.js",
                CdnPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.6.4.min.js",
                CdnDebugPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.6.4.js",
                CdnSupportsSecureConnection = true
            });
            
        }

    }
}
