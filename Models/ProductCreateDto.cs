namespace ProductApi.Models;

/// <summary>
/// Represents data transfer object for creating a new product with Name and Price properties.
/// </summary>
public class ProductCreateDto
{
    /// <summary>
    /// Represents the name of a product.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Represents the price of a product.
    /// </summary>
    public decimal Price { get; set; }
}