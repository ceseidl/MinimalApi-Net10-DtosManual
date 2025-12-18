using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

// IMPORTANTE: este using precisa bater com o namespace do arquivo dos endpoints.
using ProductApi.Endpoints;

// Se quiser usar DTOs/mapeamento em outras partes:
using ProductApi.Domain;
using ProductApi.Dtos;
using ProductApi.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Swagger + health checks (opcional, mas útil)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Health checks endpoint (opcional)
app.MapHealthChecks("/health");

// Aqui chamamos o método de extensão definido em ProductEndpoints.
// Se o using/namespace não bater, dá o erro que você viu.
app.MapGroup("/products").MapProductsEndpoints();

app.Run();

// Necessário para testes de integração com WebApplicationFactory
public partial class Program { }
``
