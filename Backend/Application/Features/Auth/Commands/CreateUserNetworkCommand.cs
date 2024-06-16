using ErrorOr;
using MediatR;
using Application.Common.Errors;
using Application.Common.Responses;
using Application.Common.Exceptions;
using Application.Contracts.Persistance;
using Application.Contracts.Services;
using Application.Features.Notifications.Dtos;

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
        private readonly INotificationService _notificationService;

        public CreateUserNetworkCommandHandler(IUnitOfWork unitOfWork, INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
        }

        public async Task<ErrorOr<BaseResponse<Unit>>> Handle(
            CreateUserNetworkCommand command,
            CancellationToken cancellationToken
        )
        {
            bool success = await _unitOfWork.UserRepository.CreateNetwork(command.Follower, command.Followee);
            if (!success)
                return ErrorFactory.BadRequestError("User", "User network already exists");

            if (await _unitOfWork.SaveAsync() == 0)
                throw new DbAccessException("Unable to save to database");

            var follower = await _unitOfWork.UserRepository.GetUserByAddress(command.Follower);
            var followee = await _unitOfWork.UserRepository.GetUserByAddress(command.Followee);

            var notification = new CreateNotificationDto
            {
                UserId = followee.Id,
                Title = "New Follower",
                Content = $"{follower.Profile.UserName} started following you",
            };
            await _notificationService.SendNotification(notification);

            return new BaseResponse<Unit>()
            {
                Message = "User network created successfully",
                Value = Unit.Value
            };
        }
    }
}
