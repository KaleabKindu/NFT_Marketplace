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
    public class UpdateOfferCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockRepo;
        private readonly UpdateOfferDto _offerDto;
        private readonly UpdateOfferCommandHandler _handler;
        public UpdateOfferCommandHandlerTest()
        {
            _mockRepo = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _offerDto = new UpdateOfferDto
            {
                Id = 1,
                Amount = 300,
            };

            _handler = new UpdateOfferCommandHandler(_mockRepo.Object, _mapper);

        }


        [Fact]
        public async Task UpdateOffer()
        {
            var result = await _handler.Handle(new UpdateOfferCommand() { Offer = _offerDto }, CancellationToken.None);
            result.IsError.ShouldBe(false);

            var Offer = await _mockRepo.Object.OfferRepository.GetByIdAsync(_offerDto.Id);
            Offer.Id.Equals(_offerDto.Id);
            Offer.Amount.Equals(_offerDto.Amount);
        }

        [Fact]
        public async Task Update_With_Invalid_OfferNO()
        {
            _offerDto.Id = -1;
            
            var result =   await _handler.Handle(new UpdateOfferCommand() { Offer = _offerDto }, CancellationToken.None);

            result.IsError.ShouldBeTrue();
            result.Errors.ShouldNotBeEmpty();
        }


    }
}



