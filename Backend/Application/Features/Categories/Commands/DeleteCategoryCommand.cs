using Application.Common;
using Application.Contracts.Persistance;
using Domain.Category;
using ErrorOr;

using MediatR;

namespace Application.Features.Categories.Commands
{
    public class DeleteCategoryCommand : IRequest<ErrorOr<Unit>>
    {
        public long Id { get; set; }
    }

    public class DeleteCategoryCommandHandler
        : IRequestHandler<DeleteCategoryCommand, ErrorOr<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Unit>> Handle(
            DeleteCategoryCommand request,
            CancellationToken cancellationToken
        )
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.Id);

            if (category == null) return CategoryError.NotFound;
            
            _unitOfWork.CategoryRepository.DeleteAsync(category);

            if (await _unitOfWork.SaveAsync() == 0)
                return CommonError.ErrorSavingChanges;

            return Unit.Value;
        }
    }
}
