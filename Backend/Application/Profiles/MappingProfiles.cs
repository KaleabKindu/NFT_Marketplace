using Application.Features.Offers.Dtos;
using AutoMapper;
using Domain;
using Domain.Offers;

namespace Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region offer 
            CreateMap<Offer, OfferDto>().ReverseMap();
            CreateMap<CreateOfferDto,Offer>();
            CreateMap<UpdateOfferDto,Offer>();
            #endregion

        
        }
    }
}
