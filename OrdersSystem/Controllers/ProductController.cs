using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrdersSystem.Application.Customers.Command;
using OrdersSystem.Application.Products;
using OrdersSystem.Application.Products.Commands;
using OrdersSystem.Application.Products.Commands.CreateProduct;
using OrdersSystem.Application.Products.Commands.RepnenishStock;
using OrdersSystem.Application.Products.Queries;
using OrdersSystem.Application.Products.Queries.GetAvailableProducts;
using OrdersSystem.Application.Products.Queries.GetProductById;
using OrdersSystem.Requests.Products;

namespace OrdersSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command, CancellationToken ct)
        {
            var productId = await _mediator.Send(command, ct);
            return Ok(productId);
        }

        [HttpPost("{id}/replenish")]
        public async Task<IActionResult> ReplenishStock(Guid id, [FromBody] ReplenishStockRequest request, CancellationToken ct)
        {
            var command = new ReplenishStockCommand(id, request.Quantity);
            await _mediator.Send(command, ct);
            return Ok();
        }

        [HttpPut("{id}/price")]
        public async Task<IActionResult> UpdatePrice(Guid id, [FromBody] UpdatePriceRequest request, CancellationToken ct)
        {
            var command = new UpdatePriceCommand(id, request.Price, request.Currency);
            await _mediator.Send(command, ct);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProduct(Guid id, CancellationToken ct)
        {
            var query = new GetProductByIdQuery(id);
            return Ok(await _mediator.Send(query, ct));
        }

        [HttpGet("available")]
        public async Task<ActionResult> GetAvailableProducts(CancellationToken ct)
        {
            var query = new GetAvailableProductsQuery();
            return Ok(await _mediator.Send(query, ct));
        }
    }
}
