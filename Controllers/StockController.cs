using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Models;

namespace ProductApi.Controllers;

/// <summary>
/// Controller for managing stocks in the API.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class StockController(AppDbContext context) : ControllerBase
{
    // GET /api/stocks
    // example response
    // [
    //     {
    //         "id": 4,
    //         "productId": 504,
    //         "product": {
    //             "id": 504,
    //             "name": "Gaming Mouse",
    //             "price": 49.99
    //         },
    //         "quantity": 20,
    //         "location": "Warehouse A"
    //     }
    // ]
    // GET /api/stocks?name=Gaming%20Mouse
    // example response
    // [
    //     {
    //         "id": 4,
    //         "productId": 504,
    //         "product": {
    //             "id": 504,
    //             "name": "Gaming Mouse",
    //             "price": 49.99
    //         },
    //         "quantity": 20,
    //         "location": "Warehouse A"
    //     }
    // ]
    // GET /api/stocks?name=UnknownItem
    // example response
    // {
    //     "type": "type..",
    //     "title": "Not Found",
    //     "status": 404,
    //     "traceId": "id.."
    // }
    /// <summary>
    /// Retrieves all stocks, or filters stocks by product name if a query is provided.
    /// </summary>
    /// <param name="name">Optional name filter for stocks by product name.</param>
    /// <returns>List of products.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(Stock[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Stock>>> GetStock(
            [FromQuery] string? name,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10
        )
    {
        if (!string.IsNullOrEmpty(name))
        {
            var query = context.Stocks
                .Where(p => p.Product.Name.Contains(name))
                .Include(p => p.Product);

            var totalItems = await query.CountAsync();

            if (totalItems == 0)
            {
                return NotFound();
            }
            var stocks = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return Ok(stocks);
        }

        var allStock = await context.Stocks
            .Include(p => p.Product)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return Ok(allStock);
    }
}