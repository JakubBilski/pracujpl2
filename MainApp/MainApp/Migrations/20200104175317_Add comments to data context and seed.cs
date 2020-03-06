using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MainApp.Migrations
{
    public partial class Addcommentstodatacontextandseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: true),
                    ApplicationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_JobApplications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "JobApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "ApplicationId", "Text" },
                values: new object[,]
                {
                    { 1, 3, "Well well well" },
                    { 2, 4, "Well well" }
                });

            migrationBuilder.UpdateData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2020, 1, 4, 18, 53, 16, 757, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2020, 1, 4, 18, 53, 16, 759, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2020, 1, 4, 18, 53, 16, 759, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2020, 1, 4, 18, 53, 16, 759, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2020, 1, 4, 18, 53, 16, 759, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2020, 1, 4, 18, 53, 16, 759, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 7,
                column: "Created",
                value: new DateTime(2020, 1, 4, 18, 53, 16, 759, DateTimeKind.Local));

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ApplicationId",
                table: "Comments",
                column: "ApplicationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

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
    }
}
