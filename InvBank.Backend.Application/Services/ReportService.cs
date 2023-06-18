using ErrorOr;
using InvBank.Backend.Application.Common.Interfaces;
using InvBank.Backend.Application.Common.Providers;
using InvBank.Backend.Contracts.Report;
using InvBank.Backend.Domain.Entities;
using InvBank.Backend.Domain.Errors;

namespace InvBank.Backend.Application.Services;

public class ReportService
{
    private readonly IAuthorizationFacade _authorizationFacade;
    private readonly IFundRepository _fundRepository;
    private readonly IDepositRepository _depositRepository;
    private readonly IPropertyAccountRepository _propertyAccountRepository;
    private readonly IAccountRepository _accountRepository;

    public ReportService(
        IDepositRepository depositRepository,
        IPropertyAccountRepository propertyAccountRepository,
        IFundRepository fundRepository,
        IAuthorizationFacade authorizationFacade,
        IAccountRepository accountRepository)
    {
        _depositRepository = depositRepository;
        _propertyAccountRepository = propertyAccountRepository;
        _fundRepository = fundRepository;
        _authorizationFacade = authorizationFacade;
        _accountRepository = accountRepository;
    }

    public async Task<ErrorOr<IEnumerable<PaymentDeposit>>> GenerateReportPay(CreatePayReportCommand request)
    {

        Auth auth = await _authorizationFacade.GetAuthenticatedUser();

        Account? account = await _accountRepository.GetAccount(auth, request.Iban);

        if (account is null)
        {
            return Errors.Account.AccountNotFound;
        }

        IEnumerable<PaymentDeposit> payments = await _depositRepository.GetPayments(account.Iban);
        payments = payments.Where(pay => pay.PaymentDate >= request.InitialDate && pay.PaymentDate <= request.EndDate);

        return payments.ToList();

    }

    public async Task<ProfitReportResponse> GenerateReportProfit(CreateProfitReportRequest request)
    {

        IEnumerable<AtiveStateDeposit> depositsActives = await _depositRepository.GetAtives();

        depositsActives = depositsActives
            .Where(da => da.InitDate >= request.InitialDate && da.InitDate <= request.EndDate)
            .Where(da => da.EndDate is not null ? (da.EndDate >= request.InitialDate && da.EndDate <= request.EndDate) : true);

        IEnumerable<AtiveStateProperty> propertyActives = await _propertyAccountRepository.GetAtives();

        propertyActives = propertyActives
                    .Where(da => da.InitDate >= request.InitialDate && da.InitDate <= request.EndDate)
                    .Where(da => da.EndDate is not null ? (da.EndDate >= request.InitialDate && da.EndDate <= request.EndDate) : true);


        decimal beforeProfitDeposit = depositsActives.Sum(da => da.Ative.DepositValue);
        decimal afterProfitDeposit = depositsActives.Sum(da => da.Ative.DepositValue * (1 + (da.Ative.TaxPercent / 100)));

        decimal beforeProfitProperty = propertyActives.Sum(da => da.Ative.RentValue);
        decimal afterProfitProperty = propertyActives.Sum(da => da.Ative.RentValue * (1 + (da.Ative.TaxPercent / 100)));

        decimal profitMean = (afterProfitDeposit + afterProfitProperty) / 2;

        return new ProfitReportResponse
        (
            beforeProfitDeposit,
            afterProfitDeposit,
            beforeProfitProperty,
            afterProfitProperty,
            profitMean
        );

    }

}