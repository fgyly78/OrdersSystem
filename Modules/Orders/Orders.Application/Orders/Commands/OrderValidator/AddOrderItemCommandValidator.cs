using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Orders.Application.Orders.Commands.AddOrderItem;

namespace Orders.Application.Orders.Commands.OrderValidator
{
    public class AddOrderItemCommandValidator : AbstractValidator<AddOrderItemCommand>
    {
        public AddOrderItemCommandValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty();
            RuleFor(x => x.ProductId)
                .NotEmpty();
            RuleFor(x => x.Quantity)
                .GreaterThan(0);
        }
    }
}
