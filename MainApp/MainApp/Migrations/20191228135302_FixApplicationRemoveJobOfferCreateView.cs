using Microsoft.EntityFrameworkCore.Migrations;

namespace MainApp.Migrations
{
    public partial class FixApplicationRemoveJobOfferCreateView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobOffers_Companies_CompanyId",
                table: "JobOffers");

            migrationBuilder.DropIndex(
                name: "IX_JobOffers_CompanyId",
                table: "JobOffers");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "JobOffers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "JobOffers",
                nullable: false,
                defaultValue: 0);

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
    }
}
