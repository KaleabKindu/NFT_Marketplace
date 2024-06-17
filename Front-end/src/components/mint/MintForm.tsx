"use client";
import {
  TypographyH4,
  TypographyP,
  TypographySmall,
} from "@/components/common/Typography";
import ThumbnailUpload from "@/components/mint/ThumbnailUpload";
import { Input } from "@/components/ui/input";
import { Textarea } from "@/components/ui/textarea";
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
import { ToastAction } from "../ui/toast";

import { File } from "nft.storage";
import { cn, storeAsset } from "@/lib/utils";
import { NFT } from "@/types";
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
import { useEffect, useState } from "react";
import { parseEther } from "viem";
import { useCreateNFTMutation } from "@/store/api";
import { useRouter } from "next/navigation";
import { Routes } from "@/routes";
import MultipleFilesUpload from "./MultipleFilesUpload";
import SelectCategory from "./SelectCategory";
import AudioFileUpload from "./AudioFileUpload";
import { CATEGORY } from "@/data";
import useContractWriteMutation from "@/hooks/useContractWriteMutation";
import ChooseCollection from "./ChooseCollection";
import { DateTimePicker } from "@/components/ui/date-time-picker/date-time-picker";
import moment from "moment";
import { useAppSelector } from "@/store/hooks";

interface FormInput {
  name: string;
  description: string;
  thumbnail?: File;
  audio?: File;
  category: string;
  files: File[];
  royalty: string;
  price: string;
  collectionId?: number;
  auction: boolean;
  auctionEnd: number;
}

const initialState: FormInput = {
  name: "",
  description: "",
  category: "",
  files: [],
  price: "",
  auction: false,
  royalty: "",
  auctionEnd: 0.0,
};
const schema = z.object({
  name: z.string().min(3, "Name must be at least 3 characters"),
  description: z.string().min(5, "Description must be at least 5 characters"),
  thumbnail: z.any().refine((file) => file, "Thumbnail is required"),
  audio: z
    .any()
    .refine(
      (file) => file && file.size <= 20 * 1024 * 1024,
      "Audio file size must be less than 20MB.",
    )
    .optional(),
  files: z
    .any()
    .refine((files) => files && files.length > 0, "Files are required"),
  category: z.string().min(1, "Category is required"),
  royalty: z.string().min(1, "Royalty is Required"),
  price: z.string().min(1, "Price is required"),
  auctionEnd: z.number().optional(),
  collectionId: z.number().optional(),
  auction: z.boolean(),
});

type Props = {};

