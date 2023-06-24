using InvBank.Web.Provider.Authorization;

namespace InvBank.Web.Provider;

public static class DependencyInjection
{
    public static IServiceCollection AddProviders(this IServiceCollection services)
    {
        services.AddScoped<IJWTModifier, JwtModifier>();

        return services;
    }
}