using ErrorOr;
using InvBank.Backend.Application.Common.Interfaces;
using InvBank.Backend.Application.Common.Providers;
using InvBank.Backend.Domain.Entities;
using InvBank.Backend.Domain.Errors;

namespace InvBank.Backend.Application.Services;

public class ProfileService
{
    private readonly IAuthorizationFacade _authorizationFacade;   
    private readonly IUserRepository _userRepository;

    public ProfileService(IUserRepository userRepository, IAuthorizationFacade authorizationFacade)
    {
        _userRepository = userRepository;
        _authorizationFacade = authorizationFacade;
    }

    public async Task<ErrorOr<Tuple<Profile, string>>> GetProfile() {

        Auth auth = await _authorizationFacade.GetAuthenticatedUser();

        var profile = await _userRepository.GetProfileByEmail(auth.Email);

        if (profile is null)
        {
            return Errors.Profile.ProfileNofFound;
        }

        return new Tuple<Profile, string>(profile, auth.Email);
        
    }

}