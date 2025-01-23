namespace UserMs.Infrastructure.Exceptions
{
    public class NullAtributeException : Exception
    {
        public NullAtributeException() { }

        public NullAtributeException(string message)
            : base(message) { }

        public NullAtributeException(string message, Exception inner)
            : base(message, inner) { }
    }
}