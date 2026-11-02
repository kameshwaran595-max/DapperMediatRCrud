using DapperMediatRCrud.Models;

namespace DapperMediatRCrud.Data;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken);
    Task<Product?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<long> CreateAsync(Product product, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Product product, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(long id, CancellationToken cancellationToken);
    Task<IEnumerable<Categories>> GetAllCategoriesAsync(CancellationToken cancellationToken);
    Task<long> CreateCategoryAsync(Categories category, CancellationToken cancellationToken);
    Task<bool> UpdateCategoryAsync(Categories category, CancellationToken cancellationToken);
    Task<bool> DeleteCategoryAsync(long id, CancellationToken cancellationToken);
}
