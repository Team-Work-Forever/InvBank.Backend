namespace InvBank.Backend.Contracts.Report;

public record ProfitReportResponse
(
    decimal BeforeProfitDeposit,
    decimal AfterProfitDeposit,
    decimal BeforeProfitProperty,
    decimal AfterProfitProperty,
    decimal ProfitMean
);