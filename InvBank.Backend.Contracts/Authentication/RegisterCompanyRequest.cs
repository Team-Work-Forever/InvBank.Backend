namespace InvBank.Backend.Contracts.Authentication;

public record RegisterCompanyRequest
(
    string Email,
    string Password,
    string Name,
    string Phone,
    string PostalCode,
    string Nif
);