using Application.Contracts.Persistance;
using Application.Responses;
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

            mockRepo.Setup(b => b.GetAllUsersAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync((string search, int pageNumber, int pageSize, string currentAddress) =>
                {
                    var paginatedUsers = new PaginatedResponse<AppUser>
                    {
                        Count = users.Count,
                        PageNumber = pageNumber,
                        PageSize = pageSize,
                        Value = users
                    };
                    return paginatedUsers;
                });
            mockRepo.Setup(b => b.IsFollowing(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((string currentAddress, string address) =>
                {
                    return true;
                });

            mockRepo.Setup(b => b.GetFollowersAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((string address, int pageNumber, int pageSize) =>
                {
                    var paginatedUsers = new PaginatedResponse<AppUser>
                    {
                        Count = users.Count,
                        PageNumber = pageNumber,
                        PageSize = pageSize,
                        Value = users
                    };
                    return paginatedUsers;
                });
            return mockRepo;
        }
    }
}