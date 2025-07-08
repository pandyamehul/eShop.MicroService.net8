using MassTransit;
using Order.Application.Extensions;

namespace Order.Application.Orders.EventHandlers.Domain;

public class OrderCreatedEventHandler
    (IPublishEndpoint publishEndpoint, ILogger<OrderCreatedEventHandler> logger)
    : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event Handled in OrderCreatedEventHandler: Order with ID {OrderId} created .", domainEvent.order.Id);
        logger.LogInformation("Domain Event Handled: {DomainEvent}", domainEvent.GetType().Name);
        // Here you can add logic to handle the order creation event, such as sending a confirmation

        var orderCreatedIntegrationEvent = domainEvent.order.ToOrderDto();
        await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
    }
}
