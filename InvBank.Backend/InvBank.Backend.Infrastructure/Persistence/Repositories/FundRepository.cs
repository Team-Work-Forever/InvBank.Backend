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

    public async Task<ActivesInvestmentFund> AssociateAccount(Account account, ActivesInvestmentFund invFund, decimal value)
    {
        _dbContext.AccountInvs.Add(new AccountInv
        {
            AccountNavigation = account,
            IdNavigation = invFund,
            AccountValue = value
        });
        
        var fund = await _dbContext.ActivesInvestmentFunds
            .Where(x => x.Id == invFund.Id)
            .FirstAsync();

        fund.InvestValue += value;

        await _dbContext.SaveChangesAsync();
        return invFund;
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
            .Where(x => x.DeletedAt == null)
            .ToListAsync();
    }

    public async Task<int> CreateFund(ActivesInvestmentFund investmentFund)
    {
        _dbContext.ActivesInvestmentFunds.Add(investmentFund);
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<PaymentInvestFund>> GetPayedTaxes()
    {
        return await _dbContext
            .PaymentInvestFunds
            .Where(x => x.PaymentDate != null)
            .ToListAsync();
    }

    public async Task<IEnumerable<PaymentInvestFund>> GetTaxes() 
    {
        return await _dbContext
            .PaymentInvestFunds
            .Where(x => x.PaymentDate == null)
            .ToListAsync();              
    }

    public async Task MakeTax(ActivesInvestmentFund fund, decimal amount)
    {
        _dbContext
            .PaymentInvestFunds
            .Add(new PaymentInvestFund{
                Amount = amount,
                Ative = fund
            });

        await _dbContext.SaveChangesAsync();
    }

    public async Task PayTax(ActivesInvestmentFund fund, decimal amount)
    {
        var payFund = await _dbContext
            .PaymentInvestFunds
            .Where(x => x.Id == fund.Id)
            .FirstAsync();

        payFund.PaymentDate = DateOnly.FromDateTime(DateTime.Now);
        _dbContext.Update<PaymentInvestFund>(payFund);

        await _dbContext.SaveChangesAsync();
    }

}