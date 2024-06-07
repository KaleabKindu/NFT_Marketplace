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
using Application.Features.UserProfiles.Dtos;

namespace Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region AppUser 
            CreateMap<AppUser, UserDto>().ReverseMap();
            CreateMap<AppUser, UserFetchDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Profile.UserName ?? src.UserName))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Profile.Avatar));

            CreateMap<AppUser, UserListDto>()
                .ForMember(dest => dest.Background, opt => opt.MapFrom(src => src.Profile.ProfileBackgroundImage))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Profile.UserName))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Profile.Avatar));

            CreateMap<AppUser, UserDetailDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Profile.Email))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Profile.UserName))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Profile.Avatar))
                .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Profile.Bio))
                .ForMember(dest => dest.BackgroundImage, opt => opt.MapFrom(src => src.Profile.ProfileBackgroundImage))
                .ForMember(dest => dest.TotalSalesCount, opt => opt.MapFrom(src => src.Profile.TotalSalesCount))
                .ForMember(dest => dest.Followers, opt => opt.MapFrom(src => src.Profile.Followers))
                .ForMember(dest => dest.YouTube, opt => opt.MapFrom(src => src.Profile.YouTube))
                .ForMember(dest => dest.Twitter, opt => opt.MapFrom(src => src.Profile.Twitter))
                .ForMember(dest => dest.Facebook, opt => opt.MapFrom(src => src.Profile.Facebook))
                .ForMember(dest => dest.Telegram, opt => opt.MapFrom(src => src.Profile.Telegram));

            CreateMap<UpdateProfileDto, UserProfile>();

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
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => new UserFetchDto { Address = src.Creator.Address, Username = src.Creator.UserName }))
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => new UserFetchDto { Address = src.Owner.Address, Username = src.Owner.UserName }));

            CreateMap<Asset, AssetListDto>().ReverseMap();
            CreateMap<Asset, UpdateAssetDto>().ReverseMap();
            CreateMap<Asset, CreateAssetDto>().ReverseMap();
            #endregion Assets

            #region Auction

            CreateMap<Auction, GetAuctionDto>()
                .ForMember(dest => dest.HighestBid, opt => opt.MapFrom(src => src.HighestBid));
            CreateMap<Auction, CreateAuctionDto>().ReverseMap();

            #endregion Auction

            #region Collections

            CreateMap<Collection, CollectionsListDto>();
            CreateMap<Collection, CollectionDetailsDto>();
            CreateMap<CreateCollectionsDto, Collection>();
            CreateMap<Collection, CollectionDto>();

            #endregion Collections

            #region Provenances

            CreateMap<Provenance, ProvenanceListDto>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.CreatedAt));
            CreateMap<Provenance, CreateProvenanceDto>();

            #endregion


        }

    }

}

