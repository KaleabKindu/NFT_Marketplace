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

import { nft_detail } from "@/data";
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
import { ChangePriceModal } from "./ChangePriceModal";

type Props = {
  asset?: NFT;
  isLoading?: boolean;
};

const NFTDetailRight = ({ asset, isLoading }: Props) => {
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
  return (
    <>
      {isLoading ? (
        <NFTRightShimmer />
      ) : (
        <div className="flex-1 p-3">
          <div className="flex flex-col gap-10">
            <div className="flex justify-between items-start p-5">
              <TypographyH2 className="capitalize" text={asset?.name} />
              <Menu asset={asset as NFT} />
            </div>
            <div className="flex flex-wrap items-center lg:divide-x-2">
              <Link
                href={`${Routes.USER}/${asset?.creator?.address}`}
                className="flex flex-1 items-center gap-3 p-5"
              >
                <Avatar className="h-12 w-12" src={asset?.creator?.avatar} />
                <div className="flex flex-col">
                  <TypographySmall text="Creator" />
                  <TypographyH4 text={asset?.creator?.address.slice(2, 8)} />
                </div>
              </Link>
              <Link
                href={`${Routes.USER}/${asset?.owner?.address}`}
                className="flex flex-1 items-center gap-3 p-5"
              >
                <Avatar className="h-12 w-12" src={asset?.owner?.avatar} />
                <div className="flex flex-col">
                  <TypographySmall text="Owner" />
                  <TypographyH4 text={asset?.owner?.address.slice(2, 8)} />
                </div>
              </Link>
              <Link
                href={`${Routes.COLLECTION}/${asset?.collection?.id}`}
                className="flex flex-1 items-center gap-3 p-5"
              >
                <Avatar
                  className="h-12 w-12"
                  src={
                    asset?.collection?.avatar || "/collection/collection.png"
                  }
                />
                <div className="flex flex-col truncate">
                  <TypographySmall text="Collection" />
                  <TypographyH4 text={asset?.collection?.name} />
                </div>
              </Link>
            </div>
            <div className="flex flex-col gap-5 ">
              {asset?.auction && (
                <div className="flex flex-col gap-5 border-b p-5">
                  <TypographyH2 text="Auction Ends in:" />
                  <TypographyH3
                    className="text-primary/60"
                    text={
                      <CountDown
                        date={new Date(asset?.auction?.auction_end * 1000)}
                        renderer={onRender}
                      />
                    }
                  />
                </div>
              )}
              <div className="flex flex-col gap-10 p-5">
                <div>
                  <TypographyP text="Current Price" />
                  <div className="flex gap-2 items-end">
                    <TypographyH2 text={`${asset?.price || 0.394} ETH`} />
                    <TypographyP
                      className="text-primary/60"
                      text={`$${807.07}`}
                    />
                  </div>
                </div>
                {asset?.auction ? (
                  <PlaceBidModal
                    auctionId={asset.auction.auctionId as number}
                  />
                ) : (
                  <BuyModal
                    tokenId={asset?.tokenId as number}
                    price={asset?.price as string}
                  />
                )}
              </div>
            </div>
            {asset?.auction && <NFTBids />}
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
        <DropdownMenuItem asChild>
          <ChangePriceModal tokenId={asset?.tokenId as number} />
        </DropdownMenuItem>
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
