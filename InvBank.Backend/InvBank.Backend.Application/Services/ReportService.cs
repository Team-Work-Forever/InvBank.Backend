using ErrorOr;
using FluentValidation;
using InvBank.Backend.Application.Common.Contracts;
using InvBank.Backend.Application.Common.Interfaces;
using InvBank.Backend.Application.Common.Providers;
using InvBank.Backend.Contracts.Report;
using InvBank.Backend.Domain.Entities;
using InvBank.Backend.Domain.Errors;

namespace InvBank.Backend.Application.Services;

public class ReportService : BaseService
{
    private readonly IValidator<CreateProfitReportRequest> _validatorProfitReport;
    private readonly IValidator<CreatePayReportCommand> _validatorPayReport;
    private readonly IDateFormatter _dateFormatter;
    private readonly IAuthorizationFacade _authorizationFacade;
    private readonly IFundRepository _fundRepository;
    private readonly IDepositRepository _depositRepository;
    private readonly IPropertyAccountRepository _propertyAccountRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IBankRepository _bankRepository;

    public ReportService(
        IDepositRepository depositRepository,
        IPropertyAccountRepository propertyAccountRepository,
        IFundRepository fundRepository,
        IAuthorizationFacade authorizationFacade,
        IAccountRepository accountRepository,
        IDateFormatter dateFormatter,
        IBankRepository bankRepository,
        IValidator<CreatePayReportCommand> validator,
        IValidator<CreateProfitReportRequest> validatorProfitReport)
    {
        _depositRepository = depositRepository;
        _propertyAccountRepository = propertyAccountRepository;
        _fundRepository = fundRepository;
        _authorizationFacade = authorizationFacade;
        _accountRepository = accountRepository;
        _dateFormatter = dateFormatter;
        _bankRepository = bankRepository;
        _validatorPayReport = validator;
        _validatorProfitReport = validatorProfitReport;
    }

    public async Task<ErrorOr<PayReportResult>> GenerateReportPay(CreatePayReportCommand request)
    {

        var validationResult = await Validate<CreatePayReportCommand>(_validatorPayReport, request);

        if (validationResult.IsError)
        {
            return validationResult.Errors;
        }

        var auth = await _authorizationFacade.GetAuthenticatedUser();

        if (auth.IsError)
        {
            return auth.Errors;
        }

        Account? account = await _accountRepository.GetAccount(auth.Value, request.Iban);

        if (account is null)
        {
            return Errors.Account.AccountNotFound;
        }

        IEnumerable<PaymentDeposit> paymentsDeposit = await _depositRepository.GetPayments(account.Iban);
        paymentsDeposit = paymentsDeposit.Where(pay => pay.PaymentDate >= _dateFormatter.ConvertToDateTime(request.InitialDate) && pay.PaymentDate <= _dateFormatter.ConvertToDateTime(request.EndDate));

        IEnumerable<PaymentProperty> paymentsProperty = await _propertyAccountRepository.GetPayments(account.Iban);
        paymentsProperty = paymentsProperty.Where(pay => pay.PaymentDate >= _dateFormatter.ConvertToDateTime(request.InitialDate) && pay.PaymentDate <= _dateFormatter.ConvertToDateTime(request.EndDate));

        return new PayReportResult
        (
            paymentsDeposit,
            paymentsProperty
        );

    }

    public async Task<ErrorOr<ProfitReportResponse>> GenerateReportProfit(CreateProfitReportRequest request)
    {

        var validationResult = await Validate<CreateProfitReportRequest>(_validatorProfitReport, request);

        if (validationResult.IsError)
        {
            return validationResult.Errors;
        }

        IEnumerable<AtiveStateDeposit> depositsActives = await _depositRepository.GetAtives();

        depositsActives = depositsActives
            .Where(da => da.InitDate >= _dateFormatter.ConvertToDateTime(request.InitialDate) && da.InitDate <= _dateFormatter.ConvertToDateTime(request.EndDate))
            .Where(da => da.EndDate is not null ? (da.EndDate >= _dateFormatter.ConvertToDateTime(request.InitialDate) && da.EndDate <= _dateFormatter.ConvertToDateTime(request.EndDate)) : true);

        IEnumerable<AtiveStateProperty> propertyActives = await _propertyAccountRepository.GetAtives();

        propertyActives = propertyActives
                    .Where(da => da.InitDate >= _dateFormatter.ConvertToDateTime(request.InitialDate) && da.InitDate <= _dateFormatter.ConvertToDateTime(request.EndDate))
                    .Where(da => da.EndDate is not null ? (da.EndDate >= _dateFormatter.ConvertToDateTime(request.InitialDate) && da.EndDate <= _dateFormatter.ConvertToDateTime(request.EndDate)) : true);


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