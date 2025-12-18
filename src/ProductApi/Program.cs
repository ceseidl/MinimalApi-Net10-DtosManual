using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation;
using FluentValidation.AspNetCore;
using ProductApi.Data;
using ProductApi.Domain;
using ProductApi.Dtos;
using ProductApi.Mapping;
using ProductApi.Repositories;
using ProductApi.Validation;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core (SQLite)
var connectionString = builder.Configuration.GetConnectionString("Default") ?? "Data Source=app.db";
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));

// Repositório
builder.Services.AddScoped<IProductRepository, EfCoreProductRepository>();

// FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductValidator>();
builder.Services.AddFluentValidationAutoValidation();

// Health checks
builder.Services.AddHealthChecks();

var app = builder.Build();

// Migrations at startup (simple)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/health");

app.MapGroup("/products").MapProductsEndpoints();

app.Run();

public partial class Program { } // Expose Program for WebApplicationFactory
