using Microsoft.EntityFrameworkCore;
using ProductApi.Factories;
using ProductApi.Models;

namespace ProductApi.Data;

public class AppDbContext : DbContext
{
    /// <summary>
    /// Represents a database context for the application, providing access to the Product and Stock entities.
    /// </summary>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Stock> Stocks { get; set; }
}