using InvBank.Backend.Infrastructure;
using InvBank.Backend.Application;
using InvBank.Backend.API.Mappers;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using InvBank.Backend.Application.Validators;

var builder = WebApplication.CreateBuilder(args);
{

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {

        options.AddSecurityDefinition(
            "AccessToken",
            new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer",
                In = ParameterLocation.Header,
                Name = HeaderNames.Authorization
            });

        options.AddSecurityRequirement(
            new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "AccessToken"
                        },
                    },
                    Array.Empty<string>()
                }
            }
        );

    });

    builder.Services
        .AddApplication()
        .AddInfraStructure()
        .AddMapper()
        .AddValidators()
        .AddWorkers()
        .AddHttpContextAccessor();

    builder.Services.AddAuth(builder.Configuration);

    builder.Services.AddCors();

};

var app = builder.Build();
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1");
    });

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.UseExceptionHandler("/error");

    app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

    await app.RunAsync();
}

