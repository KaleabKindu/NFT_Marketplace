"use client";
import {
  TypographyH2,
  TypographyH3,
  TypographyH4,
  TypographyP,
  TypographySmall,
} from "../common/Typography";
import Link from "next/link";
import CountDown from "count-down-react";
import { Button } from "../ui/button";

import { ContractWriteContext } from "@/context/ContractWrite";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";
import { nft_detail } from "@/data";
import { Avatar } from "../common/Avatar";
import { Routes } from "@/routes";
import { useContext, useEffect, useState } from "react";
import { Loader2 } from "lucide-react";
import { BsThreeDots } from "react-icons/bs";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import * as z from "zod";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { MdWallet } from "react-icons/md";
import { useAccount, useBalance } from "wagmi";
import { BiTransferAlt } from "react-icons/bi";
import { MdDelete } from "react-icons/md";
import { FaDollarSign } from "react-icons/fa6";
import NFTRightShimmer from "../common/shimmers/NFTRightShimmer";
import { NFT } from "@/types";
import NFTBids from "./NFTBids";

type Props = {
  asset?: NFT;
  isLoading?: boolean;
};

const NFTDetailRight = ({ asset, isLoading }: Props) => {
  const [auction, setAuction] = useState(false);
  const {
    isLoading: writing,
    isError,
    transactionSuccess,
    contractWrite,
  } = useContext(ContractWriteContext);

  const onRender = ({
    days,
    hours,
    minutes,
    seconds,
  }: {
    days: number;
    hours: number;
    minutes: number;
    seconds: number;
  }) => {
    return (
      <div className="flex gap-2 lg:w-[70%] flex-wrap">
        <div className="flex-1 flex flex-col gap-2">
          <div>{days}</div>
          <div>Days</div>
        </div>
        <div className="flex-1 flex flex-col gap-2">
          <div>{hours}</div>
          <div>Hours</div>
        </div>
        <div className="flex-1 flex flex-col gap-2">
          <div>{minutes}</div>
          <div>Minutes</div>
        </div>
        <div className="flex-1 flex flex-col gap-2">
          <div>{seconds}</div>
          <div>Seconds</div>
        </div>
      </div>
    );
  };

  useEffect(() => {
    if (asset) {
      setAuction(asset.auction?.auctionId !== 0);
    } else {
      setAuction(true);
    }
  }, [asset]);
  return (
    <>
      {isLoading ? (
        <NFTRightShimmer />
      ) : (
        <div className="flex-1 p-3">
          <div className="flex flex-col gap-10">
            <div className="flex justify-between items-start p-5">
              <TypographyH2 className="capitalize" text={asset?.name} />
              <Menu />
            </div>
            <div className="flex flex-wrap items-center lg:divide-x-2">
              <Link
                href={`${Routes.USER}/${asset?.creator?.address}`}
                className="flex flex-1 items-center gap-3 p-5"
              >
                <Avatar className="h-12 w-12" src={asset?.creator?.avatar} />
                <div className="flex flex-col">
                  <TypographySmall text="Creator" />
                  <TypographyH4 text={asset?.creator?.address.slice(2, 8)} />
                </div>
              </Link>
              <Link
                href={`${Routes.USER}/${asset?.owner?.address}`}
                className="flex flex-1 items-center gap-3 p-5"
              >
                <Avatar className="h-12 w-12" src={asset?.owner?.avatar} />
                <div className="flex flex-col">
                  <TypographySmall text="Owner" />
                  <TypographyH4 text={asset?.owner?.address.slice(2, 8)} />
                </div>
              </Link>
              <Link
                href={`${Routes.COLLECTION}/${asset?.collection?.id}`}
                className="flex flex-1 items-center gap-3 p-5"
              >
                <Avatar
                  className="h-12 w-12"
                  src={
                    asset?.collection?.avatar || "/collection/collection.png"
                  }
                />
                <div className="flex flex-col truncate">
                  <TypographySmall text="Collection" />
                  <TypographyH4 text={asset?.collection?.name} />
                </div>
              </Link>
            </div>
            <div className="flex flex-col gap-5 ">
              {auction && (
                <div className="flex flex-col gap-5 border-b p-5">
                  <TypographyH2 text="Auction Ends in:" />
                  <TypographyH3
                    className="text-primary/60"
                    text={
                      <CountDown
                        date={
                          new Date(
                            asset?.auction?.auction_end ||
                              nft_detail.auction.auctionEnd,
                          )
                        }
                        renderer={onRender}
                      />
                    }
                  />
                </div>
              )}
              <div className="flex flex-col gap-10 p-5">
                <div>
                  <TypographyP text="Current Price" />
                  <div className="flex gap-2 items-end">
                    <TypographyH2 text={`${asset?.price || 0.394} ETH`} />
                    <TypographyP
                      className="text-primary/60"
                      text={`$${807.07}`}
                    />
                  </div>
                </div>
                {auction ? (
                  <BidModal auctionId={asset?.auction?.auctionId as number} />
                ) : (
                  <SaleModal
                    tokenId={asset?.tokenId as number}
                    price={asset?.price as string}
                  />
                )}
              </div>
            </div>
            {auction && <NFTBids />}
          </div>
        </div>
      )}
    </>
  );
};

export default NFTDetailRight;

type MenuProps = {};

