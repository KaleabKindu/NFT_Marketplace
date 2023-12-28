using Application.Contracts.Persistance;
using Application.Features.Categories.Dtos;
using Application.Features.Offers.Dtos;
using AutoMapper;
using Domain.Category;
using Domain.Offers;
using ErrorOr;
using MediatR;

namespace Application.Features.Categories.Queries
{
    public class GetCategoryByIdQuery : IRequest<ErrorOr<CategoryListDto>>
    {
        public long Id { get; set; }
    }

    public class GetCategoryByIdQueryHandler
        : IRequestHandler<GetCategoryByIdQuery, ErrorOr<CategoryListDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<CategoryListDto>> Handle(
            GetCategoryByIdQuery request,
            CancellationToken cancellationToken
        )
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.Id);

            if (category == null) return CategoryError.NotFound;
            return _mapper.Map<CategoryListDto>(category);
        }
    }
}
