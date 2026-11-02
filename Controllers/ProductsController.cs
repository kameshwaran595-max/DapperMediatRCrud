using DapperMediatRCrud.DTOs;
using DapperMediatRCrud.Features.Products.Commands;
using DapperMediatRCrud.Features.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DapperMediatRCrud.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ProductsController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        => Ok(await sender.Send(new GetAllProductsQuery(), cancellationToken));

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken)
    {
        var product = await sender.Send(new GetProductByIdQuery(id), cancellationToken);
        return product is null ? NotFound(new { message = "Product not found." }) : Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name) || request.Price < 0)
            return BadRequest(new { message = "Name is required and price must be zero or greater." });

        var id = await sender.Send(new CreateProductCommand(request.Name, request.Description, request.Price), cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, new { id, message = "Product created." });
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, ProductRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name) || request.Price < 0)
            return BadRequest(new { message = "Name is required and price must be zero or greater." });

        var updated = await sender.Send(new UpdateProductCommand(id, request.Name, request.Description, request.Price), cancellationToken);
        return updated ? NoContent() : NotFound(new { message = "Product not found." });
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        var deleted = await sender.Send(new DeleteProductCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound(new { message = "Product not found." });
    }

    [HttpGet("categories")]
    public async Task<IActionResult> GetAllCategories(CancellationToken cancellationToken)
        => Ok(await sender.Send(new GetAllCategoriesQuery(), cancellationToken));

    [HttpPost("categories")]
    public async Task<IActionResult> CreateCategory(CategoryRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest(new { message = "Category name is required." });

        var id = await sender.Send(new CreateCategoryCommand(request.Name, request.Description), cancellationToken);
        return CreatedAtAction(nameof(GetAllCategories), new { id }, new { id, message = "Category created." });
    }

    [HttpDelete("categories/{id:long}")]
    public async Task<IActionResult> DeleteCategory(long id, CancellationToken cancellationToken)
    {
        var deleted = await sender.Send(new DeleteCategoryCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound(new { message = "Category not found." });
    }

    //[HttpPut("categories/{id:long}")]
    //public async Task<IActionResult> UpdateCategory(long id, CategoryRequest request, CancellationToken cancellationToken)
    //{
    //    if (string.IsNullOrWhiteSpace(request.Name))
    //        return BadRequest(new { message = "Category name is required." });

    //    var updated = await sender.Send(new UpdateCategoryCommand(id, request.Name, request.Description), cancellationToken);
    //    return updated ? NoContent() : NotFound(new { message = "Category not found." });
    //}
}
