using Application.Contracts.Persistance;
using Moq;

namespace Application.UnitTest.Mocks
{
    public static class MockUnitOfWork
    {
        public static Mock<IUnitOfWork> GetUnitOfWork()
        {
            var mockUow = new Mock<IUnitOfWork>();
            var mockUserManager = MockUserManager.GetUserManager();

            mockUow.Setup(r => r.SaveAsync()).ReturnsAsync(1);

            return mockUow;

        }

    }
}
