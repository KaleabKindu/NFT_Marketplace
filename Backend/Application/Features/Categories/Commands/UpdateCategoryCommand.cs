using Application.Common;
using Application.Contracts.Persistance;
using Application.Features.Categories.Dtos;
using Application.Features.Offers.Dtos;
using AutoMapper;
using Domain.Category;
using Domain.Offers;
using ErrorOr;
using MediatR;

namespace Application.Features.Categories.Commands
{
    public class UpdateCategoryCommand : IRequest<ErrorOr<Unit>>
    {
        public UpdateCategoryDto Category { get; set; }
    }

    public class UpdateCategoryCommandHandler
        : IRequestHandler<UpdateCategoryCommand, ErrorOr<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<Unit>> Handle(
            UpdateCategoryCommand request,
            CancellationToken cancellationToken
        )
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(
                request.Category.Id
            );

            if (category == null) return CategoryError.NotFound;
        
            _mapper.Map(request.Category, category);

            if (await _unitOfWork.SaveAsync() == 0)
                return CommonError.ErrorSavingChanges;
            
            return Unit.Value;
        }
    }
}
