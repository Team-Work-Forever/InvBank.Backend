
using InvBank.Backend.Domain.Entities;

namespace InvBank.Backend.Application.Common.Interfaces;

public interface IAccountRepository
{
    Task<IEnumerable<Account>> GetAllAccounts(Auth user);
    Task<Account?> GetAccount(Auth user, string IBAN);
    Task CreateAccount(Account account);
    Task<Account> UpdateAccount(Account account);
    Task<int> DeleteAccount(Account account);
}