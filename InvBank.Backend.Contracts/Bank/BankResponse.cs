namespace InvBank.Backend.Contracts.Bank;

public record BankResponse
(
    string Iban,
    string Phone,
    string PostalCode
);