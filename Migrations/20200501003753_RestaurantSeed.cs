using Microsoft.EntityFrameworkCore.Migrations;

namespace Api4u.Migrations
{
    public partial class RestaurantSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "RestaurantId", "City", "Country", "FoodType", "PostalCode", "Province", "RestaurantName", "Street" },
                values: new object[,]
                {
                    { 1, "Coquitlam", "Canada", "Western Food", "V5G 1U8", "British Columbia", "White Spot", "1096 Lougheed Highway" },
                    { 2, "Coquitlam", "Canada", "Chinese Food", "V6A 1C5", "British Columbia", "Hons", "310-3025 Lougheed Highway" },
                    { 3, "Port Coquitlam", "Canada", "Western Food", "V3B 8A4", "British Columbia", "Boston Pizza", "300 - 2325 Ottawa Street" },
                    { 4, "Maple Ridge", "Canada", "Indian Food", "V2X 1X6", "British Columbia", "Maple Leaf Indian Cuisine", "11956 207 Street" },
                    { 5, "Maple Ridge", "Canada", "Greek Food", "V2X 2P9", "British Columbia", "Socrates Grill", "20691 Lougheed Hwy #19" }
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "MenuId", "Name", "Price", "RestaurantId", "Size" },
                values: new object[,]
                {
                    { 1, "Oven Baked Lasagna", 17.989999999999998, 1, "Regular" },
                    { 2, "Spaghetti & Meatballs", 17.989999999999998, 1, "Regular" },
                    { 3, "Seafood Fettuccine", 19.989999999999998, 1, "Regular" },
                    { 4, "Barbecued Duck", 25.5, 2, "Regular" },
                    { 5, "Roasted Pork", 9.75, 2, "Regular" },
                    { 6, "Royal Hawaiian", 18.989999999999998, 3, "10 \" Small" },
                    { 7, "Royal Hawaiian", 27.989999999999998, 3, "13 \" Medium" },
                    { 8, "Chicken Tikka", 4.9900000000000002, 4, "Regular" },
                    { 9, "Butter Chicken", 12.99, 4, "Regular" },
                    { 10, "Lamb Souvlaki", 19.0, 5, "Regular" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "MenuId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "MenuId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "MenuId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "MenuId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "MenuId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "MenuId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "MenuId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "MenuId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "MenuId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "MenuId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "RestaurantId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "RestaurantId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "RestaurantId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "RestaurantId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "RestaurantId",
                keyValue: 5);
        }
    }
}
