using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MainApp.Models;
using MainApp.EntityFramework;
using Microsoft.EntityFrameworkCore;
using MainApp.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace MainApp.Controllers
{
    public class JobOfferController : Controller
    {
        private readonly DataContext _context;

        public JobOfferController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index([FromQuery(Name = "search")] string searchString)
        {
            Role role = await AuthorizationTools.GetRoleAsync(User, _context);
            ViewData.Add("role", role);
            ViewData.Add("id", AuthorizationTools.GetUserDbId(User, _context, role));
            List<JobOffer> searchResult;
            if (string.IsNullOrEmpty(searchString))
            {
                searchResult = await _context.JobOffers
                    .Include(x => x.HR)
                    .Include(x => x.HR.Company)
                    .ToListAsync();
            }
            else
            {
                searchResult = await _context
                    .JobOffers
                    .Include(x => x.HR)
                    .Include(x => x.HR.Company)
                    .Where(o => o.JobTitle.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                    || o.HR.Company.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    .ToListAsync();
            }
            string email = AuthorizationTools.GetEmail(User);
            if (role == Role.HR)
            {
                JobOfferIndexHRView jobOfferIndexHRView = new JobOfferIndexHRView();
                jobOfferIndexHRView.Offers = searchResult;
                HR us = _context.HRs.Where(h => h.EmailAddress == email).First();
                jobOfferIndexHRView.HR = us;
                return View("IndexHR", jobOfferIndexHRView);
            }
            else if (role == Role.CANDIDATE)
            {
                JobOfferIndexCandidateView jobOfferIndexCandidateView = new JobOfferIndexCandidateView();
                jobOfferIndexCandidateView.Offers = searchResult;
                Candidate us = _context.Candidates.Where(c => c.EmailAddress == email).First();
                jobOfferIndexCandidateView.Candidate = us;
                return View("IndexCandidate", jobOfferIndexCandidateView);
            }
            //role == Role.ADMIN
            return View("IndexAdmin", searchResult);
        }
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            Role role = await AuthorizationTools.GetRoleAsync(User, _context);
            ViewData.Add("role", role);
            ViewData.Add("id", AuthorizationTools.GetUserDbId(User, _context, role));
            if (role == Role.CANDIDATE)
                return new UnauthorizedResult();
            if (id == null)
            {
                return BadRequest($"id shouldn't be null");
            }
            var offer = await _context.JobOffers.FirstOrDefaultAsync(x => x.Id == id.Value);
            if (offer == null)
            {
                return NotFound($"offer not found in DB");
            }
            if (role == Role.HR)
            {
                string email = AuthorizationTools.GetEmail(User);
                HR us = _context.HRs.Where(h => h.EmailAddress == email).First();
                if (us.Id != offer.HR.Id)
                    return new UnauthorizedResult();
            }

            return View(offer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Edit(JobOffer model)
        {
            Role role = await AuthorizationTools.GetRoleAsync(User, _context);
            ViewData.Add("role", role);
            ViewData.Add("id", AuthorizationTools.GetUserDbId(User, _context, role));
            if (role == Role.CANDIDATE)
                return new UnauthorizedResult();

            if (!ModelState.IsValid || (model.SalaryFrom != null && model.SalaryTo != null && model.SalaryFrom > model.SalaryTo))
            {
                return View();
            }

            var offer = await _context.JobOffers.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (role == Role.HR)
            {
                string email = AuthorizationTools.GetEmail(User);
                HR us = _context.HRs.Where(h => h.EmailAddress == email).First();
                if (us.Id != offer.HR.Id)
                    return new UnauthorizedResult();
            }
            offer.JobTitle = model.JobTitle;
            offer.Description = model.Description;
            offer.Location = model.Location;
            offer.SalaryFrom = model.SalaryFrom;
            offer.SalaryTo = model.SalaryTo;
            offer.ValidUntil = model.ValidUntil;
            _context.Update(offer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = model.Id });
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            Role role = await AuthorizationTools.GetRoleAsync(User, _context);
            ViewData.Add("role", role);
            ViewData.Add("id", AuthorizationTools.GetUserDbId(User, _context, role));
            if (role == Role.CANDIDATE)
                return new UnauthorizedResult();

            if (id == null)
            {
                return BadRequest($"id should not be null");
            }
            var offer = await _context.JobOffers.Include(x => x.HR).FirstOrDefaultAsync(x => x.Id == id.Value);
            if (role == Role.HR)
            {
                string email = AuthorizationTools.GetEmail(User);
                HR us = _context.HRs.Where(h => h.EmailAddress == email).First();
                if (us.Id != offer.HR.Id)
                    return new UnauthorizedResult();
            }
            List<Application> apps = await _context.JobApplications.Where(x => x.JobOffer == offer).ToListAsync();
            _context.JobApplications.RemoveRange(apps);
            _context.JobOffers.Remove(offer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize]
        public async Task<ActionResult> Create()
        {
            Role role = await AuthorizationTools.GetRoleAsync(User, _context);
            ViewData.Add("role", role);
            ViewData.Add("id", AuthorizationTools.GetUserDbId(User, _context, role));
            if (role != Role.HR)
                return new UnauthorizedResult();
            var model = new JobOffer();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Create(JobOffer model)
        {
            Role role = await AuthorizationTools.GetRoleAsync(User, _context);
            ViewData.Add("role", role);
            ViewData.Add("id", AuthorizationTools.GetUserDbId(User, _context, role));
            if (role != Role.HR)
                return new UnauthorizedResult();

            if (!ModelState.IsValid || (model.SalaryFrom != null && model.SalaryTo!= null && model.SalaryFrom > model.SalaryTo))
            {
                return View(model);
            }
            string email = AuthorizationTools.GetEmail(User);
            HR us = _context.HRs.Where(h => h.EmailAddress == email).First();
            JobOffer jo = new JobOffer
            {
                Description = model.Description,
                JobTitle = model.JobTitle,
                Location = model.Location,
                SalaryFrom = model.SalaryFrom,
                SalaryTo = model.SalaryTo,
                ValidUntil = model.ValidUntil,
                Created = DateTime.Now,
                HR = us
            };

            await _context.JobOffers.AddAsync(jo);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var offer = await _context.JobOffers
                .Include(x => x.HR)
                .Include(x => x.HR.Company)
                .FirstOrDefaultAsync(x => x.Id == id);
            Role role = await AuthorizationTools.GetRoleAsync(User, _context);
            ViewData.Add("role", role);
            ViewData.Add("id", AuthorizationTools.GetUserDbId(User, _context, role));
            if (role == Role.HR)
            {
                JobOfferDetailsHRView jobOfferDetailsHRView = new JobOfferDetailsHRView();
                jobOfferDetailsHRView.Offer = offer;
                string email = AuthorizationTools.GetEmail(User);
                HR us = _context.HRs.Where(h => h.EmailAddress == email).First();
                jobOfferDetailsHRView.HR = us;
                jobOfferDetailsHRView.Applications = await _context.JobApplications.Where(ja => ja.JobOffer == offer).ToListAsync();
                return View("DetailsHR", jobOfferDetailsHRView);
            }
            if (role == Role.ADMIN)
                return View("DetailsAdmin", offer);
            return View("DetailsCandidate", offer);
        }
    }
}
