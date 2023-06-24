using AutoMapper;
using InvBank.Backend.Application.Services;
using InvBank.Backend.Contracts;
using InvBank.Backend.Contracts.Bank;
using InvBank.Backend.Infrastructure.Authentication;
using InvBank.Backend.Infrastructure.Providers;
using Microsoft.AspNetCore.Mvc;

namespace InvBank.Backend.API.Controllers;

[ApiController]
[Route("banks")]
public class BankController : BaseController
{
    private readonly IMapper _mapper;
    private readonly BankService _bankService;

    public BankController(BankService bankService, IMapper mapper)
    {
        _bankService = bankService;
        _mapper = mapper;
    }

    [AuthorizeRole(Role.ADMIN)]
    [HttpPost("create")]
    public async Task<ActionResult<SimpleResponse>> CreateBank([FromBody] RegisterBankRequest request)
    {
        var result = await _bankService.CreateBank(request);

        return result.Match(
            result => Ok(new SimpleResponse(result)),
            errors => Problem<SimpleResponse>(errors)
        );

    }

    [AuthorizeRole(Role.CLIENT, Role.USERMANAGER, Role.ADMIN)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BankResponse>>> GetBanks()
    {
        return Ok(_mapper.Map<IEnumerable<BankResponse>>(await _bankService.GetAllBanks()));
    }

}