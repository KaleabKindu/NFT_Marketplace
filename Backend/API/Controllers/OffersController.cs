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
            return HandleResult(await Mediator.Send(new GetAllOfferQuery()), "Offers fetched successfully");
        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> GetOffer(int Id)
        {
            return HandleResult(await Mediator.Send(new GetOfferByIdQuery { Id = Id }), "Offer details fetched successfully");
        }

        [HttpPost]
        public async Task<IActionResult> CreateOffer(CreateOfferDto Offer)
        {
            return  HandleResult(await Mediator.Send(new CreateOfferCommand { Offer = Offer }), "Offer created successfully");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOffer(UpdateOfferDto Offer)
        {
            return  HandleResult(await Mediator.Send(new UpdateOfferCommand { Offer = Offer }), "Offer updated successfully");
        }

      
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteActvity(int Id)
        {
            return  HandleResult(await Mediator.Send(new DeleteOfferCommand { Id = Id }), "Offer deleted successfully");
        }
    }
}