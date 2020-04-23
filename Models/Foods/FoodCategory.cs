using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api4u.Models.Foods
{
    public class FoodCategory
    {
        [Key]
        public int FoodCategoryId { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public List<Food> Foods { get; set; }
    }
}