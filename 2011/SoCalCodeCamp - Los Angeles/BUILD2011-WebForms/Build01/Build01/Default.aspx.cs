using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web.ModelBinding;
using System.Web.UI;
using Build01.Model;

namespace Build01
{
    public partial class _Default : System.Web.UI.Page
    {
        private IssuesDb _db = new IssuesDb();

        protected void Page_Load()
        {
            createdByMe.Visible = User.Identity.IsAuthenticated;
        }

        // <asp:GridView ID="issuesGrid" UpdateMethod="GetIssues" />
        public IQueryable<Issue> GetIssues([Control]DateTime? createdSince, 
                        [Control]bool? createdByMe, [CurrentUser]string currentUser)
        {
            IQueryable<Issue> query = _db.Issues;

            if (createdSince.HasValue)
            {
                query = query.Where(i => i.CreatedOn >= createdSince.Value);
            }
            if (createdByMe.HasValue && createdByMe.Value == true && !String.IsNullOrEmpty(currentUser))
            {
                query = query.Where(i => i.CreatedBy == currentUser);
            }

            return query;
        }

        // <asp:FormView ID="issueView" SelectMethod="GetIssue" />
        public Issue GetIssue([Control("issuesGrid")]int? id)
        {
            return _db.Issues.Find(id);
        }

        // <asp:FormView ID="issueView" InsertMethod="InsertIssue" />
        public void InsertIssue([CurrentUser]string currentUser)
        {
            if (String.IsNullOrEmpty(currentUser))
            {
                ModelState.AddModelError("", "You must be signed in to create new issues");
                return;
            }

            var issue = new Issue { CreatedBy = currentUser };

            TryUpdateModel(issue);

            if (ModelState.IsValid)
            {
                _db.Issues.Add(issue);
                SaveChanges(issue);
            }
        }

        // <asp:FormView ID="issueView" UpdateMethod="UpdateIssue" />
        public void UpdateIssue(int id, byte[] timestamp, [CurrentUser]string currentUser, [Control("ctl00$MainContent$issueForm$forceSave")]bool? forceSave)
        {
            if (String.IsNullOrEmpty(currentUser))
            {
                ModelState.AddModelError("", "You must be signed in to update issues");
                return;
            }

            var issue = _db.Issues.Find(id);

            TryUpdateModel(issue);
            if (forceSave.HasValue && !forceSave.Value)
            {
                // Set original timestamp value so EF can detect optimistic concurrency violation
                _db.Entry(issue).Property(i => i.Timestamp).OriginalValue = timestamp;
            }

            if (ModelState.IsValid)
            {
                SaveChanges(issue);
            }
        }

        protected void issueForm_PreRender(object source, EventArgs e)
        {
            var forceSave = issueForm.FindControl("forceSave");
            if (forceSave != null)
            {
                forceSave.Visible = ModelState["OptimisticConcurrencyError"] != null;
            }
        }

        private void SaveChanges(Issue issue)
        {
            try
            {
                _db.SaveChanges();
                Response.Redirect("~/");
            }
            catch (DbUpdateException ex)
            {
                if (!HandleDbUpdateException(ex, ModelState, issue))
                {
                    throw ex;
                }
            }
        }

        private static bool HandleDbUpdateException(DbUpdateException ex, ModelStateDictionary modelState, Issue issue)
        {
            var baseEx = ex.GetBaseException();
            if (baseEx is OptimisticConcurrencyException)
            {
                // Concurrency violation
                modelState.AddModelError("OptimisticConcurrencyError", "The issue was changed while you were editing it. Check the \"Force save changes\" box to override the latest values.");
                return true;
            }
            var sqlEx = baseEx as SqlException;
            if (sqlEx != null && sqlEx.Number == 2601 && sqlEx.Message.Contains("IDX_Issues_Title"))
            {
                // Unique index violation
                modelState.AddModelError("Title", String.Format("An issue with the title \"{0}\" already exists", issue.Title));
                return true;
            }
            return false;
        }
    }
}