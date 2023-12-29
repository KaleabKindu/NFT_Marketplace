using Domain;
using ErrorOr;
using MediatR;
using AutoMapper;
using System.Security.Cryptography;
using Application.Contracts.Persistance;
using Application.Common.Responses;
using Application.Features.Auth.Dtos;
using Microsoft.EntityFrameworkCore;
using Application.Common.Errors;
using Application.Contracts.Services;
using System.Text;
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
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthenticateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        private RSAParameters GetPublicKeyParameters(string publicKeyString)
        {
            byte[] publicKeyBytes = Convert.FromBase64String(publicKeyString);
            using (RSA rsa = RSA.Create())
            {
                rsa.ImportSubjectPublicKeyInfo(publicKeyBytes, out _);
                return rsa.ExportParameters(false); // false indicates exporting public key
            }
        }

        private bool VerifySignature(string originalNonce, string signedNonce, RSAParameters publicKeyParameters)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(publicKeyParameters);

                byte[] originalNonceBytes = Encoding.UTF8.GetBytes(originalNonce);
                byte[] signedNonceBytes = Convert.FromBase64String(signedNonce);

                using (SHA256 sha256 = SHA256.Create())
                {
                    return rsa.VerifyData(originalNonceBytes, sha256, signedNonceBytes);
                }
            }
        }

        public async Task<ErrorOr<BaseResponse<TokenDto>>> Handle(
            AuthenticateUserCommand command,
            CancellationToken cancellationToken
        )
        {

            var user = await _unitOfWork.UserManager.Users.FirstOrDefaultAsync(user => user.PublicAddress == command.PublicAddress);
            if (user == null)
            {
                return ErrorFactory.NotFound("User");
            }
            
            RSAParameters publicKeyParameters = GetPublicKeyParameters(command.PublicAddress);
            bool isSignatureValid = VerifySignature(user.Nonce, command.SignedNonce, publicKeyParameters);
            
            if (!isSignatureValid){
                var result = await _unitOfWork.UserManager.DeleteAsync(user);    
                if (!result.Succeeded) 
                    throw new DbAccessException("Unable to delete user nonce");
                return ErrorFactory.BadRequestError("User", "Invalid signed nonce");
            }

            user.Nonce = Guid.NewGuid().ToString();
            if (await _unitOfWork.SaveAsync() == 0) 
                throw new DbAccessException("Unable to update user nonce");

            return new BaseResponse<TokenDto>(){
                Message="User authenticated successfully",
                Value= new TokenDto(){
                    AccessToken = _tokenService.CreateToken(user, 15),
                    ExpireInDays = 15
                }
            };
        }
    }
}
