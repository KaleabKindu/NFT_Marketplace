"use client";
import { useState } from "react";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
} from "@/components/ui/dialog";
import { MdAdd } from "react-icons/md";
import {
  Form,
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";

import { useForm } from "react-hook-form";
import * as z from "zod";

import { File } from "nft.storage";
import { useToast } from "@/components/ui/use-toast";

import ThumbnailUpload from "../mint/ThumbnailUpload";
import { Input } from "../ui/input";
import { Textarea } from "../ui/textarea";
import { zodResolver } from "@hookform/resolvers/zod";
import { storeAsset } from "@/lib/utils";
import { useCreateCollectionMutation } from "@/store/api";
import { Button } from "../ui/button";
import { AiOutlineLoading3Quarters } from "react-icons/ai";

interface FormInput {
  name: string;
  description: string;
  image?: File | undefined;
}

const schema = z.object({
  name: z.string().min(3),
  description: z.string().min(5),
  image: z
    .any()
    .refine(
      (file) => file && file.size <= 20 * 1024 * 1024,
      "File size must be less than 20MB.",
    ),
});

const initialState: FormInput = {
  name: "",
  description: "",
};

type Props = {};

const AddCollectionModal = (props: Props) => {
  const [open, setOpen] = useState(false);
  const [loading, setLoading] = useState(false);
  const { toast } = useToast();
  const [createCollection] = useCreateCollectionMutation();
  const form = useForm<FormInput>({
    resolver: zodResolver(schema),
    defaultValues: initialState,
  });

  const onSubmit = async (values: FormInput) => {
    try {
      setLoading(true);
      const avatar_cid = await storeAsset([values.image as File]);
      const payload = {
        name: values.name,
        description: values.description,
        avatar: `https://nftstorage.link/ipfs/${avatar_cid}/${values.image?.name}`,
      };
      await createCollection(payload);
      form.reset()
    } catch (error) {
      console.log("error");
    } finally {
      setLoading(false);
      setOpen(false);
    }
  };
  return (
    <>
      <Button
        type="button"
        variant={"ghost"}
        onClick={() => setOpen(true)}
        className="flex justify-center gap-5 w-[200px] h-auto min-h-[150px] border whitespace-normal text-left items-center rounded-2xl"
      >
        <MdAdd size={30} />
      </Button>
      <Dialog open={open} onOpenChange={(open) => setOpen(open)}>
        <DialogContent className="max-h-screen overflow-y-auto hide-scrollbar">
          <DialogHeader>
            <DialogTitle className="text-left">Create a Collection</DialogTitle>
            <DialogDescription className="flex flex-col gap-5 pt-10">
              <Form {...form}>
                <form
                  onSubmit={(e) => {
                    e.stopPropagation();
                    form.handleSubmit(onSubmit)(e);
                  }}
                  className="flex flex-col gap-5 text-left"
                >
                  <FormField
                    control={form.control}
                    name="image"
                    render={({ field }) => (
                      <FormItem>
                        <FormLabel>Avatar Image</FormLabel>
                        <FormControl>
                          <ThumbnailUpload onChange={field.onChange} />
                        </FormControl>
                        <FormDescription>
                          File types supported: JPG, JPEG, PNG, GIF, SVG. Max
                          size: 100 MB
                        </FormDescription>
                        <FormMessage />
                      </FormItem>
                    )}
                  />
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
                        <FormMessage />
                      </FormItem>
                    )}
                  />
                  <Button
                    type="submit"
                    className="rounded-full self-end"
                    size="lg"
                  >
                    {loading ? (
                      <AiOutlineLoading3Quarters className="animate-spin" />
                    ) : (
                      "Submit"
                    )}
                  </Button>
                </form>
              </Form>
            </DialogDescription>
          </DialogHeader>
        </DialogContent>
      </Dialog>
    </>
  );
};

export default AddCollectionModal;
