using System.ComponentModel.DataAnnotations;

namespace Api4u.Models.Species
{
    public class Specie
    {
        [Key]
        public string SpecieName { get; set; }
    }
}