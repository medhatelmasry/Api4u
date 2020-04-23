using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api4u.Migrations
{
    public partial class Athletes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actor_Movie_MovieId",
                table: "Actor");

            migrationBuilder.DropForeignKey(
                name: "FK_City_Province_ProvinceId",
                table: "City");

            migrationBuilder.DropForeignKey(
                name: "FK_Country_Continent_ContinentName",
                table: "Country");

            migrationBuilder.DropForeignKey(
                name: "FK_Organism_Specie_SpecieName",
                table: "Organism");

            migrationBuilder.DropForeignKey(
                name: "FK_Province_Country_CountryName",
                table: "Province");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Specie",
                table: "Specie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Province",
                table: "Province");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Organism",
                table: "Organism");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Movie",
                table: "Movie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Country",
                table: "Country");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Continent",
                table: "Continent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_City",
                table: "City");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Actor",
                table: "Actor");

            migrationBuilder.RenameTable(
                name: "Specie",
                newName: "Species");

            migrationBuilder.RenameTable(
                name: "Province",
                newName: "Provinces");

            migrationBuilder.RenameTable(
                name: "Organism",
                newName: "Organisms");

            migrationBuilder.RenameTable(
                name: "Movie",
                newName: "Movies");

            migrationBuilder.RenameTable(
                name: "Country",
                newName: "Countries");

            migrationBuilder.RenameTable(
                name: "Continent",
                newName: "Continents");

            migrationBuilder.RenameTable(
                name: "City",
                newName: "Cities");

            migrationBuilder.RenameTable(
                name: "Actor",
                newName: "Actors");

            migrationBuilder.RenameIndex(
                name: "IX_Province_CountryName",
                table: "Provinces",
                newName: "IX_Provinces_CountryName");

            migrationBuilder.RenameIndex(
                name: "IX_Organism_SpecieName",
                table: "Organisms",
                newName: "IX_Organisms_SpecieName");

            migrationBuilder.RenameIndex(
                name: "IX_Country_ContinentName",
                table: "Countries",
                newName: "IX_Countries_ContinentName");

            migrationBuilder.RenameIndex(
                name: "IX_City_ProvinceId",
                table: "Cities",
                newName: "IX_Cities_ProvinceId");

            migrationBuilder.RenameIndex(
                name: "IX_Actor_MovieId",
                table: "Actors",
                newName: "IX_Actors_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Species",
                table: "Species",
                column: "SpecieName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Provinces",
                table: "Provinces",
                column: "ProvinceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Organisms",
                table: "Organisms",
                column: "OrganismId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movies",
                table: "Movies",
                column: "MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countries",
                table: "Countries",
                column: "CountryName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Continents",
                table: "Continents",
                column: "ContinentName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cities",
                table: "Cities",
                column: "CityName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Actors",
                table: "Actors",
                column: "ActorId");

            migrationBuilder.CreateTable(
                name: "Competitions",
                columns: table => new
                {
                    CompetitionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitions", x => x.CompetitionId);
                });

            migrationBuilder.CreateTable(
                name: "Athletes",
                columns: table => new
                {
                    AthleteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Country = table.Column<string>(nullable: false),
                    CompetitionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Athletes", x => x.AthleteId);
                    table.ForeignKey(
                        name: "FK_Athletes_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competitions",
                        principalColumn: "CompetitionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Competitions",
                columns: new[] { "CompetitionId", "EventName" },
                values: new object[,]
                {
                    { 1, "Men's 100m" },
                    { 2, "Men's Pole Vault" },
                    { 3, "Men's 50km Race Walking" },
                    { 4, "Women's 100m" },
                    { 5, "Women's 100m" },
                    { 6, "Women's Marathon" },
                    { 7, "Men's Marathon" }
                });

            migrationBuilder.InsertData(
                table: "Athletes",
                columns: new[] { "AthleteId", "CompetitionId", "Country", "DateOfBirth", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, 1, "USA", new DateTime(1996, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Christian", "Coleman" },
                    { 2, 1, "GBR", new DateTime(1995, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zharnel", "Hughes" },
                    { 7, 2, "SWE", new DateTime(1999, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Armand", "Duplantis" },
                    { 3, 6, "KEN", new DateTime(1994, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brigid", "Kosgei" },
                    { 4, 6, "ETH", new DateTime(1990, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Worknesh", "Degefa" },
                    { 5, 7, "ETH", new DateTime(1994, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Birhanu", "Legese" },
                    { 6, 7, "KEN", new DateTime(1988, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lawrence", "Cherono" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Athletes_CompetitionId",
                table: "Athletes",
                column: "CompetitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_Movies_MovieId",
                table: "Actors",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Provinces_ProvinceId",
                table: "Cities",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "ProvinceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_Continents_ContinentName",
                table: "Countries",
                column: "ContinentName",
                principalTable: "Continents",
                principalColumn: "ContinentName",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Organisms_Species_SpecieName",
                table: "Organisms",
                column: "SpecieName",
                principalTable: "Species",
                principalColumn: "SpecieName",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Provinces_Countries_CountryName",
                table: "Provinces",
                column: "CountryName",
                principalTable: "Countries",
                principalColumn: "CountryName",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Movies_MovieId",
                table: "Actors");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Provinces_ProvinceId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Countries_Continents_ContinentName",
                table: "Countries");

            migrationBuilder.DropForeignKey(
                name: "FK_Organisms_Species_SpecieName",
                table: "Organisms");

            migrationBuilder.DropForeignKey(
                name: "FK_Provinces_Countries_CountryName",
                table: "Provinces");

            migrationBuilder.DropTable(
                name: "Athletes");

            migrationBuilder.DropTable(
                name: "Competitions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Species",
                table: "Species");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Provinces",
                table: "Provinces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Organisms",
                table: "Organisms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Movies",
                table: "Movies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Countries",
                table: "Countries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Continents",
                table: "Continents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cities",
                table: "Cities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Actors",
                table: "Actors");

            migrationBuilder.RenameTable(
                name: "Species",
                newName: "Specie");

            migrationBuilder.RenameTable(
                name: "Provinces",
                newName: "Province");

            migrationBuilder.RenameTable(
                name: "Organisms",
                newName: "Organism");

            migrationBuilder.RenameTable(
                name: "Movies",
                newName: "Movie");

            migrationBuilder.RenameTable(
                name: "Countries",
                newName: "Country");

            migrationBuilder.RenameTable(
                name: "Continents",
                newName: "Continent");

            migrationBuilder.RenameTable(
                name: "Cities",
                newName: "City");

            migrationBuilder.RenameTable(
                name: "Actors",
                newName: "Actor");

            migrationBuilder.RenameIndex(
                name: "IX_Provinces_CountryName",
                table: "Province",
                newName: "IX_Province_CountryName");

            migrationBuilder.RenameIndex(
                name: "IX_Organisms_SpecieName",
                table: "Organism",
                newName: "IX_Organism_SpecieName");

            migrationBuilder.RenameIndex(
                name: "IX_Countries_ContinentName",
                table: "Country",
                newName: "IX_Country_ContinentName");

            migrationBuilder.RenameIndex(
                name: "IX_Cities_ProvinceId",
                table: "City",
                newName: "IX_City_ProvinceId");

            migrationBuilder.RenameIndex(
                name: "IX_Actors_MovieId",
                table: "Actor",
                newName: "IX_Actor_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Specie",
                table: "Specie",
                column: "SpecieName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Province",
                table: "Province",
                column: "ProvinceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Organism",
                table: "Organism",
                column: "OrganismId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movie",
                table: "Movie",
                column: "MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Country",
                table: "Country",
                column: "CountryName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Continent",
                table: "Continent",
                column: "ContinentName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_City",
                table: "City",
                column: "CityName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Actor",
                table: "Actor",
                column: "ActorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actor_Movie_MovieId",
                table: "Actor",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_City_Province_ProvinceId",
                table: "City",
                column: "ProvinceId",
                principalTable: "Province",
                principalColumn: "ProvinceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Country_Continent_ContinentName",
                table: "Country",
                column: "ContinentName",
                principalTable: "Continent",
                principalColumn: "ContinentName",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Organism_Specie_SpecieName",
                table: "Organism",
                column: "SpecieName",
                principalTable: "Specie",
                principalColumn: "SpecieName",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Province_Country_CountryName",
                table: "Province",
                column: "CountryName",
                principalTable: "Country",
                principalColumn: "CountryName",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
