using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class ExternalMentorMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExternalMentors",
                columns: table => new
                {
                    CompanyID = table.Column<long>(nullable: false),
                    MentorID = table.Column<int>(nullable: false),
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
                    CompanyID = table.Column<long>(nullable: false),
                    MentorID = table.Column<int>(nullable: false),
                    SerialNumber = table.Column<int>(nullable: false),
                    ContactType = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    ExternalMentorCompanyID = table.Column<long>(nullable: true),
                    ExternalMentorMentorID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalMentorContacts", x => new { x.CompanyID, x.MentorID, x.SerialNumber });
                    table.ForeignKey(
                        name: "FK_ExternalMentorContacts_ExternalMentors_ExternalMentorCompanyID_ExternalMentorMentorID",
                        columns: x => new { x.ExternalMentorCompanyID, x.ExternalMentorMentorID },
                        principalTable: "ExternalMentors",
                        principalColumns: new[] { "CompanyID", "MentorID" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExternalMentorContacts_ExternalMentorCompanyID_ExternalMentorMentorID",
                table: "ExternalMentorContacts",
                columns: new[] { "ExternalMentorCompanyID", "ExternalMentorMentorID" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExternalMentorContacts");

            migrationBuilder.DropTable(
                name: "ExternalMentors");
        }
    }
}
