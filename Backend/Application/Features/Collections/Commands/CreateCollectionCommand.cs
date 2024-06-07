using ErrorOr;
using MediatR;
using AutoMapper;
using Application.Contracts.Persistance;
using Application.Common.Responses;
using Application.Common.Errors;
using Application.Common.Exceptions;
using Application.Features.Collections.Dtos;
using Domain.Collections;

namespace Application.Features.Buys.Commands
{
    public class CreateCollectionCommand : IRequest<ErrorOr<BaseResponse<long>>>
    {
        public CreateCollectionsDto CreateCollectionDto { get; set; }
        public string UserAddress { get; set; }
    }

    public class CreateCollectionCommandHandler
        : IRequestHandler<CreateCollectionCommand, ErrorOr<BaseResponse<long>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCollectionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<BaseResponse<long>>> Handle(
            CreateCollectionCommand request,
            CancellationToken cancellationToken
        )
        {
            var user = await _unitOfWork.UserRepository.GetUserByAddress(request.UserAddress);

            if (user == null) return ErrorFactory.BadRequestError("User", "User not found");

            var collectionData = _mapper.Map<Collection>(request.CreateCollectionDto);

            collectionData.CreatorId = user.Id;

            var response = await _unitOfWork.CollectionRepository.AddAsync(collectionData);


            if (await _unitOfWork.SaveAsync() == 0)
                throw new DbAccessException("Unable to save to database");

            return new BaseResponse<long>()
            {
                Message = "Collection created successfully",
                Value = response.Id
            };
        }
    }
}
