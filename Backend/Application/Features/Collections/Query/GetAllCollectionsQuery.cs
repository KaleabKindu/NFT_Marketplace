using MediatR;
using ErrorOr;
using AutoMapper;
using Application.Responses;
using Application.Features.Common;
using Application.Contracts.Persistance;
using Application.Features.Collections.Dtos;

namespace Application.Features.Assets.Query
{
    public class GetAllCollectionsQuery : PaginatedQuery, IRequest<ErrorOr<PaginatedResponse<CollectionsListDto>>>
    {
        public string Category { get; set; }
        public double MinFloorPrice { get; set; }
        public double MaxFloorPrice { get; set; }
        public string Creator { get; set; }
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
            var result = await _unitOfWork.CollectionRepository.GetAllAsync(query.Category, query.MinFloorPrice, query.MaxFloorPrice, query.Creator, query.PageNumber, query.PageSize);
            var count  = await _unitOfWork.CollectionRepository.CountAsync(query.Category, query.MinFloorPrice, query.MaxFloorPrice, query.Creator);

            var response = new PaginatedResponse<CollectionsListDto>{
                Message = "Collections Fetched Succesfully",
                Value = _mapper.Map<List<CollectionsListDto>>(result),
                Count = count,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize
            };

            return response;


        }
    }
}