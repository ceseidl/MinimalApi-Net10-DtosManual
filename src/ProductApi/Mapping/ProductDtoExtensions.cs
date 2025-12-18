using ProductApi.Domain;
using ProductApi.Dtos;

namespace ProductApi.Mapping;

public static class ProductDtoExtensions
{
    public static ProductResponse ToDto(this Product entity)
        => new(entity.Id, entity.Name, entity.Price, entity.Quantity);

    public static Product ToEntity(this CreateProductRequest dto)
        => new()
        {
            Name = dto.Name,
            Price = dto.Price,
            Quantity = dto.Quantity,
            CreatedAt = DateTime.UtcNow
        };

    public static Product ToEntity(this UpdateProductRequest dto)
        => new()
        {
            Id = dto.Id,
            Name = dto.Name,
            Price = dto.Price,
            Quantity = dto.Quantity
        };
}
