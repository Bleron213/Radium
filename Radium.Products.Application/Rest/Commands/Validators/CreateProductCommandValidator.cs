using FluentValidation;
using Radium.Products.Application.Validators;

namespace Radium.Products.Application.Rest.Commands.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Model).SetValidator(new ProductCreateModelValidator());
        }
    }
}
