using ErrorOr;
using FluentValidation;
using InvBank.Backend.Application.Common.Interfaces;
using InvBank.Backend.Application.Common.Providers;
using InvBank.Backend.Contracts.PropertyAccount;
using InvBank.Backend.Domain.Entities;
using InvBank.Backend.Domain.Errors;

namespace InvBank.Backend.Application.Services;

public class PropertyAccountService : BaseService
{
    private readonly IValidator<CreatePropertyAccountRequest> _validator;
    private readonly IAuthorizationFacade _authorizationFacade;
    private readonly IDateFormatter _dateFormatter;
    private readonly IAccountRepository _accountRepository;
    private readonly IPropertyAccountRepository _propertyAccountRepository;

    public PropertyAccountService(
        IPropertyAccountRepository propertyAccountRepository,
        IAccountRepository accountRepository,
        IDateFormatter dateFormatter,
        IAuthorizationFacade authorizationFacade,
        IValidator<CreatePropertyAccountRequest> validator)
    {
        _propertyAccountRepository = propertyAccountRepository;
        _accountRepository = accountRepository;
        _dateFormatter = dateFormatter;
        _authorizationFacade = authorizationFacade;
        _validator = validator;
    }

    public async Task<ErrorOr<ActivesProperty>> CreatePropertyAccount(CreatePropertyAccountRequest request)
    {

        var validationResult = await Validate<CreatePropertyAccountRequest>(_validator, request);

        if (validationResult.IsError)
        {
            return validationResult.Errors;
        }

        var auth = await _authorizationFacade.GetAuthenticatedUser();

        if (auth.IsError)
        {
            return auth.Errors;
        }

        Account? findAccount = await _accountRepository.GetAccount(auth.Value, request.AccountIBAN);

        if (findAccount is null)
        {
            return Errors.Account.AccountNotFound;
        }

        var propertyAccount = await _propertyAccountRepository.CreatePropertyAccount(
            new ActivesProperty
            {
                PropertyName = request.Name,
                Duration = request.Duration,
                Designation = request.Designation,
                TaxPercent = request.TaxPercent,
                RentValue = request.RentValue,
                MonthValue = request.MonthValue,
                YearlyValue = request.YearlyValue,
                PropertyValue = request.PropertyValue,
                PostalCode = request.PostalCode,
                InitialDate = _dateFormatter.ConvertToDateTime(request.InitialDate)
            });

        findAccount.ActivesProperties.Add(propertyAccount);
        await _accountRepository.UpdateAccount(findAccount);
        return propertyAccount;

    }

    public async Task<ErrorOr<ActivesProperty>> GetPropertyAccount(Guid id)
    {

        ActivesProperty? findPropertyAccount = await _propertyAccountRepository.GetActivesProperty(id);

        if (findPropertyAccount is null)
        {
            return Errors.PropertyAccount.PropertyAccountNotFound;
        }

        return findPropertyAccount;

    }

    public async Task<ErrorOr<IEnumerable<ActivesProperty>>> GetAllPropertyAccounts(string accountIban)
    {

        var auth = await _authorizationFacade.GetAuthenticatedUser();

        if (auth.IsError)
        {
            return auth.Errors;
        }

        Account? account = await _accountRepository.GetAccount(auth.Value, accountIban);

        if (account is null)
        {
            return Errors.Account.AccountNotFound;
        }

        return (await _propertyAccountRepository.GetAllPropertyAccounts(accountIban)).ToList();

    }

    public async Task<ErrorOr<ActivesProperty>> UpdatePropertyAccount(Guid id, UpdatePropertyAccountRequest request)
    {

        var findPropertyAccount = await GetPropertyAccount(id);

        if (findPropertyAccount.IsError)
        {
            return findPropertyAccount.Errors;
        }

        findPropertyAccount.Value.PropertyName = request.Name;
        findPropertyAccount.Value.Duration = request.Duration;
        findPropertyAccount.Value.Designation = request.Designation;
        findPropertyAccount.Value.TaxPercent = request.TaxPercent;
        findPropertyAccount.Value.RentValue = request.RentValue;
        findPropertyAccount.Value.MonthValue = request.MonthValue;
        findPropertyAccount.Value.YearlyValue = request.YearlyValue;
        findPropertyAccount.Value.PropertyValue = request.PropertyValue;

        return await _propertyAccountRepository.UpdateActiveProperty(findPropertyAccount.Value);

    }

    public async Task<ErrorOr<int>> DeletePropertyAccount(Guid id)
    {
        var activesProperty = await GetPropertyAccount(id);

        if (activesProperty.IsError)
        {
            return activesProperty.Errors;
        }

        return await _propertyAccountRepository.DeleteActiveProperty(id);
    }

    public async Task<ErrorOr<string>> Pay(Guid propertyId, decimal amount)
    {

        var result = await GetPropertyAccount(propertyId);

        if (result.IsError)
        {
            return result.Errors;
        }

        if (result.Value.PropertyValue < amount)
        {
            return Errors.PropertyAccount.PropertyAmountGreater;
        }

        await _propertyAccountRepository.PayPropertyValue(propertyId, amount);
        return "Ativo Imóvel Pago";

    }
}
