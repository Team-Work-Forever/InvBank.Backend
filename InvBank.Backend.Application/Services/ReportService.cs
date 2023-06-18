using ErrorOr;
using InvBank.Backend.Application.Common.Contracts;
using InvBank.Backend.Application.Common.Interfaces;
using InvBank.Backend.Application.Common.Providers;
using InvBank.Backend.Contracts.Bank;
using InvBank.Backend.Contracts.Payment;
using InvBank.Backend.Contracts.Report;
using InvBank.Backend.Domain.Entities;
using InvBank.Backend.Domain.Errors;

namespace InvBank.Backend.Application.Services;

public class ReportService
{
    private readonly IDateFormatter _dateFormatter;
    private readonly IAuthorizationFacade _authorizationFacade;
    private readonly IFundRepository _fundRepository;
    private readonly IDepositRepository _depositRepository;
    private readonly IPropertyAccountRepository _propertyAccountRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IBankRepository _bankRepository;
    private readonly IUserRepository _userRepository;

    public ReportService(
        IDepositRepository depositRepository,
        IPropertyAccountRepository propertyAccountRepository,
        IFundRepository fundRepository,
        IAuthorizationFacade authorizationFacade,
        IAccountRepository accountRepository,
        IDateFormatter dateFormatter,
        IBankRepository bankRepository,
        IUserRepository userRepository)
    {
        _depositRepository = depositRepository;
        _propertyAccountRepository = propertyAccountRepository;
        _fundRepository = fundRepository;
        _authorizationFacade = authorizationFacade;
        _accountRepository = accountRepository;
        _dateFormatter = dateFormatter;
        _bankRepository = bankRepository;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<PayReportResult>> GenerateReportPay(CreatePayReportCommand request)
    {

        Auth auth = await _authorizationFacade.GetAuthenticatedUser();

        Account? account = await _accountRepository.GetAccount(auth, request.Iban);

        if (account is null)
        {
            return Errors.Account.AccountNotFound;
        }

        IEnumerable<PaymentDeposit> paymentsDeposit = await _depositRepository.GetPayments(account.Iban);
        paymentsDeposit = paymentsDeposit.Where(pay => pay.PaymentDate >= request.InitialDate && pay.PaymentDate <= request.EndDate);

        IEnumerable<PaymentProperty> paymentsProperty = await _propertyAccountRepository.GetPayments(account.Iban);
        paymentsProperty = paymentsProperty.Where(pay => pay.PaymentDate >= request.InitialDate && pay.PaymentDate <= request.EndDate);

        return new PayReportResult
        (
            paymentsDeposit,
            paymentsProperty
        );

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

    public async Task<ErrorOr<BanksReportResponse>> GenerateBanksReport(CreateBanksReportCommand request)
    {
        List<BanksDepositResponse> banksInfo = new List<BanksDepositResponse>();

        IEnumerable<Bank> banks = await _bankRepository.GetAllBanks();

        Dictionary<string, BankResponse> bankResponses = new Dictionary<string, BankResponse>();

        IEnumerable<Auth> auths = await _userRepository.GetAllAuths();
        foreach (Auth auth in auths)
        {
            IEnumerable<Account> accounts = await _accountRepository.GetAllAccounts(auth);

            foreach (Account account in accounts)
            {
                Bank bank = banks.FirstOrDefault(b => b.Iban == account.BankNavigation.Iban);
                if (bank != null)
                {
                    decimal amountDeposits = 0;
                    decimal taxDeposits = 0;

                    IEnumerable<ActivesDepositAccount> depositAccounts = await _depositRepository.GetAllDepositAccounts(account);

                    foreach (ActivesDepositAccount active in depositAccounts)
                    {
                        amountDeposits += active.DepositValue;
                        taxDeposits += active.DepositValue * (active.TaxPercent / 100);
                    }

                    if (!bankResponses.ContainsKey(bank.Iban))
                    {
                        BankResponse bankResponse = new BankResponse(
                            bank.Iban,
                            bank.Phone,
                            bank.PostalCode
                        );

                        bankResponses.Add(bank.Iban, bankResponse);
                    }
                    else
                    {
                        amountDeposits += banksInfo
                            .Where(b => b.bank.Iban == bank.Iban)
                            .Sum(b => b.AmountDeposits);

                        taxDeposits += banksInfo
                            .Where(b => b.bank.Iban == bank.Iban)
                            .Sum(b => b.TaxDeposits);
                    }

                    banksInfo.RemoveAll(b => b.bank.Iban == bank.Iban);

                    banksInfo.Add(new BanksDepositResponse(
                        Guid.NewGuid(),
                        amountDeposits,
                        taxDeposits,
                        bankResponses[bank.Iban]
                    ));
                }
            }
        }

        BanksReportResponse reportResponse = new BanksReportResponse(banksInfo);
        return reportResponse;
    }

}