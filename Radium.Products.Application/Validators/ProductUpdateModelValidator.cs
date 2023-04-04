using FluentValidation;
using Radium.Products.Rest.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radium.Products.Application.Validators
{
    public class ProductUpdateModelValidator : AbstractValidator<ProductUpdateModel>
    {
        public ProductUpdateModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("{PropertyName} must not be empty");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("{PropertyName} must not be empty");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("{PropertyName} must not be negative");
        }
    }
}
