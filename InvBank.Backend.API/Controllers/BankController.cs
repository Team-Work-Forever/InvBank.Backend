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
public class BankController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly BankService _bankService;

    public BankController(BankService bankService, IMapper mapper)
    {
        _bankService = bankService;
        _mapper = mapper;
    }

    [AuthorizeRole(Role.ADMIN)]
    [HttpPost]
    public async Task<ActionResult<string>> CreateBank([FromBody] RegisterBankRequest request)
    {
        await _bankService.CreateBank(request);
        return Ok(new SimpleResponse("Banco registado!"));
    }

    [AuthorizeRole(Role.CLIENT, Role.USERMANAGER, Role.ADMIN)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BankResponse>>> GetBanks()
    {
        return Ok(_mapper.Map<IEnumerable<BankResponse>>(await _bankService.GetAllBanks()));
    }

}