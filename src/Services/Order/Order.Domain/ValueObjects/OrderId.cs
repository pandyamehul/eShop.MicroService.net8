namespace Order.Domain.ValueObjects;

public record OrderId
{
    public Guid Value { get; }

    private OrderId(Guid orderId) => this.Value = orderId;

    public static OrderId Of(Guid orderId)
    {
        ArgumentNullException.ThrowIfNull(orderId);
        if (orderId == Guid.Empty)
        {
            throw new DomainException("Order Id cannot be empty");
        }
        return new OrderId(orderId);
    }
}