namespace Order.Application.Dtos;

public record AddressDto(
    string Name,
    string LastName,
    string EmailAddress,
    string AddressLine,
    string Country,
    string State,
    string City,
    string ZipCode
);