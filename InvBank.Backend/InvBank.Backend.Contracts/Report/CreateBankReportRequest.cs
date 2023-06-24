namespace InvBank.Backend.Contracts.Report;

public record CreateBankReportRequest
(
    string InitialDate,
    string EndDate
);