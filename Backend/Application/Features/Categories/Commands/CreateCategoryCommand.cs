using System.Collections.Generic;
using System.Linq;
using Application.Features.Categories.Dtos;
using ErrorOr;
using MediatR;
using AutoMapper;
using Application.Contracts.Persistance;
using Domain.Category;
using Application.Common;

namespace Application.Features.Categories.Commands
{
    public class CreateCategoryCommand : IRequest<ErrorOr<long>>
    {
        public CreateCategoryDto Category { get; set; }
    }

    public class CreateCategoryCommandHandler
        : IRequestHandler<CreateCategoryCommand, ErrorOr<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<long>> Handle(
            CreateCategoryCommand request,
            CancellationToken cancellationToken
        )
        {
            var category = _mapper.Map<Category>(request.Category);
            await _unitOfWork.CategoryRepository.AddAsync(category);

            if (await _unitOfWork.SaveAsync() == 0)
                return CommonError.ErrorSavingChanges;

            return category.Id;
        }

    }

}