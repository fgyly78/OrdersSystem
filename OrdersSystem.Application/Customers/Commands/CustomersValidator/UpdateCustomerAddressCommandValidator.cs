using FluentValidation;
using OrdersSystem.Application.Customers.Commands.UpdateCustomerAddress;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Application.Customers.Commands.CustomersValidator
{
    public class UpdateCustomerAddressCommandValidator : AbstractValidator<UpdateCustomerAddresCommand>
    {
        public UpdateCustomerAddressCommandValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty();
            RuleFor(x => x.Street)
                .NotEmpty();
            RuleFor(x => x.City)
                .NotEmpty();
            RuleFor(x => x.Country)
                .NotEmpty()
                .Length(3);
            RuleFor(x => x.PostalCode)
                .NotEmpty();
        }
    }
}
