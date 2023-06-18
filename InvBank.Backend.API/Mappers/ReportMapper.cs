using InvBank.Backend.Application.Common.Contracts;
using InvBank.Backend.Contracts.Bank;
using InvBank.Backend.Contracts.Deposit;
using InvBank.Backend.Contracts.Payment;
using InvBank.Backend.Contracts.PropertyAccount;
using InvBank.Backend.Contracts.Report;

namespace InvBank.Backend.API.Mappers;

public class ReportMapper : AutoMapper.Profile
{

    public ReportMapper()
    {
        CreateMap<PayReportResult, PayReportResponse>()
            .ConvertUsing(pr => new PayReportResponse(
                pr.PaymentDeposit.Select(pd => new PayDepositResponse
                    (pd.Id, pd.PaymentDate.ToString("dd/MM/yyyy"), pd.Amount,
                        new DepositResponse(
                            pd.Ative.Id,
                            pd.Ative.DepositName,
                            pd.Ative.InitialDate.ToString("dd/MM/yyyy"),
                            pd.Ative.Duration,
                            pd.Ative.TaxPercent,
                            pd.Ative.YearlyTax,
                            pd.Ative.Account
                            ))),
                pr.PaymentProperty.Select(pp => new PayPropertyResponse
                    (pp.Id, pp.PaymentDate.ToString("dd/MM/yyyy"), pp.Amount,
                        new PropertyAccountResponse(
                            pp.Ative.Id,
                            pp.Ative.InitialDate.ToString("dd/MM/yyyy"),
                            pp.Ative.Duration,
                            pp.Ative.TaxPercent,
                            pp.Ative.Designation,
                            pp.Ative.PostalCode,
                            pp.Ative.PropertyValue,
                            pp.Ative.RentValue,
                            pp.Ative.MonthValue,
                            pp.Ative.YearlyValue,
                            pp.Ative.Account
                        )))
            ));

        CreateMap<BankReportResult, BanksReportResponse>()
            .ConvertUsing(pr => new BanksReportResponse(
                pr.banksInfo.Select(pd => new BanksDepositResponse
                    (pd.Id, pd.AmountDeposits, pd.TaxDeposits,
                        new BankResponse(
                            pd.bank.Iban,
                            pd.bank.Phone,
                            pd.bank.PostalCode
                            )))
            ));
    }

}