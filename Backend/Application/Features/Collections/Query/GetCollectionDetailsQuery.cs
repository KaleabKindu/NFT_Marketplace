using MediatR;
using ErrorOr;
using AutoMapper;
using Application.Features.Common;
using Application.Common.Responses;
using Application.Contracts.Persistance;
using Application.Features.Collections.Dtos;

namespace Application.Features.Assets.Query
{
    public class GetCollectionDetailsQuery : PaginatedQuery, IRequest<ErrorOr<BaseResponse<CollectionDetailsDto>>>
    {
        public long Id { get; set; }
    }


     public class GetCollectionDetailsQueryHandler : IRequestHandler<GetCollectionDetailsQuery, ErrorOr<BaseResponse<CollectionDetailsDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetCollectionDetailsQueryHandler(IMapper mapper, IUnitOfWork unitOfwork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfwork;
            
        } 
 
        public async Task<ErrorOr<BaseResponse<CollectionDetailsDto>>> Handle(GetCollectionDetailsQuery query, CancellationToken cancellationToken)
        {
            var collection = await _unitOfWork.CollectionRepository.GetByIdAsync(query.Id);

            var response = new BaseResponse<CollectionDetailsDto>{
                Message = "Collection Detail Fetched Succesfully",
                Value = _mapper.Map<CollectionDetailsDto>(collection)
            };

            return response;


        }
    }
}