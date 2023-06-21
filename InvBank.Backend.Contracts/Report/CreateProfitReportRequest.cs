namespace InvBank.Backend.Contracts.Report;

public record CreateProfitReportRequest
(
    string InitialDate,
    string EndDate
);