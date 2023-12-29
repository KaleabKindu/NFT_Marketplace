﻿using Microsoft.AspNetCore.Mvc;
using Application.Features.Offers.Dtos;
using Application.Features.Offers.Queries;
using Application.Features.Offers.Commands;

namespace API.Controllers
{
    public class OffersController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllOffers([FromQuery] int PageNumber = 1, [FromQuery] int PageSize = 10)
        {
            return HandleResult(await Mediator.Send(new GetAllOfferQuery(){ PageNumber=PageNumber, PageSize=PageSize }));
        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> GetOffer(int Id)
        {
            return HandleResult(await Mediator.Send(new GetOfferByIdQuery { Id = Id }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOffer([FromBody] CreateOfferDto Offer)
        {
            return  HandleResult(await Mediator.Send(new CreateOfferCommand { Offer = Offer }));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOffer([FromBody] UpdateOfferDto Offer)
        {
            return  HandleResult(await Mediator.Send(new UpdateOfferCommand { Offer = Offer }));
        }
      
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteOffer(int Id)
        {
            return  HandleResult(await Mediator.Send(new DeleteOfferCommand { Id = Id }));
        }
    }
}