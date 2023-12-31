using ErrorOr;
using MediatR;
using AutoMapper;
using Application.Contracts.Persistance;
using Application.Common.Responses;
using Application.Features.Auth.Dtos;
using Microsoft.EntityFrameworkCore;
using Application.Common.Errors;
using Application.Contracts.Services;
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
        private readonly IJwtService _jwtService;
        private readonly IEthereumCryptoService _ethereumService;
        private readonly IMapper _mapper;

        public AuthenticateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IJwtService jwtService, IEthereumCryptoService ethereumService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtService = jwtService;
            _ethereumService = ethereumService;
        }

        public async Task<ErrorOr<BaseResponse<TokenDto>>> Handle(
            AuthenticateUserCommand command,
            CancellationToken cancellationToken
        )
        {

            var user = await _unitOfWork.UserManager.Users.FirstOrDefaultAsync(user => user.PublicAddress == command.PublicAddress);
            var roles = await _unitOfWork.UserManager.GetRolesAsync(user);
            if (user == null)
            {
                return ErrorFactory.NotFound("User");
            }

            bool isSignatureValid = _ethereumService.VerifyMessage(user.Nonce, command.SignedNonce, command.PublicAddress);
            
            if (!isSignatureValid){
                var result = await _unitOfWork.UserManager.DeleteAsync(user);    
                if (!result.Succeeded) 
                    throw new DbAccessException("Unable to delete user nonce");
                return ErrorFactory.BadRequestError("User", "Invalid signed nonce");
            }

            user.Nonce = Guid.NewGuid().ToString();
            if (await _unitOfWork.SaveAsync() == 0) 
                throw new DbAccessException("Unable to update user nonce");
            var tokenInfo = _jwtService.GenerateToken(user, roles);

            return new BaseResponse<TokenDto>(){
                Message="User authenticated successfully",
                Value= new TokenDto(){
                    AccessToken = tokenInfo.Item1,
                    ExpireInDays = Math.Round(tokenInfo.Item2 / (60 * 24), 2)
                }
            };
        }
    }
}
