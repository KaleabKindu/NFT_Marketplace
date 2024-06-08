using ErrorOr;
using MediatR;
using Application.Common.Errors;
using Application.Common.Responses;
using Application.Common.Exceptions;
using Application.Contracts.Persistance;

namespace Application.Features.Auth.Commands
{
    public class CreateUserNetworkCommand : IRequest<ErrorOr<BaseResponse<Unit>>>
    {
        public string Follower { get; set; }
        public string Followee { get; set; }
    }

    public class CreateUserNetworkCommandHandler
        : IRequestHandler<CreateUserNetworkCommand, ErrorOr<BaseResponse<Unit>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserNetworkCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<BaseResponse<Unit>>> Handle(
            CreateUserNetworkCommand command,
            CancellationToken cancellationToken
        )
        {
            bool success = await _unitOfWork.UserRepository.CreateNetwork(command.Follower, command.Followee);
            if(!success)
                return ErrorFactory.BadRequestError("User", "User network already exists");

            if (await _unitOfWork.SaveAsync() == 0)
                throw new DbAccessException("Unable to save to database");

            return new BaseResponse<Unit>(){
                Message="User network created successfully",
                Value=Unit.Value
            };
        }
    }
}
