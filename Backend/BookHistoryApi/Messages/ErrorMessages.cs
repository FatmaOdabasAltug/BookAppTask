namespace BookHistoryApi.Messages
{
    public static class ErrorMessages
    {
        public const string TitleRequired = "Title is required.";
        public const string NullOrWhiteSpace = "This field cannot be empty.";
        public const string TitleNotNullOrWhiteSpace =
            "Title cannot be empty or consist only of whitespace.";
        public const string ShortDescriptionMaxLength =
            "Short description must be a maximum of 1000 characters.";
        public const string TitleMaxLength = "Title must be a maximum of 255 characters.";
        public const string PublishDateRequired = "Publish date is required.";
        public const string AuthorNameRequired = "Author name is required.";
        public const string AuthorNameMaxLength =
            "Author name must be a maximum of 255 characters.";
        public const string AuthorNotNullOrWhiteSpace =
            "Author cannot be empty or consist only of whitespace.";
        public const string InvalidDayErrorMessage =
            "Invalid date: Day is not valid for the given month.";
        public const string InvalidMonthErrorMessage =
            "Invalid date: Month must be between 1 and 12.";
        public const string UnexpectedError = "An unexpected error occurred.";
        public const string InvalidBookData = "Invalid book data.";
        public const string BookNotFound = "Book not found.";
        public const string ConflictCreatingBook = "A conflict occurred while creating the book.";
        public const string ConflictUpdatingBook = "A conflict occurred while updating the book.";
        public const string BookIdNotFound = "Book with ID {0} not found.";
        public const string BookTitleConflict = "A book with the title '{0}' already exists.";
        public const string ChangeDescriptionRequired = "Change description is required.";
        public const string BookIdRequired = "Book ID is required.";
        public const string BookIdMismatch =
            "The provided book ID does not match the ID of the book being updated.";
        public const string AtLeastOneAuthorRequired = "At least one author is required.";
        public const string ListCannotBeNullOrEmpty = "List cannot be null or empty";
    }
}
