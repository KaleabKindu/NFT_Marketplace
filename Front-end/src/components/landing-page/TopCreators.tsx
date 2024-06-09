"use client";
import Image from "next/image";
import { TypographyH2, TypographyH4, TypographyP } from "../common/Typography";
import { Card } from "../ui/card";
import { Avatar } from "../common/Avatar";
import { Button } from "../ui/button";
import { Badge } from "../ui/badge";
import { useEffect, useState, MouseEvent } from "react";
import { Routes } from "@/routes";
import { User } from "@/types";
import Link from "next/link";
import {
  useFollowUserMutation,
  useGetTopCreatorsQuery,
  useUnFollowUserMutation,
} from "@/store/api";
import UsersShimmers from "../common/shimmers/UsersShimmers";
import Error from "../common/Error";
import NoData from "../common/NoData";
import { useAccount } from "wagmi";
import { AiOutlineLoading3Quarters } from "react-icons/ai";

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
  removeUser?: (a: string) => void;
};

export const Creator = ({
  index,
  user,
  removeUser,
  showRank = true,
}: CreatorProps) => {
  const { address } = useAccount();
  const [followUser, { isLoading: followingUser }] = useFollowUserMutation();
  const [unfollowUser, { isLoading: unfollowingUser }] =
    useUnFollowUserMutation();
  const [following, setFollowing] = useState(user.following || false);
  const handleFollow = async (e: MouseEvent<HTMLButtonElement>) => {
    e.stopPropagation();
    try {
      await followUser({
        follower: address as string,
        followee: user.address,
      }).unwrap();
    } catch (error) {
      console.log("error", error);
    } finally {
      setFollowing(!following);
    }
  };
  const handleUnfollow = async (e: MouseEvent<HTMLButtonElement>) => {
    e.stopPropagation();
    try {
      await unfollowUser({
        unfollower: address as string,
        unfollowee: user.address,
      }).unwrap();
    } catch (error) {
      console.log("error", error);
    } finally {
      setFollowing(!following);
      removeUser?.(user.address);
    }
  };

  return (
    <div className="col-span-12 sm:col-span-6 md:col-span-4 lg:col-span-3">
      <Card className="relative flex flex-col rounded-2xl justify-evenly items-center h-[15rem] bg-secondary group">
        <div className="relative w-full h-[50%]">
          <div className="relative rounded-t-2xl overflow-clip w-full h-full  ">
            <Image
              className="object-cover rounded-t-2xl group-hover:scale-105"
              src={
                user.profile_background || "/landing-page/futuristic-blue.jpg"
              }
              fill
              alt=""
            />
          </div>
          <Link
            href={`${Routes.USER}/${user.address}`}
            className="absolute -bottom-10 left-1/2 right-1/2 -translate-x-1/2 hover:border-2 hover:border-primary z-20 w-fit rounded-full bg-secondary p-3"
          >
            <Avatar
              className="h-16 w-16"
              name={user.userName}
              src={user.avatar}
            />
          </Link>
        </div>
        <div className="flex gap-3 items-center justify-around w-full h-[50%] p-3 pt-8">
          <div>
            <Link href={`${Routes.USER}/${user.address}`}>
              <TypographyH4
                className="whitespace-nowrap text-ellipsis overflow-hidden hover:text-primary"
                text={user.userName}
              />
            </Link>
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
            type="button"
            className="text-md rounded-full"
            variant={following ? "destructive" : "default"}
            onClick={following ? handleUnfollow : handleFollow}
          >
            {followingUser || unfollowingUser ? (
              <AiOutlineLoading3Quarters className="animate-spin" />
            ) : following ? (
              "Unfollow"
            ) : (
              "Follow"
            )}
          </Button>
        </div>
        {showRank && (
          <Badge className="absolute top-5 left-5 text-md bg-accent text-accent-foreground">
            {index + 1}
          </Badge>
        )}
      </Card>
    </div>
  );
};

export default TopCreators;
