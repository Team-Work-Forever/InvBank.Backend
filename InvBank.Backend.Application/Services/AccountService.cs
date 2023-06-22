using ErrorOr;
using FluentValidation;
using InvBank.Backend.Application.Common;
using InvBank.Backend.Application.Common.Interfaces;
using InvBank.Backend.Application.Common.Providers;
using InvBank.Backend.Application.Config;
using InvBank.Backend.Contracts.Account;
using InvBank.Backend.Domain.Entities;
using InvBank.Backend.Domain.Errors;

namespace InvBank.Backend.Application.Services;

public class AccountService : BaseService
{
    private readonly IValidator<MakeTransfer> _validatorMakeTransfer;
    private readonly IValidator<CreateAccountRequest> _validator;
    private readonly IUserRepository _userRepository;
    private readonly IAuthorizationFacade _authorizationFacade;
    private readonly IIBANGenerator _ibanGenerator;
    private readonly IAccountRepository _accountRepository;
    private readonly IBankRepository _bankRepository;

    public AccountService(
        IAccountRepository accountRepository,
        IBankRepository bankRepository,
        IIBANGenerator ibanGenerator,
        IAuthorizationFacade authorizationFacade,
        IUserRepository userRepository,
        IValidator<CreateAccountRequest> validator,
        IValidator<MakeTransfer> validatorMakeTransfer)
    {
        _accountRepository = accountRepository;
        _bankRepository = bankRepository;
        _ibanGenerator = ibanGenerator;
        _authorizationFacade = authorizationFacade;
        _userRepository = userRepository;
        _validator = validator;
        _validatorMakeTransfer = validatorMakeTransfer;
    }

    public async Task<ErrorOr<Account>> CreateAccount(CreateAccountRequest request)
    {

        var validationResult = await Validate<CreateAccountRequest>(_validator, request);

        if (validationResult.IsError)
        {
            return validationResult.Errors;
        }

        // Get User
        var auth = await _authorizationFacade.GetAuthenticatedUser();

        if (auth.IsError)
        {
            return auth.Errors;
        }

        // Generate new IBAN
        Bank? findTask = await _bankRepository.GetBank(request.iban);

        if (findTask is null)
        {
            return Errors.Bank.BankNotFound;
        }

        var account = new Account
        {
            Iban = _ibanGenerator.GenerateIBAN(),
        };

        findTask.Accounts.Add(account);

        await _userRepository.AssociateAccount(auth.Value, account);
        await _bankRepository.UpdateBank(findTask);

        return account;

    }

    public async Task<ErrorOr<int>> DeleteAccount(string iban)
    {

        var validationResult = await Validate<CreateAccountRequest>(_validator, new CreateAccountRequest(iban));

        if (validationResult.IsError)
        {
            return validationResult.Errors;
        }

        ErrorOr<Account> accountResult = await GetAccount(iban);

        if (accountResult.IsError)
        {
            return accountResult.Errors;
        }

        return await _accountRepository.DeleteAccount(accountResult.Value);
    }

    public async Task<ErrorOr<Account>> GetAccount(string iban)
    {

        var validationResult = await Validate<CreateAccountRequest>(_validator, new CreateAccountRequest(iban));

        if (validationResult.IsError)
        {
            return validationResult.Errors;
        }

        var auth = await _authorizationFacade.GetAuthenticatedUser();

        if (auth.IsError)
        {
            return auth.Errors;
        }


        Account? findAccount = await _accountRepository.GetAccount(auth.Value, iban);

        if (findAccount is null)
        {
            return Errors.Account.AccountNotFound;
        }

        return findAccount;

    }

    public async Task<ErrorOr<IEnumerable<Account>>> GetAllAccounts()
    {
        var auth = await _authorizationFacade.GetAuthenticatedUser();

        if (auth.IsError)
        {
            return auth.Errors;
        }

        return (await _accountRepository.GetAllAccounts(auth.Value)).ToList();
    }

    public async Task<ErrorOr<string>> MakeRequest(MakeTransfer request)
    {

        var validationResult = await Validate<MakeTransfer>(_validatorMakeTransfer, request);

        if (validationResult.IsError)
        {
            return validationResult.Errors;
        }

        ErrorOr<Account> findAccount = await GetAccount(request.AccountIban);

        if (findAccount.IsError)
        {
            return findAccount.Errors;
        }

        findAccount.Value.AmountValue += request.AmountValue / (1 + BankComission.DepositCommission);
        await _accountRepository.UpdateAccount(findAccount.Value);

        return $"A transferencia de {request.AmountValue} u.m. foi realizada com sucesso!";

    }
}