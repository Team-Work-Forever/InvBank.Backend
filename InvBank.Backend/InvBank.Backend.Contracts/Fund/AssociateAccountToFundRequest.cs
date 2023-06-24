namespace InvBank.Backend.Contracts.Fund;

public record AssociateAccountToFundRequest
(
    string IBAN,
    Guid FundId,
    decimal Amount
);