FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 5010
ENV ASPNETCORE_URLS="http://+:5010"

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
COPY ["src/Tsp.AuthService/Tsp.AuthService.csproj", "src/Tsp.AuthService/"]
COPY ["src/infrastructure/Auth/Auth.csproj", "src/infrastructure/Auth/"]
COPY ["src/infrastructure/Swagger/Swagger.csproj", "src/infrastructure/Swagger/"]
COPY ["src/infrastructure/ElasticsearchSerilog/ElasticsearchSerilog.csproj", "src/infrastructure/ElasticsearchSerilog/"]
COPY ["src/infrastructure/ExceptionHandling/ExceptionHandling.csproj", "src/infrastructure/ExceptionHandling/"]
COPY ["src/infrastructure/HealthCheck/HealthCheck.csproj", "src/infrastructure/HealthCheck/"]
RUN dotnet restore src/Tsp.AuthService/Tsp.AuthService.csproj
COPY . .
WORKDIR /src/Tsp.AuthService
RUN dotnet build "Tsp.AuthService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet build -v q "Tsp.AuthService.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tsp.AuthService.dll"]