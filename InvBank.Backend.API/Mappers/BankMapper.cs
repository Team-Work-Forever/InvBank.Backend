using InvBank.Backend.Contracts.Bank;
using InvBank.Backend.Domain.Entities;

namespace InvBank.Backend.API.Mappers;

public class BankMapper : AutoMapper.Profile
{
    public BankMapper()
    {
        CreateMap<Bank, BankResponse>()
            .ConvertUsing(b => new BankResponse(b.Iban, b.Phone, b.Phone));
    }
}