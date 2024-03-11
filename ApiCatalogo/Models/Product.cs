using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ApiCatalogo.Validation;

namespace ApiCatalogo.Models;

[Table("Products")]
public class Product : IValidatableObject
{
    [Key]
    [JsonIgnore]
    public int ProductId { get; set; }
    
    [Required]
    [StringLength(80)]
    // [FirstLetterUppercase]
    public string? Name { get; set; }
    
    [Required]
    [StringLength(300)]
    public string? Description { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }
    
    [Required]
    [StringLength(300)]
    public string? UrlImage { get; set; }
    
    public float Stock { get; set; }
    public DateTime RegisterDate { get; set; }
    public int CategoryId { get; set; }
    
    [JsonIgnore]
    public Category? Category { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!string.IsNullOrEmpty(this.Name))
        {
            var firstLetter = this.Name[0].ToString();
            if (firstLetter != firstLetter.ToUpper())
            {
                yield return new ValidationResult("First letter must be capitalized", 
                    new[] { nameof(this.Name) });
            }
        }

        if (this.Stock <= 0)
        {
            yield return new ValidationResult("Stock must be greater than zero", 
                new[] { nameof(this.Stock) });
        }
    }
}