namespace InvBank.Backend.Contracts.PropertyAccount;

public record UpdatePropertyAccountRequest
(
    string Name,
    int Duration,
    decimal TaxPercent,
    string Designation,
    decimal PropertyValue,
    decimal RentValue,
    decimal MonthValue,
    decimal YearlyValue
);