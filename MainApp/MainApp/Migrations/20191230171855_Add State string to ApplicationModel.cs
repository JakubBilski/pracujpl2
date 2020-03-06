using Microsoft.EntityFrameworkCore.Migrations;

namespace MainApp.Migrations
{
    public partial class AddStatestringtoApplicationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "JobApplications",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "JobApplications");
        }
    }
}
