using Better.Domain.Interfaces;
using Better.Domain.Models;
using Better.Domain.Services;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace Better.Test.Domain.Services
{
    public class AccountServiceTests
    {
        private readonly MockRepository _mockRepository;

        private readonly Mock<IRepositoryWrapper> _mockRepositoryWrapper;

        public AccountServiceTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _mockRepositoryWrapper = _mockRepository.Create<IRepositoryWrapper>();
        }

        private AccountService CreateService()
        {
            return new AccountService(_mockRepositoryWrapper.Object);
        }

        private static Account GenerateAccount()
        {
            return new Account()
            {
                Id = Guid.NewGuid(),
                Email = "abc@email.com",
                HaveBets = true,
            };
        }

        [Fact]
        public async Task Delete_Should_ExpectedBehavior()
        {
            // Arrange
            var service = CreateService();
            var account = GenerateAccount();

            _mockRepositoryWrapper.Setup(x => x.Account.GetFirstByProperty(It.IsAny<Expression<Func<Account, bool>>>())).ReturnsAsync(account);
            _mockRepositoryWrapper.Setup(x => x.Account.Delete(It.IsAny<Account>())).Returns(account);
            _mockRepositoryWrapper.Setup(x => x.Commit()).ReturnsAsync(true);

            // Act
            var result = await service.Delete(account);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Account>(result);
            Assert.True(result.HaveBets);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Delete_Should_CommitUnsuccessful()
        {
            // Arrange
            var service = CreateService();
            var account = GenerateAccount();

            _mockRepositoryWrapper.Setup(x => x.Account.GetFirstByProperty(It.IsAny<Expression<Func<Account, bool>>>())).ReturnsAsync(account);
            _mockRepositoryWrapper.Setup(x => x.Account.Delete(It.IsAny<Account>())).Returns(account);
            _mockRepositoryWrapper.Setup(x => x.Commit()).ReturnsAsync(false);

            // Act
            var result = await service.Delete(account);

            // Assert
            Assert.Null(result);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Delete_Should_IdNotFound()
        {
            // Arrange
            var service = CreateService();
            var account = GenerateAccount();

            _mockRepositoryWrapper.Setup(x => x.Account.GetFirstByProperty(It.IsAny<Expression<Func<Account, bool>>>())).ReturnsAsync(default(Account));

            // Act
            var result = await service.Delete(account);

            // Assert
            Assert.Null(result);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Delete_Should_AccountIsNull()
        {
            // Arrange
            var service = CreateService();
            Account account = null;

            // Act
            var result = await service.Delete(account);

            // Assert
            Assert.Null(result);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetAll_Should_ExpectedBehavior()
        {
            // Arrange
            var service = CreateService();
            var accounts = new List<Account>() { GenerateAccount(), GenerateAccount() };

            _mockRepositoryWrapper.Setup(x => x.Account.Get()).ReturnsAsync(accounts);

            // Act
            var result = await service.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Account>>(result);
            Assert.Equal(accounts.Count, result.Count);
            Assert.Equal(accounts[0].Id, result[0].Id);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetById_Should_ExpectedBehavior()
        {
            // Arrange
            var service = CreateService();
            var account = GenerateAccount();
            var id = account.Id;

            _mockRepositoryWrapper.Setup(x => x.Account.GetFirstByProperty(It.IsAny<Expression<Func<Account, bool>>>())).ReturnsAsync(account);

            // Act
            var result = await service.GetById(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Account>(result);
            Assert.Equal(account, result);
            Assert.Equal(account.Id, result.Id);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetByProperty_Should_ExpectedBehavior()
        {
            // Arrange
            var service = CreateService();
            var accounts = new List<Account>() { GenerateAccount(), GenerateAccount() };

            _mockRepositoryWrapper.Setup(x => x.Account.GetAllByProperty(It.IsAny<Expression<Func<Account, bool>>>())).ReturnsAsync(accounts);

            // Act
            var result = await service.GetByProperty(_ => true);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Account>>(result);
            Assert.Equal(accounts, result);
            Assert.Equal(accounts[0].Id, result[0].Id);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Post_Should_ExpectedBehavior()
        {
            // Arrange
            var service = CreateService();
            var account = GenerateAccount();

            _mockRepositoryWrapper.Setup(x => x.Account.Post(It.IsAny<Account>())).ReturnsAsync(account);
            _mockRepositoryWrapper.Setup(x => x.Commit()).ReturnsAsync(true);

            // Act
            var result = await service.Post(account);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Account>(result);
            Assert.Equal(account, result);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Post_Should_CommitUnsuccessful()
        {
            // Arrange
            var service = CreateService();
            var account = GenerateAccount();

            _mockRepositoryWrapper.Setup(x => x.Account.Post(It.IsAny<Account>())).ReturnsAsync(account);
            _mockRepositoryWrapper.Setup(x => x.Commit()).ReturnsAsync(false);

            // Act
            var result = await service.Post(account);

            // Assert
            Assert.Null(result);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Post_Should_GuidInvalid()
        {
            // Arrange
            var service = CreateService();
            var account = GenerateAccount();
            account.Id = Guid.Empty;

            // Act
            var result = await service.Post(account);

            // Assert
            Assert.Null(result);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Post_Should_AccountIsNull()
        {
            // Arrange
            var service = CreateService();
            Account account = null;

            // Act
            var result = await service.Post(account);

            // Assert
            Assert.Null(result);
            _mockRepository.VerifyAll();
        }
    }
}
