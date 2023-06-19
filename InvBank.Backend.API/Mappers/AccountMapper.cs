using InvBank.Backend.Contracts.Account;
using InvBank.Backend.Contracts.Deposit;
using InvBank.Backend.Contracts.PropertyAccount;
using InvBank.Backend.Domain.Entities;

namespace InvBank.Backend.API.Mappers;

public class AccountMapper : AutoMapper.Profile
{
    public AccountMapper()
    {
        CreateMap<Account, AccountResponse>()
            .ConstructUsing(acc => new AccountResponse(
                acc.Iban,
                acc.Bank,
                acc.ActivesDepositAccounts.Select(da =>
                    new DepositResponse(
                        da.Id,
                        da.DepositName,
                        da.InitialDate.ToString("dd/MM/yyyy"),
                        da.Duration,
                        da.DepositValue,
                        da.TaxPercent,
                        da.YearlyTax,
                        da.Account
                    )),
                acc.ActivesProperties.Select(ap =>
                    new PropertyAccountResponse(
                        ap.Id,
                        ap.InitialDate.ToString("dd/MM/yyyy"),
                        ap.Duration,
                        ap.TaxPercent,
                        ap.Designation,
                        ap.PostalCode,
                        ap.PropertyValue,
                        ap.RentValue,
                        ap.MonthValue,
                        ap.YearlyValue,
                        ap.Account))));
    }
}