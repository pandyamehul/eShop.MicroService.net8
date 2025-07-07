using System.Diagnostics;

namespace BuildingBlocks.Messaging.Events;

public record IntegrationEvent {
    public Guid Id => Guid.NewGuid();
    public DateTime EventTime => DateTime.UtcNow;
    public string EventType => GetType().AssemblyQualifiedName;
}