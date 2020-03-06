using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MainApp.Models
{
    public class Application
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
        [Required]
        [Display(Name = "Contact agreement")]
        public bool ContactAgreement { get; set; }
        public string CvUrl { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "You must provide CV")]
        public IFormFile File { get; set; }
        public string State { get; set; }
        public Candidate Candidate { get; set; }
        public JobOffer JobOffer { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
