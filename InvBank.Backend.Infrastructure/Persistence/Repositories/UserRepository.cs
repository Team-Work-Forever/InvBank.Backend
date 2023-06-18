using InvBank.Backend.Application.Common.Interfaces;
using InvBank.Backend.Domain.Entities;
using InvBank.Backend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InvBank.Backend.Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly InvBankDbContext _dbContext;

    public UserRepository(InvBankDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateUser(Auth auth)
    {
        _dbContext.Auths.Add(auth);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Auth?> GetProfileByEmail(string email)
    {
        return await _dbContext.Auths.FindAsync(email);
    }

    public async Task<Auth?> GetUserAuth(string email)
    {
        return await _dbContext.Auths.Where(auth => auth.Email == email).FirstOrDefaultAsync();
    }

    public async Task AssociateAccount(Auth user, Account account)
    {

        AuthAccount authAccount = new AuthAccount
        {
            Account = account.Iban,
            AccountNavigation = account,
            Auth = user.Id,
            AuthNavigation = user
        };

        await _dbContext.AuthAccounts.AddAsync(authAccount);
        await _dbContext.SaveChangesAsync();

    }

    public async Task<IEnumerable<Auth>> GetAllAuths()
    {
        return _dbContext.Auths;
    }
}
