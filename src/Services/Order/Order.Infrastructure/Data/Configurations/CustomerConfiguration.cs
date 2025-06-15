namespace Order.Infrastructure.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(customer => customer.Id);
        //Below custom conversion is used to map CustomerId object to SQL Db column
        builder.Property(customer => customer.Id).HasConversion(
            CustomerId => CustomerId.Value,
            dbId => CustomerId.Of(dbId)
        );
        builder.Property(customerName => customerName.Name).IsRequired().HasMaxLength(100);
        builder.Property(CustomerEmail => CustomerEmail.Email).IsRequired().HasMaxLength(100);
        builder.HasIndex(cusromerEmail => cusromerEmail.Email).IsUnique();
    }
}
