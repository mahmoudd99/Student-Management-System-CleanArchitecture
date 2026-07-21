using FluentValidation;
using MyApp.Application.Command;


namespace MyApp.Application.Features.Students.Validators
{
    public class AddStudentCommandValidator : AbstractValidator<CreateStudentCommand>
    {
        public AddStudentCommandValidator()
        {
            RuleFor(x => x.FName)
                .NotEmpty()
                .WithMessage("First Name is required.")
                .MaximumLength(50);

            RuleFor(x => x.LName)
                .NotEmpty()
                .WithMessage("Last Name is required.")
                .MaximumLength(50);

            RuleFor(x => x.Age)
                .GreaterThanOrEqualTo(18)
                .WithMessage("Student age must be at least 18.");
        }
    }
}