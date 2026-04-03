using FluentValidation;
using OrdersSystem.Application.Products.Commands.CreateProduct;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Application.Products.Commands.ProductsValidator
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
