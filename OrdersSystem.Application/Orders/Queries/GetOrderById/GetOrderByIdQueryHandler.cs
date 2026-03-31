using MediatR;
using OrdersSystem.Domain.Common;
using OrdersSystem.Domain.Repositories;
using OrdersSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Application.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderDto> Handle(GetOrderByIdQuery query, CancellationToken ct)
        {
            var order = await _orderRepository.GetByIdAsync(new OrderId(query.OrderId), ct);

            if (order is null) throw new DomainException("Order not found");

            return new OrderDto
            {
                Id = order.Id.Value,
                CustomerId = order.CustomerId.Value,
                Status = order.Status.ToString(),
                CreatedAt = order.CreatedAt,
                TotalPrice = order.TotalPrice.Amount,
                ShippingAddress = $"{order.ShippingAddress.Street}, {order.ShippingAddress.City}, {order.ShippingAddress.Country}, {order.ShippingAddress.PostalCode}",
                Items = order.Items.Select(i => new OrderItemDto
                {
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice.Amount,
                    TotalPrice = i.TotalPrice.Amount,
                }).ToList(),
            };
        }
    }
}
