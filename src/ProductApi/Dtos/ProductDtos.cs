namespace ProductApi.Dtos;

public sealed record CreateProductRequest(string Name, decimal Price, int Quantity);
public sealed record UpdateProductRequest(Guid Id, string Name, decimal Price, int Quantity);
public sealed record ProductResponse(Guid Id, string Name, decimal Price, int Quantity);
