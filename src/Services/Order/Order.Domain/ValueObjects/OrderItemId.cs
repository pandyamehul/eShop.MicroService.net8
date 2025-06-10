namespace Order.Domain.ValueObjects;

public record OrderItemId
{
    public Guid Value { get; }
}