using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrdersSystem.Application.Orders.Commands.AddOrderItem;
using OrdersSystem.Application.Orders.Commands.CancelOrder;
using OrdersSystem.Application.Orders.Commands.CreateOrder;
using OrdersSystem.Application.Orders.Commands.PayOrder;
using OrdersSystem.Application.Orders.Queries.GetOrderByCustomer;
using OrdersSystem.Application.Orders.Queries.GetOrderById;
using OrdersSystem.Domain.Entities;
using OrdersSystem.Domain.ValueObjects;
using OrdersSystem.Infrastructure;
using OrdersSystem.Requests;

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
        public async Task<IActionResult> CancelOrder(Guid id, [FromBody] CancelOrderRequest request)
        {
            var command = new CancelOrderCommand(id, request.Reason);
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("{id}/Pay")]
        public async Task<IActionResult> PayOrder(Guid id)
        {
            var command = new PayOrderCommand(id);
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("{id}/AddItem")]
        public async Task<IActionResult> AddItem(Guid id, [FromBody] AddItemOrderRequest request)
        {
            var command = new AddOrderItemCommand(id, request.ProductId, request.ProductName, request.UnitPrice, request.Currency, request.Quantity);
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(Guid id)
        {
            var query = new GetOrderByIdQuery(id);
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<List<OrderDto>>> GetOrderByCustomer(Guid customerId)
        {
            var query = new GetOrderByCustomerQuery(customerId);
            return Ok(await _mediator.Send(query));
        }
    }
}
