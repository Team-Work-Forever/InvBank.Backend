using ErrorOr;
using FluentValidation;
using InvBank.Backend.Application.Common;
using InvBank.Backend.Application.Common.Interfaces;
using InvBank.Backend.Application.Common.Providers;
using InvBank.Backend.Contracts.User;
using InvBank.Backend.Domain.Entities;
using InvBank.Backend.Domain.Errors;

namespace InvBank.Backend.Application.Services;

public class UserService : BaseService
{
    private readonly IDateFormatter _dateFormatter;
    private readonly IValidator<CreateUserByRoleRequest> _validator;
    private readonly IAuthorizationFacade _authorizationFacade;
    private readonly IUserRepository _userRepository;

    public UserService(
        IUserRepository userRepository,
        IAuthorizationFacade authorizationFacade,
        IValidator<CreateUserByRoleRequest> validator,
        IDateFormatter dateFormatter)
    {
        _userRepository = userRepository;
        _authorizationFacade = authorizationFacade;
        _validator = validator;
        _dateFormatter = dateFormatter;
    }

    public async Task<ErrorOr<Tuple<Profile, string>>> GetProfile()
    {
        var auth = await _authorizationFacade.GetAuthenticatedUser();

        if (auth.IsError)
        {
            return auth.Errors;
        }

        var profile = await _userRepository.GetProfileByEmail(auth.Value.Email);

        if (profile is null)
        {
            return Errors.Profile.ProfileNotFound;
        }

        return new Tuple<Profile, string>(profile, auth.Value.Email);
    }

    public async Task<ErrorOr<IEnumerable<Tuple<Profile, string>>>> GetAllClients()
    {
        IEnumerable<Profile> clients = await _userRepository.GetAllClients();

        return clients
            .Select(client =>
                new Tuple<Profile, string>(client, client.IdNavigation.Email))
                .ToList();
    }

    public async Task<ErrorOr<Tuple<Profile, string>>> CreateUserByRole(CreateUserByRoleRequest request)
    {
        var validationResult = await Validate<CreateUserByRoleRequest>(_validator, request);

        if (validationResult.IsError)
        {
            return validationResult.Errors;
        }

        var user = new Auth
        {
            Email = request.Email,
            UserPassword = "assuncao12",
            Profile = new Profile
            {
                BirthDate = _dateFormatter.ConvertToDateTime(request.BirthDate),
                Cc = request.Cc,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Nif = request.Nif,
                Phone = request.Phone,
                PostalCode = request.PostalCode
            }
        };

        return new Tuple<Profile, string>(user.Profile, user.Email);
    }

    public async Task<ErrorOr<string>> DeleteUser(Guid id)
    {

        Auth? auth = await _userRepository.GetUserAuth(id);

        if (auth is null)
        {
            return Errors.Auth.AuthNotFoundById;
        }

        await _userRepository.DeleteAuth(auth);

        return "Utilizador removido com sucesso!";

    }

    public async Task<ErrorOr<Tuple<Profile, string>>> UpdateUser(Guid id, UpdateUserByRoleRequest request)
    {

        Auth? auth = await _userRepository.GetUserAuth(id);

        if (auth is null)
        {
            return Errors.Auth.AuthNotFoundById;
        }

        if (auth.Profile is null)
        {
            return Errors.Profile.ProfileNotFound;
        }

        auth.Email = request.Email;
        auth.Profile.BirthDate = _dateFormatter.ConvertToDateTime(request.BirthDate);
        auth.Profile.FirstName = request.FirstName;
        auth.Profile.LastName = request.LastName;
        auth.Profile.Nif = request.Nif;
        auth.Profile.Cc = request.Cc;
        auth.Profile.PostalCode = request.PostalCode;
        auth.Profile.Phone = request.Phone;

        await _userRepository.UpdateAuth(auth);
        return new Tuple<Profile, string>(auth.Profile, auth.Email);

    }

    public async Task<ErrorOr<IEnumerable<Tuple<Profile, string>>>> GetAllWorkingUsers()
    {
        IEnumerable<Auth> users = await _userRepository.GetAllUsers();

        return users
            .Where(user => user.UserRole != ((int)AuthorizationRole.ADMIN))
            .Select(user => new Tuple<Profile, string>(user.Profile, user.Email))
            .ToList();
    }
}