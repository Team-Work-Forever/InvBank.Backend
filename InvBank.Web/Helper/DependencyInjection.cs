using Blazored.LocalStorage;
using InvBank.Web.Helper.Authentication;
using InvBank.Web.Helper.EndPoints;
using Microsoft.AspNetCore.Components.Authorization;

namespace InvBank.Web.Helper;

public static class DependencyInjection
{

    public static IServiceCollection AddAuthorization(this IServiceCollection services)
    {

        services.AddAuthorizationCore();
        services.AddBlazoredLocalStorage();
        services.AddScoped<AuthenticationStateProvider, AuthenticationProvider>();

        return services;
    }

    public static IServiceCollection AddHelper(this IServiceCollection services)
    {

        services.AddScoped<ApiHelper>();
        services.AddScoped<AuthApiHelper>();

        // Endpoints
        services.AddScoped<UserEndPoint>();
        services.AddScoped<AccountEndPoint>();
        services.AddScoped<AuthenticationEndPoint>();
        services.AddScoped<BankEndPoint>();
        services.AddScoped<DepositEndPoint>();
        services.AddScoped<FundEndPoint>();
        services.AddScoped<PropertyAccountEndPoint>();
        services.AddScoped<ReportEndPoint>();

        return services;
    }

}