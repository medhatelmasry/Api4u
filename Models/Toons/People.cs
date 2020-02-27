using System.ComponentModel.DataAnnotations;

namespace Api4u.Models.Toons
{
    public partial class People
    {
        public int Id { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Occupation { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        public int Votes { get; set; }
    }
}