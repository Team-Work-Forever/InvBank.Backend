using AutoMapper;
using InvBank.Backend.Application.Actives.Deposit.CreateDeposit;
using InvBank.Backend.Application.Services;
using InvBank.Backend.Contracts;
using InvBank.Backend.Contracts.Deposit;
using InvBank.Backend.Contracts.Payment;
using InvBank.Backend.Infrastructure.Authentication;
using InvBank.Backend.Infrastructure.Providers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InvBank.Backend.API.Controllers;

[ApiController]
[Route("deposits")]
public class DepositController : BaseController
{
    private readonly IMapper _mapper;
    public readonly ISender _mediator;
    public readonly DepositAccountService _accountService;

    public DepositController(
        ISender mediator,
        DepositAccountService accountService,
        IMapper mapper)
    {
        _mediator = mediator;
        _accountService = accountService;
        _mapper = mapper;
    }

    [AuthorizeRole(Role.USERMANAGER)]
    [HttpPost("create")]
    public async Task<ActionResult<SimpleResponse>> RegisterDepositAccount([FromBody] CreateDepositRequest request)
    {
        var result = await _mediator.Send(_mapper.Map<CreateDepositCommand>(request));

        return result.Match(
            result => Ok(new SimpleResponse(result)),
            firstError => Problem<SimpleResponse>(firstError)
        );
    }

    [AuthorizeRole(Role.CLIENT)]
    [HttpPost("set/money")]
    public async Task<ActionResult<SimpleResponse>> SetDepositValue([FromQuery] string depositId, [FromBody] DepositValueRequest request)
    {
        var payResult = await _accountService.SetDepositValue(depositId, request);

        return payResult.Match
        (
            payResult => Ok(new SimpleResponse(payResult)),
            firstError => Problem<SimpleResponse>(firstError)
        );
    }

    [AuthorizeRole(Role.CLIENT)]
    [HttpPost("get/money")]
    public async Task<ActionResult<SimpleResponse>> GetDepositValue([FromQuery] string depositId, [FromBody] DepositValueRequest request)
    {
        var payResult = await _accountService.GetDepositValue(depositId, request);

        return payResult.Match
        (
            payResult => Ok(new SimpleResponse(payResult)),
            firstError => Problem<SimpleResponse>(firstError)
        );
    }

    [AuthorizeRole(Role.CLIENT, Role.USERMANAGER)]
    [HttpPost("pay")]
    public async Task<ActionResult<SimpleResponse>> PayDepositAccount([FromBody] PayDepositRequest request)
    {
        var payResult = await _accountService.Pay(request.DepositId, request.Amount);

        return payResult.Match
        (
            payResult => Ok(new SimpleResponse(payResult)),
            firstError => Problem<SimpleResponse>(firstError)
        );
    }

    [AuthorizeRole(Role.CLIENT, Role.USERMANAGER)]
    [HttpGet()]
    public async Task<ActionResult<DepositResponse>> GetDeposit([FromQuery] string depositId)
    {
        var depositResult = await _accountService.GetDeposit(depositId);

        return depositResult.Match(
            depositsResult => Ok(_mapper.Map<DepositResponse>(depositsResult)),
            firstError => Problem<DepositResponse>(firstError)
        );
    }

    [AuthorizeRole(Role.CLIENT, Role.USERMANAGER)]
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<DepositResponse>>> GetAllDeposits([FromQuery] string accountIban)
    {
        var depositsResult = await _accountService.GetDepositAccounts(accountIban);

        return depositsResult.Match(
            depositsResult => Ok(_mapper.Map<IEnumerable<DepositResponse>>(depositsResult)),
            firstError => Problem<IEnumerable<DepositResponse>>(firstError)
        );
    }

    [AuthorizeRole(Role.USERMANAGER)]
    [HttpPut("update")]
    public async Task<ActionResult<SimpleResponse>> UpdateDeposit([FromQuery] string depositIban, [FromBody] UpdateDepositRequest request)
    {
        var updateResult = await _accountService.UpdateDeposit(depositIban, request);

        return updateResult.Match(
            updateResult => Ok(new SimpleResponse("Deposito Atualizado!")),
            firstError => Problem<SimpleResponse>(firstError)
        );
    }

    [AuthorizeRole(Role.USERMANAGER)]
    [HttpDelete("delete")]
    public async Task<ActionResult<SimpleResponse>> DeleteDeposit([FromQuery] string depositIban)
    {
        var deleteResult = await _accountService.DeleteDeposit(depositIban);

        return deleteResult.Match(
            updateResult => Ok(new SimpleResponse("Deposito removido!")),
            firstError => Problem<SimpleResponse>(firstError)
        );
    }

}