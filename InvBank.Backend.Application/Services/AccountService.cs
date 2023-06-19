using ErrorOr;
using InvBank.Backend.Application.Common.Interfaces;
using InvBank.Backend.Application.Common.Providers;
using InvBank.Backend.Contracts.Account;
using InvBank.Backend.Domain.Entities;
using InvBank.Backend.Domain.Errors;

namespace InvBank.Backend.Application.Services;

public class AccountService
{
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
        IUserRepository userRepository)
    {
        _accountRepository = accountRepository;
        _bankRepository = bankRepository;
        _ibanGenerator = ibanGenerator;
        _authorizationFacade = authorizationFacade;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<Account>> CreateAccount(CreateAccountRequest request)
    {

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
        ErrorOr<Account> accountResult = await GetAccount(iban);

        if (accountResult.IsError)
        {
            return accountResult.Errors;
        }

        return await _accountRepository.DeleteAccount(accountResult.Value);
    }

    public async Task<ErrorOr<Account>> GetAccount(string iban)
    {

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

}