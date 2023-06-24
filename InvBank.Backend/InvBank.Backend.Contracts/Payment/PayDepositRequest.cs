namespace InvBank.Backend.Contracts.Payment;

public record PayDepositRequest
(
    Guid DepositId,
    decimal Amount
);