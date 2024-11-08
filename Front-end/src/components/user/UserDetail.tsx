import Image from "next/image";
import { TypographyH2, TypographyP } from "../common/Typography";
import { TbCopy } from "react-icons/tb";
import { Button } from "../ui/button";
import { BsTwitterX, BsFacebook, BsTelegram, BsYoutube } from "react-icons/bs";
import Link from "next/link";
import CopyToClipboard from "react-copy-to-clipboard";
import { Badge } from "../ui/badge";
import { useToast } from "../ui/use-toast";
import { useAccount } from "wagmi";
import { MdEdit } from "react-icons/md";
import {
  useFollowUserMutation,
  useGetUserDetailsQuery,
  useUnFollowUserMutation,
} from "@/store/api";
import { Skeleton } from "../ui/skeleton";
import { Routes } from "@/routes";
import Error from "../common/Error";
import CustomImage from "../common/CustomImage";
import { useEffect, useState } from "react";
import { AiOutlineLoading3Quarters } from "react-icons/ai";
import { useAppSelector } from "@/store/hooks";
import { useWeb3Modal } from "@web3modal/wagmi/react";

type Props = {
  address: string;
};

const UserDetail = ({ address }: Props) => {
  const { toast } = useToast();
  const account = useAccount();
  const {
    data: user,
    isLoading,
    isError,
    refetch,
  } = useGetUserDetailsQuery(address);
  const [following, setFollowing] = useState(false);
  const [followUser, { isLoading: followingUser }] = useFollowUserMutation();
  const [unfollowUser, { isLoading: unfollowingUser }] =
    useUnFollowUserMutation();
  const session = useAppSelector((state) => state.auth.session);
  const { open } = useWeb3Modal();

  const handleFollow = async () => {
    if (!session) {
      open();
      return;
    }
    try {
      await followUser({
        follower: account.address as string,
        followee: user?.address as string,
      }).unwrap();
    } catch (error) {
      console.log("error", error);
    } finally {
      setFollowing(!following);
    }
  };
  const handleUnfollow = async () => {
    if (!session) {
      open();
      return;
    }
    try {
      await unfollowUser({
        unfollower: account.address as string,
        unfollowee: user?.address as string,
      }).unwrap();
    } catch (error) {
      console.log("error", error);
    } finally {
      setFollowing(!following);
    }
  };
  useEffect(() => {
    if(user){
      setFollowing(user.following as boolean)
    }
  },[user])
  return (
    <div className="relative flex flex-col lg:flex-row gap-8 -mt-[15vh] w-[90%] lg:w-[85%] mx-auto bg-background border z-40 rounded-3xl p-8">
      {isLoading ? (
        <Skeleton className="w-full h-80 rounded-3xl" />
      ) : isError ? (
        <Error retry={refetch} />
      ) : (
        <>
          <div className="relative w-full h-[200px] lg:w-[300px] lg:h-[250px]">
            <CustomImage
              className="rounded-3xl object-cover"
              src={user?.avatar || ""}
              fill
              alt=""
            />
          </div>
          <div className="flex flex-col items-start gap-5">
            <TypographyH2
              className="whitespace-nowrap text-ellipsis overflow-hidden"
              text={user?.userName}
            />
            <CopyToClipboard
              text={address}
              onCopy={() =>
                toast({
                  title: "Copied to Clipboard",
                })
              }
            >
              <Badge
                variant={"secondary"}
                className="flex gap-2 items-center cursor-pointer"
              >
                <TypographyP
                  className="whitespace-nowrap text-ellipsis overflow-hidden text-right text-base select-none"
                  text={address}
                />
                <TbCopy size={20} />
              </Badge>
            </CopyToClipboard>
            <TypographyP text={user?.bio || "No Bio."} />
            <div className="flex gap-5">
              <Link href={user?.facebook || ""}>
                <Button
                  className="rounded-full"
                  variant={"ghost"}
                  size={"icon"}
                >
                  <BsFacebook size={25} />
                </Button>
              </Link>
              <Link href={user?.twitter || ""}>
                <Button
                  className="rounded-full"
                  variant={"ghost"}
                  size={"icon"}
                >
                  <BsTwitterX size={25} />
                </Button>
              </Link>
              <Link href={user?.youtube || ""}>
                <Button
                  className="rounded-full"
                  variant={"ghost"}
                  size={"icon"}
                >
                  <BsYoutube size={25} />
                </Button>
              </Link>
              <Link href={user?.telegram || ""}>
                <Button
                  className="rounded-full"
                  variant={"ghost"}
                  size={"icon"}
                >
                  <BsTelegram size={25} />
                </Button>
              </Link>
            </div>
          </div>
          <div className="self-start flex items-center gap-5 ml-auto">
            {account.address === address ? (
              <Link href={Routes.EDIT_PROFILE}>
                <Button
                  variant={"ghost"}
                  size={"lg"}
                  className="rounded-full p-5"
                >
                  <MdEdit size={30} />
                </Button>
              </Link>
            ) : (
              <Button
                className="rounded-full"
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
            )}
          </div>
        </>
      )}
    </div>
  );
};

export default UserDetail;
