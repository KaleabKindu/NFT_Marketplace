"use client";
import { TypographyP } from "../common/Typography";

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
import useContractWriteMutation from "@/hooks/useContractWriteMutation";
import { BiTransferAlt } from "react-icons/bi";
const initialState = {
  address: "",
};
const schema = z.object({
  address: z.string(),
});

type TransferModalProps = {
  auctionId: number;
};
export const TransferModal = ({ auctionId }: TransferModalProps) => {
  const {
    isLoading,
    waitingForTransaction,
    transactionSuccess,
    writing,
    writeSuccess,
    contractWrite,
  } = useContractWriteMutation();
  const form = useForm<{ address: string }>({
    resolver: zodResolver(schema),
    defaultValues: initialState,
  });
  const onSubmit = (values: { address: string }) => {
    contractWrite("placeBid", values.address, [auctionId]);
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
          variant={"ghost"}
          className="flex gap-3 justify-start font-medium items-center w-full"
        >
          <BiTransferAlt size={20} />
          <div>Transfer</div>
        </Button>
      </DialogTrigger>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Transfer Asset</DialogTitle>
          <DialogDescription className="flex flex-col gap-5 pt-10">
            <Form {...form}>
              <form
                onSubmit={form.handleSubmit(onSubmit)}
                className="flex flex-col gap-5"
              >
                <FormField
                  control={form.control}
                  name="address"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Transfer to</FormLabel>
                      <FormControl className="flex">
                        <div className="relative flex items-stretch">
                          <Input
                            id="address"
                            placeholder="Enter Address"
                            className="h-auto"
                            {...field}
                            value={field.value}
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
                      Transfering
                    </>
                  ) : (
                    "Transfer"
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
