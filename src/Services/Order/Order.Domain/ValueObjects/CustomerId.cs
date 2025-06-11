namespace Order.Domain.ValueObjects;

public record CustomerId
{
    public Guid Value { get; }

    private CustomerId(Guid customerId) => this.Value = customerId;

    public static CustomerId Of(Guid customerId)
    {
        ArgumentNullException.ThrowIfNull(customerId);
        if (customerId == Guid.Empty)
        {
            throw new DomainException("Customer Id cannot be empty");
        }
        return new CustomerId(customerId);
    }
}
