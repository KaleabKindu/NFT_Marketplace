using System.Globalization;
using Application.Contracts.Persistance;
using Domain.Offers;
using Moq;

namespace Application.UnitTest.Mocks
{
    public static class MockOfferRepository
    {

        public static Mock<IOfferRepository> GetOfferRepository()
        {


            var offers = new List<Offer>
            {
                new() {
                    Id = 1,
                    Amount=100,
                    Asset = "asset",
                    CreatedAt =  DateTime.ParseExact("20/02/2023 00:00", "dd/MM/yyyy hh:mm", CultureInfo.InvariantCulture),
                },
                new() {
                    Id = 2,
                    Amount=100,
                    Asset = "asset",
                    CreatedAt =  DateTime.ParseExact("20/02/2023 00:00", "dd/MM/yyyy hh:mm", CultureInfo.InvariantCulture),
                },

            };


            var mockRepo = new Mock<IOfferRepository>();

            mockRepo.Setup(r => r.GetAllAsync(1, 10)).ReturnsAsync(offers);

            mockRepo.Setup(r => r.AddAsync(It.IsAny<Offer>())).ReturnsAsync((Offer offer) =>
            {
                offer.Id = offers.Count() + 1;
                offers.Add(offer);
                return offer;
            });

            mockRepo.Setup(r => r.UpdateAsync(It.IsAny<Offer>())).Callback((Offer offer) =>
            {
                var newoffers = offers.Where((r) => r.Id != offer.Id);
                offers = newoffers.ToList();
                offers.Add(offer);
            });

            mockRepo.Setup(r => r.DeleteAsync(It.IsAny<Offer>())).Callback((Offer offer) =>
            {
                offers.Remove(offer);
            });

            mockRepo.Setup(r => r.Exists(It.IsAny<long>())).ReturnsAsync((long Id) =>
            {
                var offer = offers.FirstOrDefault((r) => r.Id == Id);
                return offer != null;
            });


            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<long>())).ReturnsAsync((long Id) =>
            {
                return offers.FirstOrDefault((r) => r.Id == Id);
            });

            return mockRepo;

        }

    }
}
