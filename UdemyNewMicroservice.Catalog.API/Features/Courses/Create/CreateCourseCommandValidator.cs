using FluentValidation;

namespace UdemyNewMicroservice.Catalog.API.Features.Courses.Create
{
    public class CreateCourseCommandValidator:AbstractValidator<CreateCourseCommand>
    {
        public CreateCourseCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("{PropertyName} cannot be empty.").MaximumLength(100).WithMessage("{PropertyName} must be max 100 characters.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("{PropertyName} cannot be empty.").MaximumLength(100).WithMessage("{PropertyName} must be max 1000 characters.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("{PropertyName} must be greater than zero.");

        }
    }
}
