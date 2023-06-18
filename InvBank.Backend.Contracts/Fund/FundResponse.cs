namespace InvBank.Backend.Contracts.Fund;

public record FundResponse
(
    string Name,
    string InitialDate,
    int Duration,
    decimal Value,
    decimal TaxPercent
);