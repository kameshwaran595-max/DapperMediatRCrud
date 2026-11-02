using DapperMediatRCrud.Data;
using DapperMediatRCrud.Models;
using MediatR;

namespace DapperMediatRCrud.Features.Products.Queries;

public sealed record GetProductByIdQuery(long Id) : IRequest<Product?>;

public sealed class GetProductByIdHandler(IProductRepository repository) : IRequestHandler<GetProductByIdQuery, Product?>
{
    public Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        => repository.GetByIdAsync(request.Id, cancellationToken);
}
