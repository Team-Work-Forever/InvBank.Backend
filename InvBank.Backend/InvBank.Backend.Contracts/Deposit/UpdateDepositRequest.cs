namespace InvBank.Backend.Contracts.Deposit;

public record UpdateDepositRequest
(
    string Name,
    int Duration,
    decimal TaxPercent,
    decimal Value,
    decimal YearlyTax
);