namespace InvBank.Backend.Contracts.PropertyAccount;

public record CreatePropertyAccountRequest
(
    string Name,
    string InitialDate,
    int Duration,
    decimal TaxPercent,
    string Designation,
    string PostalCode,
    decimal PropertyValue,
    decimal RentValue,
    decimal MonthValue,
    decimal YearlyValue,
    string AccountIBAN
);