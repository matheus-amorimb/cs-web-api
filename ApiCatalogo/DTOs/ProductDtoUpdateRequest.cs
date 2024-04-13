using System.ComponentModel.DataAnnotations;

namespace ApiCatalogo.DTOs;

public class ProductDtoUpdateRequest : IValidatableObject
{
    
    public float Stock { get; set; }
    
    public DateTime RegisterDate { get; set; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (RegisterDate.Date <= DateTime.Now.Date)
        {
            yield return new ValidationResult("The date must be later than the current date",
                new []{nameof(this.RegisterDate)});
        }    
    }
}