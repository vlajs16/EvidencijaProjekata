using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class BoolMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "ProjectProposals",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approved",
                table: "ProjectProposals");
        }
    }
}