const MintForm = (props: Props) => {
  const [uploading, setUploading] = useState(false);
  const [uploadSuccess, setUploadSuccess] = useState(false);
  const router = useRouter();
  const [open, setOpen] = useState(false);
  const [payload, setPayload] = useState<NFT | undefined>(undefined);
  const { toast } = useToast();
  const session = useAppSelector(state => state.auth.session)
  const [postNFT, { isLoading: uploadNft, isSuccess: uploadNftSuccess }] =
    useCreateNFTMutation();
  const form = useForm<FormInput>({
    resolver: zodResolver(schema),
    defaultValues: initialState,
  });

  const {
    data,
    isLoading,
    isError,
    transactionHash,
    contractWrite,
    writing,
    writeSuccess,
  } = useContractWriteMutation();

  const onSubmit = async (values: FormInput) => {
    const { thumbnail, audio, files, ...others } = values;
    const image = thumbnail?.type.startsWith("image") ? thumbnail : undefined;
    const video = thumbnail?.type.startsWith("video") ? thumbnail : undefined;
    try {
      setOpen(true);
      setUploading(true);
      const thumbnail_cid = await storeAsset([image, audio, video] as File[]);
      const files_cid = await storeAsset(files);
      setUploading(false);
      setUploadSuccess(true);

      contractWrite(
        "mintProduct",
        process.env.NEXT_PUBLIC_LISTING_PRICE as string,
        [
          `ipfs://${files_cid}`,
          parseEther(others.price, "wei"),
          others.auction,
          Math.round(others.auctionEnd / 1000),
          others.royalty,
        ],
      );
      const payload: NFT = {
        name: values.name,
        description: values.description,
        image: image
          ? `https://nftstorage.link/ipfs/${thumbnail_cid}/${image?.name}`
          : undefined,
        audio: audio
          ? `https://nftstorage.link/ipfs/${thumbnail_cid}/${audio?.name}`
          : undefined,
        video: video
          ? `https://nftstorage.link/ipfs/${thumbnail_cid}/${video?.name}`
          : undefined,
        category: values.category,
        collectionId: values.collectionId && values.collectionId,
        price: values.price,
        royalty: parseInt(values.royalty),
        auction: values.auction
          ? {
              auctionEnd: Math.round(values.auctionEnd / 1000),
              highestBid: "0",
            }
          : undefined,
        transactionHash: transactionHash,
      };
      setPayload(payload);
    } catch (error) {
      toast({
        variant: "destructive",
        title: "Uh oh! Something went wrong.",
        description: "There was a problem with uploading your files.",
        action: <ToastAction altText="Try again">Try again</ToastAction>,
      });
      console.log("error", error);
      setOpen(false);
    }
  };
  const postAsset = async (asset: NFT) => {
    try {
      const id = await postNFT(asset).unwrap();
      form.reset(initialState);
      router.push(`${Routes.PRODUCT}/${id}`);
    } catch (error) {
      toast({
        variant: "destructive",
        title: "Uh oh! Something went wrong.",
        description:
          "There was a problem with uploading your metadata to server.",
        action: <ToastAction altText="Try again">Try again</ToastAction>,
      });
      console.log(error);
    } finally {
      setOpen(false);
    }
  };

  useEffect(() => {
    if (data && writeSuccess && payload) {
      const [tokenId, auctionId] = data;
      const _payload: NFT = {
        tokenId: parseInt((tokenId as bigint).toString()),
        ...payload,
        auction: payload.auction
          ? {
              ...payload.auction,
              auctionId: parseInt((auctionId as bigint).toString()),
            }
          : undefined,
        transactionHash: transactionHash,
      };
      postAsset(_payload);
    }
  }, [writeSuccess, payload, data]);

  useEffect(() => {
    if (isError) {
      setOpen(false);
    }
  }, [isError]);

  useEffect(() => {
    if(!session){
      router.push(Routes.HOME)
    }
  },[session])
  return (
    <Form {...form}>
      <form
        onSubmit={form.handleSubmit(onSubmit)}
        className="flex flex-col gap-5"
      >
        <FormField
          control={form.control}
          name="category"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Category</FormLabel>
              <FormControl>
                <SelectCategory onChange={field.onChange} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="thumbnail"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Thumbnail (Image, GIF or Video)</FormLabel>
              <FormControl>
                <ThumbnailUpload onChange={field.onChange} />
              </FormControl>
              <FormDescription>
                File types supported: JPG, JPEG, PNG, GIF, SVG. Max size: 100 MB
              </FormDescription>
              <FormMessage />
            </FormItem>
          )}
        />

        {form.watch("category") === CATEGORY.AUDIO && (
          <FormField
            control={form.control}
            name="audio"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Audio Sample(optional)</FormLabel>
                <FormControl>
                  <AudioFileUpload onChange={field.onChange} />
                </FormControl>
                <FormDescription>
                  Upload Audio sample to showcase your Product
                </FormDescription>
                <FormMessage />
              </FormItem>
            )}
          />
        )}
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
              <FormLabel>Upload File</FormLabel>
              <FormControl>
                <MultipleFilesUpload onChange={field.onChange} />
              </FormControl>
              <FormDescription>
                Upload File relevant to the Digital Product you want to mint as
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
                    <TypographyP text="Fixed" />
                  </Button>
                  <Button
                    type="button"
                    variant={field.value ? "default" : "secondary"}
                    className="flex-1 flex gap-3 item-center border rounded-md  h-auto w-[15rem]"
                    onClick={() => field.onChange(true)}
                  >
                    <RiAuctionLine size={30} />
                    <TypographyP text="Auction" />
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
                          format(field.value, "PPpp")
                        ) : (
                          <span>Auction End Date</span>
                        )}
                      </Button>
                    </PopoverTrigger>
                    <PopoverContent className="w-auto p-0">
                      <DateTimePicker
                        onChange={(dateTime: any) => {
                          const date = new Date(
                            dateTime.year,
                            dateTime.month - 1, // JavaScript months are 0-indexed (0 = January, 1 = February, etc.)
                            dateTime.day,
                            dateTime.hour,
                            dateTime.minute,
                            dateTime.second,
                            dateTime.millisecond,
                          );
                          console.log(date.toString(), moment(date).fromNow());
                          field.onChange(date.getTime());
                        }}
                        granularity={"minute"}
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
                  placeholder="Enter Price (ETH)"
                  {...field}
                  value={field.value}
                  onChange={(e) => {
                    const value = e.target.value;
                    const newValue = value
                      .replace(/[^0-9.]+/g, "")
                      .replace(/(\.\d*)\.+/g, "$1");
                    field.onChange(newValue);
                  }}
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
                  value={field.value}
                  onChange={(e) => {
                    const value = e.target.value;
                    const newValue = value.replace(/[^0-9]+/g, "");
                    field.onChange(newValue);
                  }}
                />
              </FormControl>
              <FormDescription>
                This is Royalty you would get on secondary resale of your NFT.
              </FormDescription>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="collectionId"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Add To a Collection(Optional)</FormLabel>
              <FormControl>
                <ChooseCollection onChange={field.onChange} />
              </FormControl>
              <FormDescription>
                Choose an exiting collection or create a new one
              </FormDescription>
              <FormMessage />
            </FormItem>
          )}
        />
        <Button
          type="submit"
          disabled={uploading || isLoading}
          className="rounded-full self-end"
          size="lg"
        >
          Mint Product
        </Button>
      </form>
      <Dialog open={open} onOpenChange={(open) => setOpen(open)}>
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
    </Form>
  );
};

export default MintForm;
