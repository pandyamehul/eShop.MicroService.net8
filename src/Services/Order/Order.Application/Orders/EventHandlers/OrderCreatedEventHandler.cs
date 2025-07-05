namespace Order.Application.Orders.EventHandlers;

public class OrderCreatedEventHandler
    (ILogger<OrderCreatedEventHandler> logger)
    : INotificationHandler<OrderCreatedEvent>
{
    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event Handled in OrderCreatedEventHandler: Order with ID {OrderId} created .", notification.order.Id);
        logger.LogInformation("Order Details: {DomainEvent}", notification.GetType().Name);
        // Here you can add logic to handle the order creation event, such as sending a confirmation
        return Task.CompletedTask;
    }
}
