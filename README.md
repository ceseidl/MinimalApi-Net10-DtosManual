# ProductApi (.NET 10 Minimal API)

API minimalista de produtos com **mapeamento manual de DTOs**, **EF Core (SQLite)**, **FluentValidation**, **Swagger**, **Health Checks**, **testes xUnit** e **Docker**.

## Requisitos
- .NET SDK 10.0+
- Docker (opcional)

## Executando localmente
```bash
dotnet restore
cd src/ProductApi
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```
A API iniciará em `http://localhost:5000` (ou porta configurada). Acesse `http://localhost:5000/swagger` para explorar.

## Executando com Docker
```bash
# Build
docker build -t productapi:latest -f Dockerfile .
# Run
docker run -p 8080:8080 -e ConnectionStrings__Default=Data Source=/data/app.db -v $(pwd)/data:/data productapi:latest
```

## Testes
```bash
dotnet test
```
