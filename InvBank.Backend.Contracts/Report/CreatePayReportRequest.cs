namespace InvBank.Backend.Contracts.Report;

public record CreatePayReportRequest
(
    string InitialDate,
    string EndDate
);