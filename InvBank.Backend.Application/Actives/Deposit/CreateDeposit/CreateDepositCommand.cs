using ErrorOr;
using MediatR;

namespace InvBank.Backend.Application.Actives.Deposit.CreateDeposit;

public record CreateDepositCommand
(
    string Name,
    string InitialDate,
    int Duration,
    decimal TaxPercent,
    decimal Value,
    decimal YearlyTax,
    string Account // IBAN
) : IRequest<ErrorOr<string>>;