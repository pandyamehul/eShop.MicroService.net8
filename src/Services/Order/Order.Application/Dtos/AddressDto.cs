namespace Order.Application.Dtos;

public record AddressDto(
    string Name,
    string LastName,
    string EmailAddress,
    string AddressLine,
    string City,
    string State,
    string ZipCode,
    string Country
);