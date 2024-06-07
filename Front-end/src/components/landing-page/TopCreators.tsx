"use client";
import Image from "next/image";
import { TypographyH2, TypographyH4, TypographyP } from "../common/Typography";
import { Card } from "../ui/card";
import { Avatar } from "../common/Avatar";
import { Button } from "../ui/button";
import { Badge } from "../ui/badge";
import { useEffect, useState } from "react";
import { Routes } from "@/routes";
import { User } from "@/types";
import Link from "next/link";
import { useGetTopCreatorsQuery } from "@/store/api";
import UsersShimmers from "../common/shimmers/UsersShimmers";
import Error from "../common/Error";
import NoData from "../common/NoData";

type Props = {};

const TopCreators = (props: Props) => {
  const { data, isLoading, isError, refetch } = useGetTopCreatorsQuery({
    page: 1,
    size: 8,
  });
  const [creators, setCreators] = useState<User[]>([]);
  useEffect(() => {
    if (data) {
      setCreators([...data.value]);
    }
  });
  return (
    <div className="flex flex-col gap-5">
      <TypographyH2 text="Top Creators" />
      <TypographyH4 text="Checkout Top Rated Creators" />
      <div className="grid grid-cols-12 items-center justify-center gap-5">
        {isLoading ? (
          <UsersShimmers elements={8} />
        ) : isError ? (
          <Error retry={refetch} />
        ) : creators && creators.length > 0 ? (
          creators.map((user, index) => (
            <Creator key={index} user={user} index={index} showRank={false} />
          ))
        ) : (
          <NoData message="No Creators found" />
        )}
      </div>
    </div>
  );
};

type CreatorProps = {
  index: number;
  user: User;
  showRank?: boolean;
};

export const Creator = ({ index, user, showRank = true }: CreatorProps) => {
  const [following, setFollowing] = useState(false);

  return (
    <Link
      href={`${Routes.USER}/${user.address}`}
      className="col-span-12 sm:col-span-6 md:col-span-4 lg:col-span-3"
    >
      <Card className="relative flex flex-col rounded-2xl justify-evenly items-center h-[15rem] bg-secondary group">
        <div className="relative rounded-t-2xl overflow-clip w-full h-[75%] ">
          <Image
            className="object-cover rounded-t-2xl group-hover:scale-105"
            src={user.profile_background || "/landing-page/futuristic-blue.jpg"}
            fill
            alt=""
          />
        </div>
        <div className="relative z-40 rounded-full bg-secondary -mt-12 p-3">
          <Avatar className="h-16 w-16" src={user.avatar} />
        </div>
        <div className="flex gap-3 items-center justify-around w-full p-3">
          <div>
            <TypographyH4 text={user.username.slice(0, 10)} />
            <div className="flex items-center gap-3">
              <TypographyP
                className="font-semibold text-primary/80"
                text="Sales: "
              />
              <TypographyP
                className="font-semibold"
                text={`${user.sales}ETH`}
              />
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
    </Link>
  );
};

export default TopCreators;
