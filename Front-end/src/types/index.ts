// types definition
export interface NFT {
  tokenId?: number;
  name: string;
  description: string;
  image: string;
  likes?: number;
  category?: string;
  price: string;
  royalty: number;
  collection?: {
    id: number;
    avatar: string;
    name: string;
  };
  creator?: User;
  owner?: User;
  auction?: Auction;
  transactionHash?: string;
}

export interface Auction {
  auctionId?: number;
  auction_end: number;
}

export type Address = `0x${string}`;

export interface Credentials {
  publicAddress: Address;
  signedNonce: string;
}

export interface User {
  userName: string;
  avatar: string;
  publicAddress: Address;
  bio?: string;
  profile_background?: string;
  social_media?: SocialMedia;
  sales?: number;
}
export interface SocialMedia {
  facebook: string;
  twitter: string;
  youtube: string;
  telegram: string;
}
export interface Collection {
  id: string;
  avatar: string;
  name: string;
  description: string;
  volume: string;
  floor_price: string;
  latest_price: string;
  items: number;
  creator: User;
  images?: string[];
}
