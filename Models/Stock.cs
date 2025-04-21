using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductApi.Models;

public class Stock
{
    /// <summary>
    /// Represents the unique identifier of the stock item.
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    /// <summary>
    /// The foreign key for the related product.
    /// </summary>
    [ForeignKey(nameof(Product))]
    public int ProductId { get; set; }

    /// <summary>
    /// Navigation property to the related product.
    /// </summary>
    public Product Product { get; set; } = null!;
    
    /// <summary>
    /// Quantity of the product available in stock.
    /// </summary>
    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }
    
    /// <summary>
    /// Location of Stock
    /// </summary>
    [MaxLength(200)] 
    public string Location { get; set; } = null!;
}