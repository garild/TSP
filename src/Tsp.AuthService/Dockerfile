FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 5010

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
COPY ["src/Tsp.AuthService/Tsp.AuthService.csproj", "src/Tsp.AuthService/"]
COPY ["infrastructure/Auth/Auth.csproj", "infrastructure/Auth/"]
COPY ["infrastructure/ElasticsearchSerilog/ElasticsearchSerilog.csproj", "infrastructure/ElasticsearchSerilog/"]
COPY ["infrastructure/ExceptionHandling/ExceptionHandling.csproj", "infrastructure/ExceptionHandling/"]
COPY ["infrastructure/HealthCheck/HealthCheck.csproj", "infrastructure/HealthCheck/"]
RUN dotnet restore "src/Tsp.AuthService/Tsp.AuthService.csproj" --force
COPY . .
WORKDIR "/src/Tsp.AuthService"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet build -v q -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tsp.AuthService.dll"]