using ProductApi.Data;
using ProductApi.Factories;
using ProductApi.Models;

namespace ProductApi.Seeders;

public class ProductSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (!db.Products.Any())
        {
            var products = new List<Product>(){
                new() { Name = "Gaming Mouse", Price = 49.99m },
                new() { Name = "Mechanical Keyboard", Price = 89.99m },
                new() { Name = "USB Headset", Price = 29.99m }
            };
            for (int i = 0; i < 500; i++)
            {
                products.Add(ProductFactory.Create());
            }
            db.Products.AddRange(products);
            await db.SaveChangesAsync();
        } 
    }
}