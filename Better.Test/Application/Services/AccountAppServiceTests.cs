using Better.Application.Dtos;
using Better.Application.Services;
using Better.Domain.Interfaces.Services;
using Better.Domain.Models;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace Better.Test.Application.Services
{
    public class AccountAppServiceTests
    {
        private readonly MockRepository _mockRepository;
        private readonly Mock<IAccountService> _accountService;

        public AccountAppServiceTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _accountService = _mockRepository.Create<IAccountService>();
        }

        private AccountAppService CreateService()
        {
            return new AccountAppService(_accountService.Object);
        }

        private static Account CreateAccount()
        {
            return new Account();
        }

        private static List<Account> CreateAccounts()
        {
            return new List<Account>()
            {
                new Account()
                {
                    Id = Guid.NewGuid(),
                    Email = "first@first.com",
                    Name = "firstPass",
                    HaveBets = true
                },
                new Account()
                {
                    Id = Guid.NewGuid(),
                    Email = "second@second.com",
                    Name = "secondPass",
                    HaveBets = false
                },
                new Account()
                {
                    Id = Guid.NewGuid(),
                    Email = "third@third.com",
                    Name = "thirdPass",
                    HaveBets = false
                }
            };
        }

        [Fact]
        public async Task Delete_Should_IdFound()
        {
            // Arrange
            var service = CreateService();
            var id = Guid.NewGuid();
            var account = CreateAccount();

            _accountService.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(account);
            _accountService.Setup(x => x.Delete(It.IsAny<Account>())).ReturnsAsync(account);

            // Act
            var result = await service.Delete(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Account>(result);
            Assert.Equal(account, result);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Delete_Should_IdNotFound()
        {
            // Arrange
            var service = CreateService();
            var id = Guid.NewGuid();
            var account = CreateAccount();

            _accountService.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(default(Account));

            // Act
            var result = await service.Delete(id);

            // Assert
            Assert.Null(result);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetAll_Should_ListPopulated()
        {
            // Arrange
            var service = CreateService();
            var accounts = CreateAccounts();

            _accountService.Setup(x => x.GetAll()).ReturnsAsync(accounts);

            // Act
            var result = await service.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Account>>(result);
            Assert.Equal(accounts, result);
            Assert.Equal(3, result.Count);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetAll_Should_ListEmpty()
        {
            // Arrange
            var service = CreateService();
            var accounts = new List<Account>();

            _accountService.Setup(x => x.GetAll()).ReturnsAsync(accounts);

            // Act
            var result = await service.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Account>>(result);
            Assert.Equal(accounts, result);
            Assert.False(result.Any());
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetById_Should_IdFound()
        {
            // Arrange
            var service = CreateService();
            var id = Guid.NewGuid();
            var account = CreateAccount();

            _accountService.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(account);

            // Act
            var result = await service.GetById(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Account>(result);
            Assert.Equal(account, result);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetById_Should_IdNotFound()
        {
            // Arrange
            var service = CreateService();
            var id = Guid.NewGuid();
            var account = default(Account);

            _accountService.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(account);

            // Act
            var result = await service.GetById(id);

            // Assert
            Assert.Null(result);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetByProperty_Should_EmailFound()
        {
            // Arrange
            var service = CreateService();
            var email = "first@first.com";
            Expression<Func<Account, bool>> expression = x => x.Email == email;
            var accounts = CreateAccounts();
            var expected = accounts.Where(x => x.Email == email).ToList();

            _accountService.Setup(x => x.GetByProperty(It.IsAny<Expression<Func<Account, bool>>>())).ReturnsAsync(expected);

            // Act
            var result = await service.GetByProperty(expression);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Account>>(result);
            Assert.Equal(expected, result);
            Assert.Single(result);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetByProperty_Should_EmailNotFound()
        {
            // Arrange
            var service = CreateService();
            var email = "1st@first.com";
            Expression<Func<Account, bool>> expression = x => x.Email == email;
            var accounts = new List<Account>();

            _accountService.Setup(x => x.GetByProperty(It.IsAny<Expression<Func<Account, bool>>>())).ReturnsAsync(accounts);

            // Act
            var result = await service.GetByProperty(expression);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Account>>(result);
            Assert.Equal(accounts.Where(x => x.Email == email).ToList(), result);
            Assert.False(result.Any());
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetByProperty_Should_EmailConfirmed()
        {
            // Arrange
            var service = CreateService();
            Expression<Func<Account, bool>> expression = x => x.HaveBets == false;
            var accounts = CreateAccounts();
            var expected = accounts.Where(x => x.HaveBets == false).ToList();

            _accountService.Setup(x => x.GetByProperty(It.IsAny<Expression<Func<Account, bool>>>())).ReturnsAsync(expected);

            // Act
            var result = await service.GetByProperty(expression);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Account>>(result);
            Assert.Equal(expected, result);
            Assert.True(result.Count == 2);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Post_Should_Expected()
        {
            // Arrange
            var service = CreateService();
            var dto = new AccountDto() { Email = "email", Name = "password" };
            var accounts = new List<Account>();
            var account = CreateAccount();

            _accountService.Setup(x => x.GetByProperty(It.IsAny<Expression<Func<Account, bool>>>())).ReturnsAsync(accounts);
            _accountService.Setup(x => x.Post(It.IsAny<Account>())).ReturnsAsync(account);

            // Act
            var result = await service.Post(dto);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Account>(result);
            Assert.Equal(account, result);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Post_Should_EmailAlreadyUsed()
        {
            // Arrange
            var service = CreateService();
            var dto = new AccountDto() { Email = "firs@first.com", Name = "password" };
            var accounts = new List<Account>() { CreateAccount() };

            _accountService.Setup(x => x.GetByProperty(It.IsAny<Expression<Func<Account, bool>>>())).ReturnsAsync(accounts);

            // Act
            var result = await service.Post(dto);

            // Assert
            Assert.Null(result);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task UpdateHaveBets_Should_AccountFound()
        {
            // Arrange
            var service = CreateService();
            var emailConfirmed = true;
            var id = Guid.NewGuid();
            var account = CreateAccount();

            _accountService.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(account);
            _accountService.Setup(x => x.Update(It.IsAny<Account>())).ReturnsAsync(account);

            // Act
            var result = await service.UpdateHaveBets(emailConfirmed, id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Account>(result);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task UpdateHaveBets_Should_AccountNotFound()
        {
            // Arrange
            var service = CreateService();
            var emailConfirmed = true;
            var id = Guid.NewGuid();
            var account = default(Account);

            _accountService.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(account);

            // Act
            var result = await service.UpdateHaveBets(emailConfirmed, id);

            // Assert
            Assert.Null(result);
            _mockRepository.VerifyAll();
        }
    }
}
