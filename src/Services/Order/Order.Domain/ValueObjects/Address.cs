namespace Order.Domain.ValueObjects;

public record Address
{
    public string Name { get; } = default!;
    public string LastName { get; } = default!;
    public string? EmailAddress { get; } = default!;
    public string AddressLine { get; } = default!;
    public string Country { get; } = default!;
    public string State { get; } = default!;
    public string City { get; } = default!;
    public string ZipCode { get; } = default!;

    protected Address() { }

    private Address(
        string name, 
        string lastName, 
        string? emailAddress, 
        string addressLine, 
        string country, 
        string state, 
        string city, 
        string zipCode)
    {
        Name = name;
        LastName = lastName;
        EmailAddress = emailAddress;
        AddressLine = addressLine;
        Country = country;
        State = state;
        City = city;
        ZipCode = zipCode;
    }

    public static Address Of(
        string name,
        string lastName,
        string? emailAddress,
        string addressLine,
        string country,
        string state,
        string city,
        string zipCode)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(emailAddress);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(addressLine);
        return new Address(name, lastName, emailAddress, addressLine, country, state, city, zipCode);
    }
}
