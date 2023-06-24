using InvBank.Backend.Contracts.PropertyAccount;

namespace InvBank.Backend.Contracts.Payment;

public record PayPropertyResponse
(
    Guid Id,
    string PaymentDate,
    decimal Amount,
    PropertyAccountResponse DepositAccount
);