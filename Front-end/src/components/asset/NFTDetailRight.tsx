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
import {
  Accordion,
  AccordionContent,
  AccordionItem,
  AccordionTrigger,
} from "@/components/ui/accordion";
import { ContractWriteContext } from "@/context/ContractWrite";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { bids, nft_detail, offers } from "@/data";
import { Avatar } from "../common/Avatar";
import { Routes } from "@/routes";
import { useContext, useEffect, useState } from "react";
import { Loader2 } from "lucide-react";
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
import { useGetNFTQuery } from "@/store/api";
import { Skeleton } from "../ui/skeleton";

type Props = {
  id: string;
};

const NFTDetailRight = ({ id }: Props) => {
  const { data: asset } = useGetNFTQuery(id);
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
      <div className="grid grid-cols-4 gap-2 lg:w-[70%]">
        <div>{days}</div>
        <div>{hours}</div>
        <div>{minutes}</div>
        <div>{seconds}</div>
        <div>Days</div>
        <div>Hours</div>
        <div>Minutes</div>
        <div>Seconds</div>
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
    <div className="flex-1 p-3">
      <div className="flex flex-col gap-10">
        <TypographyH2 text={asset?.name || `Clone ${id}`} />
        <div className="flex flex-wrap items-center lg:divide-x-2">
          <Link
            href={`${Routes.USER}/${asset?.creator?.publicAddress || nft_detail.creator.address}`}
            className="flex lg:min-w-[25%] items-center gap-3 p-5"
          >
            <Avatar className="h-12 w-12" />
            <div className="flex flex-col">
              <TypographySmall text="Creator" />
              <TypographyH4
                text={asset?.creator?.userName || nft_detail.creator.name}
              />
            </div>
          </Link>
          <Link
            href={`${Routes.USER}/${asset?.owner?.publicAddress || nft_detail.owner.address}`}
            className="flex lg:min-w-[25%] items-center gap-3 p-5"
          >
            <Avatar className="h-12 w-12" />
            <div className="flex flex-col">
              <TypographySmall text="Owner" />
              <TypographyH4
                text={asset?.owner?.userName || nft_detail.owner.name}
              />
            </div>
          </Link>
          <Link href={`${Routes.COLLECTION}`} className='flex items-center gap-3 p-5'>
            <Avatar className='h-12 w-12' src='/collection/collection.png'/>
            <div className='flex flex-col'>
              <TypographySmall text='Collection'/>
              <TypographyH4 text={nft_detail.collection.name}/>
            </div>
          </Link>
        </div>
        <div className="flex flex-col gap-5 border rounded-md bg-secondary/50">
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
               <BidModal
                auctionId={asset?.auction?.auctionId as number}
                /> ): (
                <SaleModal
                  tokenId={asset?.tokenId as number}
                  price={asset?.price as string}
                />
            )}
          </div>
        </div>
        {auction && (
          <Accordion type="single" collapsible defaultValue="item-1">
            <AccordionItem value="item-2">
              <AccordionTrigger className="bg-accent text-accent-foreground px-5 rounded-t-md">
                Bids
              </AccordionTrigger>
              <AccordionContent className="">
                <BidsTable />
              </AccordionContent>
            </AccordionItem>
          </Accordion>
        )}
      </div>
    </div>
  );
};

export default NFTDetailRight;

export function OffersTable() {
  return (
    <Table className="border">
      <TableHeader>
        <TableRow>
          <TableHead>Price</TableHead>
          <TableHead>USD Price</TableHead>
          <TableHead>From</TableHead>
          <TableHead>Date</TableHead>
        </TableRow>
      </TableHeader>
      <TableBody>
        {offers.map((offer, index) => (
          <TableRow key={index}>
            <TableCell className="font-medium">{`${offer.price} WETH`}</TableCell>
            <TableCell>{`$${offer.usd_price}`}</TableCell>
            <TableCell>
              <Link href={""}>{offer.from}</Link>
            </TableCell>
            <TableCell>{offer.date}</TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  );
}

export function BidsTable() {
  return (
    <Table className="border">
      <TableHeader>
        <TableRow>
          <TableHead>Bid</TableHead>
          <TableHead>Bid in USD</TableHead>
          <TableHead>From</TableHead>
          <TableHead>Date</TableHead>
        </TableRow>
      </TableHeader>
      <TableBody>
        {bids.map((bid, index) => (
          <TableRow key={index}>
            <TableCell className="font-medium">{`${bid.bid_price} WETH`}</TableCell>
            <TableCell>{`$${bid.bid_usd_price}`}</TableCell>
            <TableCell>
              <Link href={""}>{bid.from}</Link>
            </TableCell>
            <TableCell>{bid.date}</TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  );
}

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
    console.log("here");
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
        <Button
          type="button"
          className="flex-1 lg:w-[50%] w-full"
        >
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
        <Button
        className="flex-1 lg:w-[50%] w-full"
        variant={"secondary"}
        >
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
