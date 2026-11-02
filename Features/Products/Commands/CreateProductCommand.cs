using DapperMediatRCrud.Data;
using DapperMediatRCrud.Models;
using MediatR;

namespace DapperMediatRCrud.Features.Products.Commands;

public sealed record CreateProductCommand(string Name, string? Description, decimal Price) : IRequest<long>;

public sealed class CreateProductHandler(IProductRepository repository) : IRequestHandler<CreateProductCommand, long>
{
    public async Task<long> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            CreatedAt = DateTime.Now
        };

        return await repository.CreateAsync(product,  cancellationToken);
    }
}

public sealed record CreateCategoryCommand(string Category_Name, string? Description) : IRequest<long>;

public sealed class CreateCategoryHandler(IProductRepository repository) : IRequestHandler<CreateCategoryCommand, long>
{
    public async Task<long> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Categories
        {
            Category_Name =  request.Category_Name,
            Description = request.Description,
            CreatedAt = DateTime.Now
        };
        return await repository.CreateCategoryAsync(category, cancellationToken);
    }
}
