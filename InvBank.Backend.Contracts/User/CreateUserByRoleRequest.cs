namespace InvBank.Backend.Contracts.User;

public record CreateUserByRoleRequest
(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string BirthDate,
    string Nif,
    string Cc,
    string Phone,
    string PostalCode,
    int UserRole
);