using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Build01.Model
{
    public class IssuesDb : DbContext
    {
        public DbSet<Issue> Issues { get; set; }
    }
}