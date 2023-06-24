namespace InvBank.Backend.Contracts.User;

public record UpdateUserByRoleRequest
(
    string Email,
    string FirstName,
    string LastName,
    string BirthDate,
    string Nif,
    string Cc,
    string Phone,
    string PostalCode,
    int UserRole
);