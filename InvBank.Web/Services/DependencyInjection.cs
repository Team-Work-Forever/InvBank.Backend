namespace InvBank.Service;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<AuthenticationService>();

        return services;
    }
}