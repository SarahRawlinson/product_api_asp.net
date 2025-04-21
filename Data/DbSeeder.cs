using ProductApi.Seeders;

namespace ProductApi.Data;

public class DbSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        await ProductSeeder.SeedAsync(db);
        await StockSeeder.SeedAsync(db);
    }
}