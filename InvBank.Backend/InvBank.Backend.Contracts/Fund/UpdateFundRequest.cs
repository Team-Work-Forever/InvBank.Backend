namespace InvBank.Backend.Contracts.Fund;

public record UpdateFundRequest
(
    string Name,
    int Duration,
    decimal TaxPercent
);