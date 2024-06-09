import { Routes } from "@/routes";
import { MdCamera } from "react-icons/md";
import { BsSoundwave } from "react-icons/bs";
import { FaVideo } from "react-icons/fa6";
import { SiVorondesign } from "react-icons/si";
import { FaPaintBrush } from "react-icons/fa";
import { IoBookOutline } from "react-icons/io5";
import { IoTicketSharp } from "react-icons/io5";
import { BsBox } from "react-icons/bs";
import { IconType } from "react-icons";
export const discover = [
  {
    name: "NFTs",
    route: Routes.NFTS,
    protected: false,
  },
  {
    name: "Collections",
    route: Routes.COLLECTIONS,
    protected: false,
  },
  {
    name: "Users",
    route: Routes.USERS,
    protected: false,
  },
];

export const help_center = [
  {
    name: "About Us",
    route: "/",
    protected: false,
  },
  {
    name: "Contact Us",
    route: "/",
    protected: false,
  },
];

export const howitworks = [
  {
    index: 1,
    image: "step-1.png",
    name: "Filter & Discover",
    description:
      "Connect with wallet, discover, buy NFTs, sell your NFTs and earn money",
  },
  {
    index: 2,
    image: "step-2.png",
    name: "Connect Wallet",
    description:
      "Connect with wallet, discover, buy NFTs, sell your NFTs and earn money",
  },
  {
    index: 3,
    image: "step-3.png",
    name: "Start Trading",
    description:
      "Connect with wallet, discover, buy NFTs, sell your NFTs and earn money",
  },
  {
    index: 4,
    image: "step-4.png",
    name: "Earn Money",
    description:
      "Connect with wallet, discover, buy NFTs, sell your NFTs and earn money",
  },
];
export enum CATEGORY {
  ART = "art",
  PHOTOGRAPHY = "photography",
  AUDIO = "audio",
  VIDEO = "video",
  THREE_D = "three_d",
  DESIGN = "design",
  EBOOK = "ebook",
  TICKET = "ticket",
}

export enum FILTER {
  SEARCH = "search",
  SALE = "sale_type",
  MIN_PRICE = "min_price",
  MAX_PRICE = "max_price",
  CATEGORY = "category",
  SORT_BY = "sort_by",
  COLLECTION = "collectionId",
  CREATOR = "creator",
}
export const categories: {
  name: string;
  count: number;
  image: string;
  value: CATEGORY;
  icon: IconType;
}[] = [
  {
    name: "Art",
    count: 250,
    image: "art-category.jpg",
    value: CATEGORY.ART,
    icon: FaPaintBrush,
  },
  {
    name: "Photography",
    count: 330,
    image: "photography-category.jpg",
    value: CATEGORY.PHOTOGRAPHY,
    icon: MdCamera,
  },
  {
    name: "Audio",
    count: 234,
    image: "audio-category.jpg",
    value: CATEGORY.AUDIO,
    icon: BsSoundwave,
  },
  {
    name: "Video",
    count: 534,
    image: "video-category.jpg",
    value: CATEGORY.VIDEO,
    icon: FaVideo,
  },
  {
    name: "3D",
    count: 334,
    image: "3d-category.jpg",
    value: CATEGORY.THREE_D,
    icon: BsBox,
  },
  {
    name: "Design",
    count: 134,
    image: "design-category.jpg",
    value: CATEGORY.DESIGN,
    icon: SiVorondesign,
  },
  {
    name: "E-Books",
    count: 200,
    image: "ebook-category.png",
    value: CATEGORY.EBOOK,
    icon: IoBookOutline,
  },
  {
    name: "Tickets",
    count: 100,
    image: "tickets-category.jpg",
    value: CATEGORY.TICKET,
    icon: IoTicketSharp,
  },
];

export const sort_types = [
  {
    name: "Date Added",
    value: "date_added",
  },
  {
    name: "Low - High",
    value: "low_high",
  },
  {
    name: "High - Low",
    value: "high_low",
  },
];

export const sale_types = [
  {
    name: "Auction",
    value: "auction",
  },
  {
    name: "Buy Now",
    value: "fixed",
  },
];

export const tabs = [
  "created",
  "owned",
  "liked",
  "collections",
  "followers",
  "following",
];

export const collections = [
  {
    id: "1",
    name: "Collection One",
    profile_pic: "/collection/collection-pic.png",
  },
  {
    id: "2",
    name: "Collection Two",
    profile_pic: "/collection/collection-pic.png",
  },
  {
    id: "3",
    name: "Collection Three",
    profile_pic: "/collection/collection-pic.png",
  },
];

export const users = [
  {
    id: "1",
    name: "Tony Stark",
    profile_pic: "/collection/collection-pic.png",
  },
  {
    id: "2",
    name: "Bruce Banner",
    profile_pic: "/collection/collection-pic.png",
  },
  {
    id: "3",
    name: "Steve Rogers",
    profile_pic: "/collection/collection-pic.png",
  },
];

export const nft_detail = {
  tokenId: 7,
  name: "Boomrang",
  description:
    "Unleash your inner meowjesty with the Tatto Kitty Gang! These 9,999 uniquely tattooed NFTs purr with purrsonality and rebellion. Every swirl and line tells a story, from delicate flower vines to bold tribal markings. Find your perfect feline match, unlock exclusive perks like DAO membership and metaverse access, and join a purrfectly messy community of cat-loving rebels. Mint your Tatto Kitty and let the purrpetual rebellion begin.",
  image: "/landing-page/audio-category.jpg",
  category: "image",
  price: "5",
  auction: {
    auctionId: 3,
    auctionEnd: Date.now() + 5000000000,
    current_price: "0.05",
  },
  likes: 22,
  creator: {
    name: "Anthony Stark",
    address: "0xowieurweoor",
    royalty: 5,
  },
  owner: {
    name: "Bruce Banner",
    address: "0xowieurweoor",
  },
  collection: {
    name: "Avengers",
    address: "0xowieurweoor",
  },
};
