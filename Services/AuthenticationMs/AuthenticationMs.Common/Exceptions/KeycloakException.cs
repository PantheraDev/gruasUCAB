
namespace AuthenticationMs.Common.Exceptions
{
    public class KeycloakException : Exception
    {
        public KeycloakException() { }

        public KeycloakException(string message)
            : base(message) { }

        public KeycloakException(string message, Exception inner)
            : base(message, inner) { }
    }
}