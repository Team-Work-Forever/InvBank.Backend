using InvBank.Backend.Domain.Entities;

namespace InvBank.Backend.Application.Common.Interfaces;

public interface IBankRepository
{
    Task<Bank?> GetBank(string iban);

    Task<int> UpdateBank(Bank bank);

}