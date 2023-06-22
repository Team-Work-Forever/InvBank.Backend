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
    private readonly IValidator<UpdateFundRequest> _validatorUpdateFund;
    private readonly IValidator<CreateFundRequest> _validatorCreateFund;
    private readonly IDateFormatter _dateFormatter;
    private readonly IAccountRepository _accountRepository;
    private readonly IAuthorizationFacade _authorizationFacade;
    private readonly IFundRepository _fundRepository;

    public FundService(
        IFundRepository fundRepository,
        IAuthorizationFacade authorizationFacade,
        IAccountRepository accountRepository,
        IDateFormatter dateFormatter,
        IValidator<CreateFundRequest> validator,
        IValidator<UpdateFundRequest> validatorUpdateFund)
    {
        _fundRepository = fundRepository;
        _authorizationFacade = authorizationFacade;
        _accountRepository = accountRepository;
        _dateFormatter = dateFormatter;
        _validatorCreateFund = validator;
        _validatorUpdateFund = validatorUpdateFund;
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

        var validationResult = await Validate<CreateFundRequest>(_validatorCreateFund, request);

        if (validationResult.IsError)
        {
            return validationResult.Errors;
        }

        var findAccount = await GetAccount(request.Account);

        if (findAccount.IsError)
        {
            return findAccount.Errors;
        }

        if (request.Value >= findAccount.Value.AmountValue)
        {
            return Errors.Fund.FundAmountGreater;
        }

        var result = await _fundRepository.CreateFund(findAccount.Value,
            new ActivesInvestmentFund
            {
                InvestName = request.Name,
                InitialDate = _dateFormatter.ConvertToDateTime(request.InitialDate),
                Duration = request.Duration,
                InvestValue = request.Value,
                TaxPercent = request.TaxPercent
            }, request.Value);

        findAccount.Value.AmountValue -= request.Value;
        await _accountRepository.UpdateAccount(findAccount.Value);

        return result;
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

    public async Task<ErrorOr<ActivesInvestmentFund>> GetFund(Guid id)
    {

        ActivesInvestmentFund? activesInvestmentFund = await _fundRepository.GetInvestmentFund(id);

        if (activesInvestmentFund is null)
        {
            return Errors.Fund.FundNotFound;
        }

        return activesInvestmentFund;

    }

    public async Task<ErrorOr<string>> deleteFund(Guid id)
    {

        var result = await GetFund(id);

        if (result.IsError)
        {
            return result.Errors;
        }

        await _fundRepository.DeleteFund(result.Value.Id);
        return "Fund eliminado com sucesso!";

    }

    public async Task<ErrorOr<ActivesInvestmentFund>> updateFund(Guid fundId, UpdateFundRequest request)
    {

        var validationResult = await Validate<UpdateFundRequest>(_validatorUpdateFund, request);

        if (validationResult.IsError)
        {
            return validationResult.Errors;
        }

        var result = await GetFund(fundId);

        if (result.IsError)
        {
            return result.Errors;
        }

        result.Value.InvestName = request.Name;
        result.Value.Duration = request.Duration;
        result.Value.TaxPercent = request.TaxPercent;

        return await _fundRepository.UpdateFund(result.Value);

    }
}