using System;
using System.ComponentModel.DataAnnotations;
using BookHistoryApi.Messages;

namespace BookHistoryApi.Models.Validation
{
    public class ValidListAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext
        )
        {
            var list = value as List<int>;
            if (list == null || !list.Any())
            {
                return new ValidationResult(ErrorMessage ?? ErrorMessages.ListCannotBeNullOrEmpty);
            }
            return ValidationResult.Success;
        }
    }
}
