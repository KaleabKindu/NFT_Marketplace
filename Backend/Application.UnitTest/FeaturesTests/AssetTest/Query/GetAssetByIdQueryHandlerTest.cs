using ErrorOr;
using Application.Contracts.Persistance;
using Application.Features.Assets.Query;
using Application.Profiles;
using Application.UnitTest.Mocks;
using AutoMapper;
using Moq;
using Shouldly;
using Application.Common.Responses;
using Application.Features.Assets.Dtos;

namespace ApplicationUnitTest.FeaturesTests.AssetTest.Query
{
    public class GetAssetByIdQueryHandlerTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly GetAssetByIdQueryHandler _handler;

        public GetAssetByIdQueryHandlerTest()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapper = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapper.CreateMapper();

            _handler = new GetAssetByIdQueryHandler(_mapper, _mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetValidAsset()
        {
            var result = await _handler.Handle(
                new GetAssetByIdQuery() { Id = 1 },
                CancellationToken.None
            );

            result.ShouldBeOfType<ErrorOr<BaseResponse<AssetDetailDto>>>();
        }

        [Fact]
        public async Task GetInValidAsset()
        {
            var result = await _handler.Handle(
                new GetAssetByIdQuery() { Id = -1 },
                CancellationToken.None
            );
            result.ShouldBeOfType<ErrorOr<BaseResponse<AssetDetailDto>>>();
            result.Value.Value.ShouldBeNull();
        }
    }
}