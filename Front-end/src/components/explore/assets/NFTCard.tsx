"use client";
import { Card } from "../../ui/card";
import {
  TypographyH4,
  TypographyP,
  TypographySmall,
} from "../../common/Typography";
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
import { useToggleNFTlikeMutation } from "@/store/api";
import CustomImage from "@/components/common/CustomImage";
import { useWeb3Modal } from "@web3modal/wagmi/react";
import { useAppSelector } from "@/store/hooks";
import { ResellAssetModal } from "./ResellAssetModal";
import { useAccount } from "wagmi";
type Props = {
  asset: NFT;
};

const NFTCard = ({ asset }: Props) => {
  const { address } = useAccount()
  const cardRef = useRef<HTMLDivElement>(null);
  const [liked, setLiked] = useState(asset?.liked as boolean);
  const [likes, setLikes] = useState(asset?.likes as number);
  const [audioWidth, setAudioWidth] = useState(0);
  const [toggleLike] = useToggleNFTlikeMutation();
  const { open } = useWeb3Modal();
  const session = useAppSelector((state) => state.auth.session);
  const handleLikes = () => {
    if (!session) {
      open();
      return;
    }
    toggleLike(asset?.id as string).unwrap();
    setLiked(!liked);
    if (liked) {
      setLikes(likes - 1);
    } else {
      setLikes(likes + 1);
    }
  };
  useEffect(() => {
    if (cardRef.current) setAudioWidth(cardRef.current.offsetWidth as number);
  }, [cardRef]);
  const Icon = categories.find((cat) => cat.value === asset.category)
    ?.icon as IconType;
  return (
    <div className="col-span-12 sm:col-span-6 lg:col-span-3">
      <Card ref={cardRef} className="w-full rounded-3xl group ">
        <div className="relative  min-h-[20rem] h-full rounded-t-3xl overflow-clip">
          {/* Images */}
          {asset.image && (
            <CustomImage
              className="object-cover rounded-t-3xl group-hover:scale-105"
              src={asset.image}
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
        <div className="flex flex-col gap-2 p-5 pt-2">
          <Link
            href={`${Routes.PRODUCT}/${asset.id}`}
            className="flex items-center justify-between"
          >
            <TypographyH4
              className="whitespace-nowrap text-ellipsis overflow-hidden hover:text-primary capitalize"
              text={asset.name}
            />
          </Link>
          <div className="flex justify-between items-end">
            <div className="flex-1 bg-primary/5">
              <TypographyP
                className="text-primary/60"
                text={`${asset.auction ? asset.auction.highestBid : asset.price} ETH`}
              />
            </div>
            <div className="flex-1 w-[50%] flex flex-col items-end">
              {asset.auction && (
                <div className="flex gap-1 items-center">
                  <GoClock className="text-foreground/50" size={20} />
                  <TypographySmall
                    text={`${moment(new Date(asset.auction.auctionEnd * 1000)).toNow(true)} left`}
                  />
                </div>
              )}
            </div>
            {asset.owner?.address === address && asset.status === "NotOnSale" && <ResellAssetModal tokenId={asset.tokenId as number} />}
          </div>
        </div>
      </Card>
    </div>
  );
};

export default NFTCard;
