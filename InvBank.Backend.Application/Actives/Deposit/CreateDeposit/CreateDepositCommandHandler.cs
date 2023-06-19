using ErrorOr;
using InvBank.Backend.Application.Common.Interfaces;
using InvBank.Backend.Application.Common.Providers;
using InvBank.Backend.Domain.Entities;
using InvBank.Backend.Domain.Errors;
using MediatR;

namespace InvBank.Backend.Application.Actives.Deposit.CreateDeposit;

public class CreateDepositCommandHandler : IRequestHandler<CreateDepositCommand, ErrorOr<string>>
{
    private readonly IAuthorizationFacade _authorizationFacade;
    private readonly IDateFormatter _dateFormatter;
    private readonly IDepositRepository _depositRepository;
    private readonly IAccountRepository _accountRepository;

    public CreateDepositCommandHandler(
        IDepositRepository depositRepository,
        IDateFormatter dateFormatter,
        IAccountRepository accountRepository,
        IAuthorizationFacade authorizationFacade)
    {
        _depositRepository = depositRepository;
        _dateFormatter = dateFormatter;
        _accountRepository = accountRepository;
        _authorizationFacade = authorizationFacade;
    }

    public async Task<ErrorOr<string>> Handle(CreateDepositCommand request, CancellationToken cancellationToken)
    {

        var auth = await _authorizationFacade.GetAuthenticatedUser();

        if (auth.IsError)
        {
            return auth.Errors;
        }

        var findAccount = await _accountRepository.GetAccount(auth.Value, request.Account);

        if (findAccount is null)
        {
            return Errors.Account.AccountNotFound;
        }

        await _depositRepository.CreateDepositActive(findAccount, new ActivesDepositAccount
        {
            DepositName = request.Name,
            DepositValue = request.Value,
            InitialDate = _dateFormatter.ConvertToDateTime(request.InitialDate),
            TaxPercent = request.TaxPercent,
            YearlyTax = request.YearlyTax,
            Duration = request.Duration,
        });

        return "Deposito Registado!";

    }

}