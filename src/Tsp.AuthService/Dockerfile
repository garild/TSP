FROM mcr.microsoft.com/dotnet/core/runtime:3.0

ENV ASPNETCORE_URLS="http://+:5010"

COPY ./ /app
WORKDIR /app

EXPOSE 5180
ENTRYPOINT ["dotnet","Tsp.AuthService.dll"]