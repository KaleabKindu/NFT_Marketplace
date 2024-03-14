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
  collectionId?: number;
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
  publicAddress: Address;
}
