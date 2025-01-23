FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 18083

ENV DB_BUSINESS_CONNECTION_STRING="Host=gruas-ucab.postgres.database.azure.com;Port=5432;Database=providerms-dev;User Id=administrador;Password=Ae164tg=18"

ENV ASPNETCORE_URLS=http://+:18083

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["ProviderMs/ProviderMs/ProviderMs.csproj", "ProviderMs/ProviderMs/"]
RUN dotnet restore "ProviderMs/ProviderMs/ProviderMs.csproj"
COPY . .
WORKDIR "/src/ProviderMs/ProviderMs"
RUN dotnet build "ProviderMs.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "ProviderMs.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProviderMs.dll"]
