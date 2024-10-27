using System.ComponentModel.DataAnnotations;
using BookHistoryApi.Messages;

namespace BookHistoryApi.Models.Validation
{
    public class NotNullOrWhiteSpaceAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is string strValue && string.IsNullOrWhiteSpace(strValue))
        {
            return new ValidationResult(ErrorMessage ?? ErrorMessages.NullOrWhiteSpace);
        }

        return ValidationResult.Success;
    }
}
}