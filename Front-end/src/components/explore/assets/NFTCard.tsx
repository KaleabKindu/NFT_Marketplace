"use client";
import Image from "next/image";
import { Card } from "../../ui/card";
import { TypographyH4, TypographySmall } from "../../common/Typography";
import { Button } from "../../ui/button";
import { FaHeart } from "react-icons/fa";
import { Badge } from "../../ui/badge";
import { useEffect, useRef, useState } from "react";
import Link from "next/link";
import { Routes } from "@/routes";
import { NFT } from "@/types";
import { GoClock } from "react-icons/go";
import moment from "moment";
import AudioPlayer from "./AudioPlayer";
import VideoPlayer from "./VideoPlayer";
import { categories } from "@/data";
import { IconType } from "react-icons";
type Props = {
  asset: NFT;
};

const NFTCard = ({ asset }: Props) => {
  const cardRef = useRef<HTMLDivElement>(null);
  const [liked, setLiked] = useState(asset?.liked as boolean);
  const [likes, setLikes] = useState(asset?.likes as number);
  const [imgSrc, setImgSrc] = useState(asset.image as string);
  const [audioWidth, setAudioWidth] = useState(0);
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
  const handleImageError = () => {
    setImgSrc("/image-placeholder.png");
  };
  useEffect(() => {
    if (cardRef.current) setAudioWidth(cardRef.current.offsetWidth as number);
  }, [cardRef]);
  return (
    <Link
      href={`${Routes.PRODUCT}/${asset.tokenId}`}
      className="col-span-12 sm:col-span-6 lg:col-span-3"
    >
      <Card ref={cardRef} className="w-full rounded-3xl group ">
        <div className="relative  min-h-[20rem] h-full rounded-t-3xl overflow-clip">
          {/* Images */}
          {asset.image && (
            <Image
              className="object-cover rounded-t-3xl group-hover:scale-105"
              src={imgSrc}
              onError={handleImageError}
              fill
              alt=""
            />
          )}

          {/* Videos */}
          {asset.video && (
            <VideoPlayer className="rounded-t-3xl" url={asset.video} />
          )}

          {/* Audios */}
          {asset.audio && <AudioPlayer url={asset.audio} width={audioWidth} />}

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
            <Icon size={25} />
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
