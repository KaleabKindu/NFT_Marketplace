using Application.Common.Errors;
using Application.Common.Exceptions;
using Application.Common.Responses;
using Application.Contracts.Persistance;
using Application.Features.Categories.Dtos;
using AutoMapper;
using ErrorOr;
using MediatR;

namespace Application.Features.Categories.Commands
{
    public class UpdateCategoryCommand : IRequest<ErrorOr<BaseResponse<CategoryListDto>>>
    {
        public UpdateCategoryDto Category { get; set; }
    }

    public class UpdateCategoryCommandHandler
        : IRequestHandler<UpdateCategoryCommand, ErrorOr<BaseResponse<CategoryListDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<BaseResponse<CategoryListDto>>> Handle(
            UpdateCategoryCommand request,
            CancellationToken cancellationToken
        )
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(
                request.Category.Id
            );

            if (category == null) return ErrorFactory.NotFound("Category", "Category not found");
        
            _mapper.Map(request.Category, category);

            if (await _unitOfWork.SaveAsync() == 0)
                throw new DbAccessException("Unable to save to database");
            
            return new BaseResponse<CategoryListDto>(){
                Message="Bid updated successfully",
                Value=_mapper.Map<CategoryListDto>(category)
            };        
        }
    }
}
