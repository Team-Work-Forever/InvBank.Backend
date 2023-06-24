namespace InvBank.Backend.Application.Common;

public record MakeTransfer
(
    string AccountIban,
    decimal AmountValue
);