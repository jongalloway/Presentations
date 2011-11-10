using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Build01.Model
{
    public class IssuesDbInitializer : DropCreateDatabaseIfModelChanges<IssuesDb>
    {
        protected override void Seed(IssuesDb context)
        {
            context.Issues.Add(new Issue { CreatedBy = "Damian", CreatedOn = new DateTime(2011, 1, 26), Title = "Something is wrong", Description = "When I tried to do this thing, something went wrong. Sorry don't have any more details than that." });
            context.Issues.Add(new Issue { CreatedBy = "Fred", CreatedOn = new DateTime(2011, 5, 23), Title = "Can you fix it please?", Description = "Make it all work perfectly. How hard can that be?" });
            context.Issues.Add(new Issue { CreatedBy = "David", CreatedOn = new DateTime(2011, 3, 3), Title = "I think this would be a great feature, please add it", Description = "Make it so when I click on the button that other text on the other side of the form updates to say something different. That would totally rock!" });
            context.Issues.Add(new Issue { CreatedBy = "Barry", CreatedOn = new DateTime(2011, 8, 26), Title = "So, umm, like, the computer gave me this error", Description = "I logged in and like, did what I normally do and it just gave me this strange error message, that was like, FAIL, or whatever. You should totally fix that." });
            context.Issues.Add(new Issue { CreatedBy = "Jon", CreatedOn = new DateTime(2011, 10, 10), Title = "I'm just happy to be here!", Description = "Is this where I go to say hi? Hi!!!" });
        }
    }
}