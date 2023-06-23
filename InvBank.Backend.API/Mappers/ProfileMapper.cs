using InvBank.Backend.Contracts.Authentication;
using InvBank.Backend.Domain.Entities;

namespace InvBank.Backend.API.Mappers;

public class ProfileMapper : AutoMapper.Profile
{
    
    public ProfileMapper()
    {
        CreateMap<Auth, ProfileResponse>()
            .ConstructUsing(resp => 
                new ProfileResponse(
                    resp.Id,
                    resp.Email,
                    resp.Profile.FirstName,
                    resp.Profile.LastName,
                    resp.Profile.BirthDate.ToString("dd/MM/yyyy"),
                    resp.Profile.Nif,
                    resp.Profile.Cc,
                    resp.Profile.Phone,
                    resp.Profile.PostalCode,
                    resp.UserRole
                ));
    }

}