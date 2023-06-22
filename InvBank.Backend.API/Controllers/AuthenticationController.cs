using AutoMapper;
using ErrorOr;
using InvBank.Backend.Application.Authentication.Commands.RegisterClient;
using InvBank.Backend.Application.Authentication.Commands.RegisterCompany;
using InvBank.Backend.Application.Authentication.Queries.Login;
using InvBank.Backend.Application.Authentication.Queries.TokenQuerie;
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
public class AuthenticationController : BaseController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public AuthenticationController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthorizationResult>> Login([FromBody] LoginRequest request)
    {
        ErrorOr<AuthenticationResult> authResult = await _mediator.Send(_mapper.Map<LoginQuerie>(request));

        return authResult.Match(
           authResult => Ok(authResult),
           firstError => Problem<AuthorizationResult>(firstError)
       );
    }

    [HttpPost("register/client")]
    public async Task<ActionResult<AuthenticationResult>> RegisterClient([FromBody] RegisterClientRequest request)
    {
        ErrorOr<AuthenticationResult> authResult = await _mediator.Send(_mapper.Map<RegisterClientCommand>(request));

        return authResult.Match(
            authResult => Ok(authResult),
            firstError => Problem<AuthenticationResult>(firstError)
        );
    }

    [HttpPost("register/company")]
    public async Task<ActionResult<SimpleResponse>> RegisterCompany([FromBody] RegisterCompanyRequest request)
    {
        ErrorOr<string> authResult = await _mediator.Send(_mapper.Map<RegisterCompanyCommand>(request));

        return authResult.Match(
            authResult => Ok(new SimpleResponse(authResult)),
            firstError => Problem<SimpleResponse>(firstError)
        );
    }

    [HttpGet("refreshToken")]
    public async Task<ActionResult<AuthenticationResult>> GetNewTokens([FromQuery] string token)
    {
        ErrorOr<AuthenticationResult> authResult = await _mediator.Send(new TokenQuerie(token));

        return authResult.Match(
            authResult => Ok(authResult),
            firstError => Problem<AuthenticationResult>(firstError)
        );
    }

}