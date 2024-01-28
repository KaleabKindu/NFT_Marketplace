
using Application.Features.Common;

namespace Application.Features.Bids.Dtos
{
    public class BidsListDto: BaseDto
    {
        public string Bidder { set; get; }

        public string TransactionHash { set; get; }

        public double Amount { set; get; }

        public DateTime CreatedAt { set; get; }
    }
}