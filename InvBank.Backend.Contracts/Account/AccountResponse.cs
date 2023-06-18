using InvBank.Backend.Contracts.Deposit;
using InvBank.Backend.Contracts.PropertyAccount;

namespace InvBank.Backend.Contracts.Account;

public record AccountResponse
(
    string IBAN,
    string Bank,
    IEnumerable<DepositResponse> deposits,
    IEnumerable<PropertyAccountResponse> properties
);