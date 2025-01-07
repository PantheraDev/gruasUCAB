FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 18082

ENV DB_BUSINESS_CONNECTION_STRING="Host=gruas-ucab.postgres.database.azure.com;Port=5432;Database=orderms-dev;User Id=administrador;Password=Ae164tg=18"

ENV ASPNETCORE_URLS=http://+:18082

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["OrderMs/OrderMs/OrderMs.csproj", "OrderMs/OrderMs/"]
RUN dotnet restore "OrderMs/OrderMs/OrderMs.csproj"
COPY . .
WORKDIR "/src/OrderMs/OrderMs"
RUN dotnet build "OrderMs.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "OrderMs.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderMs.dll"]
