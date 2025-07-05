
using Microsoft.EntityFrameworkCore;

namespace Order.Application.Orders.Queries;

public class GetOrdersByNameHandler
    (IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
    {
        //get orders by name
        // return result
        var orders = await dbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking() // Use AsNoTracking for read-only queries to improve performance
            .Where(o => o.OrderName.Contains(query.Name, StringComparison.OrdinalIgnoreCase))
            .OrderBy(o => o.OrderName)
            .ToListAsync(cancellationToken);
        var orderDtos = ProjectToOrderDto(orders);
        return new GetOrdersByNameResult(orderDtos);
    }

    private static List<OrderDto> ProjectToOrderDto(List<Order> orders)
    {
        return orders.Select(order => new OrderDto
        (
            Id: order.Id.Value,
            CustomerId: order.CustomerId.Value,
            OrderName: order.OrderName,
            ShippingAddress: MapToAddressDto(order.ShippingAddress),
            BillingAddress: MapToAddressDto(order.BillingAddress),
            Payment: MapToPaymentDto(order.Payment),
            OrderStatus: order.OrderStatus,
            OrderItems: order.OrderItems.Select(MapToOrderItemDto).ToList()
        )).ToList();
    }

    private static AddressDto MapToAddressDto(Address address) => new(
        address.Name,
        address.LastName,
        address.EmailAddress,
        address.AddressLine,
        address.City,
        address.State,
        address.ZipCode,
        address.Country
    );

    private static PaymentDto MapToPaymentDto(Payment payment) => new(
        payment.CardName,
        payment.CardNumber,
        payment.Expiration,
        payment.Cvv,
        payment.PaymentMethod
    );

    private static OrderItemDto MapToOrderItemDto(OrderItem orderItem) => new(
        orderItem.OrderId.Value,
        orderItem.ProductId.Value,
        orderItem.Quantity,
        orderItem.Price
    );
}
