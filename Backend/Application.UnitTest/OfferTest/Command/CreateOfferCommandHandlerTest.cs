using Application.Common.Responses;
using Application.Contracts.Persistance;
using Application.Features.Offers.Commands;
using Application.Features.Offers.Dtos;
using Application.Profiles;
using Application.UnitTest.Mocks;
using AutoMapper;
using Moq;
using Shouldly;

namespace Application.UnitTest.Offertest.Command
{
    public class CreateOfferCommandHandlerTest
    {

        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockRepo;
        private readonly CreateOfferDto _offerDto;
        private readonly CreateOfferCommandHandler _handler;
        // private readonly IUserAccessor _userAccessor;

        public CreateOfferCommandHandlerTest()
        {
            _mockRepo = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _offerDto = new CreateOfferDto
            {
                Amount = 300,
                Asset = "asset",
            };

            _handler = new CreateOfferCommandHandler(_mockRepo.Object, _mapper);

        }


        [Fact]
        public async Task CreateOffer()
        {
            var result = await _handler.Handle(new CreateOfferCommand() { Offer = _offerDto }, CancellationToken.None);
            result.IsError.ShouldBeFalse();
            result.Value.ShouldBeOfType<BaseResponse<OfferDto>>();

            var offers = await _mockRepo.Object.OfferRepository.GetAllAsync();
            offers.Count().ShouldBe(3);

        }

        [Fact]
        public async Task InvalidOffer_Added()
        {
            _offerDto.Amount = -10;
            var result =  await _handler.Handle(new CreateOfferCommand() { Offer = _offerDto }, CancellationToken.None);
            result.IsError.ShouldBeTrue();
            result.Errors.ShouldNotBeEmpty();

            var offers = await _mockRepo.Object.OfferRepository.GetAllAsync();
            offers.Count().ShouldBe(2);
        }
    }
}




