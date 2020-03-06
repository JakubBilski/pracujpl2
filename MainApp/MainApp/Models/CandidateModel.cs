using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace MainApp.Models
{
    public class Candidate
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Email address")]
        public string EmailAddress { get; set; }
        public List<Application> Applications { get; set; } = new List<Application>();
    }
}
