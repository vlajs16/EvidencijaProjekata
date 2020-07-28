using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class ProjectContractMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectContracts",
                columns: table => new
                {
                    ProjectContractID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectID = table.Column<long>(nullable: true),
                    SigningDate = table.Column<DateTime>(nullable: false),
                    ExpiryDate = table.Column<DateTime>(nullable: false),
                    CompanySigner = table.Column<string>(nullable: true),
                    InternalSignerEmployeeID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectContracts", x => x.ProjectContractID);
                    table.ForeignKey(
                        name: "FK_ProjectContracts_Employees_InternalSignerEmployeeID",
                        column: x => x.InternalSignerEmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectContracts_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectContracts_InternalSignerEmployeeID",
                table: "ProjectContracts",
                column: "InternalSignerEmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectContracts_ProjectID",
                table: "ProjectContracts",
                column: "ProjectID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectContracts");
        }
    }
}
