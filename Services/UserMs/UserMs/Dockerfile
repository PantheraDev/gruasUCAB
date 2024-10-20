FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 18081

ENV ASPNETCORE_URLS=http://+:18081

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["UserMs/UserMs/UserMs.csproj", "UserMs/UserMs/"]
RUN dotnet restore "UserMs/UserMs/UserMs.csproj"
COPY . .
WORKDIR "/src/UserMs/UserMs"
RUN dotnet build "UserMs.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "UserMs.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserMs.dll"]
