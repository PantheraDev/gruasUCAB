namespace UserMs.Infrastructure.Exceptions
{
    public class DuplicateDriverLicenseIdException : Exception
    {
        public DuplicateDriverLicenseIdException() { }

        public DuplicateDriverLicenseIdException(string message)
            : base(message) { }

        public DuplicateDriverLicenseIdException(string message, Exception inner)
            : base(message, inner) { }
    }
}