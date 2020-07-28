using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class LocationCityMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityID);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    CompanyID = table.Column<long>(nullable: false),
                    CityID = table.Column<long>(nullable: false),
                    StreetName = table.Column<string>(nullable: true),
                    StreetNumber = table.Column<string>(nullable: true),
                    Floor = table.Column<string>(nullable: true),
                    AppartmentNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => new { x.CityID, x.CompanyID });
                    table.ForeignKey(
                        name: "FK_Locations_Cities_CityID",
                        column: x => x.CityID,
                        principalTable: "Cities",
                        principalColumn: "CityID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Locations_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Locations_CompanyID",
                table: "Locations",
                column: "CompanyID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
