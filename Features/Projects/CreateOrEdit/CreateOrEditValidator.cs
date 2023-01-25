
using FluentValidation;

namespace DotnetPlayground.Features.Projects.CreateOrEdit
{
    public class CreateOrEditValidator : AbstractValidator<CreateOrEditRequest>
    {
        public CreateOrEditValidator() 
        {
            RuleFor(x => x.title).NotNull();
            RuleFor(x => x.color).NotNull();
        }
    }
}
