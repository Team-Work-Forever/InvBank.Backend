using InvBank.Backend.Application.Common.Interfaces;
using InvBank.Backend.Contracts.Bank;
using InvBank.Backend.Domain.Entities;

namespace InvBank.Backend.Application.Services;

public class BankService
{
    private IBankRepository _bankRepository;

    public BankService(IBankRepository bankRepository)
    {
        _bankRepository = bankRepository;
    }

    public async Task CreateBank(RegisterBankRequest request)
    {
        Bank bank = new()
        {
            Iban = request.Iban,
            Phone = request.Phone,
            PostalCode = request.PostalCode
        };

        await _bankRepository.CreateBank(bank);
    }

    public async Task<IEnumerable<Bank>> GetAllBanks()
    {
        return await _bankRepository.GetAllBanks();
    }

}