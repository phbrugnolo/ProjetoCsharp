using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class TelefoneFormatoAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var telefone = value as string;
        if (telefone != null && !Regex.IsMatch(telefone, @"^\(\d{3}\) \d{5}-\d{4}$"))
        {
            return new ValidationResult("O telefone deve estar no formato (XXX) XXXXX-XXXX.", new[] { validationContext.MemberName });
        }
        return ValidationResult.Success;
    }
}

