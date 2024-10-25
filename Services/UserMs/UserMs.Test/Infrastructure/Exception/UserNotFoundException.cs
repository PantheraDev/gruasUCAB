using UserMs.Infrastructure.Exceptions;
using Xunit;

namespace UserMs.Test.Infrastructure.Exceptions
{
    public class UserNotFoundExceptionTest
    {
        [Fact]
        public void ShouldCreateExceptionWithOutArguments()
        {
            var userNotFoundException = new UserNotFoundException();
            Assert.NotNull(userNotFoundException);
        }

        [Fact]
        public void ShouldCreateExceptionWithMessageArguments()
        {
            var message = "User not found";
            var userNotFoundException = new UserNotFoundException(message);
            Assert.NotNull(userNotFoundException);
            Assert.Equal(message, userNotFoundException.Message);
        }

        [Fact]
        public void ShouldCreateExceptionWithMessageAndInnerExceptionArguments()
        {
            var message = "User not found";
            var innerException = new Exception("Inner exception");
            var userNotFoundException = new UserNotFoundException(message, innerException);
            Assert.NotNull(userNotFoundException);
            Assert.Equal(message, userNotFoundException.Message);
            Assert.Equal(innerException, userNotFoundException.InnerException);
        }
    }
}