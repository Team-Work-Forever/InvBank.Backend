using InvBank.Backend.Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InvBank.Backend.Infrastructure.Workers;

public class TaxWorker : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<TaxWorker> _logger;

    public TaxWorker(
        IServiceProvider serviceProvider,
        ILogger<TaxWorker> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(async () =>
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var depositRepository = scope.ServiceProvider.GetRequiredService<IDepositRepository>();
                    var fundRepository = scope.ServiceProvider.GetRequiredService<IFundRepository>();

                    var deposits = await depositRepository.GetAllDeposits();
                    var funds = await fundRepository.GetFunds();

                    foreach (var deposit in deposits)
                    {
                        if (IsOneMonthBeenPassed(deposit.InitialDate.ToDateTime(TimeOnly.MaxValue), deposit.Duration))
                        {
                            if (decimal.IsNegative(deposit.DepositValue))
                            {
                                deposit.DepositValue /= 1 + (deposit.TaxPercent / 100);
                            } else {
                                deposit.DepositValue *= 1 + (deposit.TaxPercent / 100);
                            }
                            
                            await depositRepository.UpdateDeposit(deposit);
                        }
                    }

                    foreach (var fund in funds)
                    {
                        if (IsOneMonthBeenPassed(fund.InitialDate.ToDateTime(TimeOnly.MaxValue), fund.Duration))
                        {
                            if (decimal.IsNegative(fund.TaxPercent))
                            {
                                fund.InvestValue /= 1 + (fund.TaxPercent / 100);
                            } else {
                                fund.InvestValue *= 1 + (fund.TaxPercent / 100);
                            }
                            
                            await fundRepository.UpdateFund(fund);
                        }
                    }

                }

                await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
            }
        }, cancellationToken);

        return Task.CompletedTask;

    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private bool IsOneMonthBeenPassed(DateTime date, int duration)
    {
        var currentDate = DateTime.Now;
        var endDate = date.AddDays(duration);

        var result = currentDate <= date && date <= endDate;
        return result;
    }

}