const Menu = (props: MenuProps) => {
  return (
    <DropdownMenu>
      <DropdownMenuTrigger asChild>
        <Button size={"icon"} variant={"ghost"}>
          <BsThreeDots size={25} />
        </Button>
      </DropdownMenuTrigger>
      <DropdownMenuContent className="w-[200px] rounded-2xl">
        <DropdownMenuItem className="flex items-center gap-3 py-3 cursor-pointer rounded-xl w-full">
          <FaDollarSign className="ml-2" size={20} />
          <div>Change Price</div>
        </DropdownMenuItem>
        <DropdownMenuItem className="flex items-center gap-3 py-3 cursor-pointer rounded-xl w-full">
          <BiTransferAlt className="ml-2" size={20} />
          <div>Transfer</div>
        </DropdownMenuItem>
        <DropdownMenuItem className="flex items-center gap-3 py-3 cursor-pointer rounded-xl w-full">
          <MdDelete className="ml-2 hover:text-red-500" size={20} />
          <div>Delete</div>
        </DropdownMenuItem>
      </DropdownMenuContent>
    </DropdownMenu>
  );
};

const initialState = {
  price: 0.0,
};
const schema = z.object({
  price: z.number().nonnegative(),
});

type BidModalProps = {
  auctionId: number;
};
export const BidModal = ({ auctionId }: BidModalProps) => {
  const { address } = useAccount();
  const { data: balance } = useBalance({ address: address });
  const {
    waitingForTransaction,
    transactionSuccess,
    writing,
    writeSuccess,
    contractWrite,
  } = useContext(ContractWriteContext);
  const form = useForm<{ price: number }>({
    resolver: zodResolver(schema),
    defaultValues: initialState,
  });
  const onSubmit = () => {
    contractWrite("placeBid", form.getValues("price").toString(), [auctionId]);
  };
  useEffect(() => {
    if (transactionSuccess) {
      close();
    }
  }, [transactionSuccess]);
  return (
    <Dialog>
      <DialogTrigger asChild>
        <Button type="button" className="flex-1 lg:w-[50%] w-full">
          Place Bid
        </Button>
      </DialogTrigger>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Place Bid</DialogTitle>
          <DialogDescription className="flex flex-col gap-5 pt-10">
            <Form {...form}>
              <div className="flex items-center justify-between p-2 rounded-lg ">
                <div className="flex gap-3 items-center">
                  <MdWallet size={35} />
                  <TypographyP className="font-bold" text="Balance" />
                </div>
                <TypographyP
                  className="font-bold"
                  text={`${balance?.formatted ?? "0"} ${balance?.symbol ?? "ETH"}`}
                />
              </div>
              <form
                onSubmit={form.handleSubmit(onSubmit)}
                className="flex flex-col gap-5"
              >
                <FormField
                  control={form.control}
                  name="price"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Bid Price</FormLabel>
                      <FormControl className="flex">
                        <div className="relative flex items-stretch">
                          <Input
                            id="price"
                            placeholder="Enter Bid Price"
                            className="h-auto"
                            {...field}
                            value={field.value > 0 ? field.value : undefined}
                            onChange={(e) =>
                              field.onChange(parseFloat(e.target.value))
                            }
                          />
                          <TypographyP
                            className="absolute border-l-2 right-0 px-2 py-1 font-bold"
                            text="ETH"
                          />
                        </div>
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />
                <Button
                  type="submit"
                  disabled={writing}
                  className="rounded-full self-end"
                  size="lg"
                >
                  {writing ? (
                    <>
                      <Loader2 className="mr-2 h-6 w-6 animate-spin" />
                      Bidding
                    </>
                  ) : (
                    "Bid"
                  )}
                </Button>
              </form>
            </Form>
          </DialogDescription>
        </DialogHeader>
      </DialogContent>
    </Dialog>
  );
};
type SaleModalProps = {
  tokenId: number;
  price: string;
};
export const SaleModal = ({ tokenId, price }: SaleModalProps) => {
  const { address } = useAccount();
  const { data: balance } = useBalance({ address: address });
  const {
    waitingForTransaction,
    transactionSuccess,
    writing,
    writeSuccess,
    contractWrite,
  } = useContext(ContractWriteContext);

  const handleBuy = () => {
    contractWrite("buyAsset", price, [tokenId]);
  };
  useEffect(() => {
    if (transactionSuccess) {
      close();
    }
  }, [transactionSuccess]);
  return (
    <Dialog>
      <DialogTrigger asChild>
        <Button className="flex-1 lg:w-[50%] w-full" variant={"secondary"}>
          Buy Now
        </Button>
      </DialogTrigger>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Buy Now</DialogTitle>
          <DialogDescription className="flex flex-col gap-5 pt-10">
            <div className="flex items-center justify-between p-2 rounded-lg ">
              <div className="flex gap-3 items-center">
                <MdWallet size={35} />
                <TypographyP className="font-bold" text="Balance" />
              </div>
              <TypographyP
                className="font-bold"
                text={`${balance?.formatted ?? "0"} ${balance?.symbol ?? "ETH"}`}
              />
            </div>
            <div className="flex flex-col gap-5">
              <div>
                <TypographyP text="Price" />
                <div className="flex items-stretch bg-secondary rounded-md px-2">
                  <TypographyP
                    className="flex-1 px-2 py-1 font-bold"
                    text={price}
                  />
                  <TypographyP
                    className="flex-initial border-l-2 px-2 py-1 font-bold"
                    text="ETH"
                  />
                </div>
              </div>
              <Button
                type="button"
                disabled={writing}
                className="rounded-full self-end"
                size="lg"
                onClick={handleBuy}
              >
                {writing ? (
                  <>
                    <Loader2 className="mr-2 h-6 w-6 animate-spin" />
                    Buying
                  </>
                ) : (
                  "Buy"
                )}
              </Button>
            </div>
          </DialogDescription>
        </DialogHeader>
      </DialogContent>
    </Dialog>
  );
};
