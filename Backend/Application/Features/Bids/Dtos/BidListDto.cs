
using Application.Features.Auth.Dtos;
using Application.Features.Common;

namespace Application.Features.Bids.Dtos
{
    public class BidsListDto: BaseDto
    {
        public UserFetchDto From { set; get; }

        public string TransactionHash { set; get; }

        public double Bid { set; get; }

        public DateTime Date { set; get; }


    }
}