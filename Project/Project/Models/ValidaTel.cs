using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class TelefoneFormatoAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var telefone = value as string;
        if (telefone != null)
        {
            telefone = telefone.Replace(" ", "");
            if (!Regex.IsMatch(telefone, @"^(\(\d{3}\)\d{5}-\d{4}|\d{10,11})$"))
            {
                return new ValidationResult("O telefone deve estar no formato (XXX) XXXXX-XXXX ou XXXXXXXXXX.", new[] { validationContext.MemberName });
            }
        }
        return ValidationResult.Success;
    }
}
