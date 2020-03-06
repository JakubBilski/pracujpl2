using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MainApp.Migrations
{
    public partial class Addseeddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Candidates",
                columns: new[] { "Id", "EmailAddress", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "beans@yahoo.com", "Johny", "English", "889273765" },
                    { 2, "deus@vult.com", "Ryszard", "Lwie Serce", "435777654" },
                    { 3, "hannibal@mini.pw.edu.pl", "Hannibal", "Lecter", "566433567" },
                    { 4, "jackkk@yahoo.com", "Jack", "Nicolson", "567372372" },
                    { 5, "priviet@yahoo.com", "Lyudmiła", "Pawliczenko", "678363862" },
                    { 6, "phrl@yahoo.com", "Pharrel", "Williams", "6472638263" },
                    { 7, "otylypan@donna.com", "Bartosz", "Walaszek", "2377746467" },
                    { 8, "batman@gotham.com", "Bruce", "Wayne", "367473783" },
                    { 9, "zlazoj@spoko.pl", "Slavoj", "Zizek", "74638368" }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 8, "KFG" },
                    { 7, "Dolce&Banana" },
                    { 6, "Polystation" },
                    { 5, "Sunbucks coffee" },
                    { 1, "Abibas" },
                    { 3, "Binbows" },
                    { 2, "Niky" },
                    { 9, "Hike" },
                    { 4, "Facefood" },
                    { 10, "Drunkin nonuts" }
                });

            migrationBuilder.InsertData(
                table: "HRs",
                columns: new[] { "Id", "CompanyId", "EmailAddress", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 7, 1, "katarzyna.testowa@spoko.pl", "Katarzyna", "Testowa" },
                    { 6, 3, "janette.podsiadlo@gmail.com", "Żaneta", "Podsiadło" },
                    { 5, 5, "anna.wanna@gmail.com", "Anna", "Wanna" },
                    { 4, 7, "zofia_skorupka@gmail.com", "Zofia", "Skorupka" },
                    { 1, 9, "bogusia67@wp.pl", "Bogumiła", "Kowalska" },
                    { 2, 10, "grazyna.nowak@autograf.pl", "Grażyna", "Nowak" },
                    { 3, 10, "karennn@onet.pl", "Karen", "Kurka" }
                });

            migrationBuilder.InsertData(
                table: "JobApplications",
                columns: new[] { "Id", "CandidateId", "ContactAgreement", "CvUrl", "EmailAddress", "FirstName", "JobOfferId", "LastName", "OfferId", "PhoneNumber", "State" },
                values: new object[] { 1, 7, true, "www.google.com", "otylypan@donna.com", "Bartosz", null, "Walaszek", 4, "2377746467", "Pending" });

            migrationBuilder.InsertData(
                table: "JobOffers",
                columns: new[] { "Id", "Created", "Description", "HRId", "JobTitle", "Location", "SalaryFrom", "SalaryTo", "ValidUntil" },
                values: new object[,]
                {
                    { 6, new DateTime(2020, 1, 3, 17, 50, 6, 783, DateTimeKind.Local), "Hempen halter boom bounty hornswaggle fore ballast Sink me hearties ye blow the man down. League topsail Blimey trysail yo-ho-ho rutters yawl scuttle dance the hempen jig Brethren of the Coast. Warp measured fer yer chains six pounders rope's end lugger Pieces of Eight killick black spot hempen halter man-of-war.", 7, "Younger chief executor", null, null, null, null },
                    { 7, new DateTime(2020, 1, 3, 17, 50, 6, 783, DateTimeKind.Local), "Hempen halter boom bounty hornswaggle fore ballast Sink me hearties ye blow the man down. League topsail Blimey trysail yo-ho-ho rutters yawl scuttle dance the hempen jig Brethren of the Coast. Warp measured fer yer chains six pounders rope's end lugger Pieces of Eight killick black spot hempen halter man-of-war.", 7, "Butterfly Collector", null, null, null, null },
                    { 2, new DateTime(2020, 1, 3, 17, 50, 6, 783, DateTimeKind.Local), "Belay lookout chase guns carouser draught scurvy barque haul wind strike colors weigh anchor.Walk the plank Spanish Main aye knave yo - ho - ho Cat o'nine tails furl warp hang the jib grapple. Sheet blow the man down belay gally driver Shiver me timbers jolly boat fluke loot cog.", 6, "Pirate recruiter", null, null, null, null },
                    { 1, new DateTime(2020, 1, 3, 17, 50, 6, 780, DateTimeKind.Local), "Pinnace Brethren of the Coast heave to jury mast bring a spring upon her cable mizzenmastbilge bilge rat chandler crow's nest. Cackle fruit long clothes chantey rigging topsail brig Barbary Coast long boat topmast Sea Legs. Trysail Admiral of the Black pirate jury mast draught mizzenmast execution dock mizzen no prey, no pay yawl.", 5, "Package manager", null, null, null, null },
                    { 4, new DateTime(2020, 1, 3, 17, 50, 6, 783, DateTimeKind.Local), "Scuttle clap of thunder ho salmagundi six pounders grog blossom cutlass red ensign ballast wherry. Letter of Marque Arr ye come about chase guns code of conduct scuttle jury mast handsomely gabion. Main sheet heave down lookout parrel hornswaggle coxswain handsomely six pounders clap of thunder Chain Shot.", 5, "Younger assassin", null, null, null, null },
                    { 3, new DateTime(2020, 1, 3, 17, 50, 6, 783, DateTimeKind.Local), "Pinnace wench Buccaneer chase furl chase guns heave to nipper clap of thunder tackle. Gally splice the main brace execution dock Privateer ahoy stern no prey, no pay quarterdeck bowsprit scourge of the seven seas. Tender scuttle Chain Shot stern blow the man down bucko bowsprit to go on account walk the plank flogging.", 4, "Younger assisstant", null, null, null, null },
                    { 5, new DateTime(2020, 1, 3, 17, 50, 6, 783, DateTimeKind.Local), "Hempen halter boom bounty hornswaggle fore ballast Sink me hearties ye blow the man down. League topsail Blimey trysail yo-ho-ho rutters yawl scuttle dance the hempen jig Brethren of the Coast. Warp measured fer yer chains six pounders rope's end lugger Pieces of Eight killick black spot hempen halter man-of-war.", 3, "Younger chief executor", null, null, null, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Candidates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Candidates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Candidates",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Candidates",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Candidates",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Candidates",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Candidates",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Candidates",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "HRs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "HRs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "JobApplications",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Candidates",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "HRs",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "HRs",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "HRs",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "HRs",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "HRs",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
