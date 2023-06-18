using FluentValidation;
using InvBank.Backend.Contracts.Account;
using Microsoft.Extensions.DependencyInjection;

namespace InvBank.Backend.Application.Validators;

public static class DependencyInjection
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateAccountRequest>, CreateAccountRequestValidator>();

        services.AddValidators();

        return services;
    }
}