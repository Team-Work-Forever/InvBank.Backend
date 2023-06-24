using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace InvBank.Backend.Application.Validators;

public static class DependencyInjection
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }
}