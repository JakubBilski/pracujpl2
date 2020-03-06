using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MainApp.Migrations
{
    public partial class Adddataannotationstosomemodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                table: "HRs",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Companies",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Comments",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Candidates",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Candidates",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                table: "Candidates",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2020, 1, 6, 12, 17, 47, 474, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2020, 1, 6, 12, 17, 47, 479, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2020, 1, 6, 12, 17, 47, 479, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2020, 1, 6, 12, 17, 47, 479, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2020, 1, 6, 12, 17, 47, 479, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2020, 1, 6, 12, 17, 47, 479, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 7,
                column: "Created",
                value: new DateTime(2020, 1, 6, 12, 17, 47, 479, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                table: "HRs",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Companies",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Candidates",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Candidates",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                table: "Candidates",
                nullable: true,
                oldClrType: typeof(string));

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
        }
    }
}
