using InvBank.Backend.Application.Authentication.Commands.RegisterClient;
using InvBank.Backend.Application.Authentication.Commands.RegisterCompany;
using InvBank.Backend.Application.Authentication.Queries.Login;
using InvBank.Backend.Contracts;
using InvBank.Backend.Contracts.Authentication;
using InvBank.Backend.Domain.Entities;

namespace InvBank.Backend.API.Mappers;

public class AuthMapper : AutoMapper.Profile
{
    public AuthMapper()
    {
        CreateMap<LoginRequest, LoginQuerie>();
        CreateMap<RegisterClientRequest, RegisterClientCommand>();
        CreateMap<RegisterCompanyRequest, RegisterCompanyCommand>();
    }
}