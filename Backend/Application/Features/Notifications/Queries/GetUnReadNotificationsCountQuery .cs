using Application.Contracts.Persistance;
using AutoMapper;
using MediatR;
using ErrorOr;


namespace Application.Features.Notifications.Queries;

public class GetUnreadNotificationsCountQuery : IRequest<ErrorOr<int>>
{
    public string UserId { get; set; }
}

public class GetUnreadNotificationsCountQueryHandler : IRequestHandler<GetUnreadNotificationsCountQuery, ErrorOr<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetUnreadNotificationsCountQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<int>> Handle(GetUnreadNotificationsCountQuery request,
        CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.NotificationRepository.GetUnReadNotificationsCount(request.UserId);

        return response;
    }
}