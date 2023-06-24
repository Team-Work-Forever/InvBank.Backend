using InvBank.Backend.Domain.Entities;

namespace InvBank.Backend.Application.Common.Interfaces;

public interface IDepositRepository
{
    Task CreateDepositActive(Account account, ActivesDepositAccount depositAccount);
    Task<ActivesDepositAccount?> GetDepositAccount(Guid depositId);
    Task<IEnumerable<ActivesDepositAccount>> GetAllDepositAccounts(Account account);
    Task<IEnumerable<ActivesDepositAccount>> GetAllDeposits();
    Task<int> DeleteDeposit(Guid depositId);
    Task<ActivesDepositAccount> UpdateDeposit(ActivesDepositAccount depositAccount);
    Task<IEnumerable<AtiveStateDeposit>> GetAtives();
    Task<IEnumerable<PaymentDeposit>> GetPayments(string iban);
    Task PayDepositValue(Guid depositId, decimal amount);
}