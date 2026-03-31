using MediatR;
using OrdersSystem.Application.Orders.Queries.GetOrderById;
using OrdersSystem.Domain.Common;
using OrdersSystem.Domain.Repositories;
using OrdersSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Application.Orders.Queries.GetOrderByCustomer
{
    public class GetOrderByCustomerQueryHandler : IRequestHandler<GetOrderByCustomerQuery, List<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderByCustomerQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<OrderDto>> Handle(GetOrderByCustomerQuery query, CancellationToken ct)
        {
            var orders = await _orderRepository.GetByCustomerIdAsync(new CustomerId(query.CustomerId), ct);

            if (orders is null) throw new DomainException("Order not found");

            return orders.Select(order => new OrderDto
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
                }).ToList()
            }).ToList();
        }
    }
}
