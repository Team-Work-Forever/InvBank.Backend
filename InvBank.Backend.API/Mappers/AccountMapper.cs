using InvBank.Backend.Contracts.Account;
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
                acc.ActivesDepositAccounts,
                acc.ActivesProperties));
    }
}