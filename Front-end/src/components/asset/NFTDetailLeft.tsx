"use client";
import {
  TypographyH4,
  TypographyP,
  TypographySmall,
} from "../common/Typography";
import { useEffect, useState } from "react";
import { Badge } from "../ui/badge";
import { Button } from "../ui/button";
import { FaHeart } from "react-icons/fa";
import {
  Accordion,
  AccordionContent,
  AccordionItem,
  AccordionTrigger,
} from "@/components/ui/accordion";
import clsx from "clsx";
import { CopyToClipboard } from "react-copy-to-clipboard";
import { useToast } from "../ui/use-toast";
import { LuEye, LuEyeOff } from "react-icons/lu";
import { TbCopy } from "react-icons/tb";
import { IconType } from "react-icons";
import { categories } from "@/data";
import VideoPlayer from "../explore/assets/VideoPlayer";
import AudioPlayer from "../explore/assets/AudioPlayer";
import Link from "next/link";
import { Routes } from "@/routes";
import NFTLeftShimmer from "../common/shimmers/NFTLeftShimmer";
import { NFT } from "@/types";
import CustomImage from "../common/CustomImage";
import { useToggleNFTlikeMutation } from "@/store/api";
import { useAppSelector } from "@/store/hooks";
import { useWeb3Modal } from "@web3modal/wagmi/react";
import { useAccount, useContractRead } from "wagmi";
import NftAbi from "@/data/abi/marketplace.json";

type Props = {
  asset?: NFT;
  isLoading?: boolean;
};

const NFTDetailLeft = ({ asset, isLoading }: Props) => {
  const { toast } = useToast();
  const [liked, setLiked] = useState(false);
  const [likes, setLikes] = useState(0);
  const [showFiles, setShowFiles] = useState(false);
  const [toggleLike] = useToggleNFTlikeMutation();
  const session = useAppSelector((state) => state.auth.session);
  const { open } = useWeb3Modal();
  const { address } = useAccount();
  const [args, setArgs] = useState<any[]>([]);
  const handleLikes = async () => {
    if (!session) {
      open();
      return;
    }
    toggleLike(asset?.id as number).unwrap();
    setLiked(!liked);
    if (liked) {
      setLikes(likes - 1);
    } else {
      setLikes(likes + 1);
    }
  };
  useEffect(() => {
    if (asset) {
      setLikes(asset.likes as number);
      setLiked(asset.liked as boolean);
    }
  }, [asset]);
  const Icon = categories.find((cat) => cat.value === asset?.category)
    ?.icon as IconType;

  const { data: tokenUri } = useContractRead({
    address: process.env.NEXT_PUBLIC_CONTRACT_ADDRESS as `0x${string}`,
    abi: NftAbi,
    functionName: "getTokenUri",
    args: args,
    onError: (e) => {
      console.log("error", e);
    },
  });
  useEffect(() => {
    if (asset) {
      setArgs([asset.tokenId]);
    }
  }, [asset]);
  return (
    <>
      {isLoading ? (
        <NFTLeftShimmer />
      ) : (
        <div className="flex-1 flex flex-col gap-10">
          <div className="relative h-[25rem] lg:h-[50rem]">
            {/* Image */}
            {asset?.image && (
              <CustomImage
                className="object-cover rounded-3xl"
                src={asset.image || ""}
                fill
                alt=""
              />
            )}
            {/* Videos */}
            {asset?.video && (
              <VideoPlayer className="rounded-3xl" url={asset.video} />
            )}

            {/* Audios */}
            {asset?.audio && <AudioPlayer url={asset.audio} />}

            <Badge className="flex items-center gap-3 absolute top-5 right-5 bg-background/30 hover:bg-background text-foreground">
              <Button
                variant="ghost"
                size={"sm"}
                className="rounded-full h-auto p-2"
                onClick={handleLikes}
              >
                <FaHeart
                  className={`${liked && "text-red-500"} p-0`}
                  size={25}
                />
              </Button>
              <TypographySmall text={likes} />
            </Badge>
            <Badge className="p-2 absolute top-5 left-5 bg-background/30 hover:bg-background text-foreground">
              <Icon size={30} />
            </Badge>
          </div>
          <div>
            <Accordion type="single" collapsible defaultValue="item-1">
              <AccordionItem value="item-1">
                <AccordionTrigger className="bg-accent text-accent-foreground px-5 rounded-md mb-5">
                  Description
                </AccordionTrigger>
                <AccordionContent className="px-5">
                  {asset?.description || "No Description"}
                </AccordionContent>
              </AccordionItem>

              <AccordionItem value="item-2">
                <AccordionTrigger className="bg-accent text-accent-foreground px-5 rounded-md mb-5 border-b">
                  Details
                </AccordionTrigger>
                <AccordionContent>
                  <div className="flex flex-col gap-5">
                    <div className="flex justify-between items-center">
                      <TypographyH4 text={"Token ID: "} />
                      <TypographyP text={asset?.tokenId} />
                    </div>
                    <div className="flex justify-between items-center">
                      <TypographyH4 text={"Royality: "} />
                      <TypographyP text={`${asset?.royalty}%`} />
                    </div>
                    <div className="flex justify-between items-center">
                      <TypographyH4
                        className="whitespace-nowrap pr-5"
                        text={"Transaction Hash: "}
                      />
                      <Link
                        className="hover:text-primary"
                        href={`${Routes.ETHER_TRANSACTIONS}/${asset?.transactionHash}`}
                      >
                        <TypographyP
                          className="whitespace-nowrap text-ellipsis overflow-hidden max-w-[400px]"
                          text={asset?.transactionHash}
                        />
                      </Link>
                    </div>
                    {address === asset?.owner?.address && (
                      <div className="flex justify-between items-center">
                        <TypographyH4 text={"Files: "} />
                        <div className="flex gap-1 items-center">
                          <CopyToClipboard
                            text={tokenUri as string}
                            onCopy={() =>
                              showFiles &&
                              toast({
                                title: "Copied to Clipboard",
                              })
                            }
                          >
                            <Badge
                              variant={"secondary"}
                              className={clsx(
                                "flex gap-2 items-center cursor-pointer",
                                { "blur-sm": !showFiles },
                              )}
                            >
                              <TypographyP
                                className="whitespace-nowrap text-ellipsis overflow-hidden max-w-[400px] text-right select-none"
                                text={tokenUri as string}
                              />
                              <TbCopy
                                className="hover:text-primary"
                                size={20}
                              />
                            </Badge>
                          </CopyToClipboard>
                          <Button
                            variant={"ghost"}
                            size={"icon"}
                            className="rounded-full"
                            onClick={() => setShowFiles(!showFiles)}
                          >
                            {!showFiles ? (
                              <LuEye size={20} />
                            ) : (
                              <LuEyeOff size={20} />
                            )}
                          </Button>
                        </div>
                      </div>
                    )}
                  </div>
                </AccordionContent>
              </AccordionItem>
            </Accordion>
          </div>
        </div>
      )}
    </>
  );
};

export default NFTDetailLeft;
