namespace Order.Domain.ValueObjects;

public record OrderName
{
    private const int DefaultLength = 5;
    public string Value { get; }
    private OrderName(string orderName) => this.Value = orderName;

    public static OrderName Of(string orderName)
    {
        ArgumentNullException.ThrowIfNull(orderName);
        ArgumentOutOfRangeException.ThrowIfNotEqual(orderName.Length, DefaultLength);
        return new OrderName(orderName);
    }
}
