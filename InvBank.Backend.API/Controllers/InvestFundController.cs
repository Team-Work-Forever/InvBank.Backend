using AutoMapper;
using InvBank.Backend.Application.Services;
using InvBank.Backend.Contracts.Fund;
using InvBank.Backend.Domain.Entities;
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

    [HttpPost("create")]
    public async Task<ActionResult<string>> CreateInvestFund([FromBody] CreateFundRequest request)
    {
        var fundResult = await _fundService.CreateFund(request);

        return fundResult.MatchFirst(
            fundResult => Ok("Fundo Criado!"),
            firstError => Problem(statusCode: StatusCodes.Status409Conflict, title: firstError.Description)
        );
    }

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