using System.Collections.Generic;

namespace Api4u.Models.Courses
{
    public class Instructor
    {
        public int InstructorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public List<Course> Courses { get; set; }
    }
}