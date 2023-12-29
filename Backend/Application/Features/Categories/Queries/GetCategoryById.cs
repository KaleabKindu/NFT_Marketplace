using Application.Common.Errors;
using Application.Common.Responses;
using Application.Contracts.Persistance;
using Application.Features.Categories.Dtos;
using AutoMapper;
using ErrorOr;
using MediatR;

namespace Application.Features.Categories.Queries
{
    public class GetCategoryByIdQuery : IRequest<ErrorOr<BaseResponse<CategoryListDto>>>
    {
        public long Id { get; set; }
    }

    public class GetCategoryByIdQueryHandler
        : IRequestHandler<GetCategoryByIdQuery, ErrorOr<BaseResponse<CategoryListDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<BaseResponse<CategoryListDto>>> Handle(
            GetCategoryByIdQuery request,
            CancellationToken cancellationToken
        )
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.Id);

            if (category == null) return ErrorFactory.NotFound("Category");
    
            return new BaseResponse<CategoryListDto>(){
                Message="Bid details fetched successfully",
                Value=_mapper.Map<CategoryListDto>(category)
            };
        }
    }
}
