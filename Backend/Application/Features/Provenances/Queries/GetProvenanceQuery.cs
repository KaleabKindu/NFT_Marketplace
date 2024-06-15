using Application.Contracts.Persistance;
using Application.Features.Common;
using Application.Features.Provenances.Dtos;
using Application.Responses;
using AutoMapper;
using MediatR;
using ErrorOr;


namespace Application.Features.Provenances.Queries;

public class GetProvenanceQuery : PaginatedQuery, IRequest<ErrorOr<PaginatedResponse<ProvenanceListDto>>>
{
    public long AssetId { get; set; }
}

public class GetProvenanceQueryHandler : IRequestHandler<GetProvenanceQuery, ErrorOr<PaginatedResponse<ProvenanceListDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProvenanceQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ErrorOr<PaginatedResponse<ProvenanceListDto>>> Handle(GetProvenanceQuery request,
        CancellationToken cancellationToken)
    {
        var provenances = await _unitOfWork.ProvenanceRepository.GetAssetProvenance(request.AssetId, request.PageNumber, request.PageSize);

        return new PaginatedResponse<ProvenanceListDto>
        {
            Count = provenances.Count,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            Value = _mapper.Map<List<ProvenanceListDto>>(provenances.Value),
            Message = "Fetch Successful",
        };
    }
}