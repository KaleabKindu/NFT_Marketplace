using Application.Common.Errors;
using Application.Common.Exceptions;
using Application.Common.Responses;
using Application.Contracts.Persistance;
using ErrorOr;

using MediatR;

namespace Application.Features.Categories.Commands
{
    public class DeleteCategoryCommand : IRequest<ErrorOr<BaseResponse<Unit>>>
    {
        public long Id { get; set; }
    }

    public class DeleteCategoryCommandHandler
        : IRequestHandler<DeleteCategoryCommand, ErrorOr<BaseResponse<Unit>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<BaseResponse<Unit>>> Handle(
            DeleteCategoryCommand request,
            CancellationToken cancellationToken
        )
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.Id);

            if (category == null) return ErrorFactory.NotFound("Category", "Category not found");
            
            _unitOfWork.CategoryRepository.DeleteAsync(category);

            if (await _unitOfWork.SaveAsync() == 0)
                throw new DbAccessException("Unable to save to database");

            return new BaseResponse<Unit>(){
                Message="Category deleted successfully",
                Value=Unit.Value
            };
        }
    }
}
