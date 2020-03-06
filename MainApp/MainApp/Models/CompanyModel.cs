using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MainApp.Models
{
    public class Company
    {
        public int Id { get; set; }
        [Required]
        [MinLength(1)]
        public string Name { get; set; }
        public List<HR> HRs { get; set; } = new List<HR>();
    }
}
