using ErrorOr;
using FluentValidation;
using InvBank.Backend.Application.Common.Interfaces;
using InvBank.Backend.Application.Common.Providers;
using InvBank.Backend.Contracts.Deposit;
using InvBank.Backend.Domain.Entities;
using InvBank.Backend.Domain.Errors;

namespace InvBank.Backend.Application.Services;

public class DepositAccountService : BaseService
{
    private readonly IValidator<UpdateDepositRequest> _validator;
    private readonly IAuthorizationFacade _authorizationFacade;
    private readonly IDepositRepository _depositRepository;
    private readonly IAccountRepository _accountRepository;

    public DepositAccountService(
        IDepositRepository depositRepository,
        IAccountRepository accountRepository,
        IAuthorizationFacade authorizationFacade,
        IValidator<UpdateDepositRequest> validator)
    {
        _depositRepository = depositRepository;
        _accountRepository = accountRepository;
        _authorizationFacade = authorizationFacade;
        _validator = validator;
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

        var auth = await _authorizationFacade.GetAuthenticatedUser();

        if (auth.IsError)
        {
            return auth.Errors;
        }

        Account? findAccount = await _accountRepository.GetAccount(auth.Value, accountIban);

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

        var validatorResult = await Validate<UpdateDepositRequest>(_validator, newDeposit);

        if (validatorResult.IsError)
        {
            return validatorResult.Errors;
        }

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

    public async Task<ErrorOr<string>> Pay(Guid depositId, decimal amount)
    {

        var depositResult = await GetDeposit(depositId.ToString());

        if (depositResult.IsError)
        {
            return depositResult.Errors;
        }

        if (depositResult.Value.DepositValue < amount)
        {
            return Errors.Deposit.DepositAmountGreater;
        }

        await _depositRepository.PayDepositValue(depositId, amount);
        return "Pago";
    }
}