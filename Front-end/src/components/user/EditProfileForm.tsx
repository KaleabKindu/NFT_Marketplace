"use client";

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

import { File } from "nft.storage";
import { useToast } from "@/components/ui/use-toast";

import { useState } from "react";
import { useRouter } from "next/navigation";
import ThumbnailUpload from "../mint/ThumbnailUpload";
import { Input } from "../ui/input";
import { Textarea } from "../ui/textarea";
import { Button } from "../ui/button";
import { zodResolver } from "@hookform/resolvers/zod";
import { storeAsset } from "@/lib/utils";
import { useEditProfileMutation } from "@/store/api";
import { useGetUserDetailsQuery } from "@/store/api";
import { useAccount } from "wagmi";
import { Routes } from "@/routes";
import { AiOutlineLoading3Quarters } from "react-icons/ai";

interface FormInput {
  userName: string;
  bio: string;
  facebook: string;
  twitter: string;
  youtube: string;
  telegram: string;
  image?: File | undefined;
}

const schema = z.object({
  userName: z.string().min(3),
  bio: z.string().min(5),
  facebook: z.string(),
  twitter: z.string(),
  youtube: z.string(),
  telegram: z.string(),
  image: z
    .any()
    .refine(
      (file) => file && file.size <= 20 * 1024 * 1024,
      "File size must be less than 20MB.",
    ),
});

type Props = {};

const EditProfileForm = (props: Props) => {
  const { address } = useAccount();
  const { data: user } = useGetUserDetailsQuery(address as string);
  const [loading, setLoading] = useState(false);
  const router = useRouter();
  const { toast } = useToast();
  const [editProfile] = useEditProfileMutation();
  const form = useForm<FormInput>({
    resolver: zodResolver(schema),
    defaultValues: {
      userName: user?.userName || "",
      bio: user?.bio || "",
      facebook: user?.facebook || "",
      twitter: user?.twitter || "",
      youtube: user?.youtube || "",
      telegram: user?.telegram || "",
    },
  });

  const onSubmit = async (values: FormInput) => {
    try {
      setLoading(true);
      const image_cid = await storeAsset([values.image as File]);
      const payload = {
        userName: values.userName,
        address: user?.address as `0x${string}`,
        bio: values.bio,
        avatar: `https://nftstorage.link/ipfs/${image_cid}/${values.image?.name}`,
        twitter: values.twitter,
        telegram: values.telegram,
        youtube: values.youtube,
        facebook: values.facebook,
      };
      await editProfile(payload);

      toast({
        title: "Profile Update Successfull",
        variant:"default"
      })
      router.push(`${Routes.USER}/${user?.address}`)
    } catch (error) {
      console.log("error");
    } finally {
      setLoading(false);
    }
  };

  return (
    <Form {...form}>
      <form
        onSubmit={form.handleSubmit(onSubmit)}
        className="flex flex-col gap-5"
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
                File types supported: JPG, JPEG, PNG, GIF, SVG. Max size: 100 MB
              </FormDescription>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="userName"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Username</FormLabel>
              <FormControl>
                <Input id="userName" placeholder="Username" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="bio"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Bio</FormLabel>
              <FormControl>
                <Textarea
                  placeholder="Write Bio"
                  id="bio"
                  {...field}
                  className="h-[10rem]"
                />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="facebook"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Facebook(optional)</FormLabel>
              <FormControl>
                <Input id="facebook" placeholder="Facebook" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="twitter"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Twitter(optional)</FormLabel>
              <FormControl>
                <Input id="twitter" placeholder="Twitter" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="youtube"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Youtube(optional)</FormLabel>
              <FormControl>
                <Input id="youtube" placeholder="Youtube" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="telegram"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Telegram(optional)</FormLabel>
              <FormControl>
                <Input id="telegram" placeholder="Telegram" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />

        <Button type="submit" className="rounded-full self-end" size="lg">
        {loading ? (
            <AiOutlineLoading3Quarters className="animate-spin" />
          ) : (
            "Submit"
          )}        
        </Button>
      </form>
    </Form>
  );
};

export default EditProfileForm;
