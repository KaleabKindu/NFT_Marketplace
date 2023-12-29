using Application.Features.Categories.Dtos;
using ErrorOr;
using MediatR;
using AutoMapper;
using Application.Contracts.Persistance;
using Domain;
using Application.Common.Exceptions;
using Application.Common.Responses;

namespace Application.Features.Categories.Commands
{
    public class CreateCategoryCommand : IRequest<ErrorOr<BaseResponse<CategoryListDto>>>
    {
        public CreateCategoryDto Category { get; set; }
    }

    public class CreateCategoryCommandHandler
        : IRequestHandler<CreateCategoryCommand, ErrorOr<BaseResponse<CategoryListDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<BaseResponse<CategoryListDto>>> Handle(
            CreateCategoryCommand request,
            CancellationToken cancellationToken
        )
        {
            var category = _mapper.Map<Category>(request.Category);
            await _unitOfWork.CategoryRepository.AddAsync(category);

            if (await _unitOfWork.SaveAsync() == 0)
                throw new DbAccessException("Unable to save to database");

            return new BaseResponse<CategoryListDto>(){
                Message="Category created successfully",
                Value=_mapper.Map<CategoryListDto>(category)
            };
        }

    }

}