using Microsoft.EntityFrameworkCore.Migrations;

namespace Api4u.Migrations
{
    public partial class more_models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FoodCategories",
                columns: table => new
                {
                    FoodCategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodCategories", x => x.FoodCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    InstructorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.InstructorId);
                });

            migrationBuilder.CreateTable(
                name: "VehicleManufacturers",
                columns: table => new
                {
                    VehicleManufacturerName = table.Column<string>(nullable: false),
                    Country = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleManufacturers", x => x.VehicleManufacturerName);
                });

            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    FoodId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Unit = table.Column<string>(nullable: true),
                    UnitPrice = table.Column<float>(nullable: false),
                    FoodCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.FoodId);
                    table.ForeignKey(
                        name: "FK_Foods_FoodCategories_FoodCategoryId",
                        column: x => x.FoodCategoryId,
                        principalTable: "FoodCategories",
                        principalColumn: "FoodCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    InstructorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                    table.ForeignKey(
                        name: "FK_Courses_Instructors_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Instructors",
                        principalColumn: "InstructorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Model = table.Column<string>(nullable: false),
                    Fuel = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    VehicleManufacturerName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Model);
                    table.ForeignKey(
                        name: "FK_Vehicles_VehicleManufacturers_VehicleManufacturerName",
                        column: x => x.VehicleManufacturerName,
                        principalTable: "VehicleManufacturers",
                        principalColumn: "VehicleManufacturerName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "FoodCategories",
                columns: new[] { "FoodCategoryId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, @"Bakery products, which include bread, rolls, 
                                cookies, pies, pastries, and muffins, are usually prepared from flour 
                                or meal derived from some form of grain. Bread, already a common 
                                staple in prehistoric times, provides many nutrients in the human diet.", "Bakery" },
                    { 2, @"The sweet 
                                and fleshy product of a tree or other plant that contains seed 
                                and can be eaten as food.", "Fruit" },
                    { 3, @"A plant 
                                or part of a plant used as food, typically as accompaniment to meat 
                                or fish, such as a cabbage, potato, carrot, or bean.", "Vegetables" }
                });

            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "InstructorId", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "zhang.liu@qq.com", "Zhang", "Liu" },
                    { 2, "jin.ling@123.com", "Jin", "Ling" },
                    { 3, "sam.sun@gmail.com", "Sam", "Sun" },
                    { 4, "ann.fox@outlook.com", "Ann", "Fox" }
                });

            migrationBuilder.InsertData(
                table: "VehicleManufacturers",
                columns: new[] { "VehicleManufacturerName", "Country" },
                values: new object[,]
                {
                    { "Toyota", "Japan" },
                    { "Kia", "South Korea" },
                    { "Renault", "France" },
                    { "Mercedes", "Germany" },
                    { "Tesla", "USA" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "InstructorId", "Name" },
                values: new object[,]
                {
                    { "COMP2910", 1, "Project Management" },
                    { "COMP3973", 2, "ASP.NET" },
                    { "COMP3717", 3, "Android" },
                    { "COMP1536", 4, "HTML & CSS" }
                });

            migrationBuilder.InsertData(
                table: "Foods",
                columns: new[] { "FoodId", "FoodCategoryId", "Name", "Unit", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, "Croissant", "dozen", 9.98f },
                    { 3, 2, "Apples", "lbs", 0.65f },
                    { 2, 3, "Carrots", "lbs", 0.98f }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Model", "Fuel", "Type", "VehicleManufacturerName" },
                values: new object[,]
                {
                    { "Corolla", "Gas", "Sedan", "Toyota" },
                    { "Rav4", "Gas", "SUV", "Toyota" },
                    { "Soul", "Electric", "SUV", "Kia" },
                    { "Model S", "Electric", "Sedan", "Tesla" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_InstructorId",
                table: "Courses",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_FoodCategoryId",
                table: "Foods",
                column: "FoodCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleManufacturerName",
                table: "Vehicles",
                column: "VehicleManufacturerName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DropTable(
                name: "FoodCategories");

            migrationBuilder.DropTable(
                name: "VehicleManufacturers");
        }
    }
}
