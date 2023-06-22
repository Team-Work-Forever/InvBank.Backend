using InvBank.Backend.Application.Common.Interfaces;
using InvBank.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvBank.Backend.Infrastructure.Persistence;

public class AccountRepository : BaseRepository, IAccountRepository
{
    public AccountRepository(InvBankDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Account?> GetAccount(Auth user, string IBAN)
    {
        return await _dbContext.AuthAccounts
            .Where(ac => ac.Account == IBAN)
            .Where(ac => ac.Auth == user.Id || user.UserRole != 0)
            .Select(ac => ac.AccountNavigation)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Account>> GetAllAccounts(Auth user)
    {
        return await _dbContext.AuthAccounts
            .Where(ac => ac.Auth == user.Id || user.UserRole != 0)
            .Include(ac => ac.AccountNavigation.ActivesDepositAccounts)
            .Include(ac => ac.AccountNavigation.ActivesProperties)
            .Select(ac => ac.AccountNavigation)
            .Where(ac => ac.DeletedAt == null)
            .ToListAsync();
    }

    public async Task CreateAccount(Account account)
    {
        _dbContext.Accounts.Add(account);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Account> UpdateAccount(Account account)
    {
        _dbContext.Update<Account>(account);
        await _dbContext.SaveChangesAsync();

        return account;
    }

    public async Task<int> DeleteAccount(Account account)
    {
        account.DeletedAt = DateOnly.FromDateTime(DateTime.Now);

        await UpdateAccount(account);
        return await _dbContext.SaveChangesAsync();
    }

}