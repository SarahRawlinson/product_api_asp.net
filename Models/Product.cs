using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductApi.Models;

/// <summary>
/// Represents a product with information including Id, Name, and Price.
/// </summary>
public class Product
{
    /// <summary>
    /// Represents the unique identifier of a product.
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// Represents the name of a product.
    /// </summary>
    [MaxLength(100)] 
    public string Name { get; set; }

    /// <summary>
    /// Represents the price of a product.
    /// The Price property should be a decimal value indicating the cost of the product.
    /// </summary>
    [Column (TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
}