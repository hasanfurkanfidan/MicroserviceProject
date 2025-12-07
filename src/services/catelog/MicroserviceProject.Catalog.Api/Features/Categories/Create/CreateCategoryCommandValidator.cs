namespace MicroserviceProject.Catalog.Api.Features.Categories.Create
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Category name must not be empty")
                .Length(4, 25).WithMessage("Category name must be between 4 and 25 characters");
        }
    }
}
