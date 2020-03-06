using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MainApp.Migrations
{
    public partial class ChangedJobOffer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_COmpanies",
                table: "COmpanies");

            migrationBuilder.DropColumn(
                name: "City",
                table: "JobOffers");

            migrationBuilder.RenameTable(
                name: "COmpanies",
                newName: "Companies");

            migrationBuilder.RenameColumn(
                name: "ExpirationDate",
                table: "JobOffers",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "Company",
                table: "JobOffers",
                newName: "Location");

            migrationBuilder.AlterColumn<string>(
                name: "JobTitle",
                table: "JobOffers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "JobOffers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "JobOffers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "SalaryFrom",
                table: "JobOffers",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SalaryTo",
                table: "JobOffers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidUntil",
                table: "JobOffers",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Companies",
                table: "Companies",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_JobOffers_CompanyId",
                table: "JobOffers",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobOffers_Companies_CompanyId",
                table: "JobOffers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobOffers_Companies_CompanyId",
                table: "JobOffers");

            migrationBuilder.DropIndex(
                name: "IX_JobOffers_CompanyId",
                table: "JobOffers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Companies",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "JobOffers");

            migrationBuilder.DropColumn(
                name: "SalaryFrom",
                table: "JobOffers");

            migrationBuilder.DropColumn(
                name: "SalaryTo",
                table: "JobOffers");

            migrationBuilder.DropColumn(
                name: "ValidUntil",
                table: "JobOffers");

            migrationBuilder.RenameTable(
                name: "Companies",
                newName: "COmpanies");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "JobOffers",
                newName: "Company");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "JobOffers",
                newName: "ExpirationDate");

            migrationBuilder.AlterColumn<string>(
                name: "JobTitle",
                table: "JobOffers",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "JobOffers",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "JobOffers",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_COmpanies",
                table: "COmpanies",
                column: "Id");
        }
    }
}
