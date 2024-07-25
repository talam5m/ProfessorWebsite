using FluentValidation;
using ProfessorWebsite.Models;

namespace ProfessorWebsite.Validator
{
    public class ProfessorValidator : AbstractValidator<Professor>
    {
        public ProfessorValidator() 
        {
            RuleFor(t => t.Name).NotEmpty().WithMessage("Please write your name");
            RuleFor(t => t.Class).NotEmpty().WithMessage("Please write your course");
        }
    }
}
