using Application.Contracts.Persistance;
using Application.Features.Common;
using Application.Features.Notifications.Dtos;
using Application.Responses;
using AutoMapper;
using MediatR;
using ErrorOr;


namespace Application.Features.Notifications.Queries;

public class GetNotificationsQuery : PaginatedQuery, IRequest<ErrorOr<PaginatedResponse<NotificationDto>>>
{
    public string UserId { get; set; }
}

public class GetNotificationsQueryHandler : IRequestHandler<GetNotificationsQuery, ErrorOr<PaginatedResponse<NotificationDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetNotificationsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ErrorOr<PaginatedResponse<NotificationDto>>> Handle(GetNotificationsQuery request,
        CancellationToken cancellationToken)
    {
        var notifications = await _unitOfWork.NotificationRepository.GetNotifications(request.UserId, request.PageNumber, request.PageSize);

        return new PaginatedResponse<NotificationDto>
        {
            Count = notifications.Count,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            Value = _mapper.Map<List<NotificationDto>>(notifications.Value),
            Message = "Fetch Successful",
        };
    }
}