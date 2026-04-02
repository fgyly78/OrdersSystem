using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrdersSystem.Application.Customers.Command;
using OrdersSystem.Requests;
using OrdersSystem.Requests.Customers;

namespace OrdersSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterCustomer([FromBody] RegisterCustomerCommand command)
        {
            var customerId = await _mediator.Send(command);
            return Ok(customerId);
        }

        [HttpPut("{id}/address")]
        public async Task<IActionResult> UpdateAddress(Guid id, [FromBody] UpdateCustomerAddressRequest request)
        {
            var command = new UpdateCustomerAddresCommand(id, request.Street, request.City, request.Country, request.PostalCode);
            await _mediator.Send(command);
            return Ok(command);
        }
    }
}
