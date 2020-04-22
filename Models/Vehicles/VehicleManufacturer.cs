using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api4u.Models.Vehicles
{
    public class VehicleManufacturer
    {
        [Key]
        public string VehicleManufacturerName { get; set; }

        [Required]
        public string Country { get; set; }
    }
}