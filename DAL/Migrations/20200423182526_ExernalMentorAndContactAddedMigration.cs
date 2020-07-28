using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class ExernalMentorAndContactAddedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ExternalMentorCompanyID",
                table: "ProjectProposals",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExternalMentorMentorID",
                table: "ProjectProposals",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ExternalMentors",
                columns: table => new
                {
                    CompanyID = table.Column<long>(nullable: false),
                    MentorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalMentors", x => new { x.CompanyID, x.MentorID });
                    table.ForeignKey(
                        name: "FK_ExternalMentors_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExternalMentorContacts",
                columns: table => new
                {
                    ExternalMentorCompanyID = table.Column<long>(nullable: false),
                    ExternalMentorMentorID = table.Column<int>(nullable: false),
                    SerialNumber = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactType = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalMentorContacts", x => new { x.ExternalMentorCompanyID, x.ExternalMentorMentorID, x.SerialNumber });
                    table.ForeignKey(
                        name: "FK_ExternalMentorContacts_ExternalMentors_ExternalMentorCompanyID_ExternalMentorMentorID",
                        columns: x => new { x.ExternalMentorCompanyID, x.ExternalMentorMentorID },
                        principalTable: "ExternalMentors",
                        principalColumns: new[] { "CompanyID", "MentorID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProposals_ExternalMentorCompanyID_ExternalMentorMentorID",
                table: "ProjectProposals",
                columns: new[] { "ExternalMentorCompanyID", "ExternalMentorMentorID" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectProposals_ExternalMentors_ExternalMentorCompanyID_ExternalMentorMentorID",
                table: "ProjectProposals",
                columns: new[] { "ExternalMentorCompanyID", "ExternalMentorMentorID" },
                principalTable: "ExternalMentors",
                principalColumns: new[] { "CompanyID", "MentorID" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectProposals_ExternalMentors_ExternalMentorCompanyID_ExternalMentorMentorID",
                table: "ProjectProposals");

            migrationBuilder.DropTable(
                name: "ExternalMentorContacts");

            migrationBuilder.DropTable(
                name: "ExternalMentors");

            migrationBuilder.DropIndex(
                name: "IX_ProjectProposals_ExternalMentorCompanyID_ExternalMentorMentorID",
                table: "ProjectProposals");

            migrationBuilder.DropColumn(
                name: "ExternalMentorCompanyID",
                table: "ProjectProposals");

            migrationBuilder.DropColumn(
                name: "ExternalMentorMentorID",
                table: "ProjectProposals");
        }
    }
}
