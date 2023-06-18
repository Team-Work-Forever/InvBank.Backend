using InvBank.Backend.Application.Common.Interfaces;
using InvBank.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvBank.Backend.Infrastructure.Persistence;

public class BankRepository : BaseRepository, IBankRepository
{
    public BankRepository(InvBankDbContext dbContext) : base(dbContext)
    {
    }

    public async Task CreateBank(Bank bank)
    {
        _dbContext.Banks.Add(bank);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Bank>> GetAllBank()
    {
        return await _dbContext.Banks
             .ToListAsync();
    }

    public Task<Bank?> GetBank(string iban)
    {
        return _dbContext.Banks.Where(b => b.Iban == iban).FirstOrDefaultAsync();
    }

    public async Task<int> UpdateBank(Bank bank)
    {
        _dbContext.Update<Bank>(bank);
        return await _dbContext.SaveChangesAsync();
    }

}