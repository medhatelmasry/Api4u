using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api4u.Models.Countries
{
    public class Province
    {
        [Key]
        public int ProvinceId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string CapitalCity { get; set; }

        [Required]
        public string CountryName { get; set; }

        [ForeignKey("CountryName")] 
        public Country Country { get; set; }
    }
}