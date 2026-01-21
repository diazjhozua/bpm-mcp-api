using System.ComponentModel.DataAnnotations;

namespace bpm_mcp_api.Validation
{
    /// <summary>
    /// Custom validation attribute to ensure date is not in the future
    /// </summary>
    public class NotFutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime dateValue)
            {
                if (dateValue.Date > DateTime.Now.Date)
                {
                    return new ValidationResult(ErrorMessage ?? "Date cannot be in the future");
                }
            }

            return ValidationResult.Success!;
        }
    }
}