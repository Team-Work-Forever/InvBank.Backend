using ErrorOr;
using InvBank.Backend.Application.Common.Interfaces;
using InvBank.Backend.Application.Common.Providers;
using InvBank.Backend.Contracts.Fund;
using InvBank.Backend.Domain.Entities;
using InvBank.Backend.Domain.Errors;

namespace InvBank.Backend.Application.Services;

public class FundService
{
    private readonly IDateFormatter _dateFormatter;
    private readonly IAccountRepository _accountRepository;
    private readonly IAuthorizationFacade _authorizationFacade;
    private readonly IFundRepository _fundRepository;

    public FundService(IFundRepository fundRepository, IAuthorizationFacade authorizationFacade, IAccountRepository accountRepository, IDateFormatter dateFormatter)
    {
        _fundRepository = fundRepository;
        _authorizationFacade = authorizationFacade;
        _accountRepository = accountRepository;
        _dateFormatter = dateFormatter;
    }

    private async Task<ErrorOr<Account>> GetAccount(string iban)
    {
        Auth auth = await _authorizationFacade.GetAuthenticatedUser();

        Account? findAccount = await _accountRepository.GetAccount(auth, iban);

        if (findAccount is null)
        {
            return Errors.Account.AccountNotFound;
        }

        return findAccount;
    }

    public async Task<ErrorOr<ActivesInvestmentFund>> GetInvFund(Guid fundId)
    {
        ActivesInvestmentFund? findInvFund = await _fundRepository.GetInvFundActive(fundId);

        if (findInvFund is null)
        {
            return Errors.Fund.FundNotFound;
        }

        return findInvFund;
    }

    public async Task<ErrorOr<int>> CreateFund(CreateFundRequest request)
    {

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

    public async Task<ErrorOr<ActivesInvestmentFund>> UpdateFundAccount(Guid id, UpdateFundRequest request)
    {

        var findFundAccount = await GetInvFund(id);

        if (findFundAccount.IsError)
        {
            return findFundAccount.Errors;
        }

        findFundAccount.Value.InvestName = request.Name;
        findFundAccount.Value.Duration = request.Duration;
        findFundAccount.Value.TaxPercent = request.TaxPercent;
        findFundAccount.Value.InvestValue = request.Value;

        return await _fundRepository.UpdateFundAccount(findFundAccount.Value);

    }

    public async Task<ErrorOr<int>> DeleteFundAccount(Guid id)
    {
        var activesProperty = await GetInvFund(id);

        if (activesProperty.IsError)
        {
            return activesProperty.Errors;
        }

        return await _fundRepository.DeleteFund(id);
    }

}