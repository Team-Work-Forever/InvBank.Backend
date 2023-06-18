using InvBank.Backend.Contracts.Fund;
using InvBank.Backend.Domain.Entities;

namespace InvBank.Backend.API.Mappers;

public class FundMapper : AutoMapper.Profile
{

    public FundMapper()
    {
        CreateMap<ActivesInvestmentFund, FundResponse>()
                   .ConvertUsing(acc => new FundResponse(
                        acc.Id,
                        acc.InvestName,
                        acc.InitialDate.ToString("dd/MM/yyyy"),
                        acc.Duration,
                        acc.InvestValue,
                        acc.TaxPercent));
    }

}