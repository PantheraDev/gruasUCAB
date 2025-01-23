
namespace AuthenticationMs.Common.Exceptions
{
    public class UserExistException : Exception
    {
        public UserExistException() { }

        public UserExistException(string message)
            : base(message) { }

        public UserExistException(string message, Exception inner)
            : base(message, inner) { }
    }
}