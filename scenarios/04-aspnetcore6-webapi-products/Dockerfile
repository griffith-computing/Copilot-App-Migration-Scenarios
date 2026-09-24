# Legacy .NET 6 Dockerfile -- pins EOL base image tags. A .NET 10 migration
# needs to bump both stages (and re-check any OS package/user setup that
# changed between Debian releases underlying the base images).
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ProductsApi/ProductsApi.csproj ProductsApi/
RUN dotnet restore ProductsApi/ProductsApi.csproj
COPY ProductsApi/ ProductsApi/
WORKDIR /src/ProductsApi
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ProductsApi.dll"]
