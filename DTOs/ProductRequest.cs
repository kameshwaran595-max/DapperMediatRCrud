namespace DapperMediatRCrud.DTOs;

public sealed record ProductRequest(string Name, string? Description, decimal Price);
public sealed record CategoryRequest(string Name, string? Description, decimal Price, int CategoryId, DateTime CreatedAt);