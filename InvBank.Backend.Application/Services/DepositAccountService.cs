using ErrorOr;
using InvBank.Backend.Application.Common.Interfaces;
using InvBank.Backend.Application.Common.Providers;
using InvBank.Backend.Contracts.Deposit;
using InvBank.Backend.Domain.Entities;
using InvBank.Backend.Domain.Errors;

namespace InvBank.Backend.Application.Services;

public class DepositAccountService
{
    private readonly IAuthorizationFacade _authorizationFacade;
    public readonly IDepositRepository _depositRepository;
    public readonly IAccountRepository _accountRepository;

    public DepositAccountService(
        IDepositRepository depositRepository,
        IAccountRepository accountRepository,
        IAuthorizationFacade authorizationFacade)
    {
        _depositRepository = depositRepository;
        _accountRepository = accountRepository;
        _authorizationFacade = authorizationFacade;
    }

    public async Task<ErrorOr<ActivesDepositAccount>> GetDeposit(string depositId)
    {
        ActivesDepositAccount? findDepositAccount = await _depositRepository.GetDepositAccount(Guid.Parse(depositId));

        if (findDepositAccount is null)
        {
            return Errors.Deposit.DepositNotFound;
        }

        return findDepositAccount;
    }

    public async Task<ErrorOr<IEnumerable<ActivesDepositAccount>>> GetDepositAccounts(string accountIban)
    {

        Auth auth = await _authorizationFacade.GetAuthenticatedUser();

        Account? findAccount = await _accountRepository.GetAccount(auth, accountIban);

        // Verify account existance
        if (findAccount is null)
        {
            return Errors.Account.AccountNotFound;
        }

        return (await _depositRepository.GetAllDepositAccounts(findAccount)).ToList();

    }

    public async Task<ErrorOr<int>> DeleteDeposit(string depositId)
    {

        ErrorOr<ActivesDepositAccount> depositResult = await GetDeposit(depositId);

        if (depositResult.IsError)
        {
            return depositResult.Errors;
        }

        return await _depositRepository.DeleteDeposit(depositResult.Value.Id);

    }

    public async Task<ErrorOr<ActivesDepositAccount>> UpdateDeposit(string depositId, UpdateDepositRequest newDeposit)
    {

        var depositAccount = await GetDeposit(depositId);

        if (depositAccount.IsError)
        {
            return depositAccount.Errors;
        }

        depositAccount.Value.DepositName = newDeposit.Name;
        depositAccount.Value.Duration = newDeposit.Duration;
        depositAccount.Value.TaxPercent = newDeposit.TaxPercent;
        depositAccount.Value.YearlyTax = newDeposit.YearlyTax;
        depositAccount.Value.DepositValue = newDeposit.Value;

        return await _depositRepository.UpdateDeposit(depositAccount.Value);

    }
}