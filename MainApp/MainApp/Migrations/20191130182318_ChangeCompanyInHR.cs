using Microsoft.EntityFrameworkCore.Migrations;

namespace MainApp.Migrations
{
    public partial class ChangeCompanyInHR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HRs_Companies_CompanyId",
                table: "HRs");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "HRs",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HRs_Companies_CompanyId",
                table: "HRs",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HRs_Companies_CompanyId",
                table: "HRs");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "HRs",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_HRs_Companies_CompanyId",
                table: "HRs",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
