
using Application.Features.Common;

namespace Application.Features.Bids.Dtos
{
    public class BidDto: BaseDto
    {
        public string Bidder { set; get; }

        public string Asset { set; get; }

        public double Amount { set; get; }
    }
}