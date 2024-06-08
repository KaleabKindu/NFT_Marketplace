using Application.Contracts.Persistance;
using Application.Contracts.Services;
using Application.Profiles;
using ApplicationUnitTest.Mocks;
using AutoMapper;
using Moq;

namespace Application.UnitTest.Mocks
{
    public static class MockUnitOfWork
    {
        public static Mock<IAuctionManagementService> GetAuctionManager(){
            var mockAucManager = new Mock<IAuctionManagementService>();
            mockAucManager.Setup(r => r.CloseAuction(It.IsAny<string>(), It.IsAny<long>())).ReturnsAsync(() => true);
            mockAucManager.Setup(r => r.Schedule(It.IsAny<string>(), It.IsAny<long>(), It.IsAny<long>()));
            return mockAucManager;
        }
        
        public static Mock<IUnitOfWork> GetUnitOfWork()
        {
            var mapper = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            var mapperCreated = mapper.CreateMapper();

            var mockUow = new Mock<IUnitOfWork>();
            var mockUserManager = MockUserManager.GetUserManager();
            var mockAssetRepository = MockAssetRepository.GetAssetRepository(mapperCreated);
            var mockAppUserRepository = MockAppUserRepository.GetAppUserRepository();
            var mockProvenanceRepository = MockProvenanceRepository.GetProvenanceRepository();

            mockUow.Setup(r => r.SaveAsync()).ReturnsAsync(1);
            mockUow.Setup(r => r.AssetRepository).Returns(mockAssetRepository.Object);
            mockUow.Setup(r => r.UserRepository).Returns(mockAppUserRepository.Object);
            mockUow.Setup(r => r.ProvenanceRepository).Returns(mockProvenanceRepository.Object);

            return mockUow;

        }

    }
}
