using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Build01.Model
{
    public class Issue
    {
        public int ID { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; }

        [Required, StringLength(4000)]
        public string Description { get; set; }

        [Required, StringLength(50)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

        public Issue()
        {
            CreatedOn = DateTime.Now;
        }
    }
}