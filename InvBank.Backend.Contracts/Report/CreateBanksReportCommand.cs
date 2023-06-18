namespace InvBank.Backend.Contracts.Report;

public record CreateBanksReportCommand
(
    DateOnly InitialDate,
    DateOnly EndDate
);