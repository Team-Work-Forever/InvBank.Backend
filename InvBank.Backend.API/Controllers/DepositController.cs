using AutoMapper;
using InvBank.Backend.Application.Actives.Deposit.CreateDeposit;
using InvBank.Backend.Application.Services;
using InvBank.Backend.Contracts.Deposit;
using InvBank.Backend.Infrastructure.Authentication;
using InvBank.Backend.Infrastructure.Providers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvBank.Backend.API.Controllers;

[ApiController]
[Route("deposits")]
[AuthorizeRole(Role.CLIENT)]
public class DepositController : ControllerBase
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

    [HttpPost("create")]
    public async Task<ActionResult<string>> RegisterDepositAccount([FromBody] CreateDepositRequest request)
    {

        var result = await _mediator.Send(_mapper.Map<CreateDepositCommand>(request));

        return result.MatchFirst(
            result => Ok(result),
            firstError => Problem(statusCode: StatusCodes.Status409Conflict, title: firstError.Description)
        );

    }

    [HttpGet()]
    public async Task<ActionResult<dynamic>> GetDeposit([FromQuery] string depositId)
    {
        var depositResult = await _accountService.GetDeposit(depositId);

        return depositResult.MatchFirst(
            depositsResult => Ok(_mapper.Map<DepositResponse>(depositsResult)),
            firstError => Problem(statusCode: StatusCodes.Status409Conflict, title: firstError.Description)
        );
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<dynamic>>> GetAllDeposits([FromQuery] string accountIban)
    {
        var depositsResult = await _accountService.GetDepositAccounts(accountIban);

        return depositsResult.MatchFirst(
            depositsResult => Ok(_mapper.Map<IEnumerable<DepositResponse>>(depositsResult)),
            firstError => Problem(statusCode: StatusCodes.Status409Conflict, title: firstError.Description)
        );
    }

    [HttpPut("update")]
    public async Task<ActionResult<dynamic>> UpdateDeposit([FromQuery] string depositIban, [FromBody] UpdateDepositRequest request)
    {
        var updateResult = await _accountService.UpdateDeposit(depositIban, request);

        return updateResult.MatchFirst(
            updateResult => Ok("Deposito Atualizado!"),
            firstError => Problem(statusCode: StatusCodes.Status409Conflict, title: firstError.Description)
        );
    }

    [HttpDelete("delete")]
    public async Task<ActionResult<dynamic>> DeleteDeposit([FromQuery] string depositIban)
    {
        var deleteResult = await _accountService.DeleteDeposit(depositIban);

        return deleteResult.MatchFirst(
            updateResult => Ok("Deposito removido!"),
            firstError => Problem(statusCode: StatusCodes.Status409Conflict, title: firstError.Description)
        );
    }

}