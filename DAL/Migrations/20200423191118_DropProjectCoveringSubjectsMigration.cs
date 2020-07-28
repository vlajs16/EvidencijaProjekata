using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class DropProjectCoveringSubjectsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectCoveringSubjects");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectCoveringSubjects",
                columns: table => new
                {
                    ProjectProposalID = table.Column<long>(type: "bigint", nullable: false),
                    ProjectCoveringSubjectID = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScientificAreaID = table.Column<long>(type: "bigint", nullable: true)
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
    }
}
