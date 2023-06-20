using AutoMapper;
using ErrorOr;
using InvBank.Backend.Application.Services;
using InvBank.Backend.Contracts;
using InvBank.Backend.Contracts.Account;
using InvBank.Backend.Infrastructure.Authentication;
using InvBank.Backend.Infrastructure.Providers;
using Microsoft.AspNetCore.Mvc;

namespace InvBank.Backend.API.Controllers;

[ApiController]
[Route("accounts")]
public class AccountController : BaseController
{
    private readonly IMapper _mapper;
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService, IMapper mapper)
    {
        _accountService = accountService;
        _mapper = mapper;
    }

    [AuthorizeRole(Role.CLIENT)]
    [HttpPost("create")]
    public async Task<ActionResult<AccountResponse>> CreateAccount(CreateAccountRequest request)
    {
        var createResult = await _accountService.CreateAccount(request);

        return createResult.Match(
            createResult => Ok(_mapper.Map<AccountResponse>(createResult)),
            firstError => Problem<AccountResponse>(firstError)
        );

    }

    [AuthorizeRole(Role.CLIENT, Role.USERMANAGER)]
    [HttpGet("")]
    public async Task<ActionResult<AccountResponse>> GetAccount([FromQuery] string iban)
    {
        var accountResult = await _accountService.GetAccount(iban);

        return accountResult.Match(
            accountResult => Ok(_mapper.Map<AccountResponse>(accountResult)),
            firstError => Problem<AccountResponse>(firstError)
        );

    }

    [AuthorizeRole(Role.CLIENT, Role.USERMANAGER)]
    [HttpGet("all")]
    public async Task<ActionResult<string>> GetAllAccounts()
    {
        var accountsResult = await _accountService.GetAllAccounts();

        return accountsResult.Match(
            accountsResult => Ok(_mapper.Map<IEnumerable<AccountResponse>>(accountsResult)),
            firstError => Problem<string>(firstError)
        );

    }

    [AuthorizeRole(Role.CLIENT, Role.USERMANAGER)]
    [HttpDelete("delete")]
    public async Task<ActionResult<SimpleResponse>> DeleteAccount([FromQuery] string iban)
    {
        ErrorOr<dynamic> deleteResult = await _accountService.DeleteAccount(iban);

        return deleteResult.Match(
            deleteResult => Ok(new SimpleResponse("Conta Removida!")),
            firstError => Problem<SimpleResponse>(firstError)
        );
    }

}