namespace SeminarHub.Common
{
    public static class ErrorMessages
    {
        public const string ErrorDateFormat = "Please specify date in format dd/MM/yyyy HH:mm";

        public const string ErrorSeminarTopic = "The seminar topic must be between 3 and 100 symbols.";
        public const string ErrorSeminarLecturer = "The lecturer name must be between 5 and 100 symbols.";
        public const string ErrorSeminarDetails = "The details must be between 10 and 500 symbols.";
        public const string ErrorSeminarOrganizerMissing = "Invalid organizer id.";
        public const string ErrorSeminarCategoryMissing = "Invalid category id.";
        public const string ErrorSeminarDuration = "Please specify duration between 30 and 180 minutes.";

        public const string ErrorCategoryName = "CategoryValidation name must be between 3 and 50 symbols.";

        public const string ErrorSeminarNotFound = "Invalid seminar id.";
        public const string ErrorParticipantNotFound = "Invalid participant id.";
    }
}
