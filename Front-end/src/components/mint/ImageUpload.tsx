import {
  TypographyH3,
  TypographyP,
  TypographySmall,
} from "../common/Typography";
import { LuImagePlus } from "react-icons/lu";
import {
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
} from "@/components/ui/form";
import Image from "next/image";
import { Input } from "../ui/input";
import { useRef } from "react";
import { Button } from "../ui/button";
import { MdDelete } from "react-icons/md";

type Props = {
  form: any;
};

const ImageUpload = ({ form }: Props) => {
  const ref = useRef<HTMLInputElement>(null);
  const handleDrop = (e: any) => {
    e.preventDefault();
    const files = e.dataTransfer?.files;
    form.setValue("image", files?.[0]);
  };
  return (
    <FormField
      control={form.control}
      name="image"
      render={({ field }) => (
        <FormItem>
          <FormLabel className="text-base">
            <TypographyH3 text="Product Thumbnail (Image or GIF)" />
          </FormLabel>
          <FormDescription>
            <TypographySmall text="File types supported: JPG, JPEG, PNG, GIF, SVG. Max size: 100 MB" />
          </FormDescription>
          <FormControl>
            <div
              onDrop={(e) => handleDrop(e)}
              className="relative z-40 flex flex-col items-center justify-center border-3 border-dashed border-foreground rounded-lg h-[20rem] mt-5 bg-accent hover:bg-accent/70 cursor-pointer"
              onClick={() => ref.current?.click()}
            >
              {!field.value ? (
                <>
                  <LuImagePlus size={50} />
                  <TypographyP text="Upload a file or drag and drop" />
                  <TypographySmall text="PNG, JPG, GIF up to 10MB" />
                  <Input
                    ref={ref}
                    type="file"
                    className="hidden"
                    onChange={(e) => field.onChange(e.target.files?.[0])}
                  />
                </>
              ) : (
                <>
                  {field.value.type.startsWith("image") && (
                    <div className="relative mx-auto w-full h-full">
                      <Image
                        className="rounded-2xl  object-cover"
                        src={URL.createObjectURL(field.value)}
                        fill
                        alt=""
                      />
                    </div>
                  )}
                  {field.value.type.startsWith("video") && (
                    <div className="relative mx-auto lg:w-[60%] h-full">
                      <video
                        controls
                        className="absolute h-full w-full"
                        src={URL.createObjectURL(field.value)}
                      />
                    </div>
                  )}
                  {/* <TypographyP text={field.value.name}/> */}
                  <Button
                    type="button"
                    variant="ghost"
                    size="icon"
                    className="absolute right-5 top-5 rounded-full"
                    onClick={() => form.setValue("image", null)}
                  >
                    <MdDelete size={25} />
                  </Button>
                </>
              )}
            </div>
          </FormControl>
        </FormItem>
      )}
    />
  );
};

export default ImageUpload;
