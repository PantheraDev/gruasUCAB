namespace UserMs.Infrastructure.Exceptions
{
    public class ValidatorException : Exception
    {
        public ValidatorException()
        {
        }

        public ValidatorException(string message)
            : base(message)
        {
        }

        public ValidatorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}