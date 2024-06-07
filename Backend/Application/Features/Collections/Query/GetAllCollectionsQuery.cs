using MediatR;
using ErrorOr;
using AutoMapper;
using Application.Responses;
using Application.Features.Common;
using Application.Contracts.Persistance;
using Application.Features.Collections.Dtos;
using Domain.Assets;

namespace Application.Features.Assets.Query
{
    public class GetAllCollectionsQuery : PaginatedQuery, IRequest<ErrorOr<PaginatedResponse<CollectionsListDto>>>
    {
        public string Creator { get; set; }
        public string Query { get; set; }
        public double MinVolume { get; set; }
        public double MaxVolume { get; set; }
        public string SortBy { get; set; }
    }


    public class GetAllCollectionsQueryHandler : IRequestHandler<GetAllCollectionsQuery, ErrorOr<PaginatedResponse<CollectionsListDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCollectionsQueryHandler(IMapper mapper, IUnitOfWork unitOfwork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfwork;

        }

        public async Task<ErrorOr<PaginatedResponse<CollectionsListDto>>> Handle(GetAllCollectionsQuery query, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.CollectionRepository.GetAllAsync(query.Creator, query.Query, query.MinVolume, query.MaxVolume, query.SortBy, query.PageNumber, query.PageSize);

            var response = new PaginatedResponse<CollectionsListDto>
            {
                Message = "Collections Fetched Succesfully",
                Value = _mapper.Map<List<CollectionsListDto>>(result.Item2),
                Count = result.Item1,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize
            };

            return response;


        }
    }
}