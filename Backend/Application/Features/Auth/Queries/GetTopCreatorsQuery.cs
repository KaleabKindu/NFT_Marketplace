
using Application.Contracts.Persistance;
using Application.Features.Auth.Dtos;
using AutoMapper;
using ErrorOr;
using MediatR;


namespace Application.Features.Auth.Queries
{
    public class GetTopCreatorsQuery : IRequest<ErrorOr<List<UserListDto>>>
    {

    }

    public class GetTopCreatorsQueryHandler : IRequestHandler<GetTopCreatorsQuery, ErrorOr<List<UserListDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTopCreatorsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<List<UserListDto>>> Handle(
            GetTopCreatorsQuery query,
            CancellationToken cancellationToken
        )
        {

            var topCreators = await _unitOfWork.UserRepository.GetTopCreators();

            return _mapper.Map<List<UserListDto>>(topCreators);

        }
    }
}