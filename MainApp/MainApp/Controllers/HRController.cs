using System;
using System.Collections.Generic;
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
    public class HRController : Controller
    {
        private readonly DataContext _context;

        public HRController(DataContext context)
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
            if (await AuthorizationTools.IsAdmin(User, _context) == false)
                return new UnauthorizedResult();

            if (string.IsNullOrEmpty(searchString))
                return View(await _context.HRs.Include(x => x.Company).ToListAsync());

            List<HR> searchResult = await _context
                .HRs.Include(x => x.Company)
                .Where(o => o.LastName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();

            return View(searchResult);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            Role role = await AuthorizationTools.GetRoleAsync(User, _context);
            ViewData.Add("role", role);
            ViewData.Add("id", AuthorizationTools.GetUserDbId(User, _context, role));
            string email = AuthorizationTools.GetEmail(User);
            HR us = _context.HRs.Where(h => h.EmailAddress == email).FirstOrDefault();
            if (await AuthorizationTools.IsAdmin(User, _context) == false && (us == null || us.Id != id.Value))
                return new UnauthorizedResult();

            if (id == null)
            {
                return BadRequest($"id shouldn't not be null");
            }
            var hr = await _context.HRs.FirstOrDefaultAsync(x => x.Id == id.Value);
            if (hr == null)
            {
                return NotFound($"HR not found in DB");
            }

            return View(hr);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Edit(HR model)
        {
            Role role = await AuthorizationTools.GetRoleAsync(User, _context);
            ViewData.Add("role", role);
            ViewData.Add("id", AuthorizationTools.GetUserDbId(User, _context, role));
            string email = AuthorizationTools.GetEmail(User);
            HR us = _context.HRs.Where(h => h.EmailAddress == email).FirstOrDefault();
            if (await AuthorizationTools.IsAdmin(User, _context) == false && (us == null || us.Id != model.Id))
                return new UnauthorizedResult();

            if (!ModelState.IsValid)
            {
                return View();
            }

            var hr = await _context.HRs.FirstOrDefaultAsync(x => x.Id == model.Id);
            hr.FirstName = model.FirstName;
            hr.LastName = model.LastName;
            hr.Company = model.Company;
            hr.EmailAddress = model.EmailAddress;
            _context.Update(hr);
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
            if (await AuthorizationTools.IsAdmin(User, _context) == false)
                return new UnauthorizedResult();
            if (id == null)
            {
                return BadRequest($"id should not be null");
            }

            _context.HRs.Remove(new HR() { Id = id.Value });
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<ActionResult> Create()
        {
            Role role = await AuthorizationTools.GetRoleAsync(User, _context);
            ViewData.Add("role", role);
            ViewData.Add("id", AuthorizationTools.GetUserDbId(User, _context, role));
            if (await AuthorizationTools.IsAdmin(User, _context) == false)
                return new UnauthorizedResult();
            var model = new HRCreateView
            {
                Companies = await _context.Companies.ToListAsync()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Create(HRCreateView model)
        {
            Role role = await AuthorizationTools.GetRoleAsync(User, _context);
            ViewData.Add("role", role);
            ViewData.Add("id", AuthorizationTools.GetUserDbId(User, _context, role));
            if (role != Role.ADMIN)
                return new UnauthorizedResult();
            if (!ModelState.IsValid)
            {
                model.Companies = await _context.Companies.ToListAsync();
                return View(model);
            }

            HR hr = new HR
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                CompanyId = model.CompanyId,
                EmailAddress = model.EmailAddress
            };

            await _context.HRs.AddAsync(hr);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            Role role = await AuthorizationTools.GetRoleAsync(User, _context);
            ViewData.Add("role", role);
            ViewData.Add("id", AuthorizationTools.GetUserDbId(User, _context, role));
            string email = AuthorizationTools.GetEmail(User);
            HR us = _context.HRs.Where(h => h.EmailAddress == email).FirstOrDefault();
            if (role != Role.ADMIN && (us == null || us.Id != id))
                return new UnauthorizedResult();

            if (role == Role.CANDIDATE)
                return new UnauthorizedResult();
            var hr = await _context.HRs
                .Include(x => x.Company)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (role == Role.ADMIN)
                return View("DetailsAdmin", hr);
            return View("DetailsHR", hr);
        }
    }
}