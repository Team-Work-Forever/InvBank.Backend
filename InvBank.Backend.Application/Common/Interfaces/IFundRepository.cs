using InvBank.Backend.Domain.Entities;

namespace InvBank.Backend.Application.Common.Interfaces;

public interface IFundRepository
{
    Task<ActivesInvestmentFund?> GetInvFundActive(Guid fundId);
    Task<IEnumerable<ActivesInvestmentFund>> GetInvestmentFundsOfAccount(string iban);
    Task<int> CreateFund(Account account, ActivesInvestmentFund fundAccount, decimal value);
    Task<int> DeleteFund(Guid fundId);
    Task<ActivesInvestmentFund> UpdateFundAccount(ActivesInvestmentFund fundAccount);
}