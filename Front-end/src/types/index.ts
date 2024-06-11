// types definition
export interface NFT {
  tokenId?: number;
  name: string;
  description: string;
  image?: string;
  audio?: string;
  video?: string;
  likes?: number;
  liked?: boolean;
  category?: string;
  price?: string;
  royalty: number;
  collection?: {
    id: number;
    avatar: string;
    name: string;
  };
  collectionId?: number;
  creator?: User;
  owner?: User;
  auction?: Auction;
  transactionHash?: string;
}

export interface Auction {
  auctionId?: number;
  auctionEnd: number;
  highestBid: string;
}

export type Address = `0x${string}`;

export interface Credentials {
  address: Address;
  signedNonce: string;
}

export interface User {
  userName: string;
  avatar: string;
  address: Address;
  bio?: string;
  profile_background?: string;
  facebook?: string;
  twitter?: string;
  youtube?: string;
  telegram?: string;
  sales?: number;
  following?: boolean;
}
export interface ICollection {
  id: string;
  avatar: string;
  name: string;
  description: string;
  volume: string;
  floorPrice: string;
  latestPrice: string;
  items: number;
  creator: User;
  images?: string[];
}
export interface IFilter {
  search?: string;
  category?: string;
  min_price?: string;
  max_price?: string;
  min_volume?: string;
  max_volume?: string;
  sale_type?: string;
  collectionId?: string;
  creator?: string;
  owner?: string;
  sort_by?: string;
  pageNumber?: number;
  pageSize?: number;
}

export interface IPagination {
  count: number;
}
export interface IUser {
  userName: string;
  avatar: string;
  address: string;
}
export interface IProvenance {
  event: string;
  from: IUser;
  to: IUser;
  price: string;
  hash: string;
  date: number;
}

export interface IBid {
  from: IUser;
  bid: string;
  hash: string;
  date: number;
}
export interface INotification {
  id: number;
  title: string;
  content: string;
  date: Date;
  isRead: boolean;
}

export interface IAssetsPage extends IPagination {
  value: NFT[];
}

export interface ICollectionsPage extends IPagination {
  value: ICollection[];
}

export interface IUsersPage extends IPagination {
  value: User[];
}

export interface IProvenancePage extends IPagination {
  value: IProvenance[];
}

export interface IBidPage extends IPagination {
  value: IBid[];
}

export interface INotificationPage extends IPagination {
  value: INotification[];
  count: number;
  pageNumber: number;
  pageSize: number;
}