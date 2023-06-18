namespace InvBank.Backend.Contracts.Bank;

public record RegisterBankRequest
(
    string Iban,
    string Phone,
    string PostalCode
);