using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MainApp.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        [MinLength(1)]
        [Display(Name = "Text")]
        public string Text { get; set; }
        public Application Application { get; set; }
    }
}
