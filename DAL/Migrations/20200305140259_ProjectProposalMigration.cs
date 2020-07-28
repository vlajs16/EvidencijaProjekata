using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class ProjectProposalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectProposals",
                columns: table => new
                {
                    ProjectProposalID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProposalDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Goal = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Activities = table.Column<string>(nullable: true),
                    StartDateProjectProposal = table.Column<DateTime>(nullable: false),
                    DaysDuration = table.Column<int>(nullable: false),
                    ExternalMentorCompanyID = table.Column<long>(nullable: true),
                    ExternalMentorMentorID = table.Column<int>(nullable: true),
                    CompanyID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectProposals", x => x.ProjectProposalID);
                    table.ForeignKey(
                        name: "FK_ProjectProposals_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectProposals_ExternalMentors_ExternalMentorCompanyID_ExternalMentorMentorID",
                        columns: x => new { x.ExternalMentorCompanyID, x.ExternalMentorMentorID },
                        principalTable: "ExternalMentors",
                        principalColumns: new[] { "CompanyID", "MentorID" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScientificAreas",
                columns: table => new
                {
                    ScientificAreaID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScientificAreas", x => x.ScientificAreaID);
                });

            migrationBuilder.CreateTable(
                name: "ProjectCoveringSubjects",
                columns: table => new
                {
                    ProjectProposalID = table.Column<long>(nullable: false),
                    ProjectCoveringSubjectID = table.Column<long>(nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProposals_CompanyID",
                table: "ProjectProposals",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProposals_ExternalMentorCompanyID_ExternalMentorMentorID",
                table: "ProjectProposals",
                columns: new[] { "ExternalMentorCompanyID", "ExternalMentorMentorID" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectCoveringSubjects");

            migrationBuilder.DropTable(
                name: "ProjectProposals");

            migrationBuilder.DropTable(
                name: "ScientificAreas");
        }
    }
}
