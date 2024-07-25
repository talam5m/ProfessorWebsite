using ProfessorWebsite.Models;
using System.ComponentModel.DataAnnotations;

namespace ProfessorWebsite.Dtos
{
    public class ProfessorDto
    {
        public Guid ProfessorId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Class { get; set; } = string.Empty;

        public Course Course { get; set; }
    }
}
