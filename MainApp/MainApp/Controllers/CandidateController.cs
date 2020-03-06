using System;
using System.Collections.Generic;
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
    public class CandidateController : Controller
    {
        private readonly DataContext _context;

        public CandidateController(DataContext context)
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
            if (role != Role.ADMIN)
                return new UnauthorizedResult();
            if (string.IsNullOrEmpty(searchString))
                return View(await _context.Candidates.ToListAsync());

            List<Candidate> searchResult = await _context
                .Candidates
                .Where(o => o.LastName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();

            return View(searchResult);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> IndexAjax([FromQuery(Name = "search")] string searchString)
        {
            Role role = await AuthorizationTools.GetRoleAsync(User, _context);
            ViewData.Add("role", role);
            ViewData.Add("id", AuthorizationTools.GetUserDbId(User, _context, role));
            if (role != Role.ADMIN)
                return new UnauthorizedResult();
            if (string.IsNullOrEmpty(searchString))
                return new JsonResult(await _context.Candidates.ToListAsync());

            List<Candidate> searchResult = await _context
                .Candidates
                .Where(o => o.LastName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();

            return new JsonResult(searchResult);
        }
        
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            Role role = await AuthorizationTools.GetRoleAsync(User, _context);
            ViewData.Add("role", role);
            ViewData.Add("id", AuthorizationTools.GetUserDbId(User, _context, role));
            string email = AuthorizationTools.GetEmail(User);
            Candidate us = _context.Candidates.Where(c => c.EmailAddress == email).First();
            if (role != Role.ADMIN && (us == null || us.Id != id.Value))
                return new UnauthorizedResult();

            if (id == null)
            {
                return BadRequest($"id shouldn't not be null");
            }
            var offer = await _context.Candidates.FirstOrDefaultAsync(x => x.Id == id.Value);
            if (offer == null)
            {
                return NotFound($"offer not found in DB");
            }
            return View(offer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Edit(Candidate model)
        {
            Role role = await AuthorizationTools.GetRoleAsync(User, _context);
            ViewData.Add("role", role);
            ViewData.Add("id", AuthorizationTools.GetUserDbId(User, _context, role));
            string email = AuthorizationTools.GetEmail(User);
            Candidate us = _context.Candidates.Where(c => c.EmailAddress == email).First();
            if (role != Role.ADMIN && (us == null || us.Id != model.Id))
                return new UnauthorizedResult();

            if (!ModelState.IsValid)
            {
                return View();
            }

            var candidate = await _context.Candidates.FirstOrDefaultAsync(x => x.Id == model.Id);
            candidate.FirstName = model.FirstName;
            candidate.LastName = model.LastName;
            candidate.EmailAddress = model.EmailAddress;
            candidate.PhoneNumber = model.PhoneNumber;
            _context.Update(candidate);
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
            if (role != Role.ADMIN)
                return new UnauthorizedResult();
            if (id == null)
            {
                return BadRequest($"id should not be null");
            }

            _context.Candidates.Remove(new Candidate() { Id = id.Value });
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            Role role = await AuthorizationTools.GetRoleAsync(User, _context);
            ViewData.Add("role", role);
            ViewData.Add("id", AuthorizationTools.GetUserDbId(User, _context, role));
            string email = AuthorizationTools.GetEmail(User);
            Candidate us = _context.Candidates.Where(c => c.EmailAddress == email).FirstOrDefault();
            if (role != Role.ADMIN && (us == null || us.Id != id))
                return new UnauthorizedResult();

            var candidate = await _context.Candidates
                .FirstOrDefaultAsync(x => x.Id == id);
            if (role == Role.ADMIN)
                return View("DetailsAdmin", candidate);
            return View("DetailsCandidate", candidate);
        }
    }
}