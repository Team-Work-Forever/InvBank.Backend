namespace InvBank.Backend.Contracts.Payment;

public record PayPropertyRequest
(
    Guid PropertyId,
    decimal Amount
);