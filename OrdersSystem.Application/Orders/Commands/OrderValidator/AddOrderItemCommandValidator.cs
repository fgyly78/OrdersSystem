using FluentValidation;
using OrdersSystem.Application.Orders.Commands.AddOrderItem;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Application.Orders.Commands.OrderValidator
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
