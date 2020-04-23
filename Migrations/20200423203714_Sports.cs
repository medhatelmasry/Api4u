using Microsoft.EntityFrameworkCore.Migrations;

namespace Api4u.Migrations
{
    public partial class Sports : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamName = table.Column<string>(maxLength: 30, nullable: false),
                    City = table.Column<string>(nullable: true),
                    Province = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamName);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Position = table.Column<string>(nullable: false),
                    TeamName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerId);
                    table.ForeignKey(
                        name: "FK_Players_Teams_TeamName",
                        column: x => x.TeamName,
                        principalTable: "Teams",
                        principalColumn: "TeamName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "TeamName", "City", "Country", "Province" },
                values: new object[,]
                {
                    { "Canucks", "Vancouver", "Canada", "BC" },
                    { "Sharks", "San Jose", "USA", "CA" },
                    { "Oilers", "Edmonton", "Canada", "AB" },
                    { "Flames", "Calgary", "Canada", "AB" },
                    { "Ducks", "Anaheim", "USA", "CA" },
                    { "Lightening", "Tampa Bay", "USA", "FL" },
                    { "Blackhawks", "Chicago", "USA", "IL" }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "PlayerId", "FirstName", "LastName", "Position", "TeamName" },
                values: new object[,]
                {
                    { 1, "Bob", "Fox", "Forward", "Canucks" },
                    { 2, "Sam", "Dix", "Left Wing", "Canucks" },
                    { 4, "Pat", "Plumber", "Defense", "Oilers" },
                    { 3, "John", "Rooster", "Right Wing", "Flames" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamName",
                table: "Players",
                column: "TeamName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
