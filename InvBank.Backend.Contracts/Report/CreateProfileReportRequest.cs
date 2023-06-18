namespace InvBank.Backend.Contracts.Report;

public record CreateProfitReportCommand
(
    DateOnly InitialDate,
    DateOnly EndDate
);