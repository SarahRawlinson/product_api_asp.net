using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Models;

namespace ProductApi.Controllers;

/// <summary>
/// Controller for managing products in the API.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProductsController(AppDbContext context) : ControllerBase
{
    // GET /api/products
    // example response
    // [
    //     {
    //         "id": 1,
    //         "name": "Gaming Keyboard",
    //         "price": 99.99
    //     },
    //     {
    //         "id": 2,
    //         "name": "Gaming Mouse",
    //         "price": 48.95
    //     }
    // ]
    // GET /api/products?name=Gaming%20Keyboard
    // example response
    // [
    //     {
    //         "id": 1,
    //         "name": "Gaming Keyboard",
    //         "price": 99.99
    //     }
    // ]
    // GET /api/products?name=UnknownItem
    // example response
    // {
    //     "type": "type..",
    //     "title": "Not Found",
    //     "status": 404,
    //     "traceId": "id.."
    // }
    /// <summary>
    /// Retrieves all products, or filters products by name if a query is provided.
    /// </summary>
    /// <param name="name">Optional name filter for products.</param>
    /// <returns>List of products.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(Product[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] string? name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            var products = await context.Products
                .Where(p => p.Name.Contains(name))
                .ToListAsync();

            if (!products.Any())
            {
                return NotFound();
            }

            return Ok(products);
        }

        var allProducts = await context.Products.ToListAsync();
        return Ok(allProducts);
    }

    // GET /api/products/{id}
    // example response
    // {
    //     "id": 1,
    //     "name": "Gaming Keyboard",
    //     "price": 99.99
    // }
    // example response
    // {
    //     "type": "type..",
    //     "title": "Not Found",
    //     "status": 404,
    //     "traceId": "id.."
    // }
    /// <summary>
    /// Retrieves a single product by its ID.
    /// </summary>
    /// <param name="id">The ID of the product to retrieve.</param>
    /// <returns>The product with the specified ID, or 404 if not found.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> GetProductById(int id)
    {
        var product = await context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    // POST /api/products
    // example post
    // {
    //     "name": "Gaming Keyboard",
    //     "price": 99.99
    // }
    // example response
    // {
    //     "id": 1,
    //     "name": "Gaming Keyboard",
    //     "price": 99.99
    // }
    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <param name="productDto">The product info to create (name and price).</param>
    /// <returns>The created product with its generated ID.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Product>> CreateProduct(ProductCreateDto productDto)
    {
        var product = new Product
        {
            Name = productDto.Name,
            Price = productDto.Price
        };
        context.Products.Add(product);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProducts), new {id = product.Id}, product);
    }
}

