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
using Domain.Notifications;
using Application.Features.Notifications.Dtos;

namespace Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region AppUser 
            CreateMap<AppUser, UserDto>().ReverseMap();
            CreateMap<AppUser, UserFetchDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Profile.UserName ?? src.UserName))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Profile.Avatar));

            CreateMap<AppUser, UserListDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Profile.UserName))
                .ForMember(dest => dest.Background, opt => opt.MapFrom(src => src.Profile.ProfileBackgroundImage))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Profile.Avatar))
                .ForMember(dest => dest.Sales, opt => opt.MapFrom(src => src.Profile.Volume));

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

            CreateMap<AppUser, UserNetworkDto>()
                .ForMember(dest => dest.Background, opt => opt.MapFrom(src => src.Profile.ProfileBackgroundImage))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Profile.UserName))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Profile.Avatar))
                .ForMember(dest => dest.Sales, opt => opt.MapFrom(src => src.Profile.Volume));

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
                .ForPath(dest => dest.Creator.Avatar, opt => opt.MapFrom(src => src.Creator.Profile.Avatar))
                .ForPath(dest => dest.Creator.UserName, opt => opt.MapFrom(src => src.Creator.Profile.UserName))
                .ForPath(dest => dest.Owner.Avatar, opt => opt.MapFrom(src => src.Owner.Profile.Avatar))
                .ForPath(dest => dest.Owner.UserName, opt => opt.MapFrom(src => src.Owner.Profile.UserName))
                .ForPath(dest => dest.Owner.Address, opt => opt.MapFrom(src => src.Owner.Address))
                .ForPath(dest => dest.Creator.Address, opt => opt.MapFrom(src => src.Creator.Address));

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
            CreateMap<Collection, TrendingCollectionsDto>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Assets.Select(asset => asset.Image)));

            #endregion Collections

            #region Provenances

            CreateMap<Provenance, ProvenanceListDto>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.CreatedAt));
            CreateMap<Provenance, CreateProvenanceDto>();

            #endregion

            #region 
            CreateMap<Notification, NotificationDto>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.CreatedAt));
            CreateMap<CreateNotificationDto, Notification>()
                .ForMember(dst => dst.ToId, opt => opt.MapFrom(src => src.UserId));
            #endregion
        }

    }

}

