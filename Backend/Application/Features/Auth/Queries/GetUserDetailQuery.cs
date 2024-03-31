using Application.Common.Responses;
using Application.Contracts.Persistance;
using Application.Features.Auth.Dtos;
using AutoMapper;
using ErrorOr;
using MediatR;
using Nethereum.Contracts.Standards.ENS.PublicResolver.ContractDefinition;
using Application.Common.Errors;

namespace Application.Features.Auth.Queries
{

public class GetUserDetailQuery : IRequest<ErrorOr<BaseResponse<UserDetailDto>>>
{
    public string publicAddress {get; set;}
}

public class GetUserDetailQueryHandler: IRequestHandler<GetUserDetailQuery, ErrorOr<BaseResponse<UserDetailDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<BaseResponse<UserDetailDto>>> Handle(
            GetUserDetailQuery query,
            CancellationToken cancellationToken
        )    
        {
            var User = await _unitOfWork.UserRepository.GetUserByAddress(query.publicAddress);

            if (User == null) return ErrorFactory.NotFound("User", "User not found");

            return new BaseResponse<UserDetailDto>(){
                Message="Bid details fetched successfully",
                Value=_mapper.Map<UserDetailDto>(User)
            };
        }
        }
}
