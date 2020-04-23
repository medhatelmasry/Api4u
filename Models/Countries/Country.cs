using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api4u.Models.Countries
{
    public class Country
    {
        [Key]
        public string CountryName { get; set; }

        [Required]
        public string CapitalCity { get; set; }

        [Required]
        public int AreaSqKm { get; set; }

        [Required]
        public string ContinentName { get; set; }

        [ForeignKey("ContinentName")] 
        public Continent Continent { get; set; }
    }
}