namespace InvBank.Backend.Contracts.Authentication;

public record ProfileResponse
(
    Guid Id,
    string Email,
    string FirstName,
    string LastName,
    string BirthDate,
    string Nif,
    string Cc,
    string Phone,
    string PostalCode
);