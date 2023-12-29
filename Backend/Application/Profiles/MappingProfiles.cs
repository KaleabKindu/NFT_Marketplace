using Application.Features.Categories.Dtos;
﻿using Application.Features.Bids.Dtos;
using Application.Features.Offers.Dtos;
using AutoMapper;
using Domain;
using Domain.Category;

namespace Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Offer 
            CreateMap<Offer, OfferDto>().ReverseMap();
            CreateMap<CreateOfferDto,Offer>();
            CreateMap<UpdateOfferDto,Offer>();
            #endregion

            #region Category 
            CreateMap<Category, CategoryListDto>().ReverseMap();
            CreateMap<CreateCategoryDto,Category>();
            CreateMap<UpdateCategoryDto,Category>();
            CreateMap<CategoryListDto,Category>();
            #endregion

        
            #region Bid 
            CreateMap<Bid, BidDto>();
            CreateMap<Bid, BidsListDto>();
            CreateMap<CreateBidDto, Bid>();
            CreateMap<UpdateBidDto, Bid>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            #endregion
        }
    }
}
