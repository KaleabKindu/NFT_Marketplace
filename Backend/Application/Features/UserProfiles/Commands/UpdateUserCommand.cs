using ErrorOr;
using MediatR;
using AutoMapper;
using Application.Common.Responses;
using Application.Contracts.Persistance;
using Application.Features.UserProfiles.Dtos;

namespace Application.Features.UserProfiles.Commands
{
    public class UpdateProfileCommand : IRequest<ErrorOr<BaseResponse<Unit>>>
    {
        public UpdateProfileDto UpdateProfileDto { get; set; }
        public string Address { get; set; }
    }

    public class UpdateProfileCommandHandler
        : IRequestHandler<UpdateProfileCommand, ErrorOr<BaseResponse<Unit>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProfileCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<BaseResponse<Unit>>> Handle(
            UpdateProfileCommand command,
            CancellationToken cancellationToken
        )
        {
            var response = await _unitOfWork.UserProfileRepository.GetByAddressAsync(command.Address);

            if (response.IsError) return response.Errors;

            var userProfile = response.Value;

            userProfile = _mapper.Map(command.UpdateProfileDto, userProfile);

            _unitOfWork.UserProfileRepository.UpdateAsync(userProfile);

            await _unitOfWork.SaveAsync();

            return new BaseResponse<Unit>()
            {
                Message = "User profile updated successfully",
                Value = Unit.Value
            };
        }
    }
}
