"use client";
import { TypographyP } from "../common/Typography";
import { useState } from "react";
import { Button } from "../ui/button";

import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";

import { useEffect } from "react";
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
import useContractWriteMutation from "@/hooks/useContractWriteMutation";

const initialState = {
  price: 0.0,
};
const schema = z.object({
  price: z.number().nonnegative(),
});

type PlaceBidModalProps = {
  auctionId: number;
};
export const PlaceBidModal = ({ auctionId }: PlaceBidModalProps) => {
  const [open, setOpen] = useState(false);
  const { address } = useAccount();
  const { data: balance } = useBalance({ address: address });
  const {
    isLoading,
    writeSuccess,
    contractWrite,
  } = useContractWriteMutation();
  const form = useForm<{ price: number }>({
    resolver: zodResolver(schema),
    defaultValues: initialState,
  });
  const handleClose = () => setOpen(false);
  const onSubmit = (values: { price: number }) => {
    contractWrite("placeBid", values.price.toString(), [auctionId]);
  };
  useEffect(() => {
    if (writeSuccess) {
      handleClose();
    }
  }, [writeSuccess]);
  return (
    <Dialog open={open} onOpenChange={(a) => setOpen(a)}>
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
                  text={`${parseFloat(balance?.formatted as string).toFixed(2) ?? "0"} ${balance?.symbol ?? "ETH"}`}
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
                  disabled={isLoading}
                  className="rounded-full self-end"
                  size="lg"
                >
                  {isLoading ? (
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
