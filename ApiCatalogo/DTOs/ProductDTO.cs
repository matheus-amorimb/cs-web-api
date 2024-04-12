using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCatalogo.DTOs;

public class ProductDTO
{
    public int ProductId { get; set; }
    
    [Required]
    [StringLength(80)]
    public string? Name { get; set; }
    
    [Required]
    [StringLength(300)]
    public string? Description { get; set; }
    
    public decimal Price { get; set; }
    
    [Required]
    [StringLength(300)]
    public string? UrlImage { get; set; }
    
    public int CategoryId { get; set; }
}