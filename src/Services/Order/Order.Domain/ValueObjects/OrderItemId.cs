namespace Order.Domain.ValueObjects;

public record OrderItemId
{
    public Guid Value { get; }

    private OrderItemId(Guid orderItemId) => this.Value = orderItemId;

    public static OrderItemId Of(Guid orderItemId)
    {
        ArgumentNullException.ThrowIfNull(orderItemId);
        if (orderItemId == Guid.Empty)
        {
            throw new DomainException("Order Id cannot be empty");
        }
        return new OrderItemId(orderItemId);
    }
}