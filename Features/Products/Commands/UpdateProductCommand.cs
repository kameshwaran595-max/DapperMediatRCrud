using DapperMediatRCrud.Data;
using DapperMediatRCrud.Models;
using MediatR;

namespace DapperMediatRCrud.Features.Products.Commands;

public sealed record UpdateProductCommand(long Id, string Name, string? Description, decimal Price) : IRequest<bool>;

public sealed class UpdateProductHandler(IProductRepository repository) : IRequestHandler<UpdateProductCommand, bool>
{
    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price
        };

        return await repository.UpdateAsync(product, cancellationToken);
    }

    public sealed record UpdateCategoryCommand(long Id, string Category_Name, string? Description) : IRequest<bool>;

    public sealed class UpdateCategoryHandler(IProductRepository repository) : IRequestHandler<UpdateCategoryCommand, bool>
    {
        public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Categories
            {
                Id = request.Id,
                Category_Name = request.Category_Name,
                Description = request.Description
            };
            return await repository.UpdateCategoryAsync(category, cancellationToken);
        }
    }
}
