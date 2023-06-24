using InvBank.Web.Contracts.Account;
using InvBank.Web.Contracts.Deposit;

namespace InvBank.Web.Helper.FilterActives;

public class FilterDeposits : IFilter<IEnumerable<AccountResponse>>
{
    public IEnumerable<AccountResponse> filter(IEnumerable<AccountResponse> list, string searchResult)
    {
        return  list
            .Where(account => account.DepositResponse
                .Any(response =>
                {
                    if (int.TryParse(searchResult, out var intValue))
                        return response.Value <= intValue;
                    else 
                        return response.DepositName.ToLower().Contains(searchResult.ToLower());
                }))
                .ToList();
    }
}