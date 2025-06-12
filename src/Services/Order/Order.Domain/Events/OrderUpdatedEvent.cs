namespace Order.Domain.Events;

public record OrderUpdatedEvent(Order.Domain.Models.Order order) : IDomainEvent;