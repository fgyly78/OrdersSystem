using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrdersSystem.Application.Customers.Command;
using OrdersSystem.Application.Customers.Commands.DeactivateCustomer;
using OrdersSystem.Application.Customers.Commands.RegisterCustomer;
using OrdersSystem.Application.Customers.Commands.UpdateCustomerAddress;
using OrdersSystem.Application.Customers.Queries;
using OrdersSystem.Application.Customers.Queries.GetCustomerById;
using OrdersSystem.Application.Products.Queries.GetCustomerProducts;
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

        [HttpPost("register")]
        public async Task<IActionResult> RegisterCustomer([FromBody] RegisterCustomerCommand command, CancellationToken ct)
        {
            var customerId = await _mediator.Send(command, ct);
            return Ok(customerId);
        }

        [HttpPut("{id}/address")]
        public async Task<IActionResult> UpdateAddress(Guid id, [FromBody] UpdateCustomerAddressRequest request, CancellationToken ct)
        {
            var command = new UpdateCustomerAddresCommand(id, request.Street, request.City, request.Country, request.PostalCode);
            await _mediator.Send(command, ct);
            return Ok(command);
        }

        [HttpPost("{id}/deactivate")]
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
            return Ok(await _mediator.Send(query, ct));
        }

        [HttpGet("{customerId}/products")]
        public async Task<ActionResult<List<CustomerProductSummaryDto>>> GetCustomerProducts(
            Guid customerId, CancellationToken ct)
        {
            var query = new GetProductsBuCustomerQuery(customerId);
            return Ok(await _mediator.Send(query, ct));
        }
    }
}
