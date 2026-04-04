using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrdersSystem.Application.Orders.Commands.AddOrderItem;
using OrdersSystem.Application.Orders.Commands.CancelOrder;
using OrdersSystem.Application.Orders.Commands.CreateOrder;
using OrdersSystem.Application.Orders.Commands.PayOrder;
using OrdersSystem.Application.Orders.Queries;
using OrdersSystem.Application.Orders.Queries.GetOrderByCustomer;
using OrdersSystem.Application.Orders.Queries.GetOrderById;
using OrdersSystem.Application.Products.Queries.GetCustomerProducts;
using OrdersSystem.Domain.Entities;
using OrdersSystem.Domain.ValueObjects;
using OrdersSystem.Infrastructure;
using OrdersSystem.Requests.Ordrers;

namespace OrdersSystem.Controllers
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
