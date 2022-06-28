using AuthenticationService.DataAccess;
using AuthenticationService.Models;
using Microsoft.Extensions.Configuration;
using Moq;
using System.IO;
using System.Threading.Tasks;
using UAuthMicroservice_Tests.Infrastructure;
using Xunit;

namespace AuthMicroservice_Tests
{
    public class AuthRepository_Tests
    {
        private readonly Mock<FakeUserManager> mockUserManager;
        private readonly IConfiguration _configuration;

        public AuthRepository_Tests()
        {
            mockUserManager = new FakeUserManager().CreateUserManagerMock();
            _configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();

        }

        [Fact]
        public async Task Register_Should_Return_Created_UserId_And_201_StatusCode()
        {
            //Arrange
            var sut = new AuthRepository(mockUserManager.Object, _configuration);
            var userReq = new RegRequestDto() { Email = "new@gmail.com", Password = "Password123", PhoneNumber = "08044334433", StateOfResidence = "Lagos", LGA = "Ikeja" };

            //Act
            var result = await sut.Register(userReq);

            //Assert
            Assert.NotNull(result.Data);
            Assert.Equal(201, result.StatusCode);
        }

        [Fact]
        public async Task Login_Should_Return_A_200_StatusCode_If_Phone_Is_Confirmed()
        {
            //Arrange
            var sut = new AuthRepository(mockUserManager.Object, _configuration);
            var request = new LoginReqDto() { Email = "test@pass.com", Password = "Password123" };

            //Act
            var result = await sut.Login(request);

            //Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Login_Should_Return_A_403_StatusCode_If_Phone_Is_Not_Confirmed()
        {
            //Arrange
            var sut = new AuthRepository(mockUserManager.Object, _configuration);
            var request = new LoginReqDto() { Email = "cool@gmail.com", Password = "Password123" };

            //Act
            var result = await sut.Login(request);

            //Assert
            Assert.Equal(403, result.StatusCode);
        }

        [Fact]
        public async Task Login_Should_Return_A_404_StatusCode_If_Credentials_Invalid()
        {
            //Arrange
            var sut = new AuthRepository(mockUserManager.Object, _configuration);
            var request = new LoginReqDto() { Email = "new@gmail.com", Password = "Password123" };

            //Act
            var result = await sut.Login(request);

            //Assert
            Assert.Equal(404, result.StatusCode);
        }
    }
}
