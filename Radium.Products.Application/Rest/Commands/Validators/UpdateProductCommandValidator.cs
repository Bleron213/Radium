using FluentValidation;
using Radium.Products.Application.Validators;

namespace Radium.Products.Application.Rest.Commands.Validators
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Model).SetValidator(new ProductUpdateModelValidator());
        }
    }
}
