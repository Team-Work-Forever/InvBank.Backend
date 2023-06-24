using InvBank.Web.Contracts.Account;

namespace InvBank.Web.Helper.FilterActives;

public class FilterProperties : IFilter<IEnumerable<AccountResponse>>
{
    public IEnumerable<AccountResponse> filter(IEnumerable<AccountResponse> list, string searchResult)
    {
         return list
            .Where(account => account.Properties
                .Any(response =>
                {
                    return response.Designation.ToLower().Contains(searchResult.ToLower());
                }))
                .ToList();
    }
}