using DapperMediatRCrud.Data;
using DapperMediatRCrud.Models;
using MediatR;

namespace DapperMediatRCrud.Features.Products.Queries;

public sealed record GetAllProductsQuery : IRequest<IEnumerable<Product>>;

public sealed class GetAllProductsHandler(IProductRepository repository) : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
{
    public Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        => repository.GetAllAsync(cancellationToken);
}

public sealed record GetAllCategoriesQuery : IRequest<IEnumerable<Categories>>;

public sealed class GetAllCategoriesHandler(IProductRepository repository) : IRequestHandler<GetAllCategoriesQuery, IEnumerable<Categories>>
{
    public Task<IEnumerable<Categories>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        => repository.GetAllCategoriesAsync(cancellationToken);
}
