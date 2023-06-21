namespace InvBank.Backend.Contracts.Report;

public record CreatePayReportCommand
(
    string InitialDate,
    string EndDate,
    string Iban
);