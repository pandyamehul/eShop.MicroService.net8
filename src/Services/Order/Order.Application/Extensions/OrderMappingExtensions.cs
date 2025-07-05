namespace Order.Application.Extensions;

public static class OrderMappingExtensions
{
    /// <summary>
    /// Converts a collection of Order entities to a list of OrderDto objects
    /// </summary>
    public static List<OrderDto> ToOrderDtoList(this IEnumerable<Order.Domain.Models.Order> orders)
    {
        return orders.Select(order => order.ToOrderDto()).ToList();
    }

    /// <summary>
    /// Converts a single Order entity to OrderDto
    /// </summary>
    public static OrderDto ToOrderDto(this Order.Domain.Models.Order order)
    {
        return new OrderDto(
            Id: order.Id.Value,
            CustomerId: order.CustomerId.Value,
            OrderName: order.OrderName.Value,
            ShippingAddress: order.ShippingAddress.ToAddressDto(),
            BillingAddress: order.BillingAddress.ToAddressDto(),
            Payment: order.Payment.ToPaymentDto(),
            OrderStatus: order.Status,
            OrderItems: order.OrderItems.ToOrderItemDtoList()
        );
    }

    /// <summary>
    /// Converts Address value object to AddressDto
    /// </summary>
    public static AddressDto ToAddressDto(this Address address)
    {
        return new AddressDto(
            Name: address.Name,
            LastName: address.LastName,
            EmailAddress: address.EmailAddress ?? string.Empty,
            AddressLine: address.AddressLine,
            City: address.City,
            State: address.State,
            Country: address.Country,
            ZipCode: address.ZipCode
        );
    }

    /// <summary>
    /// Converts Payment value object to PaymentDto
    /// </summary>
    public static PaymentDto ToPaymentDto(this Payment payment)
    {
        return new PaymentDto(
            CardName: payment.CardName ?? string.Empty,
            CardNumber: payment.CardNumber,
            Expiration: payment.Expiration,
            Cvv: payment.CVV,
            PaymentMethod: payment.PaymentMethod
        );
    }

    /// <summary>
    /// Converts a collection of OrderItem entities to a list of OrderItemDto objects
    /// </summary>
    public static List<OrderItemDto> ToOrderItemDtoList(this IEnumerable<OrderItem> orderItems)
    {
        return orderItems.Select(item => item.ToOrderItemDto()).ToList();
    }

    /// <summary>
    /// Converts OrderItem entity to OrderItemDto
    /// </summary>
    public static OrderItemDto ToOrderItemDto(this OrderItem orderItem)
    {
        return new OrderItemDto(
            OrderId: orderItem.OrderId.Value,
            ProductId: orderItem.ProductId.Value,
            Quantity: orderItem.Quantity,
            Price: orderItem.Price
        );
    }
}