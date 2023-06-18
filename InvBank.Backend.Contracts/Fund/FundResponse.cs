namespace InvBank.Backend.Contracts.Fund;

public record FundResponse
(
    Guid Id,
    string Name,
    string InitialDate,
    int Duration,
    decimal Value,
    decimal TaxPercent
);