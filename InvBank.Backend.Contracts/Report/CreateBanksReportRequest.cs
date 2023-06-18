namespace InvBank.Backend.Contracts.Report;

public record CreateBanksReportRequest
(
    string InitialDate,
    string EndDate
);