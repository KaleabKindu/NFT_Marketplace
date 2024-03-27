"use client";
import Image from "next/image";
import { Card } from "../../ui/card";
import { TypographyH4, TypographySmall } from "../../common/Typography";
import { CiImageOn } from "react-icons/ci";
import { Button } from "../../ui/button";
import { FaHeart } from "react-icons/fa";
import { Badge } from "../../ui/badge";
import { useState } from "react";
import Link from "next/link";
import { Routes } from "@/routes";
import { NFT } from "@/types";
import { GoClock } from "react-icons/go";
import moment from "moment";
type Props = {
  asset: NFT;
};

const NFTCard = ({ asset }: Props) => {
  const [liked, setLiked] = useState(false);
  const [likes, setLikes] = useState(asset?.likes as number);
  const handleLikes = (e: any) => {
    e.preventDefault();
    setLiked(!liked);
    if (liked) {
      setLikes(likes - 1);
    } else {
      setLikes(likes + 1);
    }
  };

  return (
    <Link href={`${Routes.PRODUCT}/${asset.tokenId}`}>
      <Card className="md:max-w-[25rem] w-full rounded-3xl group ">
        <div className="relative  min-h-[20rem] h-full rounded-t-3xl overflow-clip">
          <Image
            className="object-cover rounded-t-3xl group-hover:scale-105"
            src={asset.image}
            fill
            alt=""
          />
          <Badge className="flex items-center gap-3 absolute top-5 right-5 bg-background/30 hover:bg-background text-foreground">
            <Button
              variant="ghost"
              size={"sm"}
              className="rounded-full h-auto p-2"
              onClick={handleLikes}
            >
              <FaHeart className={`${liked && "text-red-500"} p-0`} size={20} />
            </Button>
            <TypographySmall text={likes} />
          </Badge>
          <Badge className="p-2 absolute top-5 left-5 bg-background/30 hover:bg-background text-foreground">
            <CiImageOn size={25} />
          </Badge>
        </div>
        <div className="flex flex-col p-5">
          <div className="flex items-center justify-between">
            <TypographyH4 className="capitalize" text={asset.name} />
          </div>
          <div className="flex justify-between items-end">
            <div className="flex-1 bg-primary/5">
              <TypographySmall
                className=" text-xs"
                text={asset.auction ? "Current Bid" : "Price"}
              />
              <TypographyH4
                className="text-primary/60"
                text={`${asset.auction ? asset.auction.highest_bid : asset.price}ETH`}
              />
            </div>
            <div className="flex-1 w-[50%] flex flex-col items-end">
              {asset.auction && (
                <div className="flex gap-1 items-center">
                  <GoClock className="text-foreground/50" size={20} />
                  <TypographySmall
                    text={`${moment(new Date(asset.auction.auction_end)).toNow(true)} left`}
                  />
                </div>
              )}
            </div>
          </div>
        </div>
      </Card>
    </Link>
  );
};

export default NFTCard;
