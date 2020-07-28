using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class ProjectMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdoptionDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    InternalMentorEmployeeID = table.Column<long>(nullable: true),
                    DecisionMakerEmployeeID = table.Column<long>(nullable: true),
                    ProjectProposalID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectID);
                    table.ForeignKey(
                        name: "FK_Projects_Employees_DecisionMakerEmployeeID",
                        column: x => x.DecisionMakerEmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projects_Employees_InternalMentorEmployeeID",
                        column: x => x.InternalMentorEmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projects_ProjectProposals_ProjectProposalID",
                        column: x => x.ProjectProposalID,
                        principalTable: "ProjectProposals",
                        principalColumn: "ProjectProposalID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_DecisionMakerEmployeeID",
                table: "Projects",
                column: "DecisionMakerEmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_InternalMentorEmployeeID",
                table: "Projects",
                column: "InternalMentorEmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectProposalID",
                table: "Projects",
                column: "ProjectProposalID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
