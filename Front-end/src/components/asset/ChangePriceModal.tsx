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
import useContractWriteMutation from "@/hooks/useContractWriteMutation";
import { FaDollarSign } from "react-icons/fa6";
import { parseEther } from "viem";
import { useAppDispatch } from "@/store/hooks";
import { webApi } from "@/store/api";

const initialState = {
  new_price: 0.0,
};
const schema = z.object({
  new_price: z.number().nonnegative(),
});

type ChangePriceModalProps = {
  tokenId: number;
};
export const ChangePriceModal = ({ tokenId }: ChangePriceModalProps) => {
  const [open, setOpen] = useState(false);
  const handleClose = () => setOpen(false);
  const dispatch = useAppDispatch()
  const { isLoading, writeSuccess, contractWrite } = useContractWriteMutation();
  const form = useForm<{ new_price: number }>({
    resolver: zodResolver(schema),
    defaultValues: initialState,
  });
  const onSubmit = (values: { new_price: number }) => {
    contractWrite("changePrice", undefined, [
      tokenId,
      parseEther(values.new_price.toString(), "wei"),
    ]);
  };
  useEffect(() => {
    if (writeSuccess) {
      dispatch(webApi.util.invalidateTags(["NFTs", {id:tokenId, type:"NFTs"}]))
      handleClose();
    }
  }, [writeSuccess]);
  return (
    <Dialog open={open} onOpenChange={(a) => setOpen(a)}>
      <DialogTrigger asChild>
        <Button
          variant={"ghost"}
          className="flex gap-3 justify-start font-medium items-center w-full"
        >
          <FaDollarSign size={20} />
          <div>Change Price</div>
        </Button>
      </DialogTrigger>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Change Price</DialogTitle>
          <DialogDescription className="flex flex-col gap-5 pt-10">
            <Form {...form}>
              <form
                onSubmit={form.handleSubmit(onSubmit)}
                className="flex flex-col gap-5"
              >
                <FormField
                  control={form.control}
                  name="new_price"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>New Price</FormLabel>
                      <FormControl className="flex">
                        <div className="relative flex items-stretch">
                          <Input
                            id="new_price"
                            placeholder="Enter New Price"
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
                      Changing
                    </>
                  ) : (
                    "Change Price"
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
