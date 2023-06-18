namespace InvBank.Backend.Contracts.PropertyAccount;

public record PropertyAccountResponse
(
    Guid Id,
    string InitialDate,
    int Duration,
    decimal TaxPercent,
    string Designation,
    string PostalCode,
    decimal PropertyValue,
    decimal RentValue,
    decimal MonthValue,
    decimal YearlyValue,
    string Account
);