using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class DropMentorContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExternalMentorContacts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExternalMentorContacts",
                columns: table => new
                {
                    CompanyID = table.Column<long>(type: "bigint", nullable: false),
                    MentorID = table.Column<int>(type: "int", nullable: false),
                    SerialNumber = table.Column<int>(type: "int", nullable: false),
                    ContactType = table.Column<int>(type: "int", nullable: false),
                    ExternalMentorCompanyID = table.Column<long>(type: "bigint", nullable: true),
                    ExternalMentorMentorID = table.Column<int>(type: "int", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalMentorContact", x => new { x.CompanyID, x.MentorID, x.SerialNumber });
                    table.ForeignKey(
                        name: "FK_ExternalMentorContact_ExternalMentors_ExternalMentorCompanyID_ExternalMentorMentorID",
                        columns: x => new { x.ExternalMentorCompanyID, x.ExternalMentorMentorID },
                        principalTable: "ExternalMentors",
                        principalColumns: new[] { "CompanyID", "MentorID" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExternalMentorContact_ExternalMentorCompanyID_ExternalMentorMentorID",
                table: "ExternalMentorContact",
                columns: new[] { "ExternalMentorCompanyID", "ExternalMentorMentorID" });
        }
    }
}
