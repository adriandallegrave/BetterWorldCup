using Better.Tools.Validations;
using Moq;
using Xunit;

namespace Better.Test.Tools.Validations
{
    public class HelpersTests
    {
        private readonly MockRepository _mockRepository;

        public HelpersTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
        }

        [Fact]
        public void GuidIsValid_Should_Expected()
        {
            // Arrange
            var guid = Guid.NewGuid();

            // Act
            var result = Helpers.GuidIsValid(guid);

            // Assert
            Assert.IsType<bool>(result);
            Assert.True(result);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public void GuidIsValid_Should_GuidEmpty()
        {
            // Arrange
            var guid = Guid.Empty;

            // Act
            var result = Helpers.GuidIsValid(guid);

            // Assert
            Assert.IsType<bool>(result);
            Assert.False(result);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public void GuidIsValid_Should_GuidDefault()
        {
            // Arrange
            var guid = default(Guid);

            // Act
            var result = Helpers.GuidIsValid(guid);

            // Assert
            Assert.IsType<bool>(result);
            Assert.False(result);
            _mockRepository.VerifyAll();
        }
    }
}
