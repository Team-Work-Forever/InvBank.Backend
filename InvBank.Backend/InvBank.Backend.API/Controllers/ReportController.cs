using AutoMapper;
using InvBank.Backend.Application.Common.Providers;
using InvBank.Backend.Application.Services;
using InvBank.Backend.Contracts.Report;
using InvBank.Backend.Infrastructure.Authentication;
using InvBank.Backend.Infrastructure.Providers;
using Microsoft.AspNetCore.Mvc;

namespace InvBank.Backend.API.Controllers;

[ApiController]
[Route("report")]
public class ReportController : BaseController
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
    public async Task<ActionResult<ProfitReportResponse>> GenerateReportProfit(CreatePayReportRequest request)
    {
        var profitReportResponse = await _reportService.GenerateReportProfit(
             new CreateProfitReportRequest(
                request.InitialDate,
                request.EndDate
             ));

        return profitReportResponse.Match(
            profitReportResponse => Ok(profitReportResponse),
            errors => Problem<ProfitReportResponse>(errors)
        );
    }

    [AuthorizeRole(Role.CLIENT, Role.USERMANAGER, Role.ADMIN)]
    [HttpPost("pay")]
    public async Task<ActionResult<PayReportResponse>> GenerateReportPay([FromQuery] string iban, CreatePayReportRequest request)
    {
        var payReportResult = await _reportService.GenerateReportPay(
             new CreatePayReportCommand(
                request.InitialDate,
                request.EndDate,
                iban
             ));

        return payReportResult.Match(
            payReportResult => Ok(_mapper.Map<PayReportResponse>(payReportResult)),
            firstError => Problem<PayReportResponse>(firstError)
        );
    }

}