using ErrorOr;
using FluentValidation;
using InvBank.Backend.Application.Common.Interfaces;
using InvBank.Backend.Contracts.Bank;
using InvBank.Backend.Domain.Entities;
using InvBank.Backend.Domain.Errors;

namespace InvBank.Backend.Application.Services;

public class BankService : BaseService
{
    private readonly IValidator<RegisterBankRequest> _validator;
    private readonly IBankRepository _bankRepository;

    public BankService(IBankRepository bankRepository, IValidator<RegisterBankRequest> validator)
    {
        _bankRepository = bankRepository;
        _validator = validator;
    }

    public async Task<ErrorOr<string>> CreateBank(RegisterBankRequest request)
    {

        var validationResult = await Validate<RegisterBankRequest>(_validator, request);

        if (validationResult.IsError)
        {
            return validationResult.Errors;
        }

        var findBank = await _bankRepository.GetBank(request.Iban);

        if (findBank is not null)
        {
            return Errors.Bank.DuplicateKey;
        }

        Bank bank = new()
        {
            Iban = request.Iban,
            Phone = request.Phone,
            PostalCode = request.PostalCode
        };

        await _bankRepository.CreateBank(bank);
        return "Banco criado!";

    }

    public async Task<IEnumerable<Bank>> GetAllBanks()
    {
        return await _bankRepository.GetAllBanks();
    }

}