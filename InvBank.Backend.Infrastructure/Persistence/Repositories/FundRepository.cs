using InvBank.Backend.Application.Common.Interfaces;
using InvBank.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvBank.Backend.Infrastructure.Persistence;

public class FundRepository : IFundRepository
{
    private readonly InvBankDbContext _dbContext;

    public FundRepository(InvBankDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> CreateFund(Account account, ActivesInvestmentFund depositAccount, decimal value)
    {
        _dbContext.AccountInvs.Add(new AccountInv
        {
            AccountNavigation = account,
            IdNavigation = depositAccount,
            AccountValue = value
        });

        return await _dbContext.SaveChangesAsync();
    }

    public async Task<ActivesInvestmentFund> UpdateFund(ActivesInvestmentFund investmentFund)
    {

        _dbContext.Update<ActivesInvestmentFund>(investmentFund);
        await _dbContext.SaveChangesAsync();

        return investmentFund;

    }

    public async Task<int> DeleteFund(Guid id)
    {
        var depositAccount = await _dbContext.ActivesInvestmentFunds.Where(ada => ada.Id == id).FirstAsync();
        depositAccount.DeletedAt = DateOnly.FromDateTime(DateTime.Now);

        await UpdateFund(depositAccount);
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<ActivesInvestmentFund>> GetInvestmentFundsOfAccount(string iban)
    {
        return await _dbContext.AccountInvs
            .Where(ai => ai.Account == iban)
            .Select(ai => ai.IdNavigation)
            .ToListAsync();
    }

    public async Task<ActivesInvestmentFund?> GetInvestmentFund(Guid id)
    {
        return await _dbContext
            .ActivesInvestmentFunds
            .Where(acf => acf.Id == id)
            .Where(acf => acf.DeletedAt == null)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<ActivesInvestmentFund>> GetFunds()
    {
        return await _dbContext
            .ActivesInvestmentFunds
            .ToListAsync();
    }
}