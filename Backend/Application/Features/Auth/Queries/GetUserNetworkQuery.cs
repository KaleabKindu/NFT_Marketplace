
using Application.Common.Errors;
using Application.Contracts.Persistance;
using Application.Features.Auth.Dtos;
using Application.Features.Common;
using Application.Responses;
using AutoMapper;
using Domain;
using ErrorOr;
using MediatR;


namespace Application.Features.Auth.Queries
{
    public class GetUserNetwork : PaginatedQuery ,IRequest<ErrorOr<PaginatedResponse<UserNetworkDto>>>
    {
        public string CurrentUserAddress { get; set; }
        public string Address { get; set; }
        public string Type {get; set;}
    }

    public class GetUserNetworkHandler : IRequestHandler<GetUserNetwork, ErrorOr<PaginatedResponse<UserNetworkDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserNetworkHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<PaginatedResponse<UserNetworkDto>>> Handle(
            GetUserNetwork query,
            CancellationToken cancellationToken
        )
        {
            query.Type = query.Type.ToLowerInvariant();
            if (query.Type != "following" && query.Type != "follower")
                return ErrorFactory.BadRequestError("User", "type query parameter must be either following or follower");

            PaginatedResponse<AppUser> network;
            if(query.Type == "follower"){
                network= await _unitOfWork.UserRepository.GetFollowersAsync(query.Address, query.PageNumber, query.PageSize);
            }else{
                network= await _unitOfWork.UserRepository.GetFollowingsAsync(query.Address, query.PageNumber, query.PageSize);
            }

            List<UserNetworkDto> networkDtos = _mapper.Map<List<UserNetworkDto>>(network.Value);
            foreach (UserNetworkDto nwkDto in networkDtos){
                nwkDto.Following = await _unitOfWork.UserRepository.IsFollowing(query.CurrentUserAddress, nwkDto.Address);
            }
            
            return new PaginatedResponse<UserNetworkDto>()
            {
                Count = network.Count,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
                Message = "User's network fetched successfully",
                Value = _mapper.Map<List<UserNetworkDto>>(network.Value)
            };
        }
    }
}