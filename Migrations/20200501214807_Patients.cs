using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api4u.Migrations
{
    public partial class Patients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Patients",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Patients",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "FrequencyInHours",
                table: "Medicines",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "PatientId", "City", "Country", "DateOfBirth", "Email", "FirstName", "Gender", "HospitalId", "InDate", "LastName", "OutDate", "Phone", "PostalCode", "Province", "RoomNumber", "Street" },
                values: new object[,]
                {
                    { 1, "Delta", "Canada", new DateTime(1987, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "sam@fox.ca", "Sam", "M", 1, new DateTime(2020, 4, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fox", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "604-682-2344", "V6Z 1Y6", "British Columbia", "204a", "1081 River Street" },
                    { 2, "Surrey", "Canada", new DateTime(1980, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "meg@roy.ca", "Meg", "F", 1, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Roy", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "604-286-4432", "V3Z 1P6", "British Columbia", "114b", "181 Alma Road" },
                    { 3, "Port Coquitlam", "Canada", new DateTime(1977, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "joe@day.ca", "Joe", "M", 2, new DateTime(2020, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Day", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "604-943-0807", "V3P 2C4", "British Columbia", "304", "870 Pitt River Road" },
                    { 4, "Coquitlam", "Canada", new DateTime(2005, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "ann@lee.ca", "Ann", "F", 2, new DateTime(2020, 4, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lee", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "604-942-9856", "V6Y 2Y9", "British Columbia", "194", "1870 Ottawa Avenue" },
                    { 5, "New Wesminster", "Canada", new DateTime(2015, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "bob@ray.ca", "Bob", "M", 3, new DateTime(2020, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ray", new DateTime(2020, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "604-870-4031", "V7W 2Y9", "British Columbia", "619a", "7045 Main Street" }
                });

            migrationBuilder.InsertData(
                table: "Sicknesses",
                columns: new[] { "SicknessId", "PatientId", "SicknessName" },
                values: new object[,]
                {
                    { 5, 1, "Maternity" },
                    { 3, 2, "High Blood Pressure" },
                    { 4, 3, "COVID-19" },
                    { 1, 4, "Diabetes" },
                    { 2, 4, "PTSD" }
                });

            migrationBuilder.InsertData(
                table: "Medicines",
                columns: new[] { "MedicineId", "Dosage", "DosageUnit", "FrequencyInHours", "Name", "SicknessId" },
                values: new object[,]
                {
                    { 5, 20, "mg", 24, "Benicar", 3 },
                    { 1, 50, "mg", 8, "Acarbose", 1 },
                    { 2, 850, "mg", 12, "Metformin", 1 },
                    { 3, 150, "mg", 6, "Prazosin", 2 },
                    { 4, 180, "mg", 8, "Zoloft", 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "MedicineId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "MedicineId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "MedicineId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "MedicineId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "MedicineId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Sicknesses",
                keyColumn: "SicknessId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Sicknesses",
                keyColumn: "SicknessId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Sicknesses",
                keyColumn: "SicknessId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sicknesses",
                keyColumn: "SicknessId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sicknesses",
                keyColumn: "SicknessId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "FrequencyInHours",
                table: "Medicines");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "DateOfBirth",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime));
        }
    }
}
