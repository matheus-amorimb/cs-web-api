using System.ComponentModel.DataAnnotations;

namespace ApiCatalogo.DTOs;

public class CategoryDto
{
    public int CategoryId { get; set; }
    
    [Required]
    [StringLength(80)]
    public string? Name { get; set; }
    
    [Required]
    [StringLength(300)]
    public string? UrlImage { get; set; }
    
}