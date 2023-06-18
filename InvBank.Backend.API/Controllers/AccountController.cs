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
public class AccountController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService, IMapper mapper)
    {
        _accountService = accountService;
        _mapper = mapper;
    }

    [AuthorizeRole(Role.CLIENT, Role.USERMANAGER, Role.ADMIN)]
    [HttpPost("create")]
    public async Task<ActionResult<string>> CreateAccount(CreateAccountRequest request)
    {
        var createResult = await _accountService.CreateAccount(request);

        return createResult.MatchFirst(
            createResult => Ok("Conta criada!"),
            firstError => Problem(statusCode: StatusCodes.Status409Conflict, title: firstError.Description)
        );

    }

    [HttpGet()]
    public async Task<ActionResult<AccountResponse>> GetAccount([FromQuery] string iban)
    {
        var accountResult = await _accountService.GetAccount(iban);

        return accountResult.MatchFirst(
            accountResult => Ok(_mapper.Map<AccountResponse>(accountResult)),
            firstError => Problem(statusCode: StatusCodes.Status409Conflict, title: firstError.Description)
        );

    }

    [HttpGet("all")]
    public async Task<ActionResult<string>> GetAllAccounts()
    {
        var accounts = await _accountService.GetAllAccounts();
        return Ok(_mapper.Map<IEnumerable<AccountResponse>>(accounts));
    }

    [HttpDelete("delete")]
    public async Task<ActionResult<SimpleResponse>> DeleteAccount([FromQuery] string iban)
    {
        ErrorOr<dynamic> deleteResult = await _accountService.DeleteAccount(iban);

        return deleteResult.Match(
            deleteResult => Ok(new SimpleResponse("Conta Removida!")),
            firstError => Problem()
        );

    }

}