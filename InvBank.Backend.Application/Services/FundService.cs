using ErrorOr;
using FluentValidation;
using InvBank.Backend.Application.Common.Interfaces;
using InvBank.Backend.Application.Common.Providers;
using InvBank.Backend.Contracts.Fund;
using InvBank.Backend.Domain.Entities;
using InvBank.Backend.Domain.Errors;

namespace InvBank.Backend.Application.Services;

public class FundService : BaseService
{
    private readonly IValidator<CreateFundRequest> _validator;
    private readonly IDateFormatter _dateFormatter;
    private readonly IAccountRepository _accountRepository;
    private readonly IAuthorizationFacade _authorizationFacade;
    private readonly IFundRepository _fundRepository;

    public FundService(
        IFundRepository fundRepository,
        IAuthorizationFacade authorizationFacade,
        IAccountRepository accountRepository,
        IDateFormatter dateFormatter,
        IValidator<CreateFundRequest> validator)
    {
        _fundRepository = fundRepository;
        _authorizationFacade = authorizationFacade;
        _accountRepository = accountRepository;
        _dateFormatter = dateFormatter;
        _validator = validator;
    }

    private async Task<ErrorOr<Account>> GetAccount(string iban)
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

    public async Task<ErrorOr<int>> CreateFund(CreateFundRequest request)
    {

        var validationResult = await Validate<CreateFundRequest>(_validator, request);

        if (validationResult.IsError)
        {
            return validationResult.Errors;
        }

        var findAccount = await GetAccount(request.Account);

        if (findAccount.IsError)
        {
            return findAccount.Errors;
        }

        return await _fundRepository.CreateFund(findAccount.Value,
            new ActivesInvestmentFund
            {
                InvestName = request.Name,
                InitialDate = _dateFormatter.ConvertToDateTime(request.InitialDate),
                Duration = request.Duration,
                InvestValue = request.Value,
                TaxPercent = request.TaxPercent
            }, request.Value);

    }

    public async Task<ErrorOr<IEnumerable<ActivesInvestmentFund>>> GetFundsOfAccount(string iban)
    {
        var findAccount = await GetAccount(iban);

        if (findAccount.IsError)
        {
            return findAccount.Errors;
        }

        return (await _fundRepository.GetInvestmentFundsOfAccount(findAccount.Value.Iban)).ToList();
    }

}