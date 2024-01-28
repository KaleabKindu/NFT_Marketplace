using Application.Contracts.Persistance;
using Application.Features.Transactions.Dtos;
using Application.Responses;
using AutoMapper;
using ErrorOr;
using MediatR;

namespace Application.Features.Transactions.Queries
{
    public class GetAllTransactionsQuery : IRequest<ErrorOr<PaginatedResponse<TransactionDto>>> {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int AssetId { get; set; }
     }

    public class GetAllTransactionsQueryHandler : IRequestHandler<GetAllTransactionsQuery, ErrorOr<PaginatedResponse<TransactionDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllTransactionsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<PaginatedResponse<TransactionDto>>> Handle(
            GetAllTransactionsQuery request, 
            CancellationToken cancellationToken
        )
        {
            var trasactions = await _unitOfWork.TransactionRepository.GetAllTransactionAsync(request.PageNumber, request.PageSize, request.AssetId);
            int total_count = await _unitOfWork.TransactionRepository.Count();

            return new PaginatedResponse<TransactionDto>(){
                Message="Transaction lists fetched successfully",
                PageNumber=request.PageNumber,
                PageSize=request.PageSize,
                Count=total_count,
                Value=_mapper.Map<List<TransactionDto>>(trasactions)
            };
        }
    }
}