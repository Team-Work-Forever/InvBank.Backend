using InvBank.Backend.Contracts.Deposit;

namespace InvBank.Backend.Contracts.Payment;

public record PayDepositResponse
(
    Guid Id,
    string PaymentDate,
    decimal Amount,
    DepositResponse DepositAccount
);