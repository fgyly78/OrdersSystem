using FluentValidation;
using OrdersSystem.Application.Orders.Commands.CreateOrder;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Application.Orders.Commands.OrderValidator
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x=>x.CustomerId)
                .NotEmpty();
        }
    }
}
