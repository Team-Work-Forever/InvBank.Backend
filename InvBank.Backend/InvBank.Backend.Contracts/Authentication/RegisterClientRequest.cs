namespace InvBank.Backend.Contracts;

public record RegisterClientRequest
(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string BirthDate,
    string Nif,
    string Cc,
    string Phone,
    string PostalCode
);
