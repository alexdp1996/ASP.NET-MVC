using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Notes.Models
{
    public class Note
    {
        public string Id { get; set; }

        
        [Required]
        public string Caption { get; set; }

        public string Text { get; set; }

        [Display(Name = "Status")]
        public bool IsDone { get; set; }
    }
}