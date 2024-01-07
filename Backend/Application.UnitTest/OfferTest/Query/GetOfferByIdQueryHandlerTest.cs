using Application.Contracts.Persistance;
using Application.Features.Offers.Queries;
using Application.Features.Offers.Dtos;
using Application.Profiles;
using Application.UnitTest.Mocks;
using AutoMapper;
using Moq;
using Shouldly;
using Application.Common.Responses;

namespace Application.UnitTest.Offertest.Query
{
    public class GetOfferByIdQueryHandlerTest
    {


        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockRepo;
        private long Id { get; set; }
        private readonly GetOfferByIdQueryHandler _handler;
        public GetOfferByIdQueryHandlerTest()
        {
            _mockRepo = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            Id = 1;

            _handler = new GetOfferByIdQueryHandler(_mockRepo.Object, _mapper);

        }


        [Fact]
        public async Task GetOfferById()
        {
            var result = await _handler.Handle(new GetOfferByIdQuery() { Id = Id }, CancellationToken.None);
            result.IsError.ShouldBeFalse();
            result.Value.ShouldBeOfType<BaseResponse<OfferDto>>();
        }

        [Fact]
        public async Task GetNonExistingOffer()
        {
            Id = 0;
            var result = await _handler.Handle(new GetOfferByIdQuery() { Id = Id }, CancellationToken.None);
            result.IsError.ShouldBe(true);

        }
    }
}