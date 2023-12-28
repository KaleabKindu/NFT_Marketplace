using Application.Features.Categories.Dtos;
﻿using Application.Features.Bids.Dtos;
using Application.Features.Offers.Dtos;
using AutoMapper;
using Domain;
using Domain.Category;
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

            #region category 
            CreateMap<Category, CategoryListDto>().ReverseMap();
            CreateMap<CreateCategoryDto,Category>();
            CreateMap<UpdateCategoryDto,Category>();
            CreateMap<CategoryListDto,Category>();
            #endregion

        
            #region offer 
            CreateMap<Bid, BidDto>().ReverseMap();
            CreateMap<CreateBidDto, Bid>();
            CreateMap<UpdateBidDto, Bid>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            #endregion
        }
    }
}
