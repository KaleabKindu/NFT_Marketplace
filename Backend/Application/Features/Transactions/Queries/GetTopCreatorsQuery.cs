using Application.Features.Transactions.Dtos;
using Application.Contracts.Persistance;
using AutoMapper;
using ErrorOr;
using MediatR;
using Application.Responses;

namespace Application.Features.Transactions.Queries
{
    public class GetTopCreatorsQuery : IRequest<ErrorOr<PaginatedResponse<TopCreatorDto>>>{

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetTopCreatorsQueryHandler : IRequestHandler<GetTopCreatorsQuery, ErrorOr<PaginatedResponse<TopCreatorDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTopCreatorsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<PaginatedResponse<TopCreatorDto>>> Handle(
            GetTopCreatorsQuery request,
            CancellationToken cancellationToken
        )
        {
        var topCreators = await _unitOfWork.TransactionRepository.GetCreatorSalesVolumeAsync(request.PageNumber, request.PageSize);

        // Map the data to the desired DTO structure
    var topCreatorDtos = topCreators.Select(kv => new TopCreatorDto
        {
            username = kv.Key.UserName,
            background = kv.Key.ProfileBackgroundImage,
            avatar = kv.Key.Avatar,
            address = kv.Key.Address,
            sales = kv.Value.ToString(), 
            following = false    // needs to be impelemted( check current user follows this creator)
        }).ToList();

        // Construct the response body
        var response = new PaginatedResponse<TopCreatorDto>()
        {
            Message="Top creator lists fetched successfully",
            Count = topCreators.Count(),
            Value = topCreatorDtos
        };

        return response;
    }
    }
}