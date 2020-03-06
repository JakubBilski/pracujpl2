using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApp.BlobStorage;
using MainApp.Authorization;
using MainApp.EntityFramework;
using MainApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.IO;

namespace MainApp.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly DataContext _context;
        private readonly BlobStorageService _blob;
        private readonly IHostingEnvironment _env;

        public ApplicationController(DataContext context, BlobStorageService blob, IHostingEnvironment env)
        {
            _context = context;
            _blob = blob;
            _env = env;
        }

        /// GET Application/Index
        /// <summary>
        /// Shows list of applications
        /// </summary>
        /// <remarks>
        /// Shows list of applications proper for user role.
        /// </remarks>
        /// <param name="searchString">String containing search terms from View</param>
        /// <returns>List of applications</returns>
        [Route("Index")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index([FromQuery(Name = "search")] string searchString)
        {
            Role role = await AuthorizationTools.GetRoleAsync(User, _context);
            ViewData.Add("role", role);
            ViewData.Add("id", AuthorizationTools.GetUserDbId(User, _context, role));
            List<Application> searchResult;

            if (string.IsNullOrEmpty(searchString))
                searchResult = await _context.JobApplications
                     .Include(x => x.JobOffer)
                     .Include(x => x.JobOffer.HR)
                     .Include(x => x.JobOffer.HR.Company)
                     .Include(x => x.Comments)
                     .ToListAsync();
            else
            {
                searchResult = await _context
                    .JobApplications
                    .Include(x => x.JobOffer)
                    .Include(x => x.JobOffer.HR)
                    .Include(x => x.JobOffer.HR.Company)
                    .Include(x => x.Comments)
                    .Where(o => o.LastName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    .ToListAsync();
            }
            if (role == Role.HR)
            {
                string email = AuthorizationTools.GetEmail(User);
                HR us = _context.HRs.Where(h => h.EmailAddress == email).First();
                searchResult = searchResult.Where(a => a.JobOffer.HR == us).ToList();
                return View("IndexHR", searchResult);
            }
            else if (role == Role.CANDIDATE)
            {
                string email = AuthorizationTools.GetEmail(User);
                Candidate us = _context.Candidates.Where(c => c.EmailAddress == email).First();
                searchResult = searchResult.Where(a => a.Candidate == us).ToList();
                return View("IndexCandidate", searchResult);
            }
            return View("IndexAdmin", searchResult);
        }

        
        [Authorize]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult> Create(int id)
        {
            Role role = await AuthorizationTools.GetRoleAsync(User, _context);
            ViewData.Add("role", role);
            ViewData.Add("id", AuthorizationTools.GetUserDbId(User, _context, role));

            if (role != Role.CANDIDATE)
                return new UnauthorizedResult();
            JobOffer offer = _context.JobOffers.Where(o => o.Id == id).First();
            string email = AuthorizationTools.GetEmail(User);
            Candidate candidate = _context.Candidates.Where(c => c.EmailAddress == email).First();
            var model = new Application()
            {
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                PhoneNumber = candidate.PhoneNumber,
                CvUrl = "TODO",
                EmailAddress = email,
                JobOffer = offer,
                Candidate = candidate
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Create([Bind("Id, FirstName, LastName, PhoneNumber, EmailAddress, ContactAgreement, CvUrl, File, State, JobOffer, Candidate")] Application model)
        {
            Role role = await AuthorizationTools.GetRoleAsync(User, _context);
            ViewData.Add("role", role);
            ViewData.Add("id", AuthorizationTools.GetUserDbId(User, _context, role));

            if (role != Role.CANDIDATE)
                return new UnauthorizedResult();
            JobOffer offer = _context.JobOffers.Include(x=>x.HR).Where(o => o.Id == model.Id).First();
            string email = AuthorizationTools.GetEmail(User);
            Candidate candidate = _context.Candidates.Where(c => c.EmailAddress == email).First();
            if (!ModelState.IsValid)
            {
                return View();
            }

            var uploads = Path.Combine(_env.WebRootPath, "uploads");
            bool exists = Directory.Exists(uploads);
            if (!exists)
               Directory.CreateDirectory(uploads);            
            var fileName = Path.GetFileName(model.File.FileName);
            var fileStream = new FileStream(Path.Combine(uploads, model.File.FileName), FileMode.Create);
            string mimeType = model.File.ContentType;
            byte[] fileData = null;
            using (var memoryStream = new MemoryStream())
            {
               model.File.CopyTo(memoryStream);
               fileData = memoryStream.ToArray();
            }            
            model.CvUrl = _blob.UploadFileToBlob(model.File.FileName, fileData, mimeType);

            Application application = new Application()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                EmailAddress = model.EmailAddress,
                PhoneNumber = model.PhoneNumber,
                CvUrl = model.CvUrl,
                ContactAgreement = model.ContactAgreement,
                Comments = model.Comments,
                Candidate = candidate,
                JobOffer = offer,
                State = "Pending"
            };


            await _context.JobApplications.AddAsync(application);
            await _context.SaveChangesAsync();

            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("290444@pw.edu.pl", "HRMiniApp");
            var subject = "New application";
            var to = new EmailAddress(offer.HR.EmailAddress);
            var plainTextContent = "";
            var htmlContent = $"<strong>Your Job Offer received new Application: <a href=https://localhost:5001/Application/Details/{ model.Id}>link</a> </strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var attachment = new Attachment()
            {
                Content = Convert.ToBase64String(fileData),
                Filename = "cv.pdf"
            };
            msg.AddAttachment(attachment);
            var response = await client.SendEmailAsync(msg);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreateCommentAjax(ApplicationWithComment model)
        {
            Role role = await AuthorizationTools.GetRoleAsync(User, _context);
            ViewData.Add("role", role);
            ViewData.Add("id", AuthorizationTools.GetUserDbId(User, _context, role));
            if(model.CommentText == "")
                RedirectToAction("Details", new { id = model.Id });
            if (role != Role.HR)
                return new UnauthorizedResult();
            Application app = _context.JobApplications
                .Include(x => x.Comments)
                .Include(x => x.JobOffer)
                .Include(x => x.JobOffer.HR)
                .Where(a => a.Id == model.Id).FirstOrDefault();
            string email = AuthorizationTools.GetEmail(User);
            HR hr = _context.HRs.Where(c => c.EmailAddress == email).First();
            if(app.JobOffer.HR != hr)
                return new UnauthorizedResult();

            Comment comm = new Comment() { Text = model.CommentText, Application = app };

            await _context.Comments.AddAsync(comm);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = model.Id });
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> DeleteCommentAjax(int id)
        {
            Role role = await AuthorizationTools.GetRoleAsync(User, _context);
            ViewData.Add("role", role);
            ViewData.Add("id", AuthorizationTools.GetUserDbId(User, _context, role));

            if (role != Role.HR)
                return new UnauthorizedResult();
            Comment comment = _context.Comments
                .Include(x => x.Application)
                .Include(x => x.Application.JobOffer)
                .Include(x => x.Application.JobOffer.HR)
                .Where(a => a.Id == id).FirstOrDefault();
            if (comment == null)
                return new UnauthorizedResult();
            Application app = comment.Application;
            string email = AuthorizationTools.GetEmail(User);
            HR hr = _context.HRs.Where(c => c.EmailAddress == email).First();
            if (comment.Application.JobOffer.HR != hr)
                return new UnauthorizedResult();

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            
            return RedirectToAction("Details", new { id = app.Id });
        }

        [Authorize]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest($"id shouldn't not be null");
            }
            string email = AuthorizationTools.GetEmail(User);
            Candidate us = _context.Candidates.Where(h => h.EmailAddress == email).FirstOrDefault();
            var app = await _context.JobApplications.FirstOrDefaultAsync(x => x.Id == id.Value);
            if (us == null || app == null || app.State!="Pending" || us.Id != app.Candidate.Id)
                return new UnauthorizedResult();

            return View(app);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Edit(Application model)
        {
            string email = AuthorizationTools.GetEmail(User);
            Candidate us = _context.Candidates.Where(h => h.EmailAddress == email).FirstOrDefault();
            var app = await _context.JobApplications.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (us == null || app == null || app.State != "Pending" || us.Id != app.Candidate.Id)
                return new UnauthorizedResult();

            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}
            app.FirstName = model.FirstName;
            app.LastName = model.LastName;
            app.PhoneNumber = model.PhoneNumber;
            app.EmailAddress = model.EmailAddress;
            app.ContactAgreement = model.ContactAgreement;
            app.CvUrl = model.CvUrl;
            _context.Update(app);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = model.Id });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            Role role = await AuthorizationTools.GetRoleAsync(User, _context);
            ViewData.Add("role", role);
            ViewData.Add("id", AuthorizationTools.GetUserDbId(User, _context, role));
            string email = AuthorizationTools.GetEmail(User);

            if (role == Role.ADMIN)
                return new UnauthorizedResult();

            Application app = _context.JobApplications
                .Include(x => x.JobOffer)
                .Include(x => x.Candidate)
                .Include(x => x.JobOffer.HR)
                .Include(x => x.JobOffer.HR.Company)
                .Include(x => x.Comments)
                .Where(a => a.Id == id)
                .FirstOrDefault();
            if (app == null)
                return new NotFoundResult();
            if(role == Role.HR)
            {
                HR us = _context.HRs.Where(c => c.EmailAddress == email).FirstOrDefault();
                if (us == null || us.Id != app.JobOffer.HR.Id)
                    return new UnauthorizedResult();
                ApplicationWithComment appWithComm = new ApplicationWithComment(app);
                return View("DetailsHR", appWithComm);
            }
            else
            {
                Candidate us = _context.Candidates.Where(c => c.EmailAddress == email).FirstOrDefault();
                if (us == null || us.Id != app.Candidate.Id)
                    return new UnauthorizedResult();

                return View("DetailsCandidate", app);
            }
        }

        [HttpPost, ActionName("Accept")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> AcceptConfirmed(int id)
        {
            Role role = await AuthorizationTools.GetRoleAsync(User, _context);
            ViewData.Add("role", role);
            ViewData.Add("id", AuthorizationTools.GetUserDbId(User, _context, role));
            string email = AuthorizationTools.GetEmail(User);
            if (role != Role.HR)
                return new UnauthorizedResult();
            Application app = _context.JobApplications
                .Include(x => x.JobOffer)
                .Include(x => x.Candidate)
                .Include(x => x.JobOffer.HR)
                .Include(x => x.JobOffer.HR.Company)
                .Include(x => x.Comments)
                .Where(a => a.Id == id)
                .FirstOrDefault();
            if (app == null)
            {
                return NotFound();
            }
            HR us = _context.HRs.Where(c => c.EmailAddress == email).FirstOrDefault();
            if (us == null || app.State != "Pending" || us.Id != app.JobOffer.HR.Id)
                return new UnauthorizedResult();
            app.State = "Accepted";
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Reject")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> RejectConfirmed(int id)
        {
            Role role = await AuthorizationTools.GetRoleAsync(User, _context);
            ViewData.Add("role", role);
            ViewData.Add("id", AuthorizationTools.GetUserDbId(User, _context, role));
            string email = AuthorizationTools.GetEmail(User);
            if (role != Role.HR)
                return new UnauthorizedResult();
            Application app = _context.JobApplications
                .Include(x => x.JobOffer)
                .Include(x => x.Candidate)
                .Include(x => x.JobOffer.HR)
                .Include(x => x.JobOffer.HR.Company)
                .Include(x => x.Comments)
                .Where(a => a.Id == id)
                .FirstOrDefault();
            if (app == null)
            {
                return NotFound();
            }
            HR us = _context.HRs.Where(c => c.EmailAddress == email).FirstOrDefault();
            if (us == null || app.State != "Pending" || us.Id != app.JobOffer.HR.Id)
                return new UnauthorizedResult();
            app.State = "Rejected";
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        
        [Authorize]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult> Delete(int? id)
        {
            Role role = await AuthorizationTools.GetRoleAsync(User, _context);
            ViewData.Add("role", role);
            ViewData.Add("id", AuthorizationTools.GetUserDbId(User, _context, role));
            string email = AuthorizationTools.GetEmail(User);
            if (role != Role.CANDIDATE)
                return new UnauthorizedResult();
            if (id == null)
            {
                return NotFound();
            }
            Application app = _context.JobApplications
                .Include(x => x.JobOffer)
                .Include(x => x.Candidate)
                .Include(x => x.JobOffer.HR)
                .Include(x => x.JobOffer.HR.Company)
                .Include(x => x.Comments)
                .Where(a => a.Id == id)
                .FirstOrDefault();
            if (app == null)
            {
                return NotFound();
            }
            Candidate us = _context.Candidates.Where(c => c.EmailAddress == email).FirstOrDefault();
            if (us == null || app.State != "Pending" || us.Id != app.Candidate.Id)
                return new UnauthorizedResult();
            return View(app);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Role role = await AuthorizationTools.GetRoleAsync(User, _context);
            ViewData.Add("role", role);
            ViewData.Add("id", AuthorizationTools.GetUserDbId(User, _context, role));
            string email = AuthorizationTools.GetEmail(User);
            if (role != Role.CANDIDATE)
                return new UnauthorizedResult();
            Application app = _context.JobApplications
                .Include(x => x.JobOffer)
                .Include(x => x.Candidate)
                .Include(x => x.JobOffer.HR)
                .Include(x => x.JobOffer.HR.Company)
                .Include(x => x.Comments)
                .Where(a => a.Id == id)
                .FirstOrDefault();
            if (app == null)
            {
                return NotFound();
            }
            Candidate us = _context.Candidates.Where(c => c.EmailAddress == email).FirstOrDefault();
            if (us == null || app.State != "Pending" || us.Id != app.Candidate.Id)
                return new UnauthorizedResult();
            _context.JobApplications.Remove(app);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
