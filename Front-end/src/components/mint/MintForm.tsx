"use client";
import {
  TypographyH4,
  TypographyP,
  TypographySmall,
} from "@/components/common/Typography";
import ImageUpload from "@/components/mint/ImageUpload";
import { Input } from "@/components/ui/input";
import { Textarea } from "@/components/ui/textarea";
import ChooseCollection from "@/components/mint/ChooseCollection";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import * as z from "zod";
import {
  Form,
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
} from "@/components/ui/dialog";

import { IoCheckmarkCircle } from "react-icons/io5";
import { ContractWriteContext } from "@/context/ContractWrite";
import { ToastAction } from "../ui/toast";

import { File } from "nft.storage";
import { cn, storeAsset } from "@/lib/utils";
import { Auction, NFT } from "@/types";
import { CalendarIcon, Loader2 } from "lucide-react";
import { useToast } from "@/components/ui/use-toast";
import { RiAuctionLine } from "react-icons/ri";
import { MdOutlineSell } from "react-icons/md";
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover";
import { Calendar } from "@/components/ui/calendar";
import { format } from "date-fns";
import { Button } from "../ui/button";
import { useContext, useEffect, useRef, useState } from "react";
import { parseEther } from "viem";
import { useContractEvent } from "wagmi";
import NftAbi from "@/data/abi/MyNFT.json";
import { useCreateNFTMutation } from "@/store/api";
import { useRouter } from "next/navigation";
import { Routes } from "@/routes";
import MultipleFilesUpload from "../common/MultipleFilesUpload";

interface FormInput {
  name: string;
  description: string;
  image: File | null;
  files: FileList | null;
  royalty: number;
  price: number;
  collection: string;
  auction: boolean;
  auctionEnd: number;
}

const initialState: FormInput = {
  name: "",
  description: "",
  image: null,
  files: null,
  price: 0.0,
  auction: false,
  royalty: 0.0,
  collection: "",
  auctionEnd: 0.0,
};
const schema = z.object({
  name: z.string().min(3),
  description: z.string().min(5),
  image: z
    .any()
    .refine(
      (file) => file && file.size <= 20 * 1024 * 1024,
      "File size must be less than 20MB.",
    ),
  files: z
    .any()
    .refine((files) => files && files.length > 0, "Files are required"),
  royalty: z.number().nonnegative().max(10),
  price: z.number().nonnegative(),
  auctionEnd: z.number(),
  collection: z.string(),
  auction: z.boolean(),
});

type Props = {};

