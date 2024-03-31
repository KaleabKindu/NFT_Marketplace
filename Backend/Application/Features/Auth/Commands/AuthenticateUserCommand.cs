using ErrorOr;
using MediatR;
using Application.Common.Responses;
using Application.Features.Auth.Dtos;
using Application.Contracts.Persistance;

namespace Application.Features.Auth.Commands
{
    public class AuthenticateUserCommand : IRequest<ErrorOr<BaseResponse<TokenDto>>>
    {
        public string Address { get; set; }
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
            ErrorOr<TokenDto> result = await _unitOfWork.UserRepository.AuthenticateUserAsync(command.Address, command.SignedNonce);
            if(result.IsError)
                return ErrorOr<BaseResponse<TokenDto>>.From(result.Errors);

            return new BaseResponse<TokenDto>(){
                Message="User authenticated successfully",
                Value= result.Value
            };
        }
    }
}
