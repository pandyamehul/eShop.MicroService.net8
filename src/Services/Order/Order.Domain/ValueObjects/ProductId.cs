namespace Order.Domain.ValueObjects;

public record ProductId
{
    public Guid Value { get; }

    private ProductId(Guid productId) => this.Value = productId;

    public static ProductId Of(Guid productId)
    {
        ArgumentNullException.ThrowIfNull(productId);
        if (productId == Guid.Empty)
        {
            throw new DomainException("product Id cannot be empty");
        }
        return new ProductId(productId);
    }
}