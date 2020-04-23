using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api4u.Models.Species
{
    public class Organism
    {
        public int OrganismId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string SpecieName { get; set; }

        [ForeignKey("SpecieName")] 
        public Specie Specie { get; set; }
    }
}