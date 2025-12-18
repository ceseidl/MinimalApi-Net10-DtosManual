using Microsoft.AspNetCore.Http;
using ProductApi.Dtos;
using ProductApi.Mapping;
using ProductApi.Repositories;
using ProductApi.Validation;

namespace ProductApi.Endpoints;

public static class ProductEndpoints
{
    public static RouteGroupBuilder MapProductsEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IProductRepository repo) =>
        {
            var products = await repo.GetAllAsync();
            var response = products.Select(p => p.ToDto());
            return Results.Ok(response);
        });

        group.MapGet("/{id:guid}", async (Guid id, IProductRepository repo) =>
        {
            var product = await repo.GetByIdAsync(id);
            return product is null
                ? Results.NotFound(new { message = "Produto não encontrado." })
                : Results.Ok(product.ToDto());
        }).WithName("GetProductById");

        group.MapPost("/", async (CreateProductRequest request, IProductRepository repo) =>
        {
            var entity = request.ToEntity();
            var created = await repo.CreateAsync(entity);
            return Results.CreatedAtRoute("GetProductById", new { id = created.Id }, created.ToDto());
        }).AddEndpointFilter<ValidationFilter<CreateProductRequest>>();

        group.MapPut("/{id:guid}", async (Guid id, UpdateProductRequest request, IProductRepository repo) =>
        {
            if (id != request.Id)
                return Results.BadRequest(new { message = "O ID da rota e o ID do corpo devem ser iguais." });

            var exists = await repo.ExistsAsync(id);
            if (!exists)
                return Results.NotFound(new { message = "Produto não encontrado." });

            var entity = request.ToEntity();
            await repo.UpdateAsync(entity);
            return Results.NoContent();
        }).AddEndpointFilter<ValidationFilter<UpdateProductRequest>>();

        group.MapDelete("/{id:guid}", async (Guid id, IProductRepository repo) =>
        {
            var exists = await repo.ExistsAsync(id);
            if (!exists)
                return Results.NotFound(new { message = "Produto não encontrado." });

            await repo.DeleteAsync(id);
            return Results.NoContent();
        });

        return group;
    }
}
