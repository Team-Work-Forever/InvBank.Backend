using InvBank.Backend.Application.Actives.Deposit.CreateDeposit;
using InvBank.Backend.Application.Common.Contracts;
using InvBank.Backend.Contracts.Deposit;
using InvBank.Backend.Contracts.Payment;
using InvBank.Backend.Contracts.Report;
using InvBank.Backend.Domain.Entities;

namespace InvBank.Backend.API.Mappers;

public class DepositMapper : AutoMapper.Profile
{

    public DepositMapper()
    {
        CreateMap<ActivesDepositAccount, DepositResponse>()
            .ConvertUsing(acc => new DepositResponse(
                acc.Id,
                acc.DepositName,
                acc.InitialDate.ToString("dd/MM/yyyy"),
                acc.Duration,
                acc.TaxPercent,
                acc.YearlyTax,
                acc.Account));

        CreateMap<CreateDepositRequest, CreateDepositCommand>()
            .ConvertUsing(acc => new CreateDepositCommand(
                acc.Name,
                acc.InitialDate,
                acc.Duration,
                acc.TaxPercent,
                acc.Value,
                acc.YearlyTax,
                acc.IBAN
            ));

    }

}