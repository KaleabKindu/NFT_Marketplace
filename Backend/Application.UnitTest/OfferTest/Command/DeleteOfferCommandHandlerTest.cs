using AutoMapper;
using MediatR;
using Moq;
using Shouldly;
using Application.UnitTest.Mocks;
using Application.Contracts.Persistance;
using Application.Features.Offers.Dtos;
using Application.Features.Offers.Commands;
using Application.Common.Exceptions;
using System.Globalization;
using Application.Profiles;

namespace Application.UnitTest.Offertest.Command
{
    public class DeleteOfferCommandHandlerTest
    {

        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockRepo;
        private long _id { get; set; }
        private readonly DeleteOfferCommandHandler _handler;
        // private readonly CreateOfferDto _offerDto;
        public DeleteOfferCommandHandlerTest()
        {
            _mockRepo = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _id = 2;

            _handler = new DeleteOfferCommandHandler(_mockRepo.Object);

        }


        [Fact]
        public async Task DeleteOffer()
        {

            var result = await _handler.Handle(new DeleteOfferCommand() { Id = _id }, CancellationToken.None);
            result.IsError.ShouldBeFalse();
            result.Value.ShouldBeOfType<Unit>();

            var Offers = await _mockRepo.Object.OfferRepository.GetAllAsync();
            Offers.Count().ShouldBe(1);
        }

        [Fact]
        public async Task Delete_Offer_Doesnt_Exist()
        {

            _id = 0;
            var result =  await _handler.Handle(new DeleteOfferCommand() { Id = _id }, CancellationToken.None);

            result.IsError.ShouldBeTrue();
            result.Errors.ShouldNotBeEmpty();

            var offers = await _mockRepo.Object.OfferRepository.GetAllAsync();
            offers.Count().ShouldBe(2);

        }
    }
}



