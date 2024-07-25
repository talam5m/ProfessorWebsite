using System.ComponentModel.DataAnnotations;

namespace ProfessorWebsite.Models
{
    public class Professor
    {
        public Guid ProfessorId { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Username is required")]
        [StringLength(35, ErrorMessage = "Username cannot be longer than 35 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Class is required")]
        public string Class { get; set; }= string.Empty;
        
       public virtual List<Course>? Courses { get; set; }

       // public string Image { get; set; }
    }
}
