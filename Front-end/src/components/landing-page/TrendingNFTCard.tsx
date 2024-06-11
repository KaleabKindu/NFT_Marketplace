"use client";
import Image from "next/image";
import { Card } from "../ui/card";
import {
  TypographyH3,
  TypographyH4,
  TypographyP,
  TypographySmall,
} from "../common/Typography";
import { Button } from "../ui/button";
import { FaHeart } from "react-icons/fa";
import { Badge } from "../ui/badge";
import CountDown from "count-down-react";
import { useMemo, useState } from "react";
import { Routes } from "@/routes";
import Link from "next/link";
import { NFT } from "@/types";
import { categories } from "@/data";
import { IconType } from "react-icons";
import CustomImage from "../common/CustomImage";
import { useWeb3Modal } from "@web3modal/wagmi/react";
import { useAppSelector } from "@/store/hooks";

type Props = {
  asset: NFT;
};

const TrendingNFTCard = ({ asset }: Props) => {
  const [liked, setLiked] = useState(false);
  const [likes, setLikes] = useState(22);
  const { open } = useWeb3Modal();
  const session = useAppSelector((state) => state.auth.session);
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
    if (!session) {
      open();
      return;
    }
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
  const auctionEndDate = useMemo(() => {
    const timestamp = asset?.auction?.auctionEnd;
    if (timestamp && !isNaN(timestamp)) {
      const date = new Date(timestamp * 1000);
      return date;
    }
    return new Date();
  }, [asset?.auction?.auctionEnd]);
  return (
    <Link href={`${Routes.PRODUCT}/${asset.id}`}>
      <Card className="p-5 bg-accent hover:bg-accent rounded-3xl max-w-[35rem] w-full">
        <div className="relative overflow-clip rounded-3xl h-[30rem]">
          <CustomImage
            className="object-cover rounded-3xl hover:scale-105"
            src={asset.image || ""}
            fill
            alt=""
          />
          <div className="absolute rounded-bl-3xl rounded-tr-2xl transform skew-x-[35deg] -top-0 right-0 py-[0.5rem] mr-[-1rem] w-fit px-10 bg-accent ">
            <div className="flex flex-col text-center transform skew-x-[-35deg]">
              <TypographySmall
                className="text-primary/60 text-sm"
                text="Remaining Time"
              />
              <TypographyP
                className="text-primary/60"
                text={
                  auctionEndDate && (
                    <CountDown date={auctionEndDate} renderer={onTick} />
                  )
                }
              />
            </div>
          </div>
          <div className="absolute rounded-tr-3xl rounded-bl-xl transform skew-x-[30deg] bottom-0 left-1 py-[0.5rem] ml-[-1.5rem] pl-[2rem] min-w-[50%] bg-accent ">
            <div className="flex flex-col items-start gap-3 transform skew-x-[-30deg]">
              <TypographyP
                className="whitespace-nowrap text-ellipsis overflow-hidden text-primary/60 capitalize"
                text={asset.name}
              />
              <Card className="relative p-2 px-4 bg-primary/5 border-4">
                <Badge
                  variant={"secondary"}
                  className="absolute -top-3 py-0.5 left-1"
                >
                  Bid
                </Badge>
                <TypographySmall
                  className="text-primary/60 font-semibold"
                  text={`${asset.auction?.highestBid}ETH`}
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
