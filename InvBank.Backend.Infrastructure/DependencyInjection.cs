using Microsoft.Extensions.DependencyInjection;
using InvBank.Backend.Application.Common.Interfaces;
using InvBank.Backend.Infrastructure.Persistence;
using InvBank.Backend.Application.Common.Providers;
using InvBank.Backend.Infrastructure.Providers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using InvBank.Backend.Infrastructure.Config;
using Microsoft.Extensions.Options;
using InvBank.Backend.Infrastructure.Authentication;

namespace InvBank.Backend.Infrastructure;

public static class DependencyInjection
{

    public static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configuration)
    {

        var jwtConfiguration = new JwtConfiguration();
        configuration.Bind(JwtConfiguration.SectionName, jwtConfiguration);

        services.AddSingleton(Options.Create(jwtConfiguration));
        services.AddSingleton<IJWTModifier, JWTModifier>();
        services.AddSingleton<IPasswordEncoder, PasswordEncoder>();

        services.AddScoped<IAuthorizationFacade, AuthorizationFacade>();

        services.AddAuthentication(
            options => options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtConfiguration.Issuer,
                ValidAudience = jwtConfiguration.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Secret))
            };
        });

        return services;

    }

    public static IServiceCollection AddInfraStructure(this IServiceCollection services)
    {

        // Services
        services.AddSingleton<IDateFormatter, DateTimeFormatter>();
        services.AddSingleton<IIBANGenerator, IBANGenerator>();

        // Repositories
        services.AddDbContext<InvBankDbContext>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IFundRepository, FundRepository>();
        services.AddScoped<IDepositRepository, DepositRepository>();
        services.AddScoped<IBankRepository, BankRepository>();
        services.AddScoped<IPropertyAccountRepository, PropertyAccountRepository>();

        return services;

    }

}
