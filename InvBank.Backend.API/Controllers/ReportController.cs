using InvBank.Backend.Application.Common.Providers;
using InvBank.Backend.Application.Services;
using InvBank.Backend.Contracts.Report;
using Microsoft.AspNetCore.Mvc;

namespace InvBank.Backend.API.Controllers;

[ApiController]
[Route("report")]
public class ReportController : ControllerBase
{
    private readonly IDateFormatter _dateFormatter;

    private readonly ReportService _reportService;

    public ReportController(IDateFormatter dateFormatter, ReportService reportService)
    {
        _dateFormatter = dateFormatter;
        _reportService = reportService;
    }

    [HttpGet("profit")]
    public async Task<ActionResult<string>> GenerateReportProfit()
    {
        ProfitReportResponse profitReportResponse = await _reportService.GenerateReportProfit(
             new CreateProfitReportRequest(
                 _dateFormatter.ConvertToDateTime("15/06/2023"),
                 _dateFormatter.ConvertToDateTime("19/06/2023")
             ));

        return Ok(profitReportResponse);
    }

}