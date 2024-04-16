using Application.Features.Bids.Dtos;
using Application.Features.Assets.Dtos;
using AutoMapper;
using Domain;
using Application.Features.Auth.Dtos;
using Domain.Bids;
using Domain.Assets;
using Domain.Auctions;
using Application.Features.Auctions.Dtos;
using Domain.Collections;
using Application.Features.Collections.Dtos;
using Application.Features.Provenances.Dtos;
using Domain.Provenances;

namespace Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region AppUser 
            CreateMap<AppUser, UserDto>().ReverseMap();
            CreateMap<AppUser, UserFetchDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Profile.UserName))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Profile.Avatar));

            CreateMap<AppUser, UserListDto>()
                .ForMember(dest => dest.Background, opt => opt.MapFrom(src => src.Profile.ProfileBackgroundImage))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Profile.UserName))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Profile.Avatar));

            #endregion
        
            #region Bid 
            CreateMap<Bid, BidDto>()
                .ForMember(dest => dest.Bidder, opt => opt.MapFrom(src => src.Bidder.Address))
                .ForMember(dest => dest.AssetId, opt => opt.MapFrom(src => src.Asset.Id));

            CreateMap<Bid, BidsListDto>()
                .ForMember(dest => dest.From, opt => opt.MapFrom(src => src.Bidder));

            CreateMap<CreateBidDto, Bid>();
            CreateMap<UpdateBidDto, Bid>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            #endregion


            #region Assets
            CreateMap<Asset, AssetDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator.Address))
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner.Address));

            CreateMap<Asset, AssetListOpenAuctDto>().ReverseMap();

            CreateMap<Asset, AssetDetailDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => new UserFetchDto{ Address=src.Creator.Address, Username=src.Creator.UserName}))
                .ForMember(dest => dest.Owner , opt => opt.MapFrom(src => new UserFetchDto{ Address = src.Owner.Address, Username = src.Owner.UserName}));

            CreateMap<Asset, AssetListDto>().ReverseMap();
            CreateMap<Asset, UpdateAssetDto>().ReverseMap();
            CreateMap<Asset, CreateAssetDto>().ReverseMap();
            #endregion Assets

            #region Auction

            CreateMap<Auction, GetAuctionDto>()
                .ForMember(dest => dest.HighestBid , opt => opt.MapFrom(src => src.HighestBid));  
            CreateMap<Auction, CreateAuctionDto>().ReverseMap();

            #endregion Auction

            #region Collections

            CreateMap<Collection, CollectionsListDto>()
                .ForMember(dest => dest.UserDto , opt => opt.MapFrom(src => src.Creator));  
            
            CreateMap<Collection, CollectionDetailsDto>()
                .ForMember(dest => dest.Latest_price , opt => opt.MapFrom(src => src.LatestPrice)) 
                .ForMember(dest => dest.Floor_price , opt => opt.MapFrom(src => src.FloorPrice));  
            
            #endregion Collections
            
            #region Provenances

            CreateMap<Provenance, ProvenanceListDto>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.CreatedAt));
            CreateMap<Provenance, CreateProvenanceDto>();

            #endregion


        }

    }

}

