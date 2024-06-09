
using Application.Common.Responses;
using Application.Contracts.Persistance;
using Application.Features.Auth.Dtos;
using Application.Features.Common;
using Application.Responses;
using AutoMapper;
using ErrorOr;
using MediatR;


namespace Application.Features.Auth.Queries
{
    public class GetUsersQuery : PaginatedQuery ,IRequest<ErrorOr<PaginatedResponse<UserListDto>>>
    {
        public string CurrentAddress { get; set; }
    }

    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, ErrorOr<PaginatedResponse<UserListDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<PaginatedResponse<UserListDto>>> Handle(
            GetUsersQuery command,
            CancellationToken cancellationToken
        )
        {
            var users = await _unitOfWork.UserRepository.GetAllUsersAsync(command.PageNumber, command.PageSize);
            
            List<UserListDto> dtos = _mapper.Map<List<UserListDto>>(users.Value);
            foreach (var dto in dtos)
                dto.Following = await _unitOfWork.UserRepository.IsFollowing(command.CurrentAddress, dto.Address);

            return new PaginatedResponse<UserListDto>()
            {
                Count = users.Count,
                PageNumber = command.PageNumber,
                PageSize = command.PageSize,
                Message = "Users fetched successfully",
                Value = dtos
            };
        }
    }
}