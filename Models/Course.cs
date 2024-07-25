using ProfessorWebsite.Dtos;

namespace ProfessorWebsite.Models
{
    public class Course
    {
        public int CourseId { get; set; }
       
        public string? Name { get; set; }
        public Guid ProfessorId { get; set; }
        public virtual Professor Professor { get; set; }

        
    }
}
