namespace InvBank.Backend.Contracts.Account;

public record AccountResponse
(
    string IBAN,
    string Bank,
    IEnumerable<dynamic> deposits,
    IEnumerable<dynamic> properties
);