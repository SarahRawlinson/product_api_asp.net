using ProductApi.Data;
using ProductApi.Factories;
using ProductApi.Models;

namespace ProductApi.Seeders;

public class StockSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (!db.Stocks.Any())
        {
            var stocks = new List<Stock>();
            db.Stocks.AddRange(stocks);
            if (!db.Products.Any())
            {
                await ProductSeeder.SeedAsync(db);
            }
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