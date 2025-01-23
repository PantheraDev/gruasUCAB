FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 18084

ENV ASPNETCORE_URLS=http://+:18084

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["AuthenticationMs/AuthenticationMs/AuthenticationMs.csproj", "AuthenticationMs/AuthenticationMs/"]
RUN dotnet restore "AuthenticationMs/AuthenticationMs/AuthenticationMs.csproj"
COPY . .
WORKDIR "/src/AuthenticationMs/AuthenticationMs"
RUN dotnet build "AuthenticationMs.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "AuthenticationMs.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthenticationMs.dll"]
