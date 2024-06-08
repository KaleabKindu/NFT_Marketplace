using ErrorOr;
using MediatR;
using Application.Common.Errors;
using Application.Common.Responses;
using Application.Common.Exceptions;
using Application.Contracts.Persistance;

namespace Application.Features.Auth.Commands
{
    public class RemoveUserNetworkCommand : IRequest<ErrorOr<BaseResponse<Unit>>>
    {
        public string Follower { get; set; }
        public string Followee { get; set; }
    }

    public class RemoveUserNetworkCommandHandler
        : IRequestHandler<RemoveUserNetworkCommand, ErrorOr<BaseResponse<Unit>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveUserNetworkCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<BaseResponse<Unit>>> Handle(
            RemoveUserNetworkCommand command,
            CancellationToken cancellationToken
        )
        {
            bool success = await _unitOfWork.UserRepository.RemoveNetwork(command.Follower, command.Followee);
            if(!success)
                return ErrorFactory.BadRequestError("User", "No outgoing network exists");
                
            if (await _unitOfWork.SaveAsync() == 0)
                throw new DbAccessException("Unable to save to database");

            return new BaseResponse<Unit>(){
                Message="User network removed successfully",
                Value=Unit.Value
            };
        }
    }
}
