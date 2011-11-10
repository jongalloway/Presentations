using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Scaffolding.Models
{
    public class Album
    {
        public int AlbumID { get; set; }
        public string Title { get; set; }
        public int GenreID { get; set; }
        public virtual Genre Genre { get; set; }
    }
}