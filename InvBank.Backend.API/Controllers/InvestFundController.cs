using AutoMapper;
using InvBank.Backend.Application.Services;
using InvBank.Backend.Contracts.Fund;
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

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<dynamic>>> GetAllInvestFund([FromQuery] string accountIban)
    {
        var fundResult = await _fundService.GetFundsOfAccount(accountIban);

        return fundResult.MatchFirst(
            fundResult => Ok(_mapper.Map<IEnumerable<FundResponse>>(fundResult)),
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

    [HttpPut("update")]
    public async Task<ActionResult<dynamic>> UpdateInvFund([FromQuery] Guid id, [FromBody] UpdateFundRequest request)
    {
        var fundUpdateResult = await _fundService.UpdateFundAccount(id, request);

        return fundUpdateResult.MatchFirst(
           fundUpdate => Ok(_mapper.Map<IEnumerable<FundResponse>>(fundUpdate)),
           firstError => Problem(statusCode: StatusCodes.Status409Conflict, title: firstError.Description)
       );
    }

    [HttpDelete("delete")]
    public async Task<ActionResult<string>> DeleteFundAccount([FromQuery] Guid id)
    {
        var fundUpdateResult = await _fundService.DeleteFundAccount(id);

        return fundUpdateResult.MatchFirst(
            fundUpdate => Ok("Foi removido o ativo fundo de investimento!"),
            firstError => Problem(statusCode: StatusCodes.Status409Conflict, title: firstError.Description)
        );
    }

}