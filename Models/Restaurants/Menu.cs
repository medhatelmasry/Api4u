﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api4u.Models.Restaurants
{
    public class Menu
    {
        public int MenuId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Size { get; set; }

        [Required]
        public double Price { get; set; }

        public int RestaurantId { get; set; }

        [ForeignKey("RestaurantId")]
        public Restaurant Restaurant { get; set; }
    }
}
