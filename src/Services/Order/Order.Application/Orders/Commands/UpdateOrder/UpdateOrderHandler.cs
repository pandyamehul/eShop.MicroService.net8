namespace Order.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderHandler
    (IApplicationDbContext applicationDbContext)
    : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        // update order entity from command object
        // save entity to database
        // return the result

        var orderId = OrderId.Of(command.Order.Id); //strong type order id
        var order = await applicationDbContext.Orders
            .FindAsync([orderId], cancellationToken: cancellationToken);

        if (order is null)
        {
            throw new OrderNotFoundException(command.Order.Id);
        }

        UpdateOrderWithNewValues(order, command.Order);

        applicationDbContext.Orders.Update(order);
        await applicationDbContext.SaveChangesAsync(cancellationToken);

        return new UpdateOrderResult(true);        
    }

    private void UpdateOrderWithNewValues(Domain.Models.Order order, OrderDto orderDto)
    {
        var updatedShippingAddress = Address.Of(
            orderDto.ShippingAddress.Name, 
            orderDto.ShippingAddress.LastName, 
            orderDto.ShippingAddress.EmailAddress, 
            orderDto.ShippingAddress.AddressLine, 
            orderDto.ShippingAddress.Country, 
            orderDto.ShippingAddress.State, 
            orderDto.ShippingAddress.City, 
            orderDto.ShippingAddress.ZipCode
        );
        var updatedBillingAddress = Address.Of(
            orderDto.BillingAddress.Name, 
            orderDto.BillingAddress.LastName, 
            orderDto.BillingAddress.EmailAddress, 
            orderDto.BillingAddress.AddressLine, 
            orderDto.BillingAddress.Country, 
            orderDto.BillingAddress.State, 
            orderDto.ShippingAddress.City, 
            orderDto.BillingAddress.ZipCode
        );

        var updatedPayment =  Payment.Of(
                    orderDto.Payment.CardName, 
                    orderDto.Payment.CardNumber, 
                    orderDto.Payment.Expiration, 
                    orderDto.Payment.Cvv, 
                    orderDto.Payment.PaymentMethod
        );

        order.Update(
            orderName: OrderName.Of(orderDto.OrderName),
            shippingAddress: updatedShippingAddress,
            billingAddress: updatedBillingAddress,
            payment: updatedPayment,
            orderStatus: orderDto.OrderStatus);
    }
}
