using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MainApp.Migrations
{
    public partial class Addafewapplicationstoseedandremoveofferidfromapplicationmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OfferId",
                table: "JobApplications");

            migrationBuilder.UpdateData(
                table: "JobApplications",
                keyColumn: "Id",
                keyValue: 1,
                column: "JobOfferId",
                value: 4);

            migrationBuilder.InsertData(
                table: "JobApplications",
                columns: new[] { "Id", "CandidateId", "ContactAgreement", "CvUrl", "EmailAddress", "FirstName", "JobOfferId", "LastName", "PhoneNumber", "State" },
                values: new object[,]
                {
                    { 2, 2, true, "www.google.com", "deus@vultInfidels.com", "Ryszard", 2, "Lwie Serce", "435777654", "Pending" },
                    { 3, 9, true, "www.google.com", "zlazoj@spoko.pl", "Slavoj", 6, "Zizek", "23456764", "Pending" },
                    { 4, 9, true, "www.google.com", "zlazoj@spoko.pl", "Slavoj", 7, "Zizek", "23456764", "Pending" }
                });

            migrationBuilder.UpdateData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2020, 1, 4, 15, 27, 54, 277, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2020, 1, 4, 15, 27, 54, 281, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2020, 1, 4, 15, 27, 54, 281, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2020, 1, 4, 15, 27, 54, 281, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2020, 1, 4, 15, 27, 54, 281, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2020, 1, 4, 15, 27, 54, 281, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 7,
                column: "Created",
                value: new DateTime(2020, 1, 4, 15, 27, 54, 281, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "JobApplications",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "JobApplications",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "JobApplications",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AddColumn<int>(
                name: "OfferId",
                table: "JobApplications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "JobApplications",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "JobOfferId", "OfferId" },
                values: new object[] { null, 4 });

            migrationBuilder.UpdateData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2020, 1, 3, 17, 50, 6, 780, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2020, 1, 3, 17, 50, 6, 783, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2020, 1, 3, 17, 50, 6, 783, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2020, 1, 3, 17, 50, 6, 783, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2020, 1, 3, 17, 50, 6, 783, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2020, 1, 3, 17, 50, 6, 783, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 7,
                column: "Created",
                value: new DateTime(2020, 1, 3, 17, 50, 6, 783, DateTimeKind.Local));
        }
    }
}
