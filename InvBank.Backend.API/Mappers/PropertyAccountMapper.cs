using InvBank.Backend.Contracts.Deposit;
using InvBank.Backend.Contracts.PropertyAccount;
using InvBank.Backend.Domain.Entities;

namespace InvBank.Backend.API.Mappers;

public class PropertyAccountMapper : AutoMapper.Profile
{

    public PropertyAccountMapper()
    {
        CreateMap<ActivesProperty, PropertyAccountResponse>()
            .ConvertUsing(prop => new PropertyAccountResponse(
                prop.Id,
                prop.InitialDate.ToString("dd/MM/yyyy"),
                prop.Duration,
                prop.TaxPercent,
                prop.Designation,
                prop.PostalCode,
                prop.PropertyValue,
                prop.RentValue,
                prop.MonthValue,
                prop.YearlyValue,
                prop.Account));
    }

}