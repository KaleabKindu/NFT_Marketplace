"use client";
import {
  TypographyH2,
  TypographyH3,
  TypographyH4,
  TypographyP,
  TypographySmall,
} from "../common/Typography";
import Link from "next/link";
import CountDown from "count-down-react";
import { Button } from "../ui/button";

import { Avatar } from "../common/Avatar";
import { Routes } from "@/routes";
import { BsThreeDots } from "react-icons/bs";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import NFTRightShimmer from "../common/shimmers/NFTRightShimmer";
import { NFT } from "@/types";
import NFTBids from "./NFTBids";
import { PlaceBidModal } from "./PlaceBidModal";
import { BuyModal } from "./BuyModal";
import { TransferModal } from "./TransferModal";
import { DeleteAssetModal } from "./DeleteAssetModal";
import { useAccount } from "wagmi";
import { useEffect, useMemo, useState } from "react";
import useGetUsdPrice from "@/hooks/useGetUsdPrice";
import { ResellAssetModal } from "../explore/assets/ResellAssetModal";
import { CancelAuctionModal } from "./CancelAuctionModal";
import { CancelSaleModal } from "./CancelSaleModal";

type Props = {
  asset?: NFT;
  isLoading?: boolean;
};

const NFTDetailRight = ({ asset, isLoading }: Props) => {
  const { address } = useAccount();
  const [ price, setPrice ] = useState<string>()
  const usdPrice = useGetUsdPrice(price);
  const onRender = ({
    days,
    hours,
    minutes,
    seconds,
  }: {
    days: number;
    hours: number;
    minutes: number;
    seconds: number;
  }) => {
    return (
      <div className="flex gap-2 lg:w-[70%] flex-wrap">
        <div className="flex-1 flex flex-col gap-2">
          <div>{days}</div>
          <div>Days</div>
        </div>
        <div className="flex-1 flex flex-col gap-2">
          <div>{hours}</div>
          <div>Hours</div>
        </div>
        <div className="flex-1 flex flex-col gap-2">
          <div>{minutes}</div>
          <div>Minutes</div>
        </div>
        <div className="flex-1 flex flex-col gap-2">
          <div>{seconds}</div>
          <div>Seconds</div>
        </div>
      </div>
    );
  };
  const auctionEndDate = useMemo(() => {
    const timestamp = asset?.auction?.auctionEnd;
    if (timestamp && !isNaN(timestamp)) {
      const date = new Date(timestamp * 1000);
      return date;
    }
    return null;
  }, [asset?.auction]);
  useEffect(() =>{
    if(asset){
      setPrice(asset.auction ? asset.auction.highestBid : asset.price)
    }
  },[asset])
  return (
    <>
      {isLoading ? (
        <NFTRightShimmer />
      ) : (
        <div className="flex-1 p-3">
          <div className="flex flex-col gap-10">
            <div className="flex justify-between items-start p-5">
              <TypographyH2
                className="whitespace-nowrap text-ellipsis overflow-hidden capitalize"
                text={asset?.name}
              />
              {address && asset?.owner?.address === address && (asset.status === "OnFixedSale" || asset.status === "NotOnSale") && (
                <Menu asset={asset as NFT} />
              )}
            </div>
            <div className="flex flex-wrap items-center lg:divide-x-2">
              <Link
                href={`${Routes.USER}/${asset?.creator?.address}`}
                className="flex w-[30%] items-center gap-3 p-5"
              >
                <Avatar
                  className="bg-secondary h-16 w-16"
                  name={asset?.creator?.userName}
                  src={asset?.creator?.avatar}
                />
                <div className="flex flex-col">
                  <TypographySmall className="font-bold" text="Creator" />
                  <TypographyH4
                    className="whitespace-nowrap text-ellipsis overflow-hidden w-[100px]"
                    text={asset?.creator?.userName}
                  />
                </div>
              </Link>
              <Link
                href={`${Routes.USER}/${asset?.owner?.address}`}
                className="flex w-30% items-center gap-3 p-5"
              >
                <Avatar
                  className="h-16 w-16"
                  name={asset?.owner?.userName}
                  src={asset?.owner?.avatar}
                />
                <div className="flex flex-col">
                  <TypographySmall className="font-bold" text="Owner" />
                  <TypographyH4
                    className="whitespace-nowrap text-ellipsis overflow-hidden w-[100px]"
                    text={asset?.owner?.userName}
                  />
                </div>
              </Link>
              {asset?.collection && (
                <Link
                  href={`${Routes.COLLECTION}/${asset?.collection?.id}`}
                  className="flex w-30% items-center gap-3 p-5"
                >
                  <Avatar
                    className="h-12 w-12"
                    name={asset.collection.name}
                    src={
                      asset?.collection?.avatar || "/collection/collection.png"
                    }
                  />
                  <div className="flex flex-col truncate">
                    <TypographySmall className="font-bold" text="Collection" />
                    <TypographyH4
                      className="whitespace-nowrap text-ellipsis overflow-hidden w-[100px]"
                      text={asset?.collection?.name}
                    />
                  </div>
                </Link>
              )}
            </div>
            <div className="flex flex-col gap-5 ">
              {asset?.auction && (
                <div className="flex flex-col gap-5 border-b p-5">
                  <TypographyH2 text="Auction Ends in:" />
                  <TypographyH3
                    className="text-primary/60"
                    text={
                      auctionEndDate && (
                        <CountDown date={auctionEndDate} renderer={onRender} />
                      )
                    }
                  />
                </div>
              )}
              <div className="flex flex-col gap-10 p-5">
                <div>
                  <TypographyP text="Current Price" />
                  <div className="flex gap-2 items-end">
                    <TypographyH2
                      text={`${asset?.auction ? asset.auction.highestBid : asset?.price} ETH`}
                    />
                    <TypographyP
                      className="text-primary/60"
                      text={`$${parseFloat(usdPrice).toLocaleString()}`}
                    />
                  </div>
                </div>
                {asset?.owner?.address != address ? (
                  <>
                    {asset?.auction ? (
                      <PlaceBidModal
                        tokenId={asset?.tokenId as number}
                        auctionId={asset.auction.auctionId as number}
                        floorPrice={asset.price}
                        highestBid={asset.auction.highestBid}
                      />
                    ) : (
                      <BuyModal
                        tokenId={asset?.tokenId as number}
                        price={asset?.price as string}
                      />
                    )}
                  </>
                ) : asset?.status === "NotOnSale" ? (
                  <ResellAssetModal tokenId={asset?.tokenId as number} />
                ) : asset?.auction ? (
                  <CancelAuctionModal
                    tokenId={asset.tokenId as number}
                    auctionId={asset?.auction?.auctionId as number}
                  />
                ) : (
                  <CancelSaleModal id={asset?.id as number} />
                )}
              </div>
            </div>
            {asset?.auction && <NFTBids id={asset?.id} />}
          </div>
        </div>
      )}
    </>
  );
};

export default NFTDetailRight;

type MenuProps = {
  asset: NFT;
};

const Menu = ({ asset }: MenuProps) => {
  return (
    <DropdownMenu>
      <DropdownMenuTrigger asChild>
        <Button size={"icon"} variant={"ghost"}>
          <BsThreeDots size={25} />
        </Button>
      </DropdownMenuTrigger>
      <DropdownMenuContent className="w-[200px] rounded-lg">
        {/* <DropdownMenuItem asChild>
          <ChangePriceModal tokenId={asset?.tokenId as number} />
  </DropdownMenuItem>*/}
        <DropdownMenuItem asChild>
          <TransferModal tokenId={asset.tokenId as number} />
        </DropdownMenuItem>
        <DropdownMenuItem asChild>
          <DeleteAssetModal tokenId={asset?.tokenId as number} />
        </DropdownMenuItem>
      </DropdownMenuContent>
    </DropdownMenu>
  );
};
