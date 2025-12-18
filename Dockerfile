# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY src/ProductApi/ProductApi.csproj src/ProductApi/
COPY tests/ProductApi.Tests/ProductApi.Tests.csproj tests/ProductApi.Tests/
RUN dotnet restore src/ProductApi/ProductApi.csproj

COPY . .
RUN dotnet publish src/ProductApi/ProductApi.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
ENV ConnectionStrings__Default=Data Source=/app/app.db
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "ProductApi.dll"]
