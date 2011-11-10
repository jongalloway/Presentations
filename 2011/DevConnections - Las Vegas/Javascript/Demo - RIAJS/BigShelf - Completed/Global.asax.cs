using System;
using System.Web.Security;

namespace BigShelf
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup

            // Check if users exist, if not create them
            if (Membership.GetAllUsers().Count == 0)
            {
                MembershipCreateStatus status;
                Membership.CreateUser("demo@microsoft.com", "abc123", "demo@microsoft.com", "To be?", "Or not to be?", true, Guid.Parse("3730F12D-1A2A-4499-859B-9F586B2858A4"), out status);
                Membership.CreateUser("deepm@microsoft.com", "abc123", "deepm@microsoft.com", "To be?", "Or not to be?", true, Guid.Parse("5ad7976c-7a95-47aa-87ba-29c3fc80643e"), out status);
                Membership.CreateUser("jeffhand@microsoft.com", "abc123", "jeffhand@microsoft.com", "To be?", "Or not to be?", true, Guid.Parse("9330619d-9a8c-4269-9bde-ba4cb1b7b354"), out status);
                Membership.CreateUser("yavorg@microsoft.com", "abc123", "yavorg@microsoft.com", "To be?", "Or not to be?", true, Guid.Parse("1a481f1e-1b22-4ae3-a75d-d5fa664d4085"), out status);
                Membership.CreateUser("howard@microsoft.com", "abc123", "howard@microsoft.com", "To be?", "Or not to be?", true, Guid.Parse("5df20314-4c44-44ad-9847-143f28f85bf5"), out status);
                Membership.CreateUser("gblock@microsoft.com", "abc123", "gblock@microsoft.com", "To be?", "Or not to be?", true, Guid.Parse("990c62c5-30a4-4ca9-92c5-f248241f6d44"), out status);
                Membership.CreateUser("joconnol@microsoft.com", "abc123", "jconnol@microsoft.com", "To be?", "Or not to be?", true, Guid.Parse("3e82b681-81ce-4c87-856a-61381d4ad0e8"), out status);
                Membership.CreateUser("brado@microsoft.com", "abc123", "brado@microsoft.com", "To be?", "Or not to be?", true, Guid.Parse("9fc20e29-91b1-47c0-8054-5624ac87c5d8"), out status);
            }
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
