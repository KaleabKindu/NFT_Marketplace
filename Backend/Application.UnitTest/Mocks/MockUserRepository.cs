using Application.Contracts.Persistance;
using AutoMapper;
using Domain;
using Moq;



namespace ApplicationUnitTest.Mocks
{
    public static class MockAppUserRepository
    {
        public static Mock<IUserRepository> GetAppUserRepository()
        {
            var mockRepo = new Mock<IUserRepository>();

            var users = new List<AppUser> {
                new AppUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "myUserName1",
                    Email = "email1@gmail.com",
                    Address = "address1"
                    // Password = "password",
                    // FullName  = "this is may name "
                },
                new AppUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "myUserName2",
                    Email = "email2@gmail.com",
                    Address = "address2",
                    // Password = "password",
                    // FullName  = "this is may name "
                }
            };

            mockRepo.Setup(b => b.GetUserByAddress(It.IsAny<string>())).ReturnsAsync((string address) =>
            {
                return users.Find(x => x.Address == address);
            });
            return mockRepo;
        }
    }
}