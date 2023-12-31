using ErrorOr;
using MediatR;
using Application.Contracts.Persistance;
using Application.Common.Errors;
using Application.Common.Responses;
using Application.Features.Auth.Dtos;
using Application.Common.Exceptions;

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

        public GetUserNonceQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<BaseResponse<NonceDto>>> Handle(
            GetUserNonce query,
            CancellationToken cancellationToken
        )
        {   
            try{
                string nonce = await  _unitOfWork.UserRepository.GetUserNonceAsync(query.PublicAddress);
                return new BaseResponse<NonceDto>(){
                    Message="User nonce fetched successfully",
                    Value=new NonceDto(){
                        Nonce=nonce
                    }
                };
            }catch(NotFoundException exception){
                return ErrorFactory.NotFound("User", exception.Message);
            }   
        }
    }
}
