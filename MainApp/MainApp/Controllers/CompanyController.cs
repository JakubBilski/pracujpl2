using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MainApp.Authorization;
using MainApp.EntityFramework;
using MainApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MainApp.Controllers
{
    public class CompanyController : Controller
    {

        private readonly DataContext _context;
        public CompanyController(DataContext context)
        {
            _context = context;
            Console.WriteLine("CompanyController: " + _context.JobOffers.Count());
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index([FromQuery(Name = "search")] string searchString)
        {
            Role role = await AuthorizationTools.GetRoleAsync(User, _context);
            ViewData.Add("role", role);
            ViewData.Add("id", AuthorizationTools.GetUserDbId(User, _context, role));
            List<Company> searchResult;

            if (string.IsNullOrEmpty(searchString))
                searchResult = await _context.Companies.ToListAsync();
            else
                searchResult = await _context
                .Companies
                .Where(o => o.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
            
            if(role == Role.ADMIN)
            {
                return View("IndexAdmin", searchResult);
            }
            return View("IndexHRAndCandidate", searchResult);
        }

        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            Role role = await AuthorizationTools.GetRoleAsync(User, _context);
            ViewData.Add("role", role);
            ViewData.Add("id", AuthorizationTools.GetUserDbId(User, _context, role));
            if (role != Role.ADMIN)
                return new UnauthorizedResult();
            if (id==null)
            {
                return NotFound();
            }
            Company company = await _context.Companies.FindAsync(id);
            if(company==null)
            {
                return NotFound();
            }
            return View(company);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Role role = await AuthorizationTools.GetRoleAsync(User, _context);
            ViewData.Add("role", role);
            ViewData.Add("id", AuthorizationTools.GetUserDbId(User, _context, role));
            if (role != Role.ADMIN)
                return new UnauthorizedResult();
            Company company = _context.Companies.Find(id);
            if(company==null)
            {
                return NotFound();
            }
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<ActionResult> Create()
        {
            Role role = await AuthorizationTools.GetRoleAsync(User, _context);
            ViewData.Add("role", role);
            ViewData.Add("id", AuthorizationTools.GetUserDbId(User, _context, role));
            if (role != Role.ADMIN)
                return new UnauthorizedResult();
            return View();
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> CreateConfirmed(Company company)
        {
            Role role = await AuthorizationTools.GetRoleAsync(User, _context);
            ViewData.Add("role", role);
            ViewData.Add("id", AuthorizationTools.GetUserDbId(User, _context, role));
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (role != Role.ADMIN)
                return new UnauthorizedResult();
            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            Role role = await AuthorizationTools.GetRoleAsync(User, _context);
            ViewData.Add("role", role);
            ViewData.Add("id", AuthorizationTools.GetUserDbId(User, _context, role));
            if (role != Role.ADMIN)
                return new UnauthorizedResult();
            if (id==null)
            {
                return NotFound();
            }
            Company company = await _context.Companies.FindAsync(id);
            if(company==null)
            {
                return NotFound();
            }
            return View(company);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> EditConfirmed(Company company)
        {
            Role role = await AuthorizationTools.GetRoleAsync(User, _context);
            ViewData.Add("role", role);
            ViewData.Add("id", AuthorizationTools.GetUserDbId(User, _context, role));
            if (role != Role.ADMIN)
                return new UnauthorizedResult();
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.Companies.Update(company);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index"); 
        }

        public async Task<IActionResult> Details(int id)
        {
            Role role = await AuthorizationTools.GetRoleAsync(User, _context);
            ViewData.Add("role", role);
            ViewData.Add("id", AuthorizationTools.GetUserDbId(User, _context, role));
            if (role != Role.ADMIN)
                return new UnauthorizedResult();
            var model = _context.Companies.Find(id);
            return View("Details", model);
        }
    }
}