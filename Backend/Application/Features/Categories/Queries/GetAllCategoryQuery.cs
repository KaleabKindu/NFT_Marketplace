using Application.Features.Categories.Dtos;
using Application.Contracts.Persistance;
using AutoMapper;
using ErrorOr;
using MediatR;
using Application.Responses;

namespace Application.Features.Categories.Queries
{
    public class GetAllCategoryQuery : IRequest<ErrorOr<PaginatedResponse<CategoryListDto>>> {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
     }

    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, ErrorOr<PaginatedResponse<CategoryListDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCategoryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<PaginatedResponse<CategoryListDto>>> Handle(
            GetAllCategoryQuery request,
            CancellationToken cancellationToken
        )
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync(request.PageNumber, request.PageSize);
            var total_count = await _unitOfWork.CategoryRepository.Count();

            return new PaginatedResponse<CategoryListDto>(){
                Message="Category lists fetched successfully",
                PageNumber=request.PageNumber,
                PageSize=request.PageSize,
                Count=total_count,
                Value=_mapper.Map<List<CategoryListDto>>(categories)
            };
        }
    }
}
