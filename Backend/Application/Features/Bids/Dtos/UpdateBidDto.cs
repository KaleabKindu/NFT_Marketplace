
using Application.Features.Common;

namespace Application.Features.Bids.Dtos
{
    public class UpdateBidDto: BaseDto
    {
        public double? amount { set; get; }
    }
}