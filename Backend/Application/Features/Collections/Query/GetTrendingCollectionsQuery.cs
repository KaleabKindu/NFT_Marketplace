using MediatR;
using ErrorOr;
using AutoMapper;
using Application.Responses;
using Application.Features.Common;
using Application.Contracts.Persistance;
using Application.Features.Collections.Dtos;

namespace Application.Features.Assets.Query
{
    public class GetTrendingCollectionsQuery : PaginatedQuery, IRequest<ErrorOr<PaginatedResponse<TrendingCollectionsDto>>>
    {
    }


    public class GetTrendingCollectionsQueryHandler : IRequestHandler<GetTrendingCollectionsQuery, ErrorOr<PaginatedResponse<TrendingCollectionsDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetTrendingCollectionsQueryHandler(IMapper mapper, IUnitOfWork unitOfwork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfwork;

        }

        public async Task<ErrorOr<PaginatedResponse<TrendingCollectionsDto>>> Handle(GetTrendingCollectionsQuery query, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.CollectionRepository.GetTrendingAsync(query.PageNumber, query.PageSize);

            if (result.IsError) return result.Errors;

            var response = new PaginatedResponse<TrendingCollectionsDto>
            {
                Message = "Collections Fetched Succesfully",
                Value = _mapper.Map<List<TrendingCollectionsDto>>(result.Value.Item2),
                Count = result.Value.Item1,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize
            };

            return response;


        }
    }
}