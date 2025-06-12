namespace Order.Domain.Events;

public record OrderCreatedEvent(Order.Domain.Models.Order order) : IDomainEvent;