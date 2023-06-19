using AutoMapper;
using ErrorOr;
using InvBank.Backend.Application.Authentication.Commands.RegisterClient;
using InvBank.Backend.Application.Authentication.Commands.RegisterCompany;
using InvBank.Backend.Application.Authentication.Queries.Login;
using InvBank.Backend.Application.Services;
using InvBank.Backend.Contracts;
using InvBank.Backend.Contracts.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvBank.Backend.API.Controllers;

[ApiController]
[Route("auth")]
[AllowAnonymous]
public class AuthenticationController : ControllerBase
{
    private readonly ProfileService _profileService;
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public AuthenticationController(ISender mediator, IMapper mapper, ProfileService profileService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _profileService = profileService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthorizationResult>> Login([FromBody] LoginRequest request)
    {
        ErrorOr<AuthenticationResult> authResult = await _mediator.Send(_mapper.Map<LoginQuerie>(request));

        return authResult.MatchFirst(
           authResult => Ok(authResult),
           firstError => Problem(statusCode: StatusCodes.Status409Conflict, title: firstError.Description)
       );
    }

    [HttpPost("register/client")]
    public async Task<ActionResult<AuthenticationResult>> RegisterClient([FromBody] RegisterClientRequest request)
    {
        ErrorOr<AuthenticationResult> authResult = await _mediator.Send(_mapper.Map<RegisterClientCommand>(request));

        return authResult.MatchFirst(
            authResult => Ok(authResult),
            firstError => Problem(statusCode: StatusCodes.Status409Conflict, title: firstError.Description)
        );
    }

    [HttpPost("register/company")]
    public async Task<ActionResult<SimpleResponse>> RegisterCompany([FromBody] RegisterCompanyRequest request)
    {
        ErrorOr<string> authResult = await _mediator.Send(_mapper.Map<RegisterCompanyCommand>(request));

        return authResult.MatchFirst(
            authResult => Ok(new SimpleResponse(authResult)),
            firstError => Problem(statusCode: StatusCodes.Status409Conflict, title: firstError.Description)
        );
    }

    [Authorize]
    [HttpPost("profile")]
    public async Task<ActionResult<ProfileResponse>> GetProfile()
    {

        var profileResponse = await _profileService.GetProfile();

        return profileResponse.MatchFirst
        (
            profileResponse => Ok(_mapper.Map<ProfileResponse>(profileResponse)),
            firstError => Problem(statusCode: StatusCodes.Status409Conflict, title: firstError.Description)
        );

    }

}