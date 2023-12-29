using ErrorOr;
using MediatR;
using AutoMapper;
using Application.Contracts.Persistance;
using Application.Common.Errors;
using Application.Common.Responses;
using Application.Features.Auth.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth.Queries
{
    public class GetUserNonce : IRequest<ErrorOr<BaseResponse<NonceDto>>>
    {
        public string PublicAddress { get; set; }
    }

    public class GetUserNonceQueryHandler
        : IRequestHandler<GetUserNonce, ErrorOr<BaseResponse<NonceDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserNonceQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<BaseResponse<NonceDto>>> Handle(
            GetUserNonce query,
            CancellationToken cancellationToken
        )
        {
            var user = await _unitOfWork.UserManager.Users.FirstOrDefaultAsync(user => user.PublicAddress == query.PublicAddress);

            if (user == null) return ErrorFactory.NotFound("User");
            
            return new BaseResponse<NonceDto>(){
                Message="User nonce fetched successfully",
                Value=new NonceDto(){
                    Nonce=user.Nonce
                }
            };
        }
    }
}
