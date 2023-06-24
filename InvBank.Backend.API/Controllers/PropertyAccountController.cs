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
public class PropertyAccountController : BaseController
{
    private readonly IMapper _mapper;
    private readonly PropertyAccountService _propertyAccountService;

    public PropertyAccountController(PropertyAccountService propertyAccountService, IMapper mapper)
    {
        _propertyAccountService = propertyAccountService;
        _mapper = mapper;
    }

    [AuthorizeRole(Role.USERMANAGER, Role.ADMIN)]
    [HttpPost("create")]
    public async Task<ActionResult<SimpleResponse>> CreatePropertyAccount([FromBody] CreatePropertyAccountRequest request)
    {
        var createResult = await _propertyAccountService.CreatePropertyAccount(request);

        return createResult.Match(
            createResult => Ok(new SimpleResponse("O ativo foi registado!")),
            firstError => Problem<SimpleResponse>(firstError)
        );
    }

    [AuthorizeRole(Role.CLIENT, Role.USERMANAGER, Role.ADMIN)]
    [HttpGet()]
    public async Task<ActionResult<PropertyAccountResponse>> GetPropertyAccount([FromQuery] Guid propertyAccount)
    {
        var propertyAccountResult = await _propertyAccountService.GetPropertyAccount(propertyAccount);

        return propertyAccountResult.Match(
            propertyAccount => Ok(_mapper.Map<PropertyAccountResponse>(propertyAccount)),
            firstError => Problem<PropertyAccountResponse>(firstError)
        );

    }

    [AuthorizeRole(Role.CLIENT, Role.USERMANAGER, Role.ADMIN)]
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<PropertyAccountResponse>>> GetAllPropertyAccount([FromQuery] string iban)
    {
        var propertyAccountsResult = await _propertyAccountService.GetAllPropertyAccounts(iban);

        return propertyAccountsResult.Match(
            propertyAccounts => Ok(_mapper.Map<IEnumerable<PropertyAccountResponse>>(propertyAccounts)),
            firstError => Problem<IEnumerable<PropertyAccountResponse>>(firstError)
        );

    }

    [AuthorizeRole(Role.CLIENT, Role.USERMANAGER)]
    [HttpPost("pay")]
    public async Task<ActionResult<SimpleResponse>> PayDepositAccount([FromBody] PayPropertyRequest request)
    {
        var payResult = await _propertyAccountService.Pay(request.PropertyId, request.Amount);

        return payResult.Match
        (
            payResult => Ok(new SimpleResponse(payResult)),
            firstError => Problem<SimpleResponse>(firstError)
        );
    }

    [AuthorizeRole(Role.USERMANAGER, Role.ADMIN)]
    [HttpPut("update")]
    public async Task<ActionResult<PropertyAccountResponse>> UpdatePropertyAccount([FromQuery] Guid id, [FromBody] UpdatePropertyAccountRequest request)
    {
        var propertyUpdateResult = await _propertyAccountService.UpdatePropertyAccount(id, request);

        return propertyUpdateResult.Match(
           propertyUpdate => Ok(_mapper.Map<IEnumerable<PropertyAccountResponse>>(propertyUpdate)),
           firstError => Problem<PropertyAccountResponse>(firstError)
       );
    }

    [AuthorizeRole(Role.USERMANAGER, Role.ADMIN)]
    [HttpDelete("delete")]
    public async Task<ActionResult<SimpleResponse>> DeletePropertyAccount([FromQuery] Guid id)
    {
        var propertyUpdateResult = await _propertyAccountService.DeletePropertyAccount(id);

        return propertyUpdateResult.Match(
            propertyUpdate => Ok(new SimpleResponse("Foi removido o ativo movél")),
            firstError => Problem<SimpleResponse>(firstError)
        );
    }

}