using FluentValidation;

namespace UdemyNewMicroservice.Catalog.API.Features.Categories.Create
{
    public class CreateCourseCommandValidator:AbstractValidator<CreateCategoryCommand>
    {
        public CreateCourseCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("{PropertyName} cannot be empty.").Length(4,25).WithMessage("{PropertyName} must be between 4 and 25 characters.");

        }
    }
}
