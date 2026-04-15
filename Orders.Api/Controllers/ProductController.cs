using MediatR;
using Microsoft.AspNetCore.Mvc;
using Orders.Application.Products.Commands;
using Orders.Application.Products.Commands.CreateProduct;
using Orders.Application.Products.Commands.RepnenishStock;
using Orders.Application.Products.Queries.GetAvailableProducts;
using Orders.Application.Products.Queries.GetProductById;
using Orders.Requests.Products;
using Orders.Application.Customers.Command;
using Orders.Application.Products;
using Orders.Application.Products.Queries;

namespace Orders.Controllers
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
