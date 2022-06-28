using AutoMapper;
using Moq;
using UserMicroservice.DataAccess;
using UserMicroservice_Tests.Infrastructure;
using Xunit;

namespace UserMicroservice_Tests
{
    public class UserRepository_Tests
    {
        private readonly Mock<FakeUserManager> mockUserManager;
        private static IMapper _mapper;

        public UserRepository_Tests()
        {
            mockUserManager = new FakeUserManager().CreateUserManagerMock();
            _mapper = TestMapper.GetMapper();
           
        }

        [Fact]
        public void GetExistingUser_Should_Return_List_Of_Users_With_Confirmed_PhoneNo()
        {
            //Arrange
            var sut = new UserRepository(mockUserManager.Object, _mapper);

            //Act
            var result = sut.GetExistingUsers();

            //Assert
            Assert.NotNull(result.Data);
            //Assert.Equal(2, result.Data.)
           // Assert.True((typeof(IEnumerable<UserDto>))result.Data);
            //Assert.IsType<List<UserDto>>(result.Data);
        }

        [Fact]
        public void GetUserById_Should_Return_A_User_If_It_Exist_Or_Null()
        {
            //Arrange
            var sut = new UserRepository(mockUserManager.Object, _mapper);

            //Act
            var result1 =  sut.GetUserById("2");
            var result2 =  sut.GetUserById("5");

            //Assert
            Assert.NotNull(result1.Data);
            Assert.Null(result2.Data);
            Assert.Equal("cool@gmail.com", result1.Data.Email);
           // Assert.True((typeof(IEnumerable<UserDto>))result.Data);
            //Assert.IsType<List<UserDto>>(result.Data);
        }
    }
}
