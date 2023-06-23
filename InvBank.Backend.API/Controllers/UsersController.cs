using AutoMapper;
using InvBank.Backend.Application.Services;
using InvBank.Backend.Contracts;
using InvBank.Backend.Contracts.Authentication;
using InvBank.Backend.Contracts.User;
using InvBank.Backend.Infrastructure.Authentication;
using InvBank.Backend.Infrastructure.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvBank.Backend.API.Controllers;

[ApiController]
[Route("users")]
public class UsersController : BaseController
{
    private readonly IMapper _mapper;
    private readonly UserService _userService;

    public UsersController(UserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [Authorize]
    [HttpPost("profile")]
    public async Task<ActionResult<ProfileResponse>> GetProfile()
    {
        var profileResponse = await _userService.GetProfile();

        return profileResponse.Match(
            profileResponse => Ok(_mapper.Map<ProfileResponse>(profileResponse)),
            firstError => Problem<ProfileResponse>(firstError)
        );
    }

    [AuthorizeRole(Role.USERMANAGER, Role.ADMIN)]
    [HttpGet("clients")]
    public async Task<ActionResult<IEnumerable<ProfileResponse>>> GetAllClients()
    {
        var profileResponse = await _userService.GetAllClients();

        return profileResponse.Match(
            profileResponse => Ok(_mapper.Map<IEnumerable<ProfileResponse>>(profileResponse)),
            firstError => Problem<IEnumerable<ProfileResponse>>(firstError)
        );
    }

    [AuthorizeRole(Role.ADMIN)]
    [HttpPost("create")]
    public async Task<ActionResult<ProfileResponse>> CreateUserByRole([FromBody] CreateUserByRoleRequest request)
    {
        var profileResponse = await _userService.CreateUserByRole(request);

        return profileResponse.Match(
            profileResponse => Ok(_mapper.Map<ProfileResponse>(profileResponse)),
            firstError => Problem<ProfileResponse>(firstError)
        );
    }

    [AuthorizeRole(Role.ADMIN)]
    [HttpGet()]
    public async Task<ActionResult<IEnumerable<ProfileResponse>>> GetAllWorkingUsers()
    {
        var profileResponse = await _userService.GetAllWorkingUsers();

        return profileResponse.Match(
            profileResponse => Ok(_mapper.Map<IEnumerable<ProfileResponse>>(profileResponse)),
            firstError => Problem<IEnumerable<ProfileResponse>>(firstError)
        );
    }

    [AuthorizeRole(Role.ADMIN)]
    [HttpPut("delete")]
    public async Task<ActionResult<ProfileResponse>> UpdateUser([FromQuery] Guid id, [FromQuery] UpdateUserByRoleRequest request)
    {
        var profileResponse = await _userService.UpdateUser(id, request);

        return profileResponse.Match(
            profileResponse => Ok(_mapper.Map<ProfileResponse>(profileResponse)),
            firstError => Problem<ProfileResponse>(firstError)
        );
    }

    [AuthorizeRole(Role.ADMIN)]
    [HttpDelete("delete")]
    public async Task<ActionResult<SimpleResponse>> DeleteUser([FromQuery] Guid id)
    {
        var profileResponse = await _userService.DeleteUser(id);

        return profileResponse.Match(
            profileResponse => Ok(new SimpleResponse(profileResponse)),
            firstError => Problem<SimpleResponse>(firstError)
        );
    }

}