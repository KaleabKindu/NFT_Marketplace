// types definition
export interface NFT {
    name: string;
    description: string;
    image:string;
    files: string;
    price:number;
    royalty:number,
    collection: string;
    auction:boolean;
    auctionEnd:number;
}

export type Address = `0x${string}`


export interface Credentials {
    publicAddress:Address,
    signedNonce:string
}