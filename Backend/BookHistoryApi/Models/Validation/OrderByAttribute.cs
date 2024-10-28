using System;
using System.ComponentModel.DataAnnotations;
using BookHistoryApi.Messages;
using BookHistoryApi.Models.Constants;

namespace BookHistoryApi.Validation
{
    public class OrderByAttribute : ValidationAttribute
    {
        private readonly string[] _validValues =
        {
            SortConstants.Ascending,
            SortConstants.Descending,
        };

        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext
        )
        {
            if (value is string stringValue)
            {
                if (
                    Array.Exists(
                        _validValues,
                        v => v.Equals(stringValue, StringComparison.OrdinalIgnoreCase)
                    )
                )
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(
                ErrorMessage ?? ErrorMessages.OrderMustBeAscendingOrDescending
            );
        }
    }
}
