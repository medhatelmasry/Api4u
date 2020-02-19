using System.ComponentModel.DataAnnotations;

namespace Api4u.Models.Toons
{
    public class Picture {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Url { get; set; }
    }
}