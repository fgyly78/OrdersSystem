using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrdersSystem.Application.Customers.Command;
using OrdersSystem.Application.Customers.Queries;
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
        public async Task<IActionResult> RegisterCustomer([FromBody] RegisterCustomerCommand command, CancellationToken ct)
        {
            var customerId = await _mediator.Send(command, ct);
            return Ok(customerId); 
        }

        [HttpPut("{id}/Address")]
        public async Task<IActionResult> UpdateAddress(Guid id, [FromBody] UpdateCustomerAddressRequest request, CancellationToken ct)
        {
            var command = new UpdateCustomerAddresCommand(id, request.Street, request.City, request.Country, request.PostalCode);
            await _mediator.Send(command, ct);
            return Ok(command);
        }

        [HttpPost("{id}/Deactivate")]
        public async Task<IActionResult> DeactivateCustomer(Guid id, CancellationToken ct)
        {
            var command = new DeactivateCustomerCommand(id);
            await _mediator.Send(command, ct);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCustomer(Guid id, CancellationToken ct)
        {
            var query = new GetCustomerByIdQuery(id);
            return Ok(await  _mediator.Send(query, ct));
        }
    }
}
