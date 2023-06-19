using AutoMapper;
using InvBank.Backend.Application.Services;
using InvBank.Backend.Contracts;
using InvBank.Backend.Contracts.Payment;
using InvBank.Backend.Contracts.PropertyAccount;
using InvBank.Backend.Infrastructure.Authentication;
using InvBank.Backend.Infrastructure.Providers;
using Microsoft.AspNetCore.Mvc;

namespace InvBank.Backend.API.Controllers;

[ApiController]
[Route("properties")]
public class PropertyAccountController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly PropertyAccountService _propertyAccountService;

    public PropertyAccountController(PropertyAccountService propertyAccountService, IMapper mapper)
    {
        _propertyAccountService = propertyAccountService;
        _mapper = mapper;
    }

    [AuthorizeRole(Role.CLIENT, Role.USERMANAGER)]
    [HttpPost("create")]
    public async Task<ActionResult<string>> CreatePropertyAccount([FromBody] CreatePropertyAccountRequest request)
    {
        var createResult = await _propertyAccountService.CreatePropertyAccount(request);

        return createResult.MatchFirst(
            createResult => Ok(new SimpleResponse("O ativo foi registado!")),
            firstError => Problem(statusCode: StatusCodes.Status409Conflict, title: firstError.Description)
        );
    }

    [AuthorizeRole(Role.CLIENT, Role.USERMANAGER)]
    [HttpGet()]
    public async Task<ActionResult<PropertyAccountResponse>> GetPropertyAccount([FromQuery] Guid propertyAccount)
    {
        var propertyAccountResult = await _propertyAccountService.GetPropertyAccount(propertyAccount);

        return propertyAccountResult.MatchFirst(
            propertyAccount => Ok(_mapper.Map<PropertyAccountResponse>(propertyAccount)),
            firstError => Problem(statusCode: StatusCodes.Status409Conflict, title: firstError.Description)
        );

    }

    [AuthorizeRole(Role.CLIENT, Role.USERMANAGER)]
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<PropertyAccountResponse>>> GetAllPropertyAccount([FromQuery] string iban)
    {
        var propertyAccountsResult = await _propertyAccountService.GetAllPropertyAccounts(iban);

        return propertyAccountsResult.MatchFirst(
            propertyAccounts => Ok(_mapper.Map<IEnumerable<PropertyAccountResponse>>(propertyAccounts)),
            firstError => Problem(statusCode: StatusCodes.Status409Conflict, title: firstError.Description)
        );

    }

    [AuthorizeRole(Role.CLIENT, Role.USERMANAGER)]
    [HttpPost("pay")]
    public async Task<ActionResult<SimpleResponse>> PayDepositAccount([FromBody] PayPropertyRequest request)
    {
        var payResult = await _propertyAccountService.Pay(request.PropertyId, request.Amount);

        return payResult.MatchFirst
        (
            payResult => Ok(new SimpleResponse(payResult)),
            firstError => Problem(statusCode: StatusCodes.Status409Conflict, title: firstError.Description)
        );
    }

    [AuthorizeRole(Role.CLIENT, Role.USERMANAGER)]
    [HttpPut("update")]
    public async Task<ActionResult<PropertyAccountResponse>> UpdatePropertyAccount([FromQuery] Guid id, [FromBody] UpdatePropertyAccountRequest request)
    {
        var propertyUpdateResult = await _propertyAccountService.UpdatePropertyAccount(id, request);

        return propertyUpdateResult.MatchFirst(
           propertyUpdate => Ok(_mapper.Map<IEnumerable<PropertyAccountResponse>>(propertyUpdate)),
           firstError => Problem(statusCode: StatusCodes.Status409Conflict, title: firstError.Description)
       );
    }

    [AuthorizeRole(Role.CLIENT, Role.USERMANAGER)]
    [HttpDelete("delete")]
    public async Task<ActionResult<SimpleResponse>> DeletePropertyAccount([FromQuery] Guid id)
    {
        var propertyUpdateResult = await _propertyAccountService.DeletePropertyAccount(id);

        return propertyUpdateResult.MatchFirst(
            propertyUpdate => Ok(new SimpleResponse("Foi removido o ativo movél")),
            firstError => Problem(statusCode: StatusCodes.Status409Conflict, title: firstError.Description)
        );
    }

}