FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5010
ENV ASPNETCORE_URLS="http://+:5010"

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src/Tsp.AuthService
RUN dotnet restore Tsp.AuthService.csproj
RUN dotnet build -v q "Tsp.AuthService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tsp.AuthService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tsp.AuthService.dll"]