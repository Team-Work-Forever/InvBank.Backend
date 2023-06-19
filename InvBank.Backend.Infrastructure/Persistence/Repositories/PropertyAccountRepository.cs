using InvBank.Backend.Application.Common.Interfaces;
using InvBank.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvBank.Backend.Infrastructure.Persistence;

public class PropertyAccountRepository : BaseRepository, IPropertyAccountRepository
{
    public PropertyAccountRepository(InvBankDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<ActivesProperty> CreatePropertyAccount(ActivesProperty propertyAccount)
    {
        _dbContext.ActivesProperties.Add(propertyAccount);
        _dbContext.AtiveStateProperties.Add(new AtiveStateProperty
        {
            Ative = propertyAccount,
        });

        await _dbContext.SaveChangesAsync();

        return propertyAccount;
    }

    public async Task<int> DeleteActiveProperty(Guid Id)
    {
        ActivesProperty findPropertyAccount = await _dbContext.ActivesProperties.Where(prop => prop.Id == Id).FirstAsync();
        findPropertyAccount.DeletedAt = DateOnly.FromDateTime(DateTime.Now);

        await UpdateActiveProperty(findPropertyAccount);
        return await _dbContext.SaveChangesAsync();

    }

    public async Task<ActivesProperty?> GetActivesProperty(Guid Id)
    {
        return await _dbContext.ActivesProperties.Where(prop => prop.Id == Id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<ActivesProperty>> GetAllPropertyAccounts(string iban)
    {
        return await _dbContext.ActivesProperties
            .Where(props => props.Account == iban)
            .Where(props => props.DeletedAt == null)
            .ToListAsync();
    }

    public async Task<IEnumerable<AtiveStateProperty>> GetAtives()
    {
        return await _dbContext
            .AtiveStateProperties
            .Where(act => act.IsActive)
            .Include(act => act.Ative)
            .Where(act => act.Ative.DeletedAt == null)
            .ToListAsync();
    }

    public async Task<IEnumerable<PaymentProperty>> GetPayments(string iban)
    {
        return await _dbContext
                   .PaymentProperties
                   .Include(pay => pay.Ative)
                   .Where(pay => pay.Ative.Account == iban)
                   .ToListAsync();
    }

    public async Task PayPropertyValue(Guid propertyId, decimal amount)
    {
        _dbContext
            .PaymentProperties
            .Add( new PaymentProperty {
                AtiveId = propertyId,
                PaymentDate = DateOnly.FromDateTime(DateTime.Now),
                Amount = amount
            });

        await _dbContext.SaveChangesAsync();
    }

    public async Task<ActivesProperty> UpdateActiveProperty(ActivesProperty activesProperty)
    {
        _dbContext.Update<ActivesProperty>(activesProperty);
        await _dbContext.SaveChangesAsync();

        return activesProperty;
    }
}