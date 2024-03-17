import { Routes } from "@/routes";

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
export const categories = [
  {
    name: "Art",
    count: 250,
    image: "art-category.jpg",
    route: "/",
  },
  {
    name: "Photography",
    count: 330,
    image: "photography-category.jpg",
    route: "/",
  },
  {
    name: "Audio",
    count: 234,
    image: "audio-category.jpg",
    route: "/",
  },
  {
    name: "Video",
    count: 534,
    image: "video-category.jpg",
    route: "/",
  },
  {
    name: "3D",
    count: 334,
    image: "3d-category.jpg",
    route: "/",
  },
  {
    name: "Design",
    count: 134,
    image: "design-category.jpg",
    route: "/",
  },
  {
    name: "Drawing & Painting",
    count: 434,
    image: "drawing-category.jpg",
    route: "/",
  },
  {
    name: "E-Books",
    count: 200,
    image: "ebook-category.png",
    route: "/",
  },
  {
    name: "Tickets",
    count: 100,
    image: "tickets-category.jpg",
    route: "/",
  },
];

export const category_type = [
  {
    name: "Art",
    value: "art",
  },
  {
    name: "Photography",
    value: "photography",
  },
  {
    name: "Audio",
    value: "audio",
  },
  {
    name: "Video",
    value: "video",
  },
  {
    name: "3D",
    value: "3d",
  },
  {
    name: "Design",
    value: "design",
  },
  {
    name: "Drawing & Painting",
    value: "drawing_painting",
  },
  {
    name: "E-Books",
    value: "e-books",
  },
  {
    name: "Tickets",
    value: "tickets",
  },
];

export const provenances = [
  {
    event: "Sale",
    from: "bing",
    price: "$250.00",
    to: "geller",
    date: new Date().toLocaleDateString(),
  },
  {
    event: "Transfer",
    from: "ross",
    price: "$150.00",
    to: "bing",
    date: new Date().toLocaleDateString(),
  },
  {
    event: "Transfer",
    from: "barnes",
    price: "$350.00",
    to: "ross",
    date: new Date().toLocaleDateString(),
  },
  {
    event: "Sale",
    from: "rogers",
    price: "$450.00",
    to: "barnes",
    date: new Date().toLocaleDateString(),
  },
  {
    event: "Sale",
    from: "banner",
    price: "$550.00",
    to: "rogers",
    date: new Date().toLocaleDateString(),
  },
  {
    event: "Sale",
    from: "stark",
    price: "$200.00",
    to: "banner",
    date: new Date().toLocaleDateString(),
  },
  {
    event: "Mint",
    from: "nulladdress",
    price: "$300.00",
    to: "stark",
    date: new Date().toLocaleDateString(),
  },
];

export const offers = [
  {
    from: "potts",
    price: "0.25",
    usd_price: "250.00",
    date: new Date().toLocaleDateString(),
  },
  {
    from: "banner",
    price: "0.15",
    usd_price: "150.00",
    date: new Date().toLocaleDateString(),
  },
  {
    from: "rogers",
    price: "0.22",
    usd_price: "350.00",
    date: new Date().toLocaleDateString(),
  },
  {
    from: "romanoff",
    price: "0.55",
    usd_price: "450.00",
    date: new Date().toLocaleDateString(),
  },
  {
    from: "thanos",
    price: "0.21",
    usd_price: "550.00",
    date: new Date().toLocaleDateString(),
  },
  {
    from: "odinson",
    price: "0.75",
    usd_price: "200.00",
    date: new Date().toLocaleDateString(),
  },
  {
    from: "stark",
    price: "0.05",
    usd_price: "300.00",
    date: new Date().toLocaleDateString(),
  },
];

export const bids = [
  {
    from: "potts",
    bid_price: "0.25",
    bid_usd_price: "250.00",
    date: new Date().toLocaleDateString(),
  },
  {
    from: "banner",
    bid_price: "0.15",
    bid_usd_price: "150.00",
    date: new Date().toLocaleDateString(),
  },
  {
    from: "rogers",
    bid_price: "0.22",
    bid_usd_price: "350.00",
    date: new Date().toLocaleDateString(),
  },
  {
    from: "romanoff",
    bid_price: "0.55",
    bid_usd_price: "450.00",
    date: new Date().toLocaleDateString(),
  },
  {
    from: "thanos",
    bid_price: "0.21",
    bid_usd_price: "550.00",
    date: new Date().toLocaleDateString(),
  },
  {
    from: "odinson",
    bid_price: "0.75",
    bid_usd_price: "200.00",
    date: new Date().toLocaleDateString(),
  },
  {
    from: "stark",
    bid_price: "0.05",
    bid_usd_price: "300.00",
    date: new Date().toLocaleDateString(),
  },
];

export const sort_types = [
  {
    name: "Trending",
    value: "trending",
  },
  {
    name: "Recent",
    value: "recent",
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
