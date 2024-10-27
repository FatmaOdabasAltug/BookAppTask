using System;
using System.ComponentModel.DataAnnotations;
using BookHistoryApi.Messages;

namespace BookHistoryApi.Models.Validation
{
    public class ValidDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext
        )
        {
            if (value is DateTime dateTime)
            {
                // Check if the month is between 1 and 12
                if (dateTime.Month < 1 || dateTime.Month > 12)
                {
                    return new ValidationResult(
                        ErrorMessage ?? ErrorMessages.InvalidMonthErrorMessage
                    );
                }

                // Check if the day is valid for the given month and year
                int daysInMonth = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
                if (dateTime.Day < 1 || dateTime.Day > daysInMonth)
                {
                    return new ValidationResult(
                        ErrorMessage ?? ErrorMessages.InvalidDayErrorMessage
                    );
                }
            }
            return ValidationResult.Success;
        }
    }
}
