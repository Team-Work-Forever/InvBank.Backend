using InvBank.Backend.Contracts.Bank;

namespace InvBank.Backend.Contracts.Payment;

public record BanksDepositResponse
(
        Guid Id,
        decimal AmountDeposits,
        decimal TaxDeposits,
        BankResponse bank
);