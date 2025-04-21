using ProductApi.Data;
using ProductApi.Factories;
using ProductApi.Models;

namespace ProductApi.Seeders;

/// <summary>
/// ProductSeeder class responsible for seeding Product data into the database if necessary.
/// </summary>
public static class ProductSeeder
{
    /// <summary>
    /// Asynchronously seeds the database with product data if the Products table is empty.
    /// </summary>
    /// <param name="db">The database context to seed the product data into.</param>
    /// <returns>Awaitable task that represents the asynchronous seeding operation.</returns>
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