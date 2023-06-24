using ErrorOr;
using FluentValidation;
using InvBank.Backend.Application.Common;
using InvBank.Backend.Application.Common.Interfaces;
using InvBank.Backend.Application.Common.Providers;
using InvBank.Backend.Contracts.Account;
using InvBank.Backend.Contracts.Fund;
using InvBank.Backend.Domain.Entities;
using InvBank.Backend.Domain.Errors;

namespace InvBank.Backend.Application.Services;

public class FundService : BaseService
{
    private readonly IValidator<MakeTransfer> _validatorInvest;
    private readonly IValidator<AssociateAccountToFundRequest> _validatorAssociateAccount;
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
        IValidator<UpdateFundRequest> validatorUpdateFund,
        IValidator<AssociateAccountToFundRequest> validatorAssociateAccount,
        IValidator<MakeTransfer> validatorInvest)
    {
        _fundRepository = fundRepository;
        _authorizationFacade = authorizationFacade;
        _accountRepository = accountRepository;
        _dateFormatter = dateFormatter;
        _validatorCreateFund = validator;
        _validatorUpdateFund = validatorUpdateFund;
        _validatorAssociateAccount = validatorAssociateAccount;
        _validatorInvest = validatorInvest;
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

        var result = await _fundRepository.CreateFund(
            new ActivesInvestmentFund
            {
                InvestName = request.Name,
                InitialDate = _dateFormatter.ConvertToDateTime(request.InitialDate),
                Duration = request.Duration,
                TaxPercent = request.TaxPercent
            });

        return result;
    }

    public async Task<ErrorOr<IEnumerable<ActivesInvestmentFund>>> GetAllFunds()
    {
        IEnumerable<ActivesInvestmentFund> funds = await _fundRepository.GetFunds();
        return funds.ToList();
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

    public async Task<ErrorOr<ActivesInvestmentFund>> AssociateAccountToFund(AssociateAccountToFundRequest request)
    {
        var validationResult = await Validate<AssociateAccountToFundRequest>(_validatorAssociateAccount, request);

        if (validationResult.IsError)
        {
            return validationResult.Errors;
        }

        ErrorOr<Account> findAccount = await GetAccount(request.IBAN);

        if (findAccount.IsError)
        {
            return findAccount.Errors;
        }

        var findFund = await _fundRepository.GetInvestmentFund(request.FundId);

        if (findFund is null)
        {
            return Errors.Fund.FundNotFound;
        }

        if (findAccount.Value.AmountValue < request.Amount)
        {
            return Errors.Account.CannotRaiseMore;
        }

        var fundOfAccount = await GetFundsOfAccount(findAccount.Value.Iban);

        if (fundOfAccount.IsError)
        {
            return fundOfAccount.Errors;
        }

        bool alreadyAssociatedAccount = fundOfAccount.Value.Any(x => x.Id == findFund.Id);

        if (alreadyAssociatedAccount)
        {
            return Errors.Fund.AlreadyAssociatedAccount;
        }

        await _fundRepository.AssociateAccount(
            findAccount.Value,
            findFund,
            request.Amount
        );

        findAccount.Value.AmountValue -= request.Amount;

        return findFund;

    }

    public async Task<ErrorOr<string>> InvestOnFund(string IBAN, Guid fundId, MakeTransferRequest request)
    {
        var validationResult = await Validate<MakeTransfer>(_validatorInvest, new MakeTransfer(
            IBAN,
            request.amountValue
        ));

        if (validationResult.IsError)
        {
            return validationResult.Errors;
        }

        var findAccount = await GetAccount(IBAN);

        if (findAccount.IsError)
        {
            return findAccount.Errors;
        }

        var findFundsOfAccounts = await _fundRepository.GetInvestmentFundsOfAccount(IBAN);

        var findFund = findFundsOfAccounts
            .Where(x => x.Id == fundId)
            .FirstOrDefault();

        if (findFund is null)
        {
            return Errors.Fund.FundIsNotAssociated;
        }

        if (findAccount.Value.AmountValue < request.amountValue)
        {
            return Errors.Account.CannotRaiseMore;
        }

        findFund.InvestValue += request.amountValue;
        findAccount.Value.AmountValue -= request.amountValue;
        await _accountRepository.UpdateAccount(findAccount.Value);
        await _fundRepository.UpdateFund(findFund);

        return $"Foi depositado {request.amountValue} u.m. no fundo da conta ${IBAN}";
    }
}