using System.Text.Json;
using ErrorOr;
using InvBank.Web.Contracts;
using InvBank.Web.Contracts.Users;

namespace InvBank.Web.Helper.EndPoints;

public class UserEndPoint : BaseEndPoint
{
    public UserEndPoint(AuthApiHelper apiHelper) : base(apiHelper)
    {
    }

    public async Task<ErrorOr<UserResponse>> GetClientById(Guid id)
    {
        return await MakeRequest<UserResponse>(
            () => _apiHelper.DoGet("/users?id=" + id)
        );
    }
    public async Task<ErrorOr<IEnumerable<UserResponse>>> GetAllClients()
    {
        return await MakeRequest<IEnumerable<UserResponse>>(
            () => _apiHelper.DoGet("/users/clients")
        );
    }

    public async Task<ErrorOr<UserResponse>> CreateUserByRole(CreateUserByRoleRequest request)
    {
        return await MakeRequest<UserResponse>(
            () => _apiHelper.DoPost("/users/create", JsonSerializer.Serialize(request))
        );
    }

    public async Task<ErrorOr<IEnumerable<UserResponse>>> GetAllWorkingUsers()
    {
        return await MakeRequest<IEnumerable<UserResponse>>(
            () => _apiHelper.DoGet("/users/all")
        );
    }
  
    public async Task<ErrorOr<SimpleResponse>> DeleteUser(Guid id)
    {
        return await MakeRequest<SimpleResponse>(
            () => _apiHelper.DoDelete("/users/delete?id=" + id)
        );
    }
    
    public async Task<ErrorOr<UserResponse>> UpdateUser(Guid id, UpdateUserByRoleRequest request)
    {
        return await MakeRequest<UserResponse>(
            () => _apiHelper.DoPut("/users/update?id=" + id, JsonSerializer.Serialize(request))
        );
    }

}