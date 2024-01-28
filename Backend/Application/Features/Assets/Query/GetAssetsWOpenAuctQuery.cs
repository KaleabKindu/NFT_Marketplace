using System;
using Application.Contracts.Persistance;
using Application.Features.Assets.Dtos;
using Application.Features.Common;
using Application.Responses;
using AutoMapper;
using ErrorOr;
using MediatR;

namespace Application.Features.Assets.Query
{
    public class GetAssetsWOpenAuctQuery : PaginatedQuery, IRequest<ErrorOr<PaginatedResponse<AssetListOpenAuctDto>>>
    {
        
    }


    public class GetAssetsWOpenAuctQueryHandler : IRequestHandler<GetAssetsWOpenAuctQuery, ErrorOr<PaginatedResponse<AssetListOpenAuctDto>>>{

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAssetsWOpenAuctQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            
        }

        public async  Task<ErrorOr<PaginatedResponse<AssetListOpenAuctDto>>> Handle(GetAssetsWOpenAuctQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.AssetRepository.GetAssetsWOpenAuct();

            return new PaginatedResponse<AssetListOpenAuctDto>{

                Message = "Fetch Succesful",
                Value = _mapper.Map<List<AssetListOpenAuctDto>>(result),
                Count = result.Count(),
                PageNumber = request.PageNumber,
                PageSize = request.PageSize


            };



        }
    }





}