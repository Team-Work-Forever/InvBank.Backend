using InvBank.Backend.Contracts.Payment;

namespace InvBank.Backend.Contracts.Report;

public record BankReportResult
(
     IEnumerable<BanksDepositResponse> banksInfo

);