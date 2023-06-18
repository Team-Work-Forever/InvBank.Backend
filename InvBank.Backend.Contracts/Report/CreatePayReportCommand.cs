namespace InvBank.Backend.Contracts.Report;

public record CreatePayReportCommand
(
    DateOnly InitialDate,
    DateOnly EndDate,
    string Iban
);