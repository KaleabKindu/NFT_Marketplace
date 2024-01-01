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
            var mockOfferRepository = MockOfferRepository.GetOfferRepository();
            // mockUow.Setup(m => m.UserManager).Returns(mockUserManager.Object);
            mockUow.Setup(m =>m.OfferRepository).Returns(mockOfferRepository.Object);

            mockUow.Setup(r => r.SaveAsync()).ReturnsAsync(1);

            return mockUow;

        }

    }
}
