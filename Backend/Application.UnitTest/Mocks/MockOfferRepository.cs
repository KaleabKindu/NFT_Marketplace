using System.Globalization;
using Application.Contracts.Persistance;
using Domain;
using Moq;

namespace Application.UnitTest.Mocks
{
    public static class MockOfferRepository
    {

        public static Mock<IOfferRepository> GetOfferRepository()
        {


            var Offers = new List<Offer>
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

            mockRepo.Setup(r => r.GetAllAsync(1, 10)).ReturnsAsync(Offers);

            mockRepo.Setup(r => r.AddAsync(It.IsAny<Offer>())).ReturnsAsync((Offer offer) =>
            {
                offer.Id = Offers.Count() + 1;
                Offers.Add(offer);
                return offer;
            });

            mockRepo.Setup(r => r.UpdateAsync(It.IsAny<Offer>())).Callback((Offer offer) =>
            {
                var newOffers = Offers.Where((r) => r.Id != offer.Id);
                Offers = newOffers.ToList();
                Offers.Add(offer);
            });

            mockRepo.Setup(r => r.DeleteAsync(It.IsAny<Offer>())).Callback((Offer offer) =>
            {
                Offers.Remove(offer);
            });

            mockRepo.Setup(r => r.Exists(It.IsAny<long>())).ReturnsAsync((long Id) =>
            {
                var offer = Offers.FirstOrDefault((r) => r.Id == Id);
                return offer != null;
            });


            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<long>())).ReturnsAsync((long Id) =>
            {
                return Offers.FirstOrDefault((r) => r.Id == Id);
            });

            return mockRepo;

        }

    }
}
