using InvBank.Backend.Application.Common.Interfaces;
using InvBank.Backend.Contracts.Deposit;
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

    public async Task<ActivesInvestmentFund?> GetInvFundActive(Guid fundId)
    {
        return await _dbContext.ActivesInvestmentFunds
            .Where(ada => ada.Id == fundId)
            .FirstOrDefaultAsync();
    }

    public async Task<ActivesInvestmentFund> UpdateFundAccount(ActivesInvestmentFund fundAccount)
    {

        _dbContext.Update<ActivesInvestmentFund>(fundAccount);
        await _dbContext.SaveChangesAsync();

        return fundAccount;
    }

    public async Task<int> DeleteFund(Guid fundId)
    {
        var fundAccount = await _dbContext.ActivesInvestmentFunds.Where(ada => ada.Id == fundId).FirstAsync();
        fundAccount.DeletedAt = DateOnly.FromDateTime(DateTime.Now);

        await UpdateFundAccount(fundAccount);
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<ActivesInvestmentFund>> GetInvestmentFundsOfAccount(string iban)
    {
        return await _dbContext.AccountInvs
            .Where(ai => ai.Account == iban)
            .Select(ai => ai.IdNavigation)
            .ToListAsync();
    }
}