using Application.Common.Errors;
using Application.Common.Exceptions;
using Application.Common.Responses;
using Application.Contracts.Persistance;
using Application.Features.Assets.Dtos;
using AutoMapper;
using Domain.Assets;
using Domain.Auctions;
using ErrorOr;
using MediatR;

namespace Application.Features.Assets.Command
{
    public class CreateAssetCommand : IRequest<ErrorOr<BaseResponse<long>>>
    {
        public CreateAssetDto CreateAssetDto { get; set; }
        public string PublicAddress {get; set;}

        
    }

    public class CreateAssetCommandHandler : IRequestHandler<CreateAssetCommand, ErrorOr<BaseResponse<long>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public CreateAssetCommandHandler(IMapper mapper, IUnitOfWork unitOfwork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfwork;

        }

        public async Task<ErrorOr<BaseResponse<long>>> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<long>();

            var user = await _unitOfWork.UserRepository.GetUserByPublicAddress(request.PublicAddress);

            if (user == null)
                return ErrorFactory.NotFound(nameof(user), "user not found");
            var asset = _mapper.Map<Asset>(request.CreateAssetDto);
            asset.Creator = user;
            asset.Owner = user;
            var auction = new Auction{
                TokenId = request.CreateAssetDto.TokenId,
                
            };

            await _unitOfWork.AssetRepository.AddAsync(asset);


            if (await _unitOfWork.SaveAsync() == 0)
                throw new DbAccessException("Database Error: Unable To Save");

            response.Message = "Creation Succesful";
            response.Value = asset.Id;

            return response;
        }
    }

    
}