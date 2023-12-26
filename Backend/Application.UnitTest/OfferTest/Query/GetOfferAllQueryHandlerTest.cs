using Application.Contracts.Persistance;
using Application.Features.Offers.Queries;
using Application.Features.Offers.Dtos;
using Application.Profiles;
using Application.UnitTest.Mocks;
using AutoMapper;
using Moq;
using Shouldly;

namespace Application.UnitTest.Offertest.Query
{
    public class GetOfferListQueryHandlerTest
    {


        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockRepo;
        private readonly GetAllOfferQueryHandler _handler;
        public GetOfferListQueryHandlerTest()
        {
            _mockRepo = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _handler = new GetAllOfferQueryHandler(_mockRepo.Object, _mapper);

        }


        [Fact]
        public async Task GetOfferList()
        {
            var result = await _handler.Handle(new GetAllOfferQuery(), CancellationToken.None);
            result.Value.ShouldBeOfType<List<OfferDto>>();
            result.Value.Count().ShouldBe(2);
        }
    }
}



