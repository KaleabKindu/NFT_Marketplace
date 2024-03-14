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

type Props = {};

const NFTCard = (props: Props) => {
  const [liked, setLiked] = useState(false);
  const [likes, setLikes] = useState(22);
  const handleLikes = () => {
    setLiked(!liked);
    if (liked) {
      setLikes(likes - 1);
    } else {
      setLikes(likes + 1);
    }
  };

  return (
    <Link href={`${Routes.PRODUCT}/${Math.floor(Math.random() * 10000000000)}`}>
      <Card className="md:max-w-[25rem] w-full rounded-3xl group ">
        <div className="relative  min-h-[20rem] h-full rounded-t-3xl overflow-clip">
          <Image
            className="object-cover rounded-t-3xl group-hover:scale-105"
            src="/landing-page/audio-category.jpg"
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
        <div className="flex flex-col gap-5 p-5">
          <div className="flex items-center justify-between">
            <TypographyH4 text="Clone #1234" />
          </div>
          <div className="p-3 bg-primary/5">
            <TypographySmall text="Current Bid" />
            <TypographyH4 className="text-primary/60" text="0.001245ETH" />
          </div>
        </div>
      </Card>
    </Link>
  );
};

export default NFTCard;
