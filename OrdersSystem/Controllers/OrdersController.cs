using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrdersSystem.Application.Orders.Commands.CancelOrder;
using OrdersSystem.Application.Orders.Commands.CreateOrder;
using OrdersSystem.Domain.Entities;
using OrdersSystem.Domain.ValueObjects;
using OrdersSystem.Infrastructure;

namespace OrdersSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var orderId = await _mediator.Send(command);
            return Ok(orderId);
        }

        [HttpPost("{id}/Cancel")]
        public async Task<IActionResult> CancelOrder(Guid id, [FromBody] CancelOrderCommand request)
        {
            var command = new CancelOrderCommand(id, request.Reason);
            await _mediator.Send(command);
            return Ok();
        }
    }
}
