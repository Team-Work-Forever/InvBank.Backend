using AutoMapper;
using InvBank.Backend.Application.Services;
using InvBank.Backend.Contracts;
using InvBank.Backend.Contracts.Fund;
using InvBank.Backend.Domain.Entities;
using InvBank.Backend.Infrastructure.Authentication;
using InvBank.Backend.Infrastructure.Providers;
using Microsoft.AspNetCore.Mvc;

namespace InvBank.Backend.API.Controllers;

[ApiController]
[Route("funds")]
public class InvestFundController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly FundService _fundService;

    public InvestFundController(FundService fundService, IMapper mapper)
    {
        _fundService = fundService;
        _mapper = mapper;
    }

    [AuthorizeRole(Role.CLIENT, Role.USERMANAGER)]
    [HttpPost("create")]
    public async Task<ActionResult<SimpleResponse>> CreateInvestFund([FromBody] CreateFundRequest request)
    {
        var fundResult = await _fundService.CreateFund(request);

        return fundResult.MatchFirst(
            fundResult => Ok(new SimpleResponse("Fundo Criado!")),
            firstError => Problem(statusCode: StatusCodes.Status409Conflict, title: firstError.Description)
        );
    }

    [AuthorizeRole(Role.CLIENT, Role.USERMANAGER)]
    [HttpGet()]
    public async Task<ActionResult<IEnumerable<FundResponse>>> GetInvestFundOfAccount([FromQuery] string iban)
    {
        var fundsResult = await _fundService.GetFundsOfAccount(iban);

        return fundsResult.MatchFirst(
            fundsResult => Ok(_mapper.Map<IEnumerable<FundResponse>>(fundsResult)),
            firstError => Problem(statusCode: StatusCodes.Status409Conflict, title: firstError.Description)
        );
    }

}