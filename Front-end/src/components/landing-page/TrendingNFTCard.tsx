"use client";
import Image from "next/image";
import { Card } from "../ui/card";
import {
  TypographyH3,
  TypographyH4,
  TypographySmall,
} from "../common/Typography";
import { Button } from "../ui/button";
import { FaHeart } from "react-icons/fa";
import { Badge } from "../ui/badge";
import CountDown from "count-down-react";
import { useState } from "react";
import { Routes } from "@/routes";
import Link from "next/link";
import { NFT } from "@/types";
import { categories } from "@/data";
import { IconType } from "react-icons";

type Props = {
  asset: NFT;
};

const TrendingNFTCard = ({ asset }: Props) => {
  const [liked, setLiked] = useState(false);
  const [likes, setLikes] = useState(22);
  const onTick = ({
    hours,
    minutes,
    seconds,
  }: {
    hours: number;
    minutes: number;
    seconds: number;
  }) => {
    return <>{`${hours}h:${minutes}m:${seconds}s`}</>;
  };
  const handleLikes = (e: any) => {
    e.preventDefault();
    setLiked(!liked);
    if (liked) {
      setLikes(likes - 1);
    } else {
      setLikes(likes + 1);
    }
  };
  const Icon = categories.find((cat) => cat.value === asset.category)
    ?.icon as IconType;
  return (
    <Link href={`${Routes.PRODUCT}/${asset.tokenId}`}>
      <Card className="p-5 bg-accent hover:bg-accent rounded-3xl max-w-[35rem] w-full">
        <div className="relative overflow-clip rounded-3xl h-[30rem]">
          <Image
            className="object-cover rounded-3xl hover:scale-105"
            src={asset.image as string}
            fill
            alt=""
          />
          <div className="absolute rounded-bl-3xl transform skew-x-[45deg] -top-0 right-0 py-[0.5rem] w-[60%] mr-[-3rem] bg-accent ">
            <div className="text-center transform skew-x-[-45deg]">
              <TypographySmall
                className="text-primary/60"
                text="Remaining Time"
              />
              <TypographyH3
                className="text-primary/60"
                text={
                  <CountDown
                    date={asset.auction?.auction_end as number}
                    renderer={onTick}
                  />
                }
              />
            </div>
          </div>
          <div className="absolute rounded-tr-3xl transform skew-x-[50deg] bottom-0 left-0 py-[0.5rem] w-[70%] ml-[-4rem] bg-accent ">
            <div className="flex flex-col items-start pl-[4.5rem] gap-3 transform skew-x-[-50deg]">
              <TypographyH3
                className="text-primary/60 capitalize"
                text={asset.name}
              />
              <Card className="relative p-2 bg-primary/5 border-4">
                <Badge variant={"secondary"} className="absolute -top-3 left-1">
                  Current Bid
                </Badge>
                <TypographyH4
                  className="text-primary/60"
                  text={`${asset.auction?.highest_bid}ETH`}
                />
              </Card>
            </div>
          </div>
          <Badge className="flex items-center gap-3 absolute bottom-5 right-5 bg-background/30 text-foreground">
            <Button
              variant="ghost"
              size={"sm"}
              className="rounded-full h-auto p-2"
              onClick={handleLikes}
            >
              <FaHeart className={`${liked && "text-red-500"} p-0`} size={20} />
            </Button>
            <TypographySmall text={asset.likes} />
          </Badge>
          <Badge className="p-2 absolute top-5 left-5 bg-background/30 hover:bg-background text-foreground">
            <Icon size={30} />
          </Badge>
        </div>
      </Card>
    </Link>
  );
};

export default TrendingNFTCard;
