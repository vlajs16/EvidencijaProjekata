using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class ExternalMentorContactsAddedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExternalMentorContacts");
        }
    }
}
