using MediatR;
using Microsoft.AspNetCore.Mvc;
using Orders.Application.Orders.Commands.AddOrderItem;
using Orders.Application.Orders.Commands.CancelOrder;
using Orders.Application.Orders.Commands.CreateOrder;
using Orders.Application.Orders.Commands.PayOrder;
using Orders.Application.Orders.Queries;
using Orders.Application.Orders.Queries.GetOrderByCustomer;
using Orders.Application.Orders.Queries.GetOrderById;
using Orders.Requests.Ordrers;
using Orders.Application.Products.Queries.GetCustomerProducts;
using Orders.Domain.Entities;
using Orders.Domain.ValueObjects;
using Orders.Infrastructure;

namespace Orders.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{id}/create")]
        public async Task<IActionResult> CreateOrder(Guid id, CancellationToken ct)
        {
            var command = new CreateOrderCommand(id);
            var orderId = await _mediator.Send(command, ct);
            return Ok(orderId);
        }

        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> CancelOrder(Guid id, [FromBody] CancelOrderRequest request, CancellationToken ct)
        {
            var command = new CancelOrderCommand(id, request.Reason);
            await _mediator.Send(command, ct);
            return Ok();
        }

        [HttpPost("{id}/pay")]
        public async Task<IActionResult> PayOrder(Guid id, CancellationToken ct)
        {
            var command = new PayOrderCommand(id);
            await _mediator.Send(command, ct);
            return Ok();
        }

        [HttpPost("{id}/addItem")]
        public async Task<IActionResult> AddItem(Guid id, [FromBody] AddItemOrderRequest request, CancellationToken ct)
        {
            var command = new AddOrderItemCommand(id, request.ProductId, request.Quantity);
            await _mediator.Send(command, ct);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(Guid id, CancellationToken ct)
        {
            var query = new GetOrderByIdQuery(id);
            return Ok(await _mediator.Send(query,ct));
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<List<OrderDto>>> GetOrderByCustomer(Guid customerId, CancellationToken ct)
        {
            var query = new GetOrderByCustomerQuery(customerId);
            return Ok(await _mediator.Send(query, ct));
        }
    }
}
