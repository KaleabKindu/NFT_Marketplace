using Application.Features.Offers.Commands;
using Application.Features.Offers.Dtos;
using Application.Features.Offers.Queries;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class OffersController : BasaApiController
    {
        [HttpGet] //api/Offers
        public async Task<IActionResult> GetOffer()
        {
            return HandleResult(await Mediator.Send(new GetAllOfferQuery()));
        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> GetOffer(int Id)
        {
            return HandleResult(await Mediator.Send(new GetOfferByIdQuery { Id = Id }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOffer(CreateOfferDto Offer)
        {
            return  HandleResult(await Mediator.Send(new CreateOfferCommand { Offer = Offer }));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOffer(UpdateOfferDto Offer)
        {
            return  HandleResult(await Mediator.Send(new UpdateOfferCommand { Offer = Offer }));
        }

      
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteActvity(int Id)
        {
            return  HandleResult(await Mediator.Send(new DeleteOfferCommand { Id = Id }));
        }
    }
}