const MintForm = (props: Props) => {
  const [uploading, setUploading] = useState(false);
  const [uploadSuccess, setUploadSuccess] = useState(false);
  const [uploadNftSuccess, setUploadNftSuccess] = useState(false);
  const [uploadNft, setUploadNft] = useState(false);
  const router = useRouter();
  const [open, setOpen] = useState(false);
  const payload = useRef<NFT | undefined>(undefined);
  const { toast } = useToast();
  const [postNFT] = useCreateNFTMutation();
  const form = useForm<FormInput>({
    resolver: zodResolver(schema),
    defaultValues: initialState,
  });

  const { isLoading, isError, transactionSuccess, contractWrite } =
    useContext(ContractWriteContext);

  const onSubmit = async (values: FormInput) => {
    const { image, files, ...others } = values;
    try {
      setOpen(true);
      setUploading(true);
      const image_cid = await storeAsset(image ? [image] : null);
      const files_cid = await storeAsset(files);
      const metadata = {
        name: others.name,
        description: others.description,
        image: `https://nftstorage.link/ipfs/${image_cid}/${image?.name}`,
        files: `ipfs://${files_cid}`,
      };
      const metadata_json = JSON.stringify(metadata);
      const metadata_file = new File([metadata_json], "metadata.json", {
        type: "application/json",
      });
      const asset_payload: NFT = {
        name: values.name,
        description: values.description,
        image: `https://nftstorage.link/ipfs/${image_cid}/${image?.name}`,
        category: "image",
        price: values.price.toString(),
        royalty: values.royalty,
        auction: values.auction
          ? {
              auction_end: Math.round(values.auctionEnd / 1000),
              highest_bid: "0",
            }
          : undefined,
      };
      payload.current = asset_payload;

      const metadata_cid = await storeAsset([metadata_file]);
      setUploadSuccess(true);

      contractWrite(
        "mintProduct",
        process.env.NEXT_PUBLIC_LISTING_PRICE as string,
        [
          `ipfs://${metadata_cid}`,
          parseEther(others.price.toString(), "wei"),
          others.auction,
          Math.round(others.auctionEnd / 1000),
          others.royalty,
        ],
      );
      form.reset(initialState);
    } catch (error) {
      toast({
        variant: "destructive",
        title: "Uh oh! Something went wrong.",
        description: "There was a problem with uploading your files.",
        action: <ToastAction altText="Try again">Try again</ToastAction>,
      });
      setOpen(false);
      console.log("error", error);
    } finally {
      setUploading(false);
    }
  };
  const postAsset = async (asset: NFT) => {
    try {
      setUploadNft(true);
      const id = await postNFT(asset).unwrap();
      setUploadNftSuccess(true);
      form.reset(initialState);
      router.push(`${Routes.PRODUCT}/${id}`);
    } catch (error) {
      console.log(error);
    } finally {
      setUploadNft(false);
      setOpen(false);
    }
  };

  useEffect(() => {
    if (isError) {
      setOpen(false);
    }
  }, [isError]);

  useContractEvent({
    address: process.env.NEXT_PUBLIC_CONTRACT_ADDRESS as `0x${string}`,
    abi: NftAbi,
    eventName: "ProductCreated",
    listener: (logs: any) => {
      const event = logs[0];
      const product = event.args;
      console.log("event", product);
      const _payload = payload.current;
      const asset: NFT = {
        ...(_payload as NFT),
        tokenId: parseInt((product.tokenId as bigint).toString()),
        auction: {
          ...(_payload?.auction as Auction),
          auctionId: parseInt((product.auctionId as bigint).toString()),
        },
        transactionHash: event.transactionHash,
      };
      if (_payload) {
        console.log("asset", asset);
        postAsset(asset);
      }
    },
  });
  console.log(form.getValues("files"));
  return (
    <Form {...form}>
      <form
        onSubmit={form.handleSubmit(onSubmit)}
        className="flex flex-col gap-5"
      >
        <ImageUpload form={form} />
        <FormField
          control={form.control}
          name="name"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Name</FormLabel>
              <FormControl>
                <Input id="name" placeholder="Name" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="description"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Description</FormLabel>
              <FormControl>
                <Textarea
                  placeholder="Write Description"
                  id="description"
                  {...field}
                  className="h-[10rem]"
                />
              </FormControl>
              <FormDescription>
                The description will be included on the item&apos;s detail page
                underneath its image. Markdown syntax is supported.
              </FormDescription>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="files"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Files</FormLabel>
              <FormControl>
                <MultipleFilesUpload onChange={field.onChange} />
              </FormControl>
              <FormDescription>
                Upload Files relevant to the Digital Product you want to mint as
                NFTs
              </FormDescription>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="auction"
          render={({ field }) => (
            <FormItem>
              <FormLabel className="text-base">Sale Type</FormLabel>
              <FormControl>
                <div className="flex gap-3 items-center">
                  <Button
                    type="button"
                    variant={!field.value ? "default" : "secondary"}
                    className="flex-1 flex gap-3 item-center border rounded-md  h-auto w-[15rem]"
                    onClick={() => field.onChange(false)}
                  >
                    <MdOutlineSell size={30} />
                    <TypographyP
                      className="text-foregghostround"
                      text="Fixed"
                    />
                  </Button>
                  <Button
                    type="button"
                    variant={field.value ? "default" : "secondary"}
                    className="flex-1 flex gap-3 item-center border rounded-md  h-auto w-[15rem]"
                    onClick={() => field.onChange(true)}
                  >
                    <RiAuctionLine size={30} />
                    <TypographyP className="text-foreground" text="Auction" />
                  </Button>
                </div>
              </FormControl>
              <FormDescription>
                <TypographySmall text="You’ll receive bids on this item" />
              </FormDescription>
            </FormItem>
          )}
        />
        {form.getValues("auction") && (
          <FormField
            control={form.control}
            name="auctionEnd"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Auction End</FormLabel>
                <FormControl>
                  <Popover>
                    <PopoverTrigger asChild>
                      <Button
                        variant={"secondary"}
                        className={cn(
                          "w-full justify-start text-left font-normal",
                          !field.value && "text-muted-foreground",
                        )}
                      >
                        <CalendarIcon className="mr-2 h-4 w-4" />
                        {field.value ? (
                          format(field.value, "PPP")
                        ) : (
                          <span>Auction End Date</span>
                        )}
                      </Button>
                    </PopoverTrigger>
                    <PopoverContent className="w-auto p-0">
                      <Calendar
                        mode="single"
                        selected={new Date(field.value)}
                        onSelect={(d) => field.onChange(d?.getTime())}
                        initialFocus
                      />
                    </PopoverContent>
                  </Popover>
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
        )}
        <FormField
          control={form.control}
          name="price"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Price(ETH)</FormLabel>
              <FormControl>
                <Input
                  id="price"
                  type="number"
                  placeholder="Enter Price (ETH)"
                  {...field}
                  value={field.value > 0 ? field.value : undefined}
                  onChange={(e) => field.onChange(parseFloat(e.target.value))}
                />
              </FormControl>
              <FormDescription>
                This is Price will be used as a floor price or fixed price
              </FormDescription>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="royalty"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Royality(%)</FormLabel>
              <FormControl>
                <Input
                  id="royalty"
                  type="number"
                  placeholder="Enter Royalty Percentage"
                  {...field}
                  value={field.value > 0 ? field.value : undefined}
                  onChange={(e) => field.onChange(parseFloat(e.target.value))}
                />
              </FormControl>
              <FormDescription>
                This is Royalty you would get on secondary resale of your NFT.
              </FormDescription>
              <FormMessage />
            </FormItem>
          )}
        />
        {/* <ChooseCollection/> */}
        <Button
          type="submit"
          disabled={uploading || isLoading}
          className="rounded-full self-end"
          size="lg"
        >
          {uploading || isLoading ? (
            <>
              <Loader2 className="mr-2 h-6 w-6 animate-spin" />
              Minting
            </>
          ) : (
            "Mint Product"
          )}
        </Button>
      </form>
      <MintingProgress
        open={open}
        openModal={(a) => setOpen(a)}
        uploading={uploading}
        uploadSuccess={uploadSuccess}
        uploadNft={uploadNft}
        uploadNftSuccess={uploadNftSuccess}
      />
    </Form>
  );
};

