using MediatR;

namespace Order.Domain.Abstractions;

public interface IDomainEvent : INotification
{
    Guid EventId => Guid.NewGuid();
    public DateTime EventDttm => DateTime.UtcNow;
    public string EventType => GetType().AssemblyQualifiedName;
}
