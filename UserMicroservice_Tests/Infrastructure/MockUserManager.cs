using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using UserMicroservice.Models;

namespace UserMicroservice_Tests.Infrastructure
{
    public class FakeUserManager : UserManager<User>
    {
        public FakeUserManager()
            : base(new Mock<IUserStore<User>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<User>>().Object,
                new IUserValidator<User>[0],
                new IPasswordValidator<User>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<User>>>().Object)
        { }

        public Mock<FakeUserManager> CreateUserManagerMock()
        {

            var users = new List<User>
            {
                new User
                {
                    Id = "1",
                    UserName = "Test",
                    Email = "test@pass.com",
                    EmailConfirmed = true,
                    PhoneNumber = "08012344321",
                    PhoneNumberConfirmed = true,
                    StateOfResidence = "Lagos",
                    LGA = "Ikeja"
                },
                 new User
                {
                    Id = "2",
                    UserName = "Cool",
                    Email = "cool@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "08012986021",
                    PhoneNumberConfirmed = false,
                    StateOfResidence = "Lagos",
                    LGA = "Yaba"
                },
                  new User
                {
                    Id = "3",
                    UserName = "Smith",
                    Email = "smith@test.com",
                    EmailConfirmed = true,
                    PhoneNumber = "09073454321",
                    PhoneNumberConfirmed = true,
                    StateOfResidence = "Oyo",
                    LGA = "Ibadan"
                }
            }.AsQueryable();

            var fakeUserManager = new Mock<FakeUserManager>();

            fakeUserManager.Setup(x => x.Users)
                .Returns(users);
            fakeUserManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            return fakeUserManager;
        }
    }
}
