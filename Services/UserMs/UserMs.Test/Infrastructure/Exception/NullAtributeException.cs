using UserMs.Infrastructure.Exceptions;
using Xunit;

namespace UserMs.Test.Infrastructure.Exceptions
{
    public class NullAtributeExceptionTest
    {
        [Fact]
        public void ShouldCreateExceptionWithOutArguments()
        {
            var nullAtributeException = new NullAtributeException();
            Assert.NotNull(nullAtributeException);
        }

        [Fact]
        public void ShouldCreateExceptionWithMessageArguments()
        {
            var message = "User not found";
            var nullAtributeException = new NullAtributeException(message);
            Assert.NotNull(nullAtributeException);
            Assert.Equal(message, nullAtributeException.Message);
        }

        [Fact]
        public void ShouldCreateExceptionWithMessageAndInnerExceptionArguments()
        {
            var message = "User not found";
            var innerException = new Exception("Inner exception");
            var nullAtributeException = new NullAtributeException(message, innerException);
            Assert.NotNull(nullAtributeException);
            Assert.Equal(message, nullAtributeException.Message);
            Assert.Equal(innerException, nullAtributeException.InnerException);
        }
    }
}