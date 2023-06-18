using InvBank.Backend.Domain.Entities;

namespace InvBank.Backend.Application.Common.Interfaces;

public interface IFundRepository
{
    Task<IEnumerable<ActivesInvestmentFund>> GetInvestmentFundsOfAccount(string iban);
    // Task<IEnumerable<AtiveStateInvestmentFund>> GetAtives(string iban);
    Task<int> CreateFund(Account account, ActivesInvestmentFund depositAccount, decimal value);
}