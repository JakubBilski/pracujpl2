using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MainApp.Models
{
    public class HR
    {
        public int Id { get; set; }
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Email address")]
        public string EmailAddress { get; set; }
        public virtual Company Company { get; set; }
        public virtual int CompanyId { get; set; }
        public List<JobOffer> JobOffers { get; set; } = new List<JobOffer>();
    }
}
