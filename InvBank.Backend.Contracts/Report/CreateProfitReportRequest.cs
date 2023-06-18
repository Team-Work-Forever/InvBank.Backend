namespace InvBank.Backend.Contracts.Report;

public record CreateProfitReportRequest
(
    DateOnly InitialDate,
    DateOnly EndDate
);