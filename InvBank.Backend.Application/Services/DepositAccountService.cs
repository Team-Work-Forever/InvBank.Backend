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
    private readonly IValidator<DepositValueRequest> _validatorDepositValue;
    private readonly IValidator<UpdateDepositRequest> _validator;
    private readonly IAuthorizationFacade _authorizationFacade;
    private readonly IDepositRepository _depositRepository;
    private readonly IAccountRepository _accountRepository;

    public DepositAccountService(
        IDepositRepository depositRepository,
        IAccountRepository accountRepository,
        IAuthorizationFacade authorizationFacade,
        IValidator<UpdateDepositRequest> validator,
        IValidator<DepositValueRequest> validatorDepositValue)
    {
        _depositRepository = depositRepository;
        _accountRepository = accountRepository;
        _authorizationFacade = authorizationFacade;
        _validator = validator;
        _validatorDepositValue = validatorDepositValue;
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

    public async Task<ErrorOr<string>> GetDepositValue(string depositId, DepositValueRequest request)
    {
        var validationResult = await Validate<DepositValueRequest>(_validatorDepositValue, request);

        if (validationResult.IsError)
        {
            return validationResult.Errors;
        }

        var authUser = await _authorizationFacade.GetAuthenticatedUser();

        if (authUser.IsError)
        {
            return authUser.Errors;
        }

        var findDeposit = await GetDeposit(depositId);

        if (findDeposit.IsError)
        {
            return findDeposit.Errors;
        }

        var findAccount = await _accountRepository.GetAccount(authUser.Value, findDeposit.Value.Account);

        if (findAccount is null)
        {
            return Errors.Account.AccountNotFound;
        }

        if (findDeposit.Value.DepositValue < request.AmountValue)
        {
            return Errors.Deposit.DepositGetAmount;
        }

        findDeposit.Value.DepositValue -= request.AmountValue;
        await _depositRepository.UpdateDeposit(findDeposit.Value);

        findAccount.AmountValue += request.AmountValue;
        await _accountRepository.UpdateAccount(findAccount);

        return $"Foram levantados {request.AmountValue} u.m. do deposito {findDeposit.Value.Id} para a conta {findDeposit.Value.Account}";

    }

    public async Task<ErrorOr<string>> SetDepositValue(string depositId, DepositValueRequest request)
    {
        var validationResult = await Validate<DepositValueRequest>(_validatorDepositValue, request);

        if (validationResult.IsError)
        {
            return validationResult.Errors;
        }

        var authUser = await _authorizationFacade.GetAuthenticatedUser();

        if (authUser.IsError)
        {
            return authUser.Errors;
        }

        var findDeposit = await GetDeposit(depositId);

        if (findDeposit.IsError)
        {
            return findDeposit.Errors;
        }

        var findAccount = await _accountRepository.GetAccount(authUser.Value, findDeposit.Value.Account);

        if (findAccount is null)
        {
            return Errors.Account.AccountNotFound;
        }

        if (findAccount.AmountValue < request.AmountValue)
        {
            return Errors.Account.CannotRaiseMore;
        }

        findAccount.AmountValue -= request.AmountValue;
        await _accountRepository.UpdateAccount(findAccount);

        findDeposit.Value.DepositValue += request.AmountValue;
        await _depositRepository.UpdateDeposit(findDeposit.Value);

        return $"Foram depositados {request.AmountValue} u.m. do deposito {findDeposit.Value.Id} da conta {findDeposit.Value.Account}";
    }
}