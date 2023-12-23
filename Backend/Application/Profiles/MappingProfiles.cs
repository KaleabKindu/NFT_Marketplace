﻿using Application.Features.Categories.Dtos;
﻿using Application.Features.Bids.Dtos;
using Application.Features.Offers.Dtos;
using Application.Features.Assets.Dtos;
using AutoMapper;
using Domain;

namespace Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Offer 
            CreateMap<Offer, OfferDto>().ReverseMap();
            CreateMap<CreateOfferDto,Offer>();
            CreateMap<UpdateOfferDto,Offer>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            #endregion

            #region Category 
            CreateMap<Category, CategoryListDto>().ReverseMap();
            CreateMap<CreateCategoryDto,Category>();
            CreateMap<UpdateCategoryDto,Category>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            #endregion

        
            #region Bid 
            CreateMap<Bid, BidDto>();
            CreateMap<Bid, BidsListDto>();
            CreateMap<CreateBidDto, Bid>();
            CreateMap<UpdateBidDto, Bid>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            #endregion
        }
            #region Assets

            CreateMap<Asset, AssetDto>().ReverseMap();
            CreateMap<Asset, UpdateAssetDto>().ReverseMap();
            CreateMap<Asset, CreateAssetDto>().ReverseMap();
            #endregion Assets


        }


    }
}
