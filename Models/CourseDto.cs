using System.ComponentModel.DataAnnotations;

namespace ProfessorWebsite.Models
{
    public class CourseDto

    {
        public int CourseId { get; set; }

        [Display(Name= "Professor")]
        public Guid ProfessorId { get; set; }
        public string Name { get; set; }
        public List<Professor>? Professors { get; set; }

    }
}
