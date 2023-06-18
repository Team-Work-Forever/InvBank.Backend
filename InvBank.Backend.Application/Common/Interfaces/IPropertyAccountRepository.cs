using InvBank.Backend.Domain.Entities;

namespace InvBank.Backend.Application.Common.Interfaces;

public interface IPropertyAccountRepository
{
    Task<ActivesProperty> CreatePropertyAccount(ActivesProperty propertyAccount);
    Task<ActivesProperty?> GetActivesProperty(Guid Id);
    Task<ActivesProperty> UpdateActiveProperty(ActivesProperty activesProperty);
    Task<int> DeleteActiveProperty(Guid Id);
    Task<IEnumerable<ActivesProperty>> GetAllPropertyAccounts(string iban);
    Task<IEnumerable<AtiveStateProperty>> GetAtives();
}