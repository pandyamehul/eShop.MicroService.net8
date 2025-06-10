namespace Order.Domain.ValueObjects;

public record ProductId
{
    public Guid Value { get; }
}