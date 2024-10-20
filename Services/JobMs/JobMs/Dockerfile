FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 18085

ENV ASPNETCORE_URLS=http://+:18085

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["JobMs/JobMs/JobMs.csproj", "JobMs/JobMs/"]
RUN dotnet restore "JobMs/JobMs/JobMs.csproj"
COPY . .
WORKDIR "/src/JobMs/JobMs"
RUN dotnet build "JobMs.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "JobMs.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JobMs.dll"]
