using InvBank.Backend.Contracts.Fund;
using InvBank.Backend.Domain.Entities;

namespace InvBank.Backend.API.Mappers;

public class FundMapper : AutoMapper.Profile
{

    public FundMapper()
    {
        CreateMap<PaymentInvestFund, PaymentFundResponse>()
            .ConvertUsing(x => new PaymentFundResponse(
                new FundResponse(
                    x.Ative.Id,
                    x.Ative.InvestName,
                    x.Ative.InitialDate.ToString("dd/MM/yyyy"),
                    x.Ative.Duration,
                    x.Ative.Duration,
                    x.Ative.TaxPercent
                ),
                x.PaymentDate.ToString("dd/MM/yyyy"),
                x.Amount
            ));
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