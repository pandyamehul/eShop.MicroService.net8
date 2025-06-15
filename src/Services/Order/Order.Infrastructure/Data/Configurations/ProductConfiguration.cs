namespace Order.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(product => product.Id);
        builder.Property(product => product.Id).HasConversion(
            ProductId => ProductId.Value,
            dbId => ProductId.Of(dbId)
        );
        builder.Property(productName => productName.Name).IsRequired().HasMaxLength(100);
    }
}
