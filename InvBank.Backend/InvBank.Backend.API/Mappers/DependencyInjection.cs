namespace InvBank.Backend.API.Mappers;

public static class DependencyInjection
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {

        services.AddAutoMapper(typeof(DependencyInjection));

        return services;
    }
}