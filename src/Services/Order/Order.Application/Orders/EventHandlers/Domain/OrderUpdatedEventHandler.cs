namespace Order.Application.Orders.EventHandlers.Domain;

public class OrderUpdatedEventHandler
    (ILogger<OrderUpdatedEventHandler> logger)
    : INotificationHandler<OrderUpdatedEvent>
{
    public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event Handled in OrderUpdatedEventHandler: Order with ID {OrderId} updated.", notification.order.Id);
        logger.LogInformation("Order Details: {DomainEvent}", notification.GetType().Name);
        // Here you can add logic to handle the order update event, such as sending a confirmation
        return Task.CompletedTask;
    }
}