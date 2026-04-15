using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Orders.Application.Products.Commands.CreateProduct;

namespace Orders.Application.Products.Commands.ProductsValidator
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x=> x.Name)
                .NotEmpty()
                .MaximumLength(200);
            RuleFor(x => x.Price)
                .GreaterThan(0);
            RuleFor(x=>x.Currency)
                .NotEmpty()
                .Length(3);
            RuleFor(x => x.InitialStock)
                .GreaterThanOrEqualTo(0);
        }
    }
}
