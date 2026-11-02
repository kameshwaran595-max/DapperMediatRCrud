using DapperMediatRCrud.Data;
using MediatR;

namespace DapperMediatRCrud.Features.Products.Commands;

public sealed record DeleteProductCommand(long Id) : IRequest<bool>;

public sealed class DeleteProductHandler(IProductRepository repository) : IRequestHandler<DeleteProductCommand, bool>
{
    public Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        => repository.DeleteAsync(request.Id, cancellationToken);
}

public sealed record DeleteCategoryCommand(long Id) : IRequest<bool>;

public sealed class DeleteCategoryHandler(IProductRepository repository) : IRequestHandler<DeleteCategoryCommand, bool>
{
    public Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        => repository.DeleteCategoryAsync(request.Id, cancellationToken);
}