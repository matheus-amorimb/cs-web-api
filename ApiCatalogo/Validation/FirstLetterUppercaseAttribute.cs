using System.ComponentModel.DataAnnotations;

namespace ApiCatalogo.Validation;

public class FirstLetterUppercaseAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, 
        ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrEmpty(value.ToString()))
        {
            return ValidationResult.Success;
        }

        var firstLetter = value.ToString()[0].ToString();
        if (firstLetter != firstLetter.ToUpper())
        {
            return new ValidationResult("First letter must be capitalizez");
        }
        
        return ValidationResult.Success;

    }
}