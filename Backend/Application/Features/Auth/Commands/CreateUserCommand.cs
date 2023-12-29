using Domain;
using ErrorOr;
using MediatR;
using AutoMapper;
using Application.Contracts.Persistance;
using Application.Common.Exceptions;
using Application.Common.Responses;
using Application.Features.Auth.Dtos;
using Microsoft.EntityFrameworkCore;
using Application.Common.Errors;

namespace Application.Features.Auth.Commands
{
    public class CreateUserCommand : IRequest<ErrorOr<BaseResponse<UserDto>>>
    {
        public string PublicAddress { get; set; }
    }

    public class CreateUserCommandHandler
        : IRequestHandler<CreateUserCommand, ErrorOr<BaseResponse<UserDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<BaseResponse<UserDto>>> Handle(
            CreateUserCommand command,
            CancellationToken cancellationToken
        )
        {
            if (await _unitOfWork.UserManager.Users.AnyAsync(user => user.PublicAddress == command.PublicAddress))
            {
                return ErrorFactory.Conflict("User");
            }

            var user = new AppUser
            {
                PublicAddress = command.PublicAddress,
                Nonce = Guid.NewGuid().ToString()
            };

            var result = await _unitOfWork.UserManager.CreateAsync(user);
            if (result.Succeeded)
                throw new DbAccessException("Unable to save user to database");

            return new BaseResponse<UserDto>(){
                Message="User created successfully",
                Value=_mapper.Map<UserDto>(user)
            };
        }
    }
}
