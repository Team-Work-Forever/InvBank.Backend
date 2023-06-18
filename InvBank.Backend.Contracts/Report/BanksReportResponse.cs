using InvBank.Backend.Contracts.Payment;

namespace InvBank.Backend.Contracts.Report;

public record BanksReportResponse
(
     IEnumerable<BanksDepositResponse> banksInfo

);