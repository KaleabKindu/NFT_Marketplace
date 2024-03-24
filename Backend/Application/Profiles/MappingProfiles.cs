using Application.Features.Categories.Dtos;
using Application.Features.Bids.Dtos;
using Application.Features.Offers.Dtos;
using Application.Features.Assets.Dtos;
using AutoMapper;
using Domain;
using Application.Features.Auth.Dtos;
using Domain.Offers;
using Domain.Categories;
using Domain.Bids;
using Domain.Assets;
using Domain.Transactions;
using Application.Features.Transactions.Dtos;
using Domain.Auctions;
using Application.Features.Auctions.Dtos;

namespace Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region AppUser 
            CreateMap<AppUser, UserDto>().ReverseMap();
            CreateMap<AppUser, UserFetchDto>().ReverseMap();
            CreateMap<AppUser, UserProfile>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            // CreateMap<CreateOfferDto,Offer>();
            // CreateMap<UpdateOfferDto,Offer>()
            //     .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            #endregion

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
            CreateMap<Bid, BidDto>()
                .ForMember(dest => dest.Bidder, opt => opt.MapFrom(src => src.Bidder.PublicAddress))
                .ForMember(dest => dest.AssetId, opt => opt.MapFrom(src => src.Asset.Id));

            CreateMap<Bid, BidsListDto>()
                .ForMember(dest => dest.Bidder, opt => opt.MapFrom(src => src.Bidder.PublicAddress));

            CreateMap<CreateBidDto, Bid>();
            CreateMap<UpdateBidDto, Bid>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            #endregion


            #region Assets
            CreateMap<Asset, AssetDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator.PublicAddress))
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner.PublicAddress));

            CreateMap<Asset, AssetListOpenAuctDto>().ReverseMap();

            CreateMap<Asset, AssetDetailDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => new UserFetchDto{ PublicAddress=src.Creator.PublicAddress, UserName=src.Creator.UserName}))
                .ForMember(dest => dest.Owner , opt => opt.MapFrom(src => new UserFetchDto{ PublicAddress = src.Owner.PublicAddress, UserName = src.Owner.UserName}));

            CreateMap<Asset, AssetListDto>().ReverseMap();
            CreateMap<Asset, UpdateAssetDto>().ReverseMap();
            CreateMap<Asset, CreateAssetDto>().ReverseMap();
            #endregion Assets

            #region Transaction
            CreateMap<Transaction, TransactionDto>()
                    .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString())); 
            #endregion Transaction


            #region Auction

            CreateMap<Auction, GetAuctionDto>()
                .ForMember(dest => dest.HighestBid , opt => opt.MapFrom(src => src.HighestBid));  
            CreateMap<Auction, CreateAuctionDto>().ReverseMap();

            #endregion Auction

        
        }

    }

}

