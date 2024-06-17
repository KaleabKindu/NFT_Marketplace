"use client";
import { TypographyP, TypographySmall } from "@/components/common/Typography";
import { useState } from "react";
import { Button } from "@/components/ui/button";

import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";

import { useEffect } from "react";
import { CalendarIcon, Loader2 } from "lucide-react";
import {
  Form,
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import * as z from "zod";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { MdOutlineSell } from "react-icons/md";
import { useAccount } from "wagmi";
import useContractWriteMutation from "@/hooks/useContractWriteMutation";
import { useAppDispatch, useAppSelector } from "@/store/hooks";
import { useWeb3Modal } from "@web3modal/wagmi/react";
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover";
import { cn } from "@/lib/utils";
import { format } from "date-fns";
import { Calendar } from "@/components/ui/calendar";
import { RiAuctionLine } from "react-icons/ri";
import { parseEther } from "viem";
import { webApi } from "@/store/api";

interface FormInput {
  price: string;
  auction: boolean;
  auctionEnd: number;
}

const initialState: FormInput = {
  price: "",
  auction: false,
  auctionEnd: 0.0,
};
const schema = z.object({
  price: z.string().min(1, "Price is required"),
  auctionEnd: z.number().optional(),
  auction: z.boolean(),
});
type PlaceBidModalProps = {
  tokenId: number;
};
export const ResellAssetModal = ({ tokenId }: PlaceBidModalProps) => {
  const session = useAppSelector((state) => state.auth.session);
  const { open } = useWeb3Modal();

  const [showModal, setShowModal] = useState(false);
  const { address } = useAccount();
  const { isLoading, writeSuccess, contractWrite } = useContractWriteMutation();
  const form = useForm<FormInput>({
    resolver: zodResolver(schema),
    defaultValues: initialState,
  });
  const dispatch = useAppDispatch()
  const handleClose = () => setShowModal(false);
  const onSubmit = (values: FormInput) => {
    contractWrite("resellProduct", values.price.toString(), [tokenId, parseEther(values.price, "wei"), values.auction, Math.round(values.auctionEnd / 1000)]);
  };
  useEffect(() => {
    if (writeSuccess) {
      dispatch(
        webApi.util.invalidateTags([
          "NFTs",
          { id: tokenId, type: "NFTs" },
        ]),
      );
      handleClose();
    }
  }, [writeSuccess]);
  return (
    <Dialog
      open={showModal}
      onOpenChange={(a) => {
        if (!session) {
          open();
          return;
        }
        setShowModal(a);
      }}
    >
      <DialogTrigger asChild>
        <Button type="button" className="flex-1 lg:w-[50%] w-full">
          Resell
        </Button>
      </DialogTrigger>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Resell Asset</DialogTitle>
          <DialogDescription className="flex flex-col gap-5 pt-10">
            <Form {...form}>
              <form
                onSubmit={form.handleSubmit(onSubmit)}
                className="flex flex-col gap-5"
              >
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
                            className="flex-1 flex gap-3 item-center border rounded-md  h-auto"
                            onClick={() => field.onChange(false)}
                          >
                            <MdOutlineSell size={30} />
                            <TypographyP text="Fixed" />
                          </Button>
                          <Button
                            type="button"
                            variant={field.value ? "default" : "secondary"}
                            className="flex-1 flex gap-3 item-center border rounded-md  h-auto"
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
                                onSelect={(d) => {
                                  const date = new Date(d?.getTime() as number);
                                  date.setHours(23, 59, 59, 999);
                                  field.onChange(date.getTime());
                                }}
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
                        This is Price will be used as a floor price or fixed
                        price
                      </FormDescription>
                      <FormMessage />
                    </FormItem>
                  )}
                />
                <Button
                  type="submit"
                  disabled={isLoading}
                  className="rounded-full self-end"
                  size="lg"
                >
                  {isLoading ? (
                    <>
                      <Loader2 className="mr-2 h-6 w-6 animate-spin" />
                      Waiting
                    </>
                  ) : (
                    "Resell"
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
