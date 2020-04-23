using Microsoft.EntityFrameworkCore.Migrations;

namespace Api4u.Migrations
{
    public partial class CountryMovieSpecies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_VehicleManufacturers_VehicleManufacturerName",
                table: "Vehicles");

            migrationBuilder.AlterColumn<string>(
                name: "VehicleManufacturerName",
                table: "Vehicles",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Vehicles",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Fuel",
                table: "Vehicles",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "VehicleManufacturers",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                table: "Foods",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Foods",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "FoodCategories",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "FoodCategories",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Continent",
                columns: table => new
                {
                    ContinentName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Continent", x => x.ContinentName);
                });

            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    MovieId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    DirectorFirstName = table.Column<string>(nullable: false),
                    DirectorLastName = table.Column<string>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Rating = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.MovieId);
                });

            migrationBuilder.CreateTable(
                name: "Specie",
                columns: table => new
                {
                    SpecieName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specie", x => x.SpecieName);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    CountryName = table.Column<string>(nullable: false),
                    CapitalCity = table.Column<string>(nullable: false),
                    AreaSqKm = table.Column<int>(nullable: false),
                    ContinentName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.CountryName);
                    table.ForeignKey(
                        name: "FK_Country_Continent_ContinentName",
                        column: x => x.ContinentName,
                        principalTable: "Continent",
                        principalColumn: "ContinentName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Actor",
                columns: table => new
                {
                    ActorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    MovieId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actor", x => x.ActorId);
                    table.ForeignKey(
                        name: "FK_Actor_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Organism",
                columns: table => new
                {
                    OrganismId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    SpecieName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organism", x => x.OrganismId);
                    table.ForeignKey(
                        name: "FK_Organism_Specie_SpecieName",
                        column: x => x.SpecieName,
                        principalTable: "Specie",
                        principalColumn: "SpecieName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Province",
                columns: table => new
                {
                    ProvinceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    CapitalCity = table.Column<string>(nullable: false),
                    CountryName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province", x => x.ProvinceId);
                    table.ForeignKey(
                        name: "FK_Province_Country_CountryName",
                        column: x => x.CountryName,
                        principalTable: "Country",
                        principalColumn: "CountryName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    CityName = table.Column<string>(nullable: false),
                    ProvinceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.CityName);
                    table.ForeignKey(
                        name: "FK_City_Province_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Province",
                        principalColumn: "ProvinceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Continent",
                column: "ContinentName",
                values: new object[]
                {
                    "Africa",
                    "Asia",
                    "Europe",
                    "South America",
                    "Australia",
                    "North America"
                });

            migrationBuilder.InsertData(
                table: "Movie",
                columns: new[] { "MovieId", "DirectorFirstName", "DirectorLastName", "Name", "Rating", "Year" },
                values: new object[,]
                {
                    { 6, "Stanley", "Kubrick", "Spartacus", "PG", 1960 },
                    { 5, "Hugh", "Hudson", "Chariots of Fire", "PG", 1981 },
                    { 4, "Walter", "Lang", "The King and I", "G", 1956 },
                    { 3, "George", "Cukor", "My Fair Lady", "PG", 1964 },
                    { 2, "Robert", "Wise", "The Sound of Music", "G", 196 },
                    { 1, "Richard", "Attenborough", "Gandhi", "PG", 1982 }
                });

            migrationBuilder.InsertData(
                table: "Specie",
                column: "SpecieName",
                values: new object[]
                {
                    "Bird",
                    "Mammal",
                    "Insect",
                    "Reptile",
                    "Fish",
                    "Amphibians"
                });

            migrationBuilder.InsertData(
                table: "Actor",
                columns: new[] { "ActorId", "FirstName", "LastName", "MovieId" },
                values: new object[,]
                {
                    { 4, "Julie", "Andrews", 2 },
                    { 7, "Rex", "Harrison", 3 },
                    { 6, "Audrey", "Hepburn", 3 },
                    { 5, "Christopher", "Plummer", 2 },
                    { 3, "Rohini", "Hattangadi", 1 },
                    { 1, "Ben", "Kingsley", 1 },
                    { 2, "John", "Gielgud", 1 }
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "CountryName", "AreaSqKm", "CapitalCity", "ContinentName" },
                values: new object[,]
                {
                    { "India", 3287590, "New Delhi", "Asia" },
                    { "Canada", 9976140, "Ottawa", "North America" },
                    { "Brazil", 8511965, "Brasilia", "South America" },
                    { "China", 9596960, "Beijing", "Asia" },
                    { "USA", 9629091, "Washington DC", "North America" }
                });

            migrationBuilder.InsertData(
                table: "Organism",
                columns: new[] { "OrganismId", "Name", "SpecieName" },
                values: new object[,]
                {
                    { 5, "Atlantic salmon", "Fish" },
                    { 1, "Australian brush turkey", "Bird" },
                    { 2, "Egyptian plover", "Bird" },
                    { 3, "White stork", "Bird" },
                    { 4, "Kingfisher", "Bird" },
                    { 6, "Great white shark", "Fish" }
                });

            migrationBuilder.InsertData(
                table: "Province",
                columns: new[] { "ProvinceId", "CapitalCity", "CountryName", "Name" },
                values: new object[,]
                {
                    { 4, "Fuzhou", "China", "Fujian" },
                    { 5, "Wuhan", "China", "Hubei" },
                    { 6, "Toronto", "Canada", "Ontario" },
                    { 7, "Edmopnton", "Canada", "Alberta" },
                    { 1, "Montgomery", "USA", "Aalabama" },
                    { 2, "Sacramento", "USA", "California" },
                    { 3, "Honolulu", "USA", "Hawaii" }
                });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "CityName", "ProvinceId" },
                values: new object[,]
                {
                    { "Xiamen", 4 },
                    { "Quanzhou", 4 },
                    { "Ottawa", 6 },
                    { "Windsor", 6 },
                    { "Kingston", 6 },
                    { "Mississauga", 6 },
                    { "Hamilton", 6 },
                    { "Los Angeles", 2 },
                    { "San Francisco", 2 },
                    { "San Diego", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actor_MovieId",
                table: "Actor",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_City_ProvinceId",
                table: "City",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_ContinentName",
                table: "Country",
                column: "ContinentName");

            migrationBuilder.CreateIndex(
                name: "IX_Organism_SpecieName",
                table: "Organism",
                column: "SpecieName");

            migrationBuilder.CreateIndex(
                name: "IX_Province_CountryName",
                table: "Province",
                column: "CountryName");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_VehicleManufacturers_VehicleManufacturerName",
                table: "Vehicles",
                column: "VehicleManufacturerName",
                principalTable: "VehicleManufacturers",
                principalColumn: "VehicleManufacturerName",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_VehicleManufacturers_VehicleManufacturerName",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "Actor");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Organism");

            migrationBuilder.DropTable(
                name: "Movie");

            migrationBuilder.DropTable(
                name: "Province");

            migrationBuilder.DropTable(
                name: "Specie");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Continent");

            migrationBuilder.AlterColumn<string>(
                name: "VehicleManufacturerName",
                table: "Vehicles",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Fuel",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "VehicleManufacturers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                table: "Foods",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Foods",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "FoodCategories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "FoodCategories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_VehicleManufacturers_VehicleManufacturerName",
                table: "Vehicles",
                column: "VehicleManufacturerName",
                principalTable: "VehicleManufacturers",
                principalColumn: "VehicleManufacturerName",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
