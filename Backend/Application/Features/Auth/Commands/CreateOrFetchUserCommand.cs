using ErrorOr;
using MediatR;
using AutoMapper;
using Application.Common.Responses;
using Application.Features.Auth.Dtos;
using Application.Contracts.Persistance;

namespace Application.Features.Auth.Commands
{
    public class CreateOrFetchUserCommand : IRequest<ErrorOr<BaseResponse<UserDto>>>
    {
        public string PublicAddress { get; set; }
    }

    public class CreateOrFetchUserCommandHandler
        : IRequestHandler<CreateOrFetchUserCommand, ErrorOr<BaseResponse<UserDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateOrFetchUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<BaseResponse<UserDto>>> Handle(
            CreateOrFetchUserCommand command,
            CancellationToken cancellationToken
        )
        {
            var user = await _unitOfWork.UserRepository.CreateOrFetchUserAsync(command.PublicAddress);
            return new BaseResponse<UserDto>(){
                Message="User created successfully",
                Value=_mapper.Map<UserDto>(user)
            };
        }
    }
}
