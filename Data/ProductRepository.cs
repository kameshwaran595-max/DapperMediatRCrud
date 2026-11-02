using Dapper;
using DapperMediatRCrud.Models;
using Npgsql;

namespace DapperMediatRCrud.Data;

public sealed class ProductRepository(NpgsqlConnection connection) : IProductRepository
{
    public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
    {
        const string sql = "SELECT id AS Id, name AS Name, description AS Description, price AS Price, created_at AS CreatedAt FROM products ORDER BY id;";
        return await connection.QueryAsync<Product>(new CommandDefinition(sql, cancellationToken: cancellationToken));
    }

    public async Task<Product?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        const string sql = "SELECT id AS Id, name AS Name, description AS Description, price AS Price, created_at AS CreatedAt FROM products WHERE id = @Id;";
        return await connection.QuerySingleOrDefaultAsync<Product>(new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken));
    }

    public async Task<long> CreateAsync(Product product, CancellationToken cancellationToken)
    {
        const string sql = """
            INSERT INTO products (name, description, price)
            VALUES (@Name, @Description, @Price)
            RETURNING id;
            """;
        return await connection.ExecuteScalarAsync<long>(new CommandDefinition(sql, product, cancellationToken: cancellationToken));
    }

    public async Task<bool> UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        const string sql = """
            UPDATE products
            SET name = @Name, description = @Description, price = @Price
            WHERE id = @Id;
            """;
        var rows = await connection.ExecuteAsync(new CommandDefinition(sql, product, cancellationToken: cancellationToken));
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        const string sql = "DELETE FROM products WHERE id = @Id;";
        var rows = await connection.ExecuteAsync(new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken));
        return rows > 0;
    }

    public async Task<IEnumerable<Categories>> GetAllCategoriesAsync(CancellationToken cancellationToken)
    {
        const string sql = "SELECT id AS Id, category_name AS Category_Name, description AS Description, created_at AS CreatedAt FROM categories ORDER BY id;";
        return await connection.QueryAsync<Categories>(new CommandDefinition(sql, cancellationToken: cancellationToken));
    }
    public async Task<long> CreateCategoryAsync(Categories category, CancellationToken cancellationToken)
    {
        const string sql = """
            INSERT INTO categories (category_name, description)
            VALUES (@Category_Name, @Description)
            RETURNING id;
            """;
        return await connection.ExecuteScalarAsync<long>(new CommandDefinition(sql, category, cancellationToken: cancellationToken));
    }
    public async Task<bool> UpdateCategoryAsync(Categories category, CancellationToken cancellationToken)
    {
        const string sql = """
            UPDATE categories
            SET category_name = @Category_Name, description = @Description
            WHERE id = @Id;
            """;
        var rows = await connection.ExecuteAsync(new CommandDefinition(sql, category, cancellationToken: cancellationToken));
        return rows > 0;
    }
    public async Task<bool> DeleteCategoryAsync(long id, CancellationToken cancellationToken)
    {
        const string sql = "DELETE FROM categories WHERE id = @Id;";
        var rows = await connection.ExecuteAsync(new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken));
        return rows > 0;
    }
}
