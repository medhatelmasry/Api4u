using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api4u.Models.Courses
{
    public class Course
    {
        [Key]
        public string CourseId { get; set; }
        public string Name { get; set; }
        public int InstructorId { get; set; }
                
        [ForeignKey("InstructorId")] 
        public Instructor Instructor { get; set; }
    }
}