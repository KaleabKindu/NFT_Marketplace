
using Application.Features.Common;

namespace Application.Features.Bids.Dtos
{
    public class BidsListDto: BaseDto
    {
        public string bidder { set; get; }

        public string asset { set; get; }

        public double amount { set; get; }
    }
}