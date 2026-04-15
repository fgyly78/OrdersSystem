using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Orders.Application.Customers.Commands.RegisterCustomer;

namespace Orders.Application.Customers.Command.CustomersValidator
{
    public class RegisterCustomerCommandValidator : AbstractValidator<RegisterCustomerCommand>
    {
        public RegisterCustomerCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
