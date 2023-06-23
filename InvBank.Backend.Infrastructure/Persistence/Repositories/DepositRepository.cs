using InvBank.Backend.Application.Common.Interfaces;
using InvBank.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvBank.Backend.Infrastructure.Persistence;

public class DepositRepository : BaseRepository, IDepositRepository
{
    public DepositRepository(InvBankDbContext dbContext) : base(dbContext)
    {
    }

    public async Task CreateDepositActive(Account account, ActivesDepositAccount depositAccount)
    {
        account.ActivesDepositAccounts.Add(depositAccount);
        _dbContext.AtiveStateDeposits.Add(new AtiveStateDeposit
        {
            Ative = depositAccount,
        });

        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<ActivesDepositAccount>> GetAllDepositAccounts(Account account)
    {
        return await _dbContext.ActivesDepositAccounts
            .Where(act => act.Account == account.Iban)
            .Where(act => act.DeletedAt == null)
            .ToListAsync();
    }

    public async Task<ActivesDepositAccount?> GetDepositAccount(Guid depositId)
    {
        return await _dbContext.ActivesDepositAccounts
            .Where(ada => ada.Id == depositId)
            .FirstOrDefaultAsync();
    }

    public async Task<ActivesDepositAccount> UpdateDeposit(ActivesDepositAccount depositAccount)
    {

        _dbContext.Update<ActivesDepositAccount>(depositAccount);
        await _dbContext.SaveChangesAsync();

        return depositAccount;

    }

    public async Task<int> DeleteDeposit(Guid depositId)
    {
        var depositAccount = await _dbContext.ActivesDepositAccounts.Where(ada => ada.Id == depositId).FirstAsync();
        depositAccount.DeletedAt = DateOnly.FromDateTime(DateTime.Now);

        await UpdateDeposit(depositAccount);
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<AtiveStateDeposit>> GetAtives()
    {
        return await _dbContext
            .AtiveStateDeposits
            .Where(act => act.IsActive)
            .Include(act => act.Ative)
            .Where(act => act.Ative.DeletedAt == null)
            .ToListAsync();
    }

    public async Task<IEnumerable<PaymentDeposit>> GetPayments(string iban)
    {
        return await _dbContext
            .PaymentDeposits
            .Include(pay => pay.Ative)
            .Where(pay => pay.Ative.Account == iban)
            .ToListAsync();
    }

    public async Task PayDepositValue(Guid depositId, decimal amount)
    {

        _dbContext
            .PaymentDeposits
            .Add(new PaymentDeposit
            {
                AtiveId = depositId,
                PaymentDate = DateOnly.FromDateTime(DateTime.Now),
                Amount = amount
            });

        _dbContext
            .ActivesDepositAccounts
            .Where(deposit => deposit.Id == depositId)
            .First()
            .DepositValue -= amount;

        await _dbContext.SaveChangesAsync();

    }

    public async Task<IEnumerable<ActivesDepositAccount>> GetAllDeposits()
    {
        return await _dbContext
            .ActivesDepositAccounts
            .ToListAsync();
    }
}