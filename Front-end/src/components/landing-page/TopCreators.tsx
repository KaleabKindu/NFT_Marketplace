"use client";
import Image from "next/image";
import { TypographyH2, TypographyH4, TypographyP } from "../common/Typography";
import { Card } from "../ui/card";
import { Avatar } from "../common/Avatar";
import { Button } from "../ui/button";
import { Badge } from "../ui/badge";
import { useState } from "react";

type Props = {};

const TopCreators = (props: Props) => {
  return (
    <div className="flex flex-col gap-5">
      <TypographyH2 text="Top Creators" />
      <TypographyH4 text="Checkout Top Rated Creators on the NFT Marketplace" />
      <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 items-center gap-5">
        {Array.from({ length: 8 }).map((_, index) => (
          <Creator key={index} index={index} />
        ))}
      </div>
    </div>
  );
};

type CreatorProps = {
  index: number;
  showRank?: boolean;
};

export const Creator = ({ index, showRank = true }: CreatorProps) => {
  const [following, setFollowing] = useState(false);

  return (
    <Card className="relative flex flex-col rounded-2xl justify-evenly items-center h-[15rem] bg-secondary group">
      <div className="relative rounded-t-2xl overflow-clip w-full h-[75%] ">
        <Image
          className="object-cover rounded-t-2xl group-hover:scale-105"
          src="/landing-page/futuristic-blue.jpg"
          fill
          alt=""
        />
      </div>
      <div className="relative z-40 rounded-full bg-secondary -mt-12 p-3">
        <Avatar className="h-16 w-16" />
      </div>
      <div className="flex gap-3 items-center justify-around w-full p-3">
        <div>
          <TypographyH4 text="Bruce Banner" />
          <div className="flex items-center gap-3">
            <TypographyP
              className="font-semibold text-primary/80"
              text="Sales: "
            />
            <TypographyP className="font-semibold" text="34.5ETH" />
          </div>
        </div>
        <Button
          className="text-md rounded-full"
          onClick={() => setFollowing(!following)}
        >
          {following ? "Following" : "Follow"}
        </Button>
      </div>
      {showRank && (
        <Badge className="absolute top-5 left-5 text-md bg-accent text-accent-foreground">
          {index + 1}
        </Badge>
      )}
    </Card>
  );
};

export default TopCreators;
