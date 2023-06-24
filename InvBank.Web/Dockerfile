FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["InvBank.Web.csproj", "."]
RUN dotnet restore "./InvBank.Web.csproj"
COPY . .

WORKDIR "/src/."
RUN dotnet build "InvBank.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InvBank.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "wwwroot/_framework/InvBank.Web.dll" ]
