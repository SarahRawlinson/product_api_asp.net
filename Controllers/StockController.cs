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

    // ]
    // GET /api/stocks?name=Gaming%20Keyboard
    // example response
    // [

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
    /// Retrieves all products, or filters products by name if a query is provided.
    /// </summary>
    /// <param name="name">Optional name filter for products.</param>
    /// <returns>List of products.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(Stock[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Stock>>> GetStock([FromQuery] string? name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            var stocks = await context.Stocks
                .Where(p => p.Product.Name.Contains(name))
                .Include(p => p.Product)
                .ToListAsync();

            if (!stocks.Any())
            {
                return NotFound();
            }

            return Ok(stocks);
        }

        var allStock = await context.Stocks
            .Include(p => p.Product)
            .ToListAsync();
        return Ok(allStock);
    }
}