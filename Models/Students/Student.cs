using System.ComponentModel.DataAnnotations;

namespace Api4u.Models.Students
{
    public class Student
    {
        public string StudentId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string School { get; set; }
    }
}