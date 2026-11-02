namespace DapperMediatRCrud.Models;

public sealed class Categories
{
    public long Id { get; set; }
    public string Category_Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
}

