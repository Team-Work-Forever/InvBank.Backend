using AutoMapper;
using InvBank.Backend.Application.Common.Providers;
using InvBank.Backend.Application.Services;
using InvBank.Backend.Contracts.Report;
using InvBank.Backend.Domain.Entities;
using InvBank.Backend.Infrastructure.Authentication;
using InvBank.Backend.Infrastructure.Providers;
using Microsoft.AspNetCore.Mvc;

namespace InvBank.Backend.API.Controllers;

[ApiController]
[Route("report")]
public class ReportController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IDateFormatter _dateFormatter;

    private readonly ReportService _reportService;

    public ReportController(IDateFormatter dateFormatter, ReportService reportService, IMapper mapper)
    {
        _dateFormatter = dateFormatter;
        _reportService = reportService;
        _mapper = mapper;
    }

    [AuthorizeRole(Role.CLIENT, Role.USERMANAGER, Role.ADMIN)]
    [HttpPost("profit")]
    public async Task<ActionResult<string>> GenerateReportProfit(CreatePayReportRequest request)
    {
        ProfitReportResponse profitReportResponse = await _reportService.GenerateReportProfit(
             new CreateProfitReportRequest(
                 _dateFormatter.ConvertToDateTime(request.InitialDate),
                 _dateFormatter.ConvertToDateTime(request.EndDate)
             ));

        return Ok(profitReportResponse);
    }

    [AuthorizeRole(Role.CLIENT, Role.USERMANAGER, Role.ADMIN)]
    [HttpPost("pay")]
    public async Task<ActionResult<PayReportResponse>> GenerateReportPay([FromQuery] string iban, CreatePayReportRequest request)
    {
        var payReportResult = await _reportService.GenerateReportPay(
             new CreatePayReportCommand(
                _dateFormatter.ConvertToDateTime(request.InitialDate),
                _dateFormatter.ConvertToDateTime(request.EndDate),
                iban
             ));

        return payReportResult.MatchFirst(
            payReportResult => Ok(_mapper.Map<PayReportResponse>(payReportResult)),
            firstError => Problem(statusCode: StatusCodes.Status409Conflict, title: firstError.Description)
        );
    }

}