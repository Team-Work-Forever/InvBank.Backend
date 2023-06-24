namespace InvBank.Backend.Contracts.Deposit;

public record CreateDepositRequest
(
    string Name,
    string InitialDate,
    int Duration,
    decimal TaxPercent,
    decimal Value,
    decimal YearlyTax,
    string IBAN
);