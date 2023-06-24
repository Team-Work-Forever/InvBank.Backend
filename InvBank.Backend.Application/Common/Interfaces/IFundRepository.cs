using InvBank.Backend.Domain.Entities;

namespace InvBank.Backend.Application.Common.Interfaces;

public interface IFundRepository
{
    Task<IEnumerable<ActivesInvestmentFund>> GetInvestmentFundsOfAccount(string iban);
    Task<IEnumerable<ActivesInvestmentFund>> GetFunds();
    Task<ActivesInvestmentFund?> GetInvestmentFund(Guid id);
    Task<IEnumerable<PaymentInvestFund>> GetPayedTaxes();
    Task<IEnumerable<PaymentInvestFund>> GetTaxes();
    Task MakeTax(ActivesInvestmentFund fund, decimal amount);
    Task PayTax(ActivesInvestmentFund fund, decimal amount);
    Task<ActivesInvestmentFund> AssociateAccount(Account account, ActivesInvestmentFund depositAccount, decimal value);
    Task<int> CreateFund(ActivesInvestmentFund investmentFund);
    Task<ActivesInvestmentFund> UpdateFund(ActivesInvestmentFund investmentFund);
    Task<int> DeleteFund(Guid id);

}