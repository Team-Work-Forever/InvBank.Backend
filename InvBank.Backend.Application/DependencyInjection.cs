using System.Reflection;
using InvBank.Backend.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace InvBank.Backend.Application;
public static class DependencyInjection
{

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        services.AddScoped<AccountService>();
        services.AddScoped<DepositAccountService>();
        services.AddScoped<DepositAccountService>();
        services.AddScoped<PropertyAccountService>();
        services.AddScoped<FundService>();
        services.AddScoped<ReportService>();
        services.AddScoped<BankService>();

        return services;

    }

}
