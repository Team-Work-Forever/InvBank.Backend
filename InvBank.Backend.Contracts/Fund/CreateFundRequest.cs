namespace InvBank.Backend.Contracts.Fund;

public record CreateFundRequest
(
    string Name,
    string InitialDate,
    int Duration,
    decimal Value,
    decimal TaxPercent,
    string Account
);