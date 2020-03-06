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

namespace MainApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {   
            if(User != null && User.Identity.IsAuthenticated)
            {
                Role role = await AuthorizationTools.GetRoleAsync(User, _context);
                ViewData.Add("role", role);
                ViewData.Add("id", AuthorizationTools.GetUserDbId(User, _context, role));
            }
            else
            {
                ViewData.Add("role", Role.CANDIDATE);
            }

            return View();
        }



		public IActionResult About()
		{
			ViewData["Message"] = "Your application description page.";
            ViewData.Add("role", Role.CANDIDATE);
            return View();
 
		}

		public IActionResult Contact()
		{
			ViewData["Message"] = "Your contact page.";
            ViewData.Add("role", Role.CANDIDATE);
            return View();
		}

        public IActionResult Privacy()
        {
            ViewData.Add("role", Role.CANDIDATE);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ViewData.Add("role", Role.CANDIDATE);
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ClearContext()
        {
            _context.Candidates.RemoveRange(_context.Candidates);
            _context.Companies.RemoveRange(_context.Companies);
            _context.JobApplications.RemoveRange(_context.JobApplications);
            _context.HRs.RemoveRange(_context.HRs);
            _context.JobOffers.RemoveRange(_context.JobOffers);
            _context.SaveChanges();
            return View("Index");
        }

        public async Task<IActionResult> AddTestDataToContext()
        {
            _context.Candidates.Add(new Candidate() {

                FirstName = "Johny",
                LastName = "English",
                EmailAddress = "beans@yahoo.com",
                PhoneNumber = "889273765"
            });
            _context.Candidates.Add(new Candidate() {

                FirstName = "Ryszard",
                LastName = "Lwie Serce",
                EmailAddress = "deus@vult.com",
                PhoneNumber = "435777654"
            });
            _context.Candidates.Add(new Candidate() {

                FirstName = "Hannibal",
                LastName = "Lecter",
                EmailAddress = "hannibal@mini.pw.edu.pl",
                PhoneNumber = "566433567"
            });
            _context.Candidates.Add(new Candidate() {

                FirstName = "Jack",
                LastName = "Nicolson",
                EmailAddress = "jackkk@yahoo.com",
                PhoneNumber = "567372372"
            });
            _context.Candidates.Add(new Candidate() {

                FirstName = "Lyudmiła",
                LastName = "Pawliczenko",
                EmailAddress = "priviet@yahoo.com",
                PhoneNumber = "678363862"
            });
            _context.Candidates.Add(new Candidate() {

                FirstName = "Pharrel",
                LastName = "Williams",
                EmailAddress = "phrl@yahoo.com",
                PhoneNumber = "6472638263"
            });
            _context.Candidates.Add(new Candidate() {

                FirstName = "Grzegorz",
                LastName = "Zizek",
                EmailAddress = "zlazoj@protonmail.com",
                PhoneNumber = "736482963"
            });
            _context.Candidates.Add(new Candidate() {

                FirstName = "Bartosz",
                LastName = "Walaszek",
                EmailAddress = "otylypan@donna.com",
                PhoneNumber = "2377746467"
            });
            _context.Candidates.Add(new Candidate() {

                FirstName = "Bruce",
                LastName = "Wayne",
                EmailAddress = "batman@gotham.com",
                PhoneNumber = "367473783"
            });
            _context.Candidates.Add(new Candidate() {

                FirstName = "Slavoj",
                LastName = "Zizek",
                EmailAddress = "zlazoj@spoko.pl",
                PhoneNumber = "74638368"
            });
            _context.SaveChanges();
            _context.Companies.Add(new Company() {
                Name = "Abibas"
            });
            _context.Companies.Add(new Company() {
                Name = "Niky"
            });
            _context.Companies.Add(new Company() {
                Name = "Binbows"
            });
            _context.Companies.Add(new Company() {
                Name = "Facefood"
            });
            _context.Companies.Add(new Company() {
                Name = "Sunbucks coffee"
            });
            _context.Companies.Add(new Company() {
                Name = "Polystation"
            });
            _context.Companies.Add(new Company() {
                Name = "Dolce&Banana"
            });
            _context.Companies.Add(new Company() {
                Name = "KFG"
            });
            _context.Companies.Add(new Company() {
                Name = "Hike"
            });
            _context.Companies.Add(new Company() {
                Name = "Drunkin nonuts"
            });
            _context.SaveChanges();
            _context.HRs.Add(new HR()
            {
                FirstName = "Bogumiła",
                LastName = "Kowalska",
                EmailAddress = "bogusia67@wp.pl",
                Company = await _context.Companies.FirstOrDefaultAsync(x => x.Name == "Hike")
            });
            _context.HRs.Add(new HR()
            {
                FirstName = "Grażyna",
                LastName = "Nowak",
                EmailAddress = "grazyna.nowak@autograf.pl",
                Company = await _context.Companies.FirstOrDefaultAsync(x => x.Name == "Drunkin nonuts")
            });
            _context.HRs.Add(new HR()
            {
                FirstName = "Karen",
                LastName = "Kurka",
                EmailAddress = "karennn@onet.pl",
                Company = await _context.Companies.FirstOrDefaultAsync(x => x.Name == "Drunkin nonuts")
            });
            _context.HRs.Add(new HR()
            {
                FirstName = "Zofia",
                LastName = "Skorupka",
                EmailAddress = "zofia_skorupka@gmail.com",
                Company = await _context.Companies.FirstOrDefaultAsync(x => x.Name == "Dolce&Banana")
            });
            _context.HRs.Add(new HR()
            {
                FirstName = "Anna",
                LastName = "Wanna",
                EmailAddress = "anna.wanna@gmail.com",
                Company = await _context.Companies.FirstOrDefaultAsync(x => x.Name == "Sunbucks coffee")
            });
            _context.HRs.Add(new HR()
            {
                FirstName = "Żaneta",
                LastName = "Podsiadło",
                EmailAddress = "janette.podsiadlo@gmail.com",
                Company = await _context.Companies.FirstOrDefaultAsync(x => x.Name == "Binbows")
            });
            _context.HRs.Add(new HR()
            {
                FirstName = "Katarzyna",
                LastName = "Testowa",
                EmailAddress = "katarzyna.testowa@spoko.pl",
                Company = await _context.Companies.FirstOrDefaultAsync(x => x.Name == "Abibas")
            });
            _context.SaveChanges();
            _context.JobOffers.Add(new JobOffer()
            {
                JobTitle = "Package manager",
                HR = await _context.HRs.FirstOrDefaultAsync(x => x.LastName == "Wanna"),
                Created = DateTime.Now,
                Description = "Pinnace Brethren of the Coast heave to jury mast bring a spring upon her cable mizzenmast" +
                "bilge bilge rat chandler crow's nest. Cackle fruit long clothes chantey rigging topsail brig Barbary Coast " +
                "long boat topmast Sea Legs. Trysail Admiral of the Black pirate jury mast draught mizzenmast execution dock mizzen no prey, no pay yawl."
            });
            _context.JobOffers.Add(new JobOffer()
            {
                JobTitle = "Pirate recruiter",
                HR = await _context.HRs.FirstOrDefaultAsync(x => x.LastName == "Podsiadło"),
                Created = DateTime.Now,
                Description = "Belay lookout chase guns carouser draught scurvy barque haul wind strike colors weigh anchor." +
                "Walk the plank Spanish Main aye knave yo - ho - ho Cat o'nine tails furl warp hang the jib grapple. " +
                "Sheet blow the man down belay gally driver Shiver me timbers jolly boat fluke loot cog."
            });
            _context.JobOffers.Add(new JobOffer()
            {
                JobTitle = "Younger assisstant",
                HR = await _context.HRs.FirstOrDefaultAsync(x => x.LastName == "Skorupka"),
                Created = DateTime.Now,
                Description = "Pinnace wench Buccaneer chase furl chase guns heave to nipper clap of thunder tackle. " +
                "Gally splice the main brace execution dock Privateer ahoy stern no prey, no pay quarterdeck bowsprit scourge of the seven seas." +
                " Tender scuttle Chain Shot stern blow the man down bucko bowsprit to go on account walk the plank flogging."
            });
            _context.JobOffers.Add(new JobOffer()
            {
                JobTitle = "Younger assassin",
                HR = await _context.HRs.FirstOrDefaultAsync(x => x.LastName == "Wanna"),
                Created = DateTime.Now,
                Description = "Scuttle clap of thunder ho salmagundi six pounders grog blossom cutlass red ensign ballast wherry." +
                " Letter of Marque Arr ye come about chase guns code of conduct scuttle jury mast handsomely gabion." +
                " Main sheet heave down lookout parrel hornswaggle coxswain handsomely six pounders clap of thunder Chain Shot."
            });
            _context.JobOffers.Add(new JobOffer()
            {
                JobTitle = "Younger chief executor",
                HR = await _context.HRs.FirstOrDefaultAsync(x => x.LastName == "Kurka"),
                Created = DateTime.Now,
                Description = "Hempen halter boom bounty hornswaggle fore ballast Sink me hearties ye blow the man down." +
                " League topsail Blimey trysail yo-ho-ho rutters yawl scuttle dance the hempen jig Brethren of the Coast." +
                " Warp measured fer yer chains six pounders rope's end lugger Pieces of Eight killick black spot hempen halter man-of-war."
            });
            _context.JobOffers.Add(new JobOffer()
            {
                JobTitle = "Younger chief executor",
                HR = await _context.HRs.FirstOrDefaultAsync(x => x.LastName == "Testowa"),
                Created = DateTime.Now,
                Description = "Hempen halter boom bounty hornswaggle fore ballast Sink me hearties ye blow the man down." +
                " League topsail Blimey trysail yo-ho-ho rutters yawl scuttle dance the hempen jig Brethren of the Coast." +
                " Warp measured fer yer chains six pounders rope's end lugger Pieces of Eight killick black spot hempen halter man-of-war."
            });
            _context.JobOffers.Add(new JobOffer()
            {
                JobTitle = "Butterfly Collector",
                HR = await _context.HRs.FirstOrDefaultAsync(x => x.LastName == "Testowa"),
                Created = DateTime.Now,
                Description = "Hempen halter boom bounty hornswaggle fore ballast Sink me hearties ye blow the man down." +
                " League topsail Blimey trysail yo-ho-ho rutters yawl scuttle dance the hempen jig Brethren of the Coast." +
                " Warp measured fer yer chains six pounders rope's end lugger Pieces of Eight killick black spot hempen halter man-of-war."
            });
            _context.SaveChanges();
            _context.JobApplications.Add(new Application()
            {
                FirstName = "Bartosz",
                LastName = "Walaszek",
                EmailAddress = "otylypan@donna.com",
                PhoneNumber = "2377746467",
                ContactAgreement = true,
                Candidate = await _context.Candidates.FirstOrDefaultAsync(x => x.EmailAddress == "otylypan@donna.com"),
                JobOffer = await _context.JobOffers.FirstOrDefaultAsync(x => x.JobTitle == "Pirate recruiter" && x.HR.Company.Name == "Binbows"),
                CvUrl = "www.google.com",
                State = "Rejected"
            });
            _context.JobApplications.Add(new Application()
            {
                FirstName = "Ryszard",
                LastName = "Lwie Serce",
                EmailAddress = "deus@vultInfidels.com",
                PhoneNumber = "435777654",
                ContactAgreement = true,
                Candidate = await _context.Candidates.FirstOrDefaultAsync(x => x.LastName == "Lwie Serce"),
                JobOffer = await _context.JobOffers.FirstOrDefaultAsync(x => x.JobTitle == "Pirate recruiter" && x.HR.Company.Name == "Binbows"),
                CvUrl = "www.google.com",
                State = "Pending"
            });
            _context.JobApplications.Add(new Application()
            {
                FirstName = "Bartosz",
                LastName = "Walaszek",
                EmailAddress = "otylypan@donna.com",
                PhoneNumber = "2377746467",
                ContactAgreement = true,
                Candidate = await _context.Candidates.FirstOrDefaultAsync(x => x.EmailAddress == "otylypan@donna.com"),
                JobOffer = await _context.JobOffers.FirstOrDefaultAsync(x => x.JobTitle == "Younger assisstant" && x.HR.Company.Name == "Dolce&Banana"),
                CvUrl = "www.google.com",
                State = "Pending"
            });
            _context.JobApplications.Add(new Application()
            {
                FirstName = "Bartosz",
                LastName = "Walaszek",
                EmailAddress = "otylypan@donna.com",
                PhoneNumber = "2377746467",
                ContactAgreement = true,
                Candidate = await _context.Candidates.FirstOrDefaultAsync(x => x.EmailAddress == "otylypan@donna.com"),
                JobOffer = await _context.JobOffers.FirstOrDefaultAsync(x => x.JobTitle == "Butterfly Collector" && x.HR.Company.Name == "Abibas"),
                CvUrl = "www.google.com",
                State = "Pending"
            });
            _context.JobApplications.Add(new Application()
            {
                FirstName = "Slavoj",
                LastName = "Zizek",
                EmailAddress = "zlazoj@spoko.pl",
                PhoneNumber = "23456764",
                ContactAgreement = true,
                Candidate = await _context.Candidates.FirstOrDefaultAsync(x => x.EmailAddress == "zlazoj@spoko.pl"),
                JobOffer = await _context.JobOffers.FirstOrDefaultAsync(x => x.JobTitle == "Younger chief executor" && x.HR.Company.Name == "Abibas"),
                CvUrl = "www.google.com",
                State = "Pending"
            });
            _context.SaveChanges();
            return View("Index");
        }
    }
}
