﻿using System.Collections.Generic;
using Api4u.Models.Restaurants;

namespace Api4u.Data.Seed
{
    public class RestaurantSeedData
    {
        public static List<Restaurant> GetRestaurants()
        {
            List<Restaurant> restaurants = new List<Restaurant>()
            {
                 new Restaurant()
                 {
                     RestaurantId = 1,
                     RestaurantName = "White Spot",
                     Street = "1096 Lougheed Highway",
                     City = "Coquitlam",
                     Province = "British Columbia",
                     PostalCode = "V5G 1U8",
                     Country = "Canada",
                     FoodType = "Western Food"
                 },
                 new Restaurant()
                 {
                     RestaurantId = 2,
                     RestaurantName = "Hons",
                     Street = "310-3025 Lougheed Highway",
                     City = "Coquitlam",
                     Province = "British Columbia",
                     PostalCode = "V6A 1C5",
                     Country = "Canada",
                     FoodType = "Chinese Food"
                 },
                 new Restaurant()
                 {
                     RestaurantId = 3,
                     RestaurantName = "Boston Pizza",
                     Street = "300 - 2325 Ottawa Street",
                     City = "Port Coquitlam",
                     Province = "British Columbia",
                     PostalCode = "V3B 8A4",
                     Country = "Canada",
                     FoodType = "Western Food"
                 },
                 new Restaurant()
                 {
                     RestaurantId = 4,
                     RestaurantName = "Maple Leaf Indian Cuisine",
                     Street = "11956 207 Street",
                     City = "Maple Ridge",
                     Province = "British Columbia",
                     PostalCode = "V2X 1X6",
                     Country = "Canada",
                     FoodType = "Indian Food"
                 },
                 new Restaurant()
                 {
                     RestaurantId = 5,
                     RestaurantName = "Socrates Grill",
                     Street = "20691 Lougheed Hwy #19",
                     City = "Maple Ridge",
                     Province = "British Columbia",
                     PostalCode = "V2X 2P9",
                     Country = "Canada",
                     FoodType = "Greek Food"
                 },
            };

            return restaurants;
        }

        public static List<Menu> GetMenuItems()
        {
            List<Menu> menuitems = new List<Menu>()
            {
                 new Menu()
                 {
                    MenuId = 1,
                    Name = "Oven Baked Lasagna",
                    Size = "Regular",
                    Price = 17.99,
                    RestaurantId = 1
                 },
                 new Menu()
                 {
                    MenuId = 2,
                    Size = "Regular",
                    Name = "Spaghetti & Meatballs",
                    Price = 17.99,
                    RestaurantId = 1
                 },
                 new Menu()
                 {
                    MenuId = 3,
                    Name = "Seafood Fettuccine",
                    Size = "Regular",
                    Price = 19.99,
                    RestaurantId = 1
                 },
                 new Menu()
                 {
                    MenuId = 4,
                    Name = "Barbecued Duck",
                    Price = 25.50,
                    Size = "Regular",
                    RestaurantId = 2
                 },
                 new Menu()
                 {
                    MenuId = 5,
                    Name = "Roasted Pork",
                    Price = 9.75,
                    Size = "Regular",
                    RestaurantId = 2
                 },
                 new Menu()
                 {
                    MenuId = 6,
                    Name = "Royal Hawaiian",
                    Size = "10 \" Small",
                    Price = 18.99,
                    RestaurantId = 3
                 },

                 new Menu()
                 {
                    MenuId = 7,
                    Name = "Royal Hawaiian",
                    Size = "13 \" Medium",
                    Price = 27.99,
                    RestaurantId = 3
                 },
                 new Menu()
                 {
                    MenuId = 8,
                    Name = "Chicken Tikka",
                    Size = "Regular",
                    Price = 4.99,
                    RestaurantId = 4
                 },
                 new Menu()
                 {
                    MenuId = 9,
                    Name = "Butter Chicken",
                    Size = "Regular",
                    Price = 12.99,
                    RestaurantId = 4
                 },
                 new Menu()
                 {
                    MenuId = 10,
                    Name = "Lamb Souvlaki",
                    Size = "Regular",
                    Price = 19.00,
                    RestaurantId = 5
                 },
            };

            return menuitems;
        }
    }
}
