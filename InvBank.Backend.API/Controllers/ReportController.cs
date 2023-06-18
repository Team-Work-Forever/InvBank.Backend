using AutoMapper;
using InvBank.Backend.Application.Common.Providers;
using InvBank.Backend.Application.Services;
using InvBank.Backend.Contracts.Report;
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

    [HttpPost("profit")]
    public async Task<ActionResult<string>> GenerateReportProfit()
    {
        ProfitReportResponse profitReportResponse = await _reportService.GenerateReportProfit(
             new CreateProfitReportRequest(
                 _dateFormatter.ConvertToDateTime("15/06/2023"),
                 _dateFormatter.ConvertToDateTime("19/06/2023")
             ));

        return Ok(profitReportResponse);
    }

    [HttpPost("pay")]
    public async Task<ActionResult<PayReportResponse>> GenerateReportPay([FromQuery] string iban, CreatePayReportRequest request)
    {
        var payReportResult = await _reportService.GenerateReportPay(
             new CreatePayReportCommand(
                _dateFormatter.ConvertToDateTime("15/06/2023"),
                _dateFormatter.ConvertToDateTime("19/06/2023"),
                iban
             ));

        return payReportResult.MatchFirst(
            payReportResult => Ok(_mapper.Map<PayReportResponse>(payReportResult)),
            firstError => Problem(statusCode: StatusCodes.Status409Conflict, title: firstError.Description)
        );
    }

    [HttpPost("banks")]
    public async Task<ActionResult<BanksReportResponse>> GenerateReportBanks(CreateBanksReportRequest request)
    {
        var bankReportResult = await _reportService.GenerateBanksReport(
             new CreateBanksReportCommand(
                _dateFormatter.ConvertToDateTime("15/06/2023"),
                _dateFormatter.ConvertToDateTime("19/06/2023")
             ));

        return bankReportResult.MatchFirst(
            bankReportResult => Ok(_mapper.Map<BanksReportResponse>(bankReportResult)),
            firstError => Problem(statusCode: StatusCodes.Status409Conflict, title: firstError.Description)
        );
    }

}