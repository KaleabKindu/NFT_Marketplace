using Application.Features.Categories.Dtos;
using Application.Contracts.Persistance;
using AutoMapper;
using ErrorOr;
using MediatR;

namespace Application.Features.Categories.Queries
{
    public class GetAllCategoryQuery : IRequest<ErrorOr<List<CategoryListDto>>> { }

    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, ErrorOr<List<CategoryListDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCategoryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<List<CategoryListDto>>> Handle(
            GetAllCategoryQuery request,
            CancellationToken cancellationToken
        )
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();

            return _mapper.Map<List<CategoryListDto>>(categories);
        }
    }
}
