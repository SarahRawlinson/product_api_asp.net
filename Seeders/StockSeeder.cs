using ProductApi.Data;
using ProductApi.Factories;
using ProductApi.Models;

namespace ProductApi.Seeders;

/// <summary>
/// StockSeeder class responsible for seeding Stock data into the database if necessary.
/// </summary>
public static class StockSeeder
{
    /// <summary>
    /// Asynchronously seeds the database with stock data if the Stocks table is empty.
    /// </summary>
    /// <param name="db">The database context to seed the stock data into.</param>
    /// <returns>Awaitable task that represents the asynchronous seeding operation.</returns>
    public static async Task SeedAsync(AppDbContext db)
    {
        if (!db.Stocks.Any())
        {
            
            if (!db.Products.Any())
            {
                await ProductSeeder.SeedAsync(db);
            }
            Product p = db.Products.First(p => p.Name == "Gaming Mouse");
            
            var stocks = new List<Stock>
            {
                new ()
                {
                    ProductId = p.Id,
                    Quantity = 20,
                    Location = "Warehouse A"
                }
            };
            
            var productIds = db.Products.Select(p => p.Id).ToList();
            foreach (var product in productIds)
            {
                stocks.Add(StockFactory.Create(product));
            }
            db.Stocks.AddRange(stocks);
            await db.SaveChangesAsync();
        } 
    }
}