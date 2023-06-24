using AutoMapper;
using InvBank.Backend.Application.Services;
using InvBank.Backend.Contracts;
using InvBank.Backend.Contracts.Account;
using InvBank.Backend.Contracts.Fund;
using InvBank.Backend.Infrastructure.Authentication;
using InvBank.Backend.Infrastructure.Providers;
using Microsoft.AspNetCore.Mvc;

namespace InvBank.Backend.API.Controllers;

[ApiController]
[Route("funds")]
public class InvestFundController : BaseController
{
    private readonly IMapper _mapper;
    private readonly FundService _fundService;

    public InvestFundController(FundService fundService, IMapper mapper)
    {
        _fundService = fundService;
        _mapper = mapper;
    }

    [AuthorizeRole(Role.CLIENT ,Role.USERMANAGER, Role.ADMIN)]
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<FundResponse>>> GetAllFunds()
    {
        var fundResult = await _fundService.GetAllFunds();

        return fundResult.Match(
            fundResult => Ok(_mapper.Map<IEnumerable<FundResponse>>(fundResult)),
            firstError => Problem<IEnumerable<FundResponse>>(firstError)
        );
    }

    [AuthorizeRole(Role.CLIENT ,Role.USERMANAGER, Role.ADMIN)]
    [HttpPost("associate")]
    public async Task<ActionResult<FundResponse>> AssociateAccountToFund(AssociateAccountToFundRequest request)
    {
        var fundResult = await _fundService.AssociateAccountToFund(request);

        return fundResult.Match(
            fundResult => Ok(_mapper.Map<FundResponse>(fundResult)),
            firstError => Problem<FundResponse>(firstError)
        );
    }

    [AuthorizeRole(Role.CLIENT ,Role.USERMANAGER, Role.ADMIN)]
    [HttpPost("invest")]
    public async Task<ActionResult<SimpleResponse>> InvestOnFund([FromQuery] string iban, [FromQuery] Guid fundId, [FromBody] MakeTransferRequest request)
    {
        var fundResult = await _fundService.InvestOnFund(iban, fundId, request);

        return fundResult.Match(
            fundResult => Ok(new SimpleResponse(fundResult)),
            firstError => Problem<SimpleResponse>(firstError)
        );
    }

    [AuthorizeRole(Role.CLIENT ,Role.USERMANAGER, Role.ADMIN)]
    [HttpGet()]
    public async Task<ActionResult<FundResponse>> GetFund([FromQuery] Guid fundId)
    {
        var fundResult = await _fundService.GetFund(fundId);

        return fundResult.Match(
            fundResult => Ok(_mapper.Map<FundResponse>(fundResult)),
            firstError => Problem<FundResponse>(firstError)
        );
    }

    [AuthorizeRole(Role.USERMANAGER, Role.ADMIN)]
    [HttpPost("create")]
    public async Task<ActionResult<SimpleResponse>> CreateInvestFund([FromBody] CreateFundRequest request)
    {
        var fundResult = await _fundService.CreateFund(request);

        return fundResult.Match(
            fundResult => Ok(new SimpleResponse("Fundo Criado!")),
            firstError => Problem<SimpleResponse>(firstError)
        );
    }

    [AuthorizeRole(Role.USERMANAGER, Role.CLIENT, Role.ADMIN)]
    [HttpGet("account")]
    public async Task<ActionResult<IEnumerable<FundResponse>>> GetInvestFundOfAccount([FromQuery] string iban)
    {
        var fundsResult = await _fundService.GetFundsOfAccount(iban);

        return fundsResult.Match(
            fundsResult => Ok(_mapper.Map<IEnumerable<FundResponse>>(fundsResult)),
            firstError => Problem<IEnumerable<FundResponse>>(firstError)
        );
    }

    [AuthorizeRole(Role.USERMANAGER, Role.ADMIN)]
    [HttpPut("update")]
    public async Task<ActionResult<FundResponse>> UpdateInvestmentFund([FromQuery] Guid fundId, [FromBody] UpdateFundRequest request)
    {
        var updateResult = await _fundService.updateFund(fundId, request);

        return updateResult.Match(
            updateResult => Ok(_mapper.Map<FundResponse>(updateResult)),
            firstError => Problem<FundResponse>(firstError)
        );
    }

    [AuthorizeRole(Role.USERMANAGER, Role.ADMIN)]
    [HttpDelete("delete")]
    public async Task<ActionResult<SimpleResponse>> DeleteInvestmentFund([FromQuery] Guid fundId)
    {
        var deleteResult = await _fundService.deleteFund(fundId);

        return deleteResult.Match(
            deleteResult => Ok(new SimpleResponse(deleteResult)),
            firstError => Problem<SimpleResponse>(firstError)
        );
    }

}