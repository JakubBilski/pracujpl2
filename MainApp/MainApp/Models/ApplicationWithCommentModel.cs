using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MainApp.Models
{
    public class ApplicationWithComment : Application
    {
        public ApplicationWithComment() : base()
        {

        }
        public ApplicationWithComment(Application app)
        {
            this.Candidate = app.Candidate;
            this.Comments = app.Comments;
            this.ContactAgreement = app.ContactAgreement;
            this.CvUrl = app.CvUrl;
            this.EmailAddress = app.EmailAddress;
            this.FirstName = app.FirstName;
            this.Id = app.Id;
            this.JobOffer = app.JobOffer;
            this.LastName = app.LastName;
            this.PhoneNumber = app.PhoneNumber;
            this.State = app.State;
            this.CommentText = "";
        }
        public string CommentText { get; set; }
    }
}