export default MintForm;

type ProgressProps = {
  open: boolean;
  openModal: (a: boolean) => void;
  uploading: boolean;
  uploadSuccess: boolean;
  uploadNft: boolean;
  uploadNftSuccess: boolean;
};

export const MintingProgress = ({
  open,
  openModal,
  uploading,
  uploadSuccess,
  uploadNft,
  uploadNftSuccess,
}: ProgressProps) => {
  const { waitingForTransaction, transactionSuccess, writing, writeSuccess } =
    useContext(ContractWriteContext);
  return (
    <Dialog open={open} onOpenChange={(open) => openModal(open)}>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Minting Your Assets</DialogTitle>
          <DialogDescription className="flex flex-col gap-5 pt-10">
            <div className="flex items-center gap-5">
              <div className="flex justify-center items-center rounded-full bg-secondary p-2 h-[3.5rem] w-[3.5rem]">
                {uploading ? (
                  <Loader2 className="h-8 w-8 animate-spin" />
                ) : uploadSuccess ? (
                  <IoCheckmarkCircle size={40} />
                ) : (
                  <TypographyH4 text="1" />
                )}
              </div>
              <div className="flex flex-col">
                <TypographyH4 text="Uploading Files to Decentralized Storage" />
                <TypographySmall text="This might take a few minutes" />
              </div>
            </div>
            <div className="flex items-center gap-5">
              <div className="flex justify-center items-center rounded-full bg-secondary p-2 h-[3.5rem] w-[3.5rem]">
                {writing ? (
                  <Loader2 className="h-8 w-8 animate-spin" />
                ) : writeSuccess ? (
                  <IoCheckmarkCircle size={40} />
                ) : (
                  <TypographyH4 text="2" />
                )}
              </div>
              <div className="flex flex-col">
                <TypographyH4 text="Go to your wallet to approve this transaction" />
                <TypographySmall text="A blockchain transaction is required to mint your NFT." />
              </div>
            </div>
            <div className="flex items-center gap-5">
              <div className="flex justify-center items-center rounded-full bg-secondary p-2 h-[3.5rem] w-[3.5rem]">
                {uploadNft ? (
                  <Loader2 className="h-8 w-8 animate-spin" />
                ) : uploadNftSuccess ? (
                  <IoCheckmarkCircle size={40} />
                ) : (
                  <TypographyH4 text="3" />
                )}
              </div>
              <div className="flex flex-col">
                <TypographyH4 text="Uploading Your Assets to Server" />
                <TypographySmall text="Your asset metadata is stored on our server" />
              </div>
            </div>
          </DialogDescription>
        </DialogHeader>
      </DialogContent>
    </Dialog>
  );
};
