using ErrorOr;
using MediatR;
using Application.Contracts.Persistance;
using Application.Common.Responses;
using Application.Features.Auth.Dtos;
using Application.Common.Errors;
using Application.Common.Exceptions;

namespace Application.Features.Auth.Commands
{
    public class AuthenticateUserCommand : IRequest<ErrorOr<BaseResponse<TokenDto>>>
    {
        public string PublicAddress { get; set; }
        public string SignedNonce { get; set; }
    }

    public class AuthenticateUserCommandHandler
        : IRequestHandler<AuthenticateUserCommand, ErrorOr<BaseResponse<TokenDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticateUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<BaseResponse<TokenDto>>> Handle(
            AuthenticateUserCommand command,
            CancellationToken cancellationToken
        )
        {
            try{
                TokenDto tokenInfo = await _unitOfWork.UserRepository.AuthenticateUserAsync(command.PublicAddress, command.SignedNonce);

                return new BaseResponse<TokenDto>(){
                    Message="User authenticated successfully",
                    Value= tokenInfo
                };
            }catch(NotFoundException exception){
                return ErrorFactory.NotFound("User", exception.Message);
            }catch(EthereumVerificationException exception){
                return ErrorFactory.BadRequestError("User", exception.Message);
            }
        }
    }
}
