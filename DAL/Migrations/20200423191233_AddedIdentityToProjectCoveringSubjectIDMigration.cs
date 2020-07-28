using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddedIdentityToProjectCoveringSubjectIDMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectCoveringSubjects",
                columns: table => new
                {
                    ProjectProposalID = table.Column<long>(nullable: false),
                    ProjectCoveringSubjectID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ScientificAreaID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectCoveringSubjects", x => new { x.ProjectProposalID, x.ProjectCoveringSubjectID });
                    table.ForeignKey(
                        name: "FK_ProjectCoveringSubjects_ProjectProposals_ProjectProposalID",
                        column: x => x.ProjectProposalID,
                        principalTable: "ProjectProposals",
                        principalColumn: "ProjectProposalID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectCoveringSubjects_ScientificAreas_ScientificAreaID",
                        column: x => x.ScientificAreaID,
                        principalTable: "ScientificAreas",
                        principalColumn: "ScientificAreaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCoveringSubjects_ScientificAreaID",
                table: "ProjectCoveringSubjects",
                column: "ScientificAreaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectCoveringSubjects");
        }
    }
}
