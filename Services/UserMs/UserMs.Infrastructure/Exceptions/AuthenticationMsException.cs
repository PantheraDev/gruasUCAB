namespace UserMs.Infrastructure.Exceptions
{
    public class AuthenticationMsException : Exception
    {
        public AuthenticationMsException() { }

        public AuthenticationMsException(string message)
            : base(message) { }

        public AuthenticationMsException(string message, Exception inner)
            : base(message, inner) { }
    }
}