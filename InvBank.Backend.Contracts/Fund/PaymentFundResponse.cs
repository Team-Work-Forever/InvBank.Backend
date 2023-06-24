namespace InvBank.Backend.Contracts.Fund;

public record PaymentFundResponse
(
    FundResponse fund,
    string PaymentDate,
    decimal Amount
);