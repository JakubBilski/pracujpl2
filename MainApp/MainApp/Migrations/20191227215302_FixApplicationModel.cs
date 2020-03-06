using Microsoft.EntityFrameworkCore.Migrations;

namespace MainApp.Migrations
{
    public partial class FixApplicationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HRId",
                table: "JobApplications",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_HRId",
                table: "JobApplications",
                column: "HRId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_HRs_HRId",
                table: "JobApplications",
                column: "HRId",
                principalTable: "HRs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_HRs_HRId",
                table: "JobApplications");

            migrationBuilder.DropIndex(
                name: "IX_JobApplications_HRId",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "HRId",
                table: "JobApplications");
        }
    }
}
