using InvBank.Backend.Contracts.Authentication;
using InvBank.Backend.Domain.Entities;

namespace InvBank.Backend.API.Mappers;

public class ProfileMapper : AutoMapper.Profile
{
    
    public ProfileMapper()
    {
        CreateMap<Tuple<Profile, string>, ProfileResponse>()
            .ConstructUsing(resp => 
                new ProfileResponse(
                    resp.Item1.Id,
                    resp.Item2,
                    resp.Item1.FirstName,
                    resp.Item1.LastName,
                    resp.Item1.BirthDate.ToString("dd/MM/yyyy"),
                    resp.Item1.Nif,
                    resp.Item1.Cc,
                    resp.Item1.Phone,
                    resp.Item1.PostalCode
                ));
    }

}