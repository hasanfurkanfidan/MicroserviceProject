namespace MicroserviceProject.Catalog.Api.Features.Courses.Create
{
    public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
    {
        public CreateCourseCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is requiered")
                .MaximumLength(100).WithMessage("{PropertyName} is not exceed 100 characters");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("{PropertyName} is requiered")
                .MaximumLength(1000).WithMessage("{PropertyName} is not exceed 1000 characters");

            RuleFor(p=>p.Price)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than zero");

            RuleFor(p=>p.CategoryId)
                .NotEmpty().WithMessage("{PropertyName} is requiered");
        }
    }
}
