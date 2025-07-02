namespace Order.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler
    (IApplicationDbContext applicationDbContext)
    : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        // Create order entity from command object
        // save entity to database
        // return the result

        var order = CreateNewOrder(command.Order);
        applicationDbContext.Orders.Add(order);
        await applicationDbContext.SaveChangesAsync(cancellationToken);
        return new CreateOrderResult(order.Id.Value);        
    }

    private Domain.Models.Order CreateNewOrder(OrderDto orderDto)
    {
        var shippingAddress = Address.Of(
            orderDto.ShippingAddress.Name, 
            orderDto.ShippingAddress.LastName, 
            orderDto.ShippingAddress.EmailAddress, 
            orderDto.ShippingAddress.AddressLine, 
            orderDto.ShippingAddress.Country, 
            orderDto.ShippingAddress.State, 
            orderDto.ShippingAddress.City, 
            orderDto.ShippingAddress.ZipCode
        );
        var billingAddress = Address.Of(
            orderDto.BillingAddress.Name, 
            orderDto.BillingAddress.LastName, 
            orderDto.BillingAddress.EmailAddress, 
            orderDto.BillingAddress.AddressLine, 
            orderDto.BillingAddress.Country, 
            orderDto.BillingAddress.State, 
            orderDto.ShippingAddress.City, 
            orderDto.BillingAddress.ZipCode
        );

        var newOrder = Domain.Models.Order.Create(
                orderId : OrderId.Of(Guid.NewGuid()),
                customerId: CustomerId.Of(orderDto.CustomerId),
                orderName: OrderName.Of(orderDto.OrderName),
                shippingAddress: shippingAddress,
                billingAddress: billingAddress,
                payment: Payment.Of(
                    orderDto.Payment.CardName, 
                    orderDto.Payment.CardNumber, 
                    orderDto.Payment.Expiration, 
                    orderDto.Payment.Cvv, 
                    orderDto.Payment.PaymentMethod
        ));

        foreach (var orderItemDto in orderDto.OrderItems)
        {
            newOrder.Add(
                ProductId.Of(orderItemDto.ProductId), 
                orderItemDto.Quantity, 
                orderItemDto.Price
            );
        }
        return newOrder;
    }
}
