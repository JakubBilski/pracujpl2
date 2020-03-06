using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApp.Models;
using System;

namespace MainApp.EntityFramework
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Application> JobApplications { get; set; }
        public DbSet<JobOffer> JobOffers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<HR> HRs { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Comment> Comments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidate>().HasData(
            new
            {
                Id = 1,
                FirstName = "Johny",
                LastName = "English",
                EmailAddress = "beans@yahoo.com",
                PhoneNumber = "889273765"
            },
            new
            {

                Id = 2,
                FirstName = "Ryszard",
                LastName = "Lwie Serce",
                EmailAddress = "deus@vult.com",
                PhoneNumber = "435777654"
            },
            new
            {

                Id = 3,
                FirstName = "Hannibal",
                LastName = "Lecter",
                EmailAddress = "hannibal@mini.pw.edu.pl",
                PhoneNumber = "566433567"
            },
            new
            {
                Id = 4,

                FirstName = "Jack",
                LastName = "Nicolson",
                EmailAddress = "jackkk@yahoo.com",
                PhoneNumber = "567372372"
            },
            new
            {
                Id = 5,

                FirstName = "Lyudmiła",
                LastName = "Pawliczenko",
                EmailAddress = "priviet@yahoo.com",
                PhoneNumber = "678363862"
            },
            new
            {
                Id = 6,

                FirstName = "Pharrel",
                LastName = "Williams",
                EmailAddress = "phrl@yahoo.com",
                PhoneNumber = "6472638263"
            },
            new
            {
                Id = 7,

                FirstName = "Bartosz",
                LastName = "Walaszek",
                EmailAddress = "otylypan@donna.com",
                PhoneNumber = "2377746467"
            },
            new
            {
                Id = 8,

                FirstName = "Bruce",
                LastName = "Wayne",
                EmailAddress = "batman@gotham.com",
                PhoneNumber = "367473783"
            },
            new
            {
                Id = 9,

                FirstName = "Slavoj",
                LastName = "Zizek",
                EmailAddress = "zlazoj@spoko.pl",
                PhoneNumber = "74638368"
            });
            modelBuilder.Entity<Company>().HasData(
            new Company()
            {
                Id = 1,
                Name = "Abibas"
            },
            new
            {
                Id = 2,
                Name = "Niky"
            },
            new
            {
                Id = 3,
                Name = "Binbows"
            },
            new
            {
                Id = 4,
                Name = "Facefood"
            },
            new
            {
                Id = 5,
                Name = "Sunbucks coffee"
            },
            new
            {
                Id = 6,
                Name = "Polystation"
            },
            new
            {
                Id = 7,
                Name = "Dolce&Banana"
            },
            new
            {
                Id = 8,
                Name = "KFG"
            },
            new
            {
                Id = 9,
                Name = "Hike"
            },
            new
            {
                Id = 10,
                Name = "Drunkin nonuts"
            });
            modelBuilder.Entity<HR>().HasData(
            new
            {
                Id = 1,
                FirstName = "Bogumiła",
                LastName = "Kowalska",
                EmailAddress = "bogusia67@wp.pl",
                CompanyId = 9
            },
            new
            {
                Id = 2,
                FirstName = "Grażyna",
                LastName = "Nowak",
                EmailAddress = "grazyna.nowak@autograf.pl",
                CompanyId = 10
            },
            new
            {
                Id = 3,
                FirstName = "Karen",
                LastName = "Kurka",
                EmailAddress = "karennn@onet.pl",
                CompanyId = 10
            },
            new
            {
                Id = 4,
                FirstName = "Zofia",
                LastName = "Skorupka",
                EmailAddress = "zofia_skorupka@gmail.com",
                CompanyId = 7
            },
            new
            {
                Id = 5,
                FirstName = "Anna",
                LastName = "Wanna",
                EmailAddress = "anna.wanna@gmail.com",
                CompanyId = 5
            },
            new
            {
                Id = 6,
                FirstName = "Żaneta",
                LastName = "Podsiadło",
                EmailAddress = "janette.podsiadlo@gmail.com",
                CompanyId = 3
            },
            new
            {
                Id = 7,
                FirstName = "Katarzyna",
                LastName = "Testowa",
                EmailAddress = "katarzyna.testowa@spoko.pl",
                CompanyId = 1
            });
            modelBuilder.Entity<JobOffer>().HasData(
            new
            {
                Id = 1,
                JobTitle = "Package manager",
                HRId = 5,
                Created = DateTime.Now,
                Description = "Pinnace Brethren of the Coast heave to jury mast bring a spring upon her cable mizzenmast" +
                "bilge bilge rat chandler crow's nest. Cackle fruit long clothes chantey rigging topsail brig Barbary Coast " +
                "long boat topmast Sea Legs. Trysail Admiral of the Black pirate jury mast draught mizzenmast execution dock mizzen no prey, no pay yawl."
            },
            new
            {
                Id = 2,
                JobTitle = "Pirate recruiter",
                HRId = 6,
                Created = DateTime.Now,
                Description = "Belay lookout chase guns carouser draught scurvy barque haul wind strike colors weigh anchor." +
                "Walk the plank Spanish Main aye knave yo - ho - ho Cat o'nine tails furl warp hang the jib grapple. " +
                "Sheet blow the man down belay gally driver Shiver me timbers jolly boat fluke loot cog."
            },
            new
            {
                Id = 3,
                JobTitle = "Younger assisstant",
                HRId = 4,
                Created = DateTime.Now,
                Description = "Pinnace wench Buccaneer chase furl chase guns heave to nipper clap of thunder tackle. " +
                "Gally splice the main brace execution dock Privateer ahoy stern no prey, no pay quarterdeck bowsprit scourge of the seven seas." +
                " Tender scuttle Chain Shot stern blow the man down bucko bowsprit to go on account walk the plank flogging."
            },
            new
            {
                Id = 4,
                JobTitle = "Younger assassin",
                HRId = 5,
                Created = DateTime.Now,
                Description = "Scuttle clap of thunder ho salmagundi six pounders grog blossom cutlass red ensign ballast wherry." +
                " Letter of Marque Arr ye come about chase guns code of conduct scuttle jury mast handsomely gabion." +
                " Main sheet heave down lookout parrel hornswaggle coxswain handsomely six pounders clap of thunder Chain Shot."
            },
            new
            {
                Id = 5,
                JobTitle = "Younger chief executor",
                HRId = 3,
                Created = DateTime.Now,
                Description = "Hempen halter boom bounty hornswaggle fore ballast Sink me hearties ye blow the man down." +
                " League topsail Blimey trysail yo-ho-ho rutters yawl scuttle dance the hempen jig Brethren of the Coast." +
                " Warp measured fer yer chains six pounders rope's end lugger Pieces of Eight killick black spot hempen halter man-of-war."
            },
            new
            {
                Id = 6,
                JobTitle = "Younger chief executor",
                HRId = 7,
                Created = DateTime.Now,
                Description = "Hempen halter boom bounty hornswaggle fore ballast Sink me hearties ye blow the man down." +
                " League topsail Blimey trysail yo-ho-ho rutters yawl scuttle dance the hempen jig Brethren of the Coast." +
                " Warp measured fer yer chains six pounders rope's end lugger Pieces of Eight killick black spot hempen halter man-of-war."
            },
            new
            {
                Id = 7,
                JobTitle = "Butterfly Collector",
                HRId = 7,
                Created = DateTime.Now,
                Description = "Hempen halter boom bounty hornswaggle fore ballast Sink me hearties ye blow the man down." +
                " League topsail Blimey trysail yo-ho-ho rutters yawl scuttle dance the hempen jig Brethren of the Coast." +
                " Warp measured fer yer chains six pounders rope's end lugger Pieces of Eight killick black spot hempen halter man-of-war."
            });
            modelBuilder.Entity<Application>().HasData(
            new
            {
                Id = 1,
                FirstName = "Bartosz",
                LastName = "Walaszek",
                EmailAddress = "otylypan@donna.com",
                PhoneNumber = "2377746467",
                ContactAgreement = true,
                CandidateId = 7,
                JobOfferId = 4,
                CvUrl = "www.google.com",
                State = "Pending"
            },
            new
            {
                Id = 2,
                FirstName = "Ryszard",
                LastName = "Lwie Serce",
                EmailAddress = "deus@vultInfidels.com",
                PhoneNumber = "435777654",
                ContactAgreement = true,
                CandidateId = 2,
                JobOfferId = 2,
                CvUrl = "www.google.com",
                State = "Pending"
            },
            new
            {
                Id = 3,
                FirstName = "Slavoj",
                LastName = "Zizek",
                EmailAddress = "zlazoj@spoko.pl",
                PhoneNumber = "23456764",
                ContactAgreement = true,
                CandidateId = 9,
                JobOfferId = 6,
                CvUrl = "www.google.com",
                State = "Pending"
            },
            new
            {
                Id = 4,
                FirstName = "Slavoj",
                LastName = "Zizek",
                EmailAddress = "zlazoj@spoko.pl",
                PhoneNumber = "23456764",
                ContactAgreement = true,
                CandidateId = 9,
                JobOfferId = 7,
                CvUrl = "www.google.com",
                State = "Pending"
            });
            modelBuilder.Entity<Comment>().HasData(
            new
            {
                Id = 1,
                Text = "Well well well",
                ApplicationId = 3
            },
            new
            {
                Id = 2,
                Text = "Well well",
                ApplicationId = 4
            }
            );
        }
    }
}
