using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Orders.Application.Orders.Commands.CreateOrder;

namespace Orders.Application.Orders.Commands.OrderValidator
